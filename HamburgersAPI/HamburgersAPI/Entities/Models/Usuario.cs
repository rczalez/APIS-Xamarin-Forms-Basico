using System;
using System.Collections.Generic;

namespace HamburgersAPI.Entities.Models
{
    public partial class Usuario
    {
        public Usuario()
        {
            Contacto = new HashSet<Contacto>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; }
        public string NombreUsuario { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public DateTime FechaIngreso { get; set; }
        public string ColorFavorito { get; set; }
        public string Sexo { get; set; }

        public virtual ICollection<Contacto> Contacto { get; set; }
    }
}
