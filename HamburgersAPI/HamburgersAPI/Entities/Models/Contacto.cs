﻿using System;
using System.Collections.Generic;

namespace HamburgersAPI.Entities.Models
{
    public partial class Contacto
    {
        public int Id { get; set; }
        public int IdUsuario { get; set; }
        public int IdContacto { get; set; }
        public DateTime FechaCreacion { get; set; }

        public virtual Usuario IdUsuarioNavigation { get; set; }
    }
}
