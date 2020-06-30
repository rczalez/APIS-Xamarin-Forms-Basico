using HamburgersAPI.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HamburgersAPI.Entities.DTO
{
    public class ContactoList
    {
        public Usuario usuario = new Usuario();
        public List<Usuario> contactosAgregados = new List<Usuario>();
    }
}
