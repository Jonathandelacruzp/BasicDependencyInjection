﻿using System;
using DIBuild.Models;

namespace DIBuild
{
    internal static class Program
    {
        internal static void Main(string[] args)
        {
            var container = new DependencyContainer();
            container.AddSingleton<TestObject>();
            container.AddSingleton(typeof(TestObjectSingleton));
            container.AddTransient<TestObjectTransient>();
            container.AddScoped<TestObjectScope>();

            for (var i = 0; i < 10; i++)
            {
                Console.WriteLine($"-------------------------------------- {i} --------------------------------------");
                using var resolver = new ScopeDependencyResolver(container);
                var testObject = resolver.GetService<TestObject>();
                var testObjectSingleton = resolver.GetService<TestObjectSingleton>();
                var testObjectTransient = resolver.GetService<TestObjectTransient>();
                var testObjectScope = resolver.GetService<TestObjectScope>();

                testObject!.Print();
                testObjectSingleton!.Print();
                testObjectScope!.Print();
                testObjectTransient!.Print();
            
                var testInstanceScope2 = resolver.GetService<TestObjectScope>();
                testInstanceScope2!.Print();
            }
        }
    }
}