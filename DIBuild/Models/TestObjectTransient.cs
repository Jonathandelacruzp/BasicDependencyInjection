using System;

namespace DIBuild.Models
{
    public class TestObjectTransient
    {
        private Guid Guid { get; }

        public TestObjectTransient()
        {
            Guid = Guid.NewGuid();
        }

        public void Print()
        {
            Console.WriteLine(this);
            Console.WriteLine("----------------");
        }

        public override string ToString()
        {
            return $"{GetType().Name} {Guid}";
        }
    }
}