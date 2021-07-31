using System;

namespace DIBuild.Models
{
    public class TestObjectScope : TestObject
    {
        private readonly TestObjectSingleton _testObjectSingleton;
        private string Guid { get; }

        public TestObjectScope(TestObjectSingleton testObjectSingleton)
        {
            _testObjectSingleton = testObjectSingleton;
            Guid = System.Guid.NewGuid().ToString().Split('-')[0];
        }

        public override void Print()
        {
            Console.WriteLine(this);
            Console.WriteLine(_testObjectSingleton);
            Console.WriteLine("----------------");
        }

        public override string ToString()
        {
            return $"{GetType().Name} {Guid}";
        }
    }
}