using System;

namespace DIBuild.Models
{
    public class TestObjectTransient
    {
        private readonly TestObject _testObject;
        private readonly TestObjectSingleton _testObjectSingleton;
        private readonly TestObjectScope _testObjectScope;
        private string Guid { get; }

        public TestObjectTransient(TestObject testObject, TestObjectSingleton testObjectSingleton, TestObjectScope  testObjectScope)
        {
            _testObject = testObject;
            _testObjectSingleton = testObjectSingleton;
            _testObjectScope = testObjectScope;
            Guid = System.Guid.NewGuid().ToString().Split('-')[0];
        }

        public void Print()
        {
            Console.WriteLine(this);
            Console.WriteLine(_testObject);
            Console.WriteLine(_testObjectSingleton);
            Console.WriteLine(_testObjectScope);
            Console.WriteLine("----------------");
        }

        public override string ToString()
        {
            return $"{GetType().Name} {Guid}";
        }
    }
}