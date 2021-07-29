using System;

namespace DIBuild
{
    internal static class Program
    {
        internal static void Main(string[] args)
        {
            var container = new ServiceContainer();
            container.Add<ServiceTest>();
            
            var serviceTestInstance = (ServiceTest) Activator.CreateInstance(container.GetDependencyType(typeof(ServiceTest)));
            serviceTestInstance!.Print();
        }
    }

    public class ServiceTest
    {
        public Guid Guid { get; set; }

        public ServiceTest()
        {
            Guid = Guid.NewGuid();
        }

        public void Print()
        {
            Console.WriteLine(Guid);
        }
    }
}