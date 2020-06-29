using HamburgersAPI.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HamburgersAPI.Services
{
    public interface IUnitOfWork
    {

        IRepository<Usuario> Usuarios { get; }
        IRepository<Contacto> Contactos { get; }

        void Save();

    }
}
