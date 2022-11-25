using System.ComponentModel.DataAnnotations;
using System;
using System.Reflection.Metadata.Ecma335;

namespace PruebaDev.Modelos.Dto
{
    public class PersonaDto
    {
        public int Id { get; set; }
        public string NumeroIdentificacion { get; set; }
        
        public string Nombres { get; set; }

        public string Apellidos { get; set; }

        public string TipoDocumento { get; set; }

        public DateTime FechaCreacion { get; set; }
     
        public string NombreCompleto { get; }
   
        public string Identificador { get;  }
    }
}
