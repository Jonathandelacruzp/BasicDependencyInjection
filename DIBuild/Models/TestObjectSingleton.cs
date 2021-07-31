using System;

namespace DIBuild.Models
{
    public class TestObjectSingleton
    {
        private readonly TestObject _testObject;
        private string Guid { get; }

        public TestObjectSingleton(TestObject testObject)
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
}