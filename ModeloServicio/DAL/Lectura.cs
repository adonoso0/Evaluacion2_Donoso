using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModeloServicio.DAL
{
    public interface Lectura
    {
        void AgregarMensaje(LecturaMedidor lecturamedidor);
        List<LecturaMedidor> ObtenerMensajes();
    }
}

