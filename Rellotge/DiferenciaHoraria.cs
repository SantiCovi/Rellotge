using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rellotge
{
   
    [Serializable()]
    public class DiferenciaHoraria
    {
        private String Nombre;
        public long Hora;

        public void setNombre(String value)
        {
            Nombre = value;
        }

        public String getNombre()
        {
            return Nombre;
        }

        public void setHora(long value)
        {
            Hora = value;
        }

        public long getHora()
        {
            return Hora;
        }

    }
}

