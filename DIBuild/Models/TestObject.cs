using System;

namespace DIBuild.Models
{
    public class TestObject
    {
        private string Guid { get; }

        public TestObject()
        {
            Guid = System.Guid.NewGuid().ToString().Split('-')[0];
        }

        public virtual void Print()
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