using System;
using System.Linq;

namespace DIBuild
{
    internal static class Program
    {
        internal static void Main(string[] args)
        {
            var container = new ServiceContainer();
            container.AddService<ServiceTest>();
            container.AddService(typeof(ServiceTestConstructor));

            var resolver = new ServiceResolver(container);

            var serviceTestInstance = resolver.GetService<ServiceTest>();
            var serviceTestInstanceConst = resolver.GetService<ServiceTestConstructor>();

            serviceTestInstance!.Print();
            serviceTestInstanceConst!.Print();
        }
    }

    public class ServiceTest
    {
        public Guid Guid { get; }

        public ServiceTest()
        {
            Guid = Guid.NewGuid();
        }

        public void Print()
        {
            Console.WriteLine(Guid);
        }
    }

    public class ServiceTestConstructor
    {
        private readonly ServiceTest _serviceTest;
        private readonly Guid _guid;

        public ServiceTestConstructor(ServiceTest serviceTest)
        {
            _serviceTest = serviceTest;
            _guid = Guid.NewGuid();
        }

        public void Print()
        {
            Console.WriteLine(_guid + " " + _serviceTest.Guid);
        }
    }
}