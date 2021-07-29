using System;
using DIBuild.Models;

namespace DIBuild
{
    internal static class Program
    {
        internal static void Main(string[] args)
        {
            var container = new DependencyContainer();
            container.AddSingleton<TestObject>();
            container.AddSingleton(typeof(ServiceTestConstructor));
            container.AddTransient<TestObjectTransient>();
            container.AddScoped<TestObjectScope>();

            using var resolver = new ScopeDependencyResolver(container);
            var serviceTestInstance = resolver.GetService<TestObject>();
            var serviceTestInstanceConst = resolver.GetService<ServiceTestConstructor>();
            var serviceTestInstanceTransient = resolver.GetService<TestObjectTransient>();
            var serviceTestInstanceScope = resolver.GetService<TestObjectScope>();

            serviceTestInstance!.Print();
            serviceTestInstanceConst!.Print();
            serviceTestInstanceScope!.Print();
            serviceTestInstanceTransient!.Print();
            
            var serviceTestInstanceScope2 = resolver.GetService<TestObjectScope>();
            serviceTestInstanceScope2!.Print();
        }
    }


}