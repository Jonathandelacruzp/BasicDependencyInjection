using System;

namespace DIBuild.Models
{
    public class TestObjectScope : TestObject
    {
        public Guid Guid { get; }

        public TestObjectScope(TestObjectTransient testObjectTransient) : base(testObjectTransient)
        {
            Guid = Guid.NewGuid();
        }

        public override void Print()
        {
            Console.WriteLine(GetType().Name + "Begin");
            Console.WriteLine(Guid);
            Console.WriteLine(base.Guid);
            Console.WriteLine(TestObjectTransient.Guid);
            Console.WriteLine(GetType().Name + "End");
            Console.WriteLine("----------------");
        }
    }
}