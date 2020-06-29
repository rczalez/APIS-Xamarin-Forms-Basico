using HamburgersAPI.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HamburgersAPI.Services
{
    public class UnitOfWork : IUnitOfWork
    {

        private HamburgersContext _context;
        private BaseRepository<Usuario> _usuario;
        private BaseRepository<Contacto> _contacto;

        public UnitOfWork(HamburgersContext dbcontext)
        {

            _context = dbcontext;

        }
        public IRepository<Usuario> Usuarios

        {
            get
            {
                return _usuario ?? (_usuario = new BaseRepository<Usuario>(_context));
            }
        }

        public IRepository<Contacto> Contactos

        {
            get
            {
                return _contacto ?? (_contacto = new BaseRepository<Contacto>(_context));
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
