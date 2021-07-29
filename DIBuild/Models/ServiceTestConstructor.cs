using System;

namespace DIBuild.Models
{
    public class ServiceTestConstructor
    {
        private readonly TestObject _testObject;
        private readonly TestObjectTransient _testObjectTransient;
        private readonly TestObjectScope _testObjectScope;
        private readonly Guid _guid;

        public ServiceTestConstructor(TestObject testObject, TestObjectTransient testObjectTransient, TestObjectScope testObjectScope)
        {
            _testObject = testObject;
            _testObjectTransient = testObjectTransient;
            _testObjectScope = testObjectScope;
            _guid = Guid.NewGuid();
        }

        public void Print()
        {
            Console.WriteLine(GetType().Name + "Begin");
            Console.WriteLine(_guid);
            Console.WriteLine(_testObject.Guid);
            Console.WriteLine(_testObjectScope.Guid);
            Console.WriteLine(_testObjectTransient.Guid);
            Console.WriteLine(GetType().Name + "End");
            Console.WriteLine("----------------");
        }
    }
}