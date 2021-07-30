using System;

namespace DIBuild.Models
{
    public class TestObjectScope : TestObject
    {
        private Guid Guid { get; }

        public TestObjectScope(TestObjectTransient testObjectTransient) : base(testObjectTransient)
        {
            Guid = Guid.NewGuid();
        }

        public override void Print()
        {
            Console.WriteLine(this);
            Console.WriteLine(TestObjectTransient);
            Console.WriteLine("----------------");
        }
    }
}