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

        public readonly HeroiContexto _context;
        public ValueController(HeroiContexto contexto)
        {
            _context = contexto;
        }

        [HttpGet("{getallheros}")]
        public ActionResult GetAllHeros()
        {
            var HeroiList = _context.Herois.ToList();
            return Ok(HeroiList);
        }


        [HttpGet("Atualizar/{nameHero}")]
        public ActionResult Get(string nameHero)
        {
            //var heroi = new Heroi { 
            //Nome = nameHero
            //};

            var heroi = _context.Herois
                            .Where(h => h.Id == 5)
                            .FirstOrDefault();

            heroi.Nome = "Homem Aranha";
            //_context.Herois.Add(heroi);
            _context.SaveChanges();

            return Ok();
        }

        [HttpGet("filtro/{nome}")]
        public ActionResult GetFiltro(string nome)
        {
            var listHeroi = _context.Herois
                .Where(h => h.Nome.Contains(nome))
                .ToList();

           return Ok(listHeroi);
        }

        [HttpGet("AddRange")]
        public ActionResult GetAddRange()
        {

            _context.AddRange(
                new Heroi { Nome = "Capitão America"},
                new Heroi { Nome = "Doutor Estranho"},
                new Heroi { Nome = "Pantera Negra"},
                new Heroi { Nome = "Viúva Negra "},
                new Heroi { Nome = "Hulk"},
                new Heroi { Nome = "Gavião Arqueiro"},
                new Heroi { Nome = "Capitã Marvel"}
                );

            _context.SaveChanges();

            return Ok();
        }

        [HttpGet("deletehero/{id}")]
        public void DeleteHero(int id)
        {
            var heroi = _context.Herois
                 .Where(h => h.Id == id)
                 .Single();

            _context.Herois.Remove(heroi);
            _context.SaveChanges();
        }
    }
}
