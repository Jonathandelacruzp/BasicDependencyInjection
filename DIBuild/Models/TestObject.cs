using System;

namespace DIBuild.Models
{
    public class TestObject
    {
        protected readonly TestObjectTransient TestObjectTransient;
        private Guid Guid { get; }

        public TestObject(TestObjectTransient testObjectTransient)
        {
            TestObjectTransient = testObjectTransient;
            Guid = Guid.NewGuid();
        }

        public virtual void Print()
        {
            Console.WriteLine(this);
            Console.WriteLine(TestObjectTransient);
            Console.WriteLine("----------------");
        }

        public override string ToString()
        {
            return $"{GetType().Name} {Guid}";
        }
    }
}