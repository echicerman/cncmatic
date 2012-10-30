using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommandPreprocessor
{
    class CommandPreprocessorTest
    {
        static void Main(string[] args)
        {

            Console.WriteLine("***** BIENVENIDO AL TEST DEL PREPROCESADOR DE COMANDOS DE CNCmatic ® *****");
            Console.WriteLine("***** Escriba un comando de codigo G, y el preprocesador le devolverá los comandos procesados que serán enviados a la maquina (Para salir inserte 'exit') *****");
            Console.WriteLine();
            
            string input = Console.ReadLine();

            //Configuracion del preprocesador
            CommandPreprocessor.GetInstance().ReferencePosition = new Position();
            Configuration.absoluteProgamming = true;
            Configuration.defaultFeedrate = 60;
            Configuration.millimetersCurveSection = 0.5;
            Configuration.millimetersProgramming = true;

            while (input != "exit")
            {
                try
                {
                    Console.WriteLine("Comandos a enviar a la maquina:");
                    List<string> result = CommandPreprocessor.GetInstance().ProcessProgram(new List<string> { input });
                    for (int i = 0; i < result.Count; i++)
                    {
                        Console.WriteLine("Comando " + i.ToString("00") + ": " + result[i]);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("ERROR AL PROCESAR COMANDO: " + ex);
                }
                finally
                {
                    Console.WriteLine();
                    Console.WriteLine("Escriba nuevo comando o 'exit' para salir");
                    input = Console.ReadLine();
                }
            }
            
        }
    }
}
