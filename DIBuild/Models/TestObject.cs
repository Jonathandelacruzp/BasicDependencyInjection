using System;

namespace DIBuild.Models
{
    public class TestObject
    {
        protected readonly TestObjectTransient TestObjectTransient;
        public Guid Guid { get; }

        public TestObject(TestObjectTransient testObjectTransient)
        {
            TestObjectTransient = testObjectTransient;
            Guid = Guid.NewGuid();
        }

        public virtual void Print()
        {
            Console.WriteLine(GetType().Name + "Begin");
            Console.WriteLine(Guid);
            Console.WriteLine(TestObjectTransient.Guid);
            Console.WriteLine(GetType().Name + "End");
            Console.WriteLine("----------------");
        }
    }
}