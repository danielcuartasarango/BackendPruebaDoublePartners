using System;
using System.ComponentModel.DataAnnotations;

namespace PruebaDev.Modelos
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }
        public string User { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public DateTime FechaCreacion { get; set; }


    }
}
