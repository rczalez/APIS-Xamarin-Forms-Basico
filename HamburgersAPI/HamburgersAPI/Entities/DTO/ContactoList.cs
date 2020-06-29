using HamburgersAPI.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HamburgersAPI.Entities.DTO
{
    public class ContactoList
    {
        public int idUser { get; set; }
        public List<Usuario> contactosAgregados { get; set; }
    }
}
