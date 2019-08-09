using System;
using System.Collections.Generic;
using static System.Console;
using CSScriptLibrary;

namespace DSL {

    interface IContext {
        string Id { get; }
    }

    interface IService {
        object Get(string nombre);
    }

    interface IType {
        IRequire WhenType(string tipo);
    }

    interface IRequire : IOperation {
        IRequire Require(string servicio);
    }

    interface IOperation {
        IOperation DefineOperation(Operation accion);
    }

    delegate void Operation(IContext contexto, IService servicio);

    class DynamicProvider: IType, IRequire, IOperation {
        public string Evento = null;
        public string Tipo   = null;
        public List<String>    Servicios   = new List<String>();
        public List<Operation> Operaciones = new List<Operation>();

        private static List<DynamicProvider> definiciones = null;

        public static IType OnEvent(string evento) {
            if(definiciones == null) definiciones = new List<DynamicProvider>();
            var dp = new DynamicProvider(evento);
            definiciones.Add(dp);
            return dp;
        }

        private DynamicProvider(string evento) {
            this.Evento = evento;
            WriteLine($"\n Para [{evento}]");
        }

        public IRequire WhenType(string tipo) {
            this.Tipo = tipo;
            WriteLine($"   Cuando es [{tipo}]");
            return this;
        }

        public IRequire Require(string servicio) {
            this.Servicios.Add(servicio);
            if(this.Servicios.Count == 1)
                WriteLine($"   Usando [{servicio}]");
            else
                WriteLine($"        y [{servicio}]");
            return this;
        }

        public IOperation DefineOperation(Operation operacion) {
            WriteLine($" Ejecute\n   [{operacion}]");
            WriteLine();
            Operaciones.Add(operacion);
            return this;
        }
    }

     class DynamicProviderHost {
        public IType OnEvent(string evento) {
            return DynamicProvider.OnEvent(evento);
        }
    }

    class XX : DynamicProviderHost {
        public void Load() {
            OnEvent("AgregateResource")
                .WhenType("Resource")
                    .Require("ActiveDirectory")
                .DefineOperation((contexto, servicios) => {
                    var s = servicios.Get("ActiveDirectory");
                    var id = contexto.Id;
                });
        }
    }

    class Program {

        

        static void Main() {
            WriteLine("DEMO DSL Fluent Progressive!");

            DynamicProvider
                .OnEvent("AgregateResource")
                    .WhenType("Resource")
                    .Require("ActiveDirectory")
                    .DefineOperation((contexto, servicios) => {
                        var s  = servicios.Get("ActiveDirectory");
                        var id = contexto.Id;
                    })
                    .DefineOperation((contexto, servicios) => {
                        var s = servicios.Get("ActiveDirectory");
                        var id = contexto.Id;
                    });

            DynamicProvider
                .OnEvent("DeleteResource")
                    .WhenType("Resource")
                    .Require("MailBox")
                    .Require("ActiveDirectory")
                .DefineOperation((c, s) => {

                });

            DynamicProvider
                .OnEvent("PurgeResource")
                .WhenType("Resource")
                    .Require("AD")
                    .Require("MB")
                .DefineOperation((c, s) => { });

            DynamicProvider
                .OnEvent("XXX")
                    .WhenType("RRR")
                    .DefineOperation((c, s) => { });

            DynamicProvider
                .OnEvent("XXXX")
                    .WhenType("YYYY")
                        .Require("AAA")
                        .Require("BBBB")
                    .DefineOperation((c, s) => { });


            CSScript.EvaluatorConfig.Engine = EvaluatorEngine.Roslyn;
            //EvaluatorEngine.Mono;
            //EvaluatorEngine.CodeDom;

            var sqr = CSScript.Evaluator
                              .CreateDelegate(@"int Sqr(int a)
                                    {
                                        return a * a;
                                    }");

            var r = sqr(3);

            //dynamic script = CSScript.LoadCode(
            //               @"using System.Console;
            //                 public class Script
            //                 {
            //                     public void SayHello(string greeting)
            //                     {
            //                         Console.WriteLine(""Greeting: "" + greeting);
            //                     }
            //                 }")
            //                 .CreateObject("*");
            //script.SayHello("Hello World!");

            ReadLine();
        }
    }
}
