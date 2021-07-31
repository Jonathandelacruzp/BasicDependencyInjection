using System;
using DIBuild.interfaces;

namespace DIBuild.Models
{
    public class TestObject : ITestObject
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