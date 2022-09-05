using EFCore.Domain;
using EFCore.Repo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EFCore.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HeroiController : ControllerBase
    {
        private readonly IEFCoreRepository _repo;

        public HeroiController(IEFCoreRepository repo)
        {
            _repo = repo;
        }

        // GET: api/<HeroiController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var herois = await _repo.GetAllHerois(true);
                return Ok(herois);
            }
            catch(Exception ex)
            {
                return BadRequest($"Erro: {ex}");
            }
        }

        // GET api/<HeroiController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var heroi = await _repo.GetHeroiById(id, true);

                if (heroi != null) return Ok(heroi);
                else return Ok("Heroi não encontrado.");

            } 
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // POST api/<HeroiController>
        [HttpPost]
        public async Task<IActionResult> Post(Heroi model)
        {
            try
            {
                _repo.Add(model);

                if (await _repo.SaveChangeAsync())
                {
                    return Ok($"O Heroi {model.Nome} foi criado com sucesso !");
                }
                else
                {
                    return Ok($"Não foi possível cadastrar o heroi {model.Nome}");
                }

            }
            catch (Exception ex)
            {
                return BadRequest($"Não foi possível cadastrar o heroi : {ex}");
            }
        }

        // PUT api/<HeroiController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Heroi model)
        {
            try
            {
                var heroi = await _repo.GetHeroiById(id);
                
                if(heroi != null)
                {
                    _repo.Update(model);
                    if (await _repo.SaveChangeAsync()) return Ok($"O heroi {model.Nome} foi atualizado com sucesso !");
                }

                return Ok("Heroi não encontrado !");

            }
            catch (Exception ex)
            {
                return BadRequest($"Não foi possível atualizar o heroi: {ex}");
            }
        }

        // DELETE api/<HeroiController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var heroi = await _repo.GetHeroiById(id);

                if (heroi != null)
                {
                    _repo.Delete(heroi);
                    if (await _repo.SaveChangeAsync()) return Ok($"O heroi {heroi.Nome} foi excluído com sucesso !");
                }

                return Ok("Heroi não encontrado !");

            }
            catch (Exception ex)
            {
                return BadRequest($"Não foi possível excluir o heroi: {ex}");
            }

        }
    }
}
