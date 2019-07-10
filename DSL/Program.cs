using System;
using System.Collections.Generic;
using static System.Console;

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
        void DefineOperation(Operation accion);
    }

    delegate void Operation(IContext contexto, IService servicio);

    class DynamicProvider: IType, IRequire, IOperation {
        public string Evento = null;
        public string Tipo   = null;

        public List<String> Servicios = new List<String>();
        public Operation Operacion = null;

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

        public void DefineOperation(Operation operacion) {
            WriteLine($" Ejecute\n   [{operacion}]");
            WriteLine();
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

            ReadLine();
        }
    }
}
