using Servicio.Comunicacion;
using ModeloServicio;
using ModeloServicio.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Servicio
{
    public class Program
    {
        private static Lectura mensajesDAL = Archivos.GetInstancia();

        static bool Menu()
        {
            bool continuar = true;

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Elija una opción");
            Console.ResetColor();

            Console.WriteLine("0. Salir");
            Console.WriteLine("1. Ingresar datos");
            Console.WriteLine("2. Mostrar datos ingresados");

            switch (Console.ReadLine().Trim())
            {
                case "1":
                    Ingresar();
                    break;
                case "2":
                    Mostrar();
                    break;
                case "0":
                    continuar = false;
                    Environment.Exit(0);
                    break;
                default:
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Opción no válida, intente nuevamente");
                    Console.ResetColor();

                    break;
            }
            return continuar;
        }

        static void Main(string[] args)
        {
            Servidor hebra = new Servidor();
            Thread t = new Thread(new ThreadStart(hebra.Ejecutar));
            t.Start();
            while (Menu()) ;
        }

        static void Ingresar()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Número del medidor: ");
            Console.ResetColor();
            string nromedidor = Console.ReadLine().Trim();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Fecha: (yyyy-MM-dd HH:mm:ss) ");
            Console.ResetColor();
            string fecha = Console.ReadLine().Trim();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Consumo(Kw/h): Ejemplo=123,4");
            Console.ResetColor();
            string valorconsumo = Console.ReadLine().Trim();
            LecturaMedidor lecturamedidor = new LecturaMedidor()
            {
                NroMedidor = Convert.ToInt32(nromedidor),
                Fecha = fecha,
                ValorConsumo = Convert.ToDecimal(valorconsumo)
            };
            lock (mensajesDAL)
            {
                mensajesDAL.AgregarMensaje(lecturamedidor);
            }
        }

        static void Mostrar()
        {
            List<LecturaMedidor> lecturamedidores = null;
            lock (mensajesDAL)
            {
                lecturamedidores = mensajesDAL.ObtenerMensajes();
            }
            foreach (LecturaMedidor lecturamedidor in lecturamedidores)
            {
                Console.WriteLine(lecturamedidor);
            }

        }
    }
}


