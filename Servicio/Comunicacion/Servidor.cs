using ModeloServicio.DAL;
using ServidorSocket;
using System;
using System.Configuration;
using System.Net.Sockets;
using System.Threading;


namespace Servicio.Comunicacion
{
    public class Servidor
    {
        private Lectura mensajesDAL = Archivos.GetInstancia();
        public void Ejecutar()
        {
            int puerto = Convert.ToInt32(ConfigurationManager.AppSettings["puerto"]);
            ServerSocket servidor = new ServerSocket(puerto);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("[Servidor]: Servidor iniciado en puerto {0}", puerto);
            Console.ResetColor();
            if (servidor.Iniciar())
            {
                while (true)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("[Servidor]: Esperando conexión");
                    Console.ResetColor();
                    Socket cliente = servidor.ObtenerCliente();

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("[Servidor]: Cliente conectado");
                    Console.ResetColor();
                    ClienteCom clienteCom = new ClienteCom(cliente);

                    Cliente clienteThread = new Cliente(clienteCom);
                    Thread t = new Thread(new ThreadStart(clienteThread.ejecutar));
                    t.IsBackground = true;
                    t.Start();
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Error {0}", puerto);
                Console.ResetColor();
            }
        }
    }
}

