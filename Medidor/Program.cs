using MedidorUtils;     
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medidor
{
    public class Program
    {
        static void Main(string[] args)
        {
            int puerto;
            string servidor;


            while (true)
            {
                Console.WriteLine("Ingrese IP a conectar: ");
                Console.ResetColor();
                servidor = Console.ReadLine().Trim();


                Console.WriteLine("Puerto: ");
                Console.ResetColor();
                puerto = Convert.ToInt32(Console.ReadLine().Trim());


                ClienteSocket clienteSocket = new ClienteSocket(servidor, puerto);

                if (clienteSocket.Conectar())
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Conexión correcta");
                    Console.ResetColor();

                    bool seguir = true;
                    string respuesta;
                    string mensaje_server;

                    do
                    {
                        mensaje_server = clienteSocket.Leer();
                        if (mensaje_server == "chao")
                        {
                            seguir = false;
                            clienteSocket.Desconectar();
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine("Desconectado");
                            Console.ResetColor();
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("[Servidor]: " + mensaje_server);
                            Console.ResetColor();
                            Console.Write("Escriba una respuesta: ");
                            Console.ResetColor();
                            respuesta = Console.ReadLine().Trim();
                            clienteSocket.Escribir(respuesta);

                        }

                    } while (seguir);
                }
                else
                {
                    Console.WriteLine("Error de conexión con el puerto {0}, posiblemente esté en uso", puerto);
                }
            }
        }
    }
}
