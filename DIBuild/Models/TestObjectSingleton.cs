namespace DIBuild.Models;

public class TestObjectSingleton
{
    private readonly ITestObject _testObject;
    private string Guid { get; }

    public TestObjectSingleton(ITestObject testObject)
    {
        _testObject = testObject;
        Guid = System.Guid.NewGuid().ToString().Split('-')[0];
    }

    public void Print()
    {
        Console.WriteLine(this);
        Console.WriteLine(_testObject);
        Console.WriteLine("----------------");
    }

    public override string ToString()
    {
        return $"{GetType().Name} {Guid}";
    }
}