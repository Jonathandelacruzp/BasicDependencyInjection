using System;
using System.Linq;

namespace DIBuild
{
    internal static class Program
    {
        internal static void Main(string[] args)
        {
            var container = new ServiceContainer();
            container.AddSingleton<ServiceTest>();
            container.AddSingleton(typeof(ServiceTestConstructor));
            container.AddTransient<ServiceTestTrans>();

            var resolver = new ServiceResolver(container);

            var serviceTestInstance = resolver.GetService<ServiceTest>();
            var serviceTestInstanceConst = resolver.GetService<ServiceTestConstructor>();
            var serviceTestInstanceTrans = resolver.GetService<ServiceTestTrans>();

            serviceTestInstance!.Print();
            serviceTestInstanceConst!.Print();
            serviceTestInstanceTrans!.Print();
        }
    }

    public class ServiceTest
    {
        private readonly ServiceTestTrans _serviceTestTrans;
        public Guid Guid { get; }

        public ServiceTest(ServiceTestTrans serviceTestTrans)
        {
            _serviceTestTrans = serviceTestTrans;
            Guid = Guid.NewGuid();
        }

        public void Print()
        {
            Console.WriteLine(GetType().Name + "Begin");
            Console.WriteLine(Guid);
            Console.WriteLine(_serviceTestTrans.Guid);
            Console.WriteLine(GetType().Name + "End");
            Console.WriteLine("----------------");
        }
    }

    public class ServiceTestConstructor
    {
        private readonly ServiceTest _serviceTest;
        private readonly ServiceTestTrans _serviceTestTrans;
        private readonly Guid _guid;

        public ServiceTestConstructor(ServiceTest serviceTest, ServiceTestTrans serviceTestTrans)
        {
            _serviceTest = serviceTest;
            _serviceTestTrans = serviceTestTrans;
            _guid = Guid.NewGuid();
        }

        public void Print()
        {
            Console.WriteLine(GetType().Name + "Begin");
            Console.WriteLine(_guid);
            Console.WriteLine(_serviceTestTrans.Guid);
            Console.WriteLine(_serviceTest.Guid);
            Console.WriteLine(GetType().Name + "End");
            Console.WriteLine("----------------");
        }
    }

    public class ServiceTestTrans
    {
        public Guid Guid { get; }

        public ServiceTestTrans()
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