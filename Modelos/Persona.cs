using System.ComponentModel.DataAnnotations;
using System;
using System.IO.Pipelines;

namespace PruebaDev.Modelos
{
    public class Persona
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string NumeroIdentificacion { get; set; }
        [Required]
        public string Nombres { get; set; }
        [Required]
        public string Apellidos { get; set; }
        [Required]
        public string TipoDocumento { get; set; }
        [Required]
        public DateTime FechaCreacion { get; set; }
     
        public string NombreCompleto { get;}
      
        public string Identificador { get; }
    }
}
