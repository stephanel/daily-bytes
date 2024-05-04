using CSharp.ChainOfResponsibility.Tests.Handlers;

namespace CSharp.ChainOfResponsibility.Tests;

public abstract class AbstractHandler : IHandler
{
    private readonly IHandler? _nextHandler;

    protected AbstractHandler()
    { }

    protected AbstractHandler(IHandler next)
    {
        _nextHandler = next;
    }

    public virtual void Handle(object request)
    {
        _nextHandler?.Handle(request);
    }
}