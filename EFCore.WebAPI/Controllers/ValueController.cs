using EFCore.Domain;
using EFCore.Repo;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace EFCore.WebAPI.Controllers
{
    [ApiController]
    [Route("api")]
    public class ValueController : ControllerBase
    {

        public readonly HeroiContexto _contexto;
        public ValueController(HeroiContexto contexto)
        {
            _contexto = contexto;
        }

        [HttpGet("{getallheros}")]
        public ActionResult GetAllHeros()
        {
            var HeroiList = _contexto.Herois.ToList();
            return Ok(HeroiList);
        }


        [HttpGet("Atualizar/{nameHero}")]
        public ActionResult Get(string nameHero)
        {
            //var heroi = new Heroi { 
            //Nome = nameHero
            //};

            var heroi = _contexto.Herois
                            .Where(h => h.Id == 5)
                            .FirstOrDefault();

            heroi.Nome = "Homem Aranha";
            //_contexto.Herois.Add(heroi);
            _contexto.SaveChanges();

            return Ok();
        }

        [HttpGet("filtro/{nome}")]
        public ActionResult GetFiltro(string nome)
        {
            var listHeroi = _contexto.Herois
                .Where(h => h.Nome.Contains(nome))
                .ToList();

           return Ok(listHeroi);
        }

        [HttpGet("AddRange")]
        public ActionResult GetAddRange()
        {

            _contexto.AddRange(
                new Heroi { Nome = "Capitão America"},
                new Heroi { Nome = "Doutor Estranho"},
                new Heroi { Nome = "Pantera Negra"},
                new Heroi { Nome = "Viúva Negra "},
                new Heroi { Nome = "Hulk"},
                new Heroi { Nome = "Gavião Arqueiro"},
                new Heroi { Nome = "Capitã Marvel"}
                );

            _contexto.SaveChanges();

            return Ok();
        }

        [HttpGet("deletehero/{id}")]
        public void DeleteHero(int id)
        {
            var heroi = _contexto.Herois
                 .Where(h => h.Id == id)
                 .Single();

            _contexto.Herois.Remove(heroi);
            _contexto.SaveChanges();
        }
    }
}
