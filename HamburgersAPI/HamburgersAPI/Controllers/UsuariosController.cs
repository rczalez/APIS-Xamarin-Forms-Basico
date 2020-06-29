using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using HamburgersAPI.Entities.Models;
using HamburgersAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace HamburgersAPI.Controllers
{
    [Route("api/v1/HamburgersAPI/[controller]")]
    public class UsuariosController : Controller
    {
        private HamburgersContext _context = new HamburgersContext();
        private UnitOfWork _unitOfWork = new UnitOfWork(new HamburgersContext());

        [HttpGet]
        public IActionResult GetAllUsers()
        {
            var usuarios = _unitOfWork.Usuarios.Get();

            if (usuarios != null)

            {
                return Ok(usuarios);
            }
            else
            {
                return Ok();
            }
        }



        [HttpGet("id")]

        public IActionResult GetById(int id)
        {

            Usuario usuario = _unitOfWork.Usuarios.GetById(id);

            if (usuario != null)
            {
                return Ok(usuario);
            }
            else
            {
                return BadRequest("No se ha encontrado el usuario con este Id");
            }
        }

        [HttpPut]

        public IActionResult UpdateUser([FromBody] Usuario usuario)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _unitOfWork.Usuarios.Update(usuario);
                    _unitOfWork.Save();
                    return Ok();
                }

                else
                {
                    return BadRequest();
                }


            }
            catch (DataException ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpDelete("id")]

        public IActionResult DeleteUser(int id)
        {
            if (id != 0)

            {
                _unitOfWork.Usuarios.Delete(id);
                _unitOfWork.Save();
                return Ok("Usuario eliminado");
            }

            else
            {
                return BadRequest("Usuario con Id invalido");
            }
        }
    }
}
