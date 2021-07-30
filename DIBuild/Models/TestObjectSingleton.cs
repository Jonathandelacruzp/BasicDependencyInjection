using System;

namespace DIBuild.Models
{
    public class TestObjectSingleton
    {
        private readonly TestObject _testObject;
        private readonly TestObjectTransient _testObjectTransient;
        private readonly TestObjectScope _testObjectScope;
        private Guid Guid { get; }

        public TestObjectSingleton(TestObject testObject, TestObjectTransient testObjectTransient, TestObjectScope testObjectScope)
        {
            _testObject = testObject;
            _testObjectTransient = testObjectTransient;
            _testObjectScope = testObjectScope;
            Guid = Guid.NewGuid();
        }

        public void Print()
        {
            Console.WriteLine(this);
            Console.WriteLine(_testObject);
            Console.WriteLine(_testObjectScope);
            Console.WriteLine(_testObjectTransient);
            Console.WriteLine("----------------");
        }

        public override string ToString()
        {
            return $"{GetType().Name} {Guid}";
        }
    }
}