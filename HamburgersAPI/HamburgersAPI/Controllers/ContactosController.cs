using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using HamburgersAPI.Entities.DTO;
using HamburgersAPI.Entities.Models;
using HamburgersAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace HamburgersAPI.Controllers
{
    [Route("api/v1/HamburgersAPI/[controller]")]

    public class ContactosController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork(new HamburgersContext());

        [HttpGet("idUsuario")]

        public IActionResult GetContacts(int idUsuario)
        {
            
            if (idUsuario !=0)
            {

                var user = unitOfWork.Usuarios.Get(x => x.Id == idUsuario);

                if (user != null)
                {
                    var contactos = unitOfWork.Contactos.Get(x => x.IdUsuario == idUsuario);
                    var result = CreateMappedObject(contactos, idUsuario);
                    var serializedList = JsonConvert.SerializeObject(result, Formatting.Indented,
                        new JsonSerializerSettings()
                        {
                            ReferenceLoopHandling = ReferenceLoopHandling.Serialize
                        });

                    return Ok(serializedList);
                }
                else
               
                    return BadRequest();
            }
            else

                return BadRequest();
        }

        [HttpPost("{idUsuario}")]
        public IActionResult CreateContact([FromBody] Usuario contactoAmigo, int idUsuario)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    unitOfWork.Usuarios.Insert(contactoAmigo);
                    unitOfWork.Save();

                    Contacto contacto = new Contacto();
                    contacto.IdUsuario = idUsuario;
                    contacto.IdContacto = contactoAmigo.Id;
                    contacto.FechaCreacion = DateTime.Now;
                    unitOfWork.Contactos.Insert(contacto);
                    unitOfWork.Save();
                    return Created("HamburgernDemo/CreateContact", contactoAmigo);
                }
            }
            catch (DataException ex)
            {
                return BadRequest(ex);
            }
            return BadRequest(contactoAmigo);
        }

        private ContactoList CreateMappedObject(IEnumerable<Contacto> contactos, int idUsuario)
        {

            ContactoList listaAmigos = new ContactoList();
            foreach (var item in contactos)
            {

                Usuario contactoAmigo = unitOfWork.Usuarios.GetById(item.IdContacto);
                listaAmigos.contactosAgregados.Add(contactoAmigo);


            }

            listaAmigos.idUser = idUsuario;
            return listaAmigos;
           
        }

       

        [HttpPut]

        public IActionResult UpdateContact([FromRoute] int id, [FromBody] Contacto contacto)
        {
            Contacto ContactoSearch = unitOfWork.Contactos.GetById(id);
            if (ContactoSearch != null)
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        unitOfWork.Contactos.Update(contacto);
                        unitOfWork.Save();
                        return Ok();
                    }


                }
                catch (DataException ex)
                {
                    return BadRequest(ex);
                }
            }

            else
            {
                return NotFound("El usuario que intenta actualizar no existe");
            }

            return BadRequest();
           
        }

        [HttpDelete("id")]

        public IActionResult DeleteUser(int id)
        {
            if (id != 0)

            {
                unitOfWork.Contactos.Delete(id);
                unitOfWork.Save();
                return Ok("Usuario eliminado");
            }

            else
            {
                return NoContent();
            }
        }
    }
}
