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
            CommandPreprocessor.GetInstance();
            while (input != "exit")
            {
                Console.WriteLine("Comandos a enviar a la maquina:");
                List<string> result = CommandPreprocessor.GetInstance().ProcessCommand(input);
                for(int i = 0; i < result.Count; i++)
                {
                    Console.WriteLine("Comando " + i.ToString("00") + ": " + result[i]);
                }

                Console.WriteLine();
                Console.WriteLine("Escriba nuevo comando o 'exit' para salir");
                input = Console.ReadLine();
            }
            
        }
    }
}
