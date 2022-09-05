using EFCore.Domain;
using EFCore.Repo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EFCore.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HeroiController : ControllerBase
    {
        private readonly HeroiContexto _context;

        public HeroiController(HeroiContexto context)
        {
            _context = context;
        }

        // GET: api/<HeroiController>
        [HttpGet]
        public ActionResult Get()
        {
            try
            {
                return Ok(_context.Herois.ToList());
            }
            catch(Exception ex)
            {
                return BadRequest($"Erro: {ex}");
            }
        }

        // GET api/<HeroiController>/5
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            return Ok("value");
        }

        // POST api/<HeroiController>
        [HttpPost]
        public ActionResult Post(Heroi model)
        {
            try
            {
                _context.Add(model);
                _context.SaveChanges();

                return Ok($"O heroi {model.Nome} foi adicionado com sucesso !");
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex}");
            }
        }

        // PUT api/<HeroiController>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, Heroi model)
        {
            try
            {
               if (_context.Herois.AsNoTracking().FirstOrDefault(h => h.Id == id) != null)
                {
                    _context.Update(model);
                    _context.SaveChanges();

                    return Ok($"O heroi {model.Nome} foi atualizado com sucesso !");
                }

                return NotFound($"O heroi {model.Nome} não foi encontrado!");

            }
            catch (Exception ex)
            {
                return BadRequest($"Não foi possível atualizar o heroi. {ex}");
            }

        }

        // DELETE api/<HeroiController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
