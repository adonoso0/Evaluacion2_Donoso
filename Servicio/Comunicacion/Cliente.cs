using ModeloServicio;
using ModeloServicio.DAL;
using ServidorSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicio.Comunicacion
{
    public class Cliente
    {
        private ClienteCom clienteCom;
        private Lectura mensajesDAL = Archivos.GetInstancia();

        public Cliente(ClienteCom clienteCom)
        {
            this.clienteCom = clienteCom;
        }

        public void ejecutar()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            clienteCom.Escribir("Número del medidor: ");
            Console.ResetColor();
            string nromedidor = clienteCom.Leer();

            Console.ForegroundColor = ConsoleColor.Yellow;
            clienteCom.Escribir("Fecha: (yyyy-MM-dd HH:mm:ss) ");
            Console.ResetColor();
            string fecha = clienteCom.Leer();

            Console.ForegroundColor = ConsoleColor.Yellow;
            clienteCom.Escribir("Consumo(Kw/h): Ejemplo=123,4");

            string valorconsumo = clienteCom.Leer();
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

            clienteCom.Escribir("Datos ingresados correctamente");
            clienteCom.Desconectar();
        }
    }
}

