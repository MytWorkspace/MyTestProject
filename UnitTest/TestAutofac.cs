﻿using Autofac;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest
{
    [TestClass]
    public class TestAutofac
    {

        private static IContainer Container { get; set; }

        [TestMethod]
        public void TestModelAutofac() {

            var builder = new ContainerBuilder();
            builder.RegisterType<ConsoleOutput>().As<IOutput>();
            builder.RegisterType<TodayWriter>().As<IDateWriter>();
            Container = builder.Build();

            WriteDate();  //调用
        }

        public static void WriteDate()
        {
            // Create the scope, resolve your IDateWriter,
            // use it, then dispose of the scope.
            using (var scope = Container.BeginLifetimeScope())
            {
                var writer = scope.Resolve<IDateWriter>();
                writer.WriteDate();
            }
        }


        public interface IOutput
        {
            void Write(string content);
        }

        public class ConsoleOutput : IOutput
        {
            public void Write(string content)
            {
                Console.WriteLine(content);
            }
        }
  
        public interface IDateWriter
        {
            void WriteDate();
        }
    
        public class TodayWriter : IDateWriter
        {
            private IOutput _output;
            public TodayWriter(IOutput output)
            {
                this._output = output;
            }

            public void WriteDate()
            {
                this._output.Write(DateTime.Today.ToShortDateString());
            }
        }
    }
}
