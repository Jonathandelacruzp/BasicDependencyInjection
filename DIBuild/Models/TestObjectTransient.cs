using System;

namespace DIBuild.Models
{
    public class TestObjectTransient
    {
        public Guid Guid { get; }

        public TestObjectTransient()
        {
            Guid = Guid.NewGuid();
        }

        public void Print()
        {
            Console.WriteLine(GetType().Name + "Begin");
            Console.WriteLine(Guid);
            Console.WriteLine(GetType().Name + "End");
            Console.WriteLine("----------------");
        }
    }
}