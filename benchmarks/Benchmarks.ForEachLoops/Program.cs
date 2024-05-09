using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Running;

BenchmarkRunner.Run<ForEachLoopsBenchmarks>();

//var run = new ForEachLoopsBenchmarks();
//run.collectionSize = 10;
//run.collectionType = CollectionType.Array;
//run.Setup();
//run.UsingDoWhile();

public enum CollectionType
{
    Array,
    List
}

[MemoryDiagnoser]
public class ForEachLoopsBenchmarks
{
    private IList<string> collection = null!;

    const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz 0123456789";

    [Params(10, 1_000, 10_000, 100_000, 1_000_000)]
    public int collectionSize;

    [Params(CollectionType.Array, CollectionType.List)] 
    public CollectionType collectionType;

    [GlobalSetup]
    public void Setup()
    {
        Random random = new();
        collection = ChangeType(
            Enumerable.Range(0, collectionSize).Select<int, string>(i => RandomString(random))
        );
    }

    IList<string> ChangeType(IEnumerable<string> collection)
    {
        return collectionType switch
        {
            CollectionType.Array => collection.ToArray(),
            CollectionType.List => collection.ToList(),
            _ => throw new NotImplementedException()
        };
    }

    string RandomString(Random random)
        => new string(Enumerable.Repeat(chars, 150)
            .Select(s => s[random.Next(s.Length)]).ToArray());

    [Benchmark(Description = "Index based for loop")]
    public void UsingIndexBasedForLoop()
    {
        string current = null!;
        var size = collection.Count();
        for(int i = 0; i < size; i++)
        {
            current = collection[i];
        }
    }

    [Benchmark(Description = "Foreach loop")]
    public void UsingForEeachLoop()
    {
        foreach (var item in collection)
        { }
    }

    [Benchmark(Description = "Linq ForEach")]
    public void UsingLinqEach()
    {
        collection.ToList().ForEach(item => { });
    }

    [Benchmark(Description = "While")]
    public void UsingWhile()
    {
        var size = collection.Count();
        var index = 0;
        while(index < size)
        {
            var item = collection[index++];
        }
    }

    [Benchmark(Description = "Do/While")]
    public void UsingDoWhile()
    {
        var size = collection.Count();
        var index = 0;
        do
        {
            var item = collection[index++];
        } while (index < size);
    }

    [Benchmark(Description = "Parallel.ForEach")]
    public void UsingParallelForEach()
    {
        Parallel.ForEach(collection, item => { });
    }

    [Benchmark(Description = "Linq as parallel")]
    public void UsingLinqAsParallel()
    {
        collection.AsParallel().ForAll(item => { });
    }
}