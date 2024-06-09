using Argon;
using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;
using MbDotNet;
using MbDotNet.Models;
using MbDotNet.Models.Imposters;
using MbDotNet.Models.Responses.Fields;
using MbDotNet.Models.Stubs;
using System.Net;

namespace Common.TestFramework.Fixtures;

public sealed class MountebackFixture : IAsyncLifetime, IDisposable
{
    private IContainer _container = null!;
    private MountebankClient _mountebankClient = null!;

    private string DockerImage => "jkris/mountebank:2.8.1";
    private string ContainerName => "booksStore-tests-mountebank";
    private string Hostname => "mountebank";
    public int MountebankPort => 2525;
    public int ImpostersPort => 3000;
    
    /// <summary>
    /// This method is ionvoked by XUnit. Do not invoke directly.
    /// </summary>
    /// <returns></returns>
    public async Task InitializeAsync()
    {
        // FIXME: no mounted volume as the tests are run in NCrunch workspaces.
        // it is not easy to target a folder that is out of the workspace,
        // without hardcoding at least part of the path. need to investigate
        _container = new ContainerBuilder()
            .WithImage(DockerImage)
            .WithName($"{ContainerName}-{Guid.NewGuid().ToString()}")
            .WithHostname(Hostname)
            .WithPortBinding(MountebankPort, MountebankPort)
            .WithExposedPort(ImpostersPort)
            .WithExposedPort(3100)
            .WithPortBinding(ImpostersPort, ImpostersPort)
            .WithPortBinding(3100, 3100)
            .WithCommand("--allowInjection")
            .WithWaitStrategy(Wait.ForUnixContainer().UntilHttpRequestIsSucceeded(r => r.ForPort((ushort)MountebankPort)))
            .Build();

        await _container.StartAsync()
          .ConfigureAwait(false);

        _mountebankClient = await InitializeClientAndImposterAsync();
    }

    #region IAsyncLifetime
    public async Task DisposeAsync()
    {
        await _container.StopAsync().ConfigureAwait(false);
        await _container.DisposeAsync();
    }
    #endregion

    #region IDisposable
    public void Dispose(bool disposing)
    {
        if (disposing)
        {
            // do nothing
        }
        // Dispose managed resources
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    ~MountebackFixture()
    {
        Dispose(false);
    }
    #endregion

    private async Task<MountebankClient> InitializeClientAndImposterAsync()
    {
        var client = new MountebankClient();
        await client.DeleteImposterAsync(ImpostersPort);
        var imposter = new HttpImposter(ImpostersPort, "Default", new HttpImposterOptions
        {
            DefaultResponse = new HttpResponseFields
            {
                StatusCode = HttpStatusCode.BadGateway
            },
            RecordRequests = true
        });
        await client.CreateHttpImposterAsync(imposter);
        return client;
    }

    public Task StubGetEndpointAsync<T>(string endpointPath, T responsebody) where T : class
        => StubEndpointAsync(endpointPath, Method.Get, responsebody);

    private Task StubEndpointAsync<T>(string endpointPath, Method method, T responsebody) where T : class
    {
        var stub = new HttpStub()
           .OnPathAndMethodEqual(endpointPath, method)
           .ReturnsJson<T>(HttpStatusCode.OK, responsebody);
        //.ReturnsBody(HttpStatusCode.OK, JsonConvert.SerializeObject(responsebody));
        return _mountebankClient.AddHttpImposterStubAsync(ImpostersPort, stub);
    }
}
