using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agenda_de_contactos_prueba
{
    internal class Contacto
    {
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public DateTime FechaHora { get; set; }
        public string Lugar { get; set; }
        public string Asunto { get; set; }

        public override string ToString()
        {
            return $"Nombre: {Nombre}\n" +
                   $"Teléfono: {Telefono}\n" +
                   $"Correo: {Correo}\n" +
                   $"Fecha y Hora: {FechaHora}\n" +
                   $"Lugar: {Lugar}\n" +
                   $"Asunto: {Asunto}\n";
        }

        internal char ToCSV()
        {
            throw new NotImplementedException();
        }
    }
}