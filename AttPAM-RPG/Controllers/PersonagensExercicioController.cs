using Microsoft.AspNetCore.Mvc;
using AttPAM_RPG.Models.Enuns;
using AttPAM_RPG.Models;
using Microsoft.SqlServer.Server;
using System.Runtime.InteropServices;

namespace AttPAM_RPG.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonagensExercicioController : ControllerBase
    {
        private static List<Personagem> personagens = new List<Personagem>()
        {
            new Personagem() { Id = 1, Nome = "Frodo", PontosVida=100, Forca=17, Defesa=23, Inteligencia=33, Classe=ClasseEnum.Cavaleiro},
            new Personagem() { Id = 2, Nome = "Sam", PontosVida=100, Forca=15, Defesa=25, Inteligencia=30, Classe=ClasseEnum.Cavaleiro},
            new Personagem() { Id = 3, Nome = "Galadriel", PontosVida=100, Forca=18, Defesa=21, Inteligencia=35, Classe=ClasseEnum.Clerigo },
            new Personagem() { Id = 4, Nome = "Gandalf", PontosVida=100, Forca=18, Defesa=18, Inteligencia=37, Classe=ClasseEnum.Mago },
            new Personagem() { Id = 5, Nome = "Hobbit", PontosVida=100, Forca=20, Defesa=17, Inteligencia=31, Classe=ClasseEnum.Cavaleiro },
            new Personagem() { Id = 6, Nome = "Celeborn", PontosVida=100, Forca=21, Defesa=13, Inteligencia=34, Classe=ClasseEnum.Clerigo },
            new Personagem() { Id = 7, Nome = "Radagast", PontosVida=100, Forca=25, Defesa=11, Inteligencia=35, Classe=ClasseEnum.Mago }
        };

        [HttpGet("GetByNome/{nome}")]
        public IActionResult GetByNome(string nome)
        {
            List<Personagem> listaBusca = personagens.FindAll(p => p.Nome.Contains(nome));
            return Ok(listaBusca);
        }

        [HttpGet("GetClerigoMago")]
        public IActionResult GetClerigoMago()
        {
            List<Personagem> listaBusca = personagens.FindAll(p => p.Classe != ClasseEnum.Cavaleiro).OrderByDescending(p => p.PontosVida).ToList();
            return Ok(listaBusca);
        }

        [HttpGet("GetEstatisticas")]
        public IActionResult GetQuantidade()
        {
            return Ok(new
            {
                QuantidadePersonagens = personagens.Count,
                SomaInteligencia = personagens.Sum(p => p.Inteligencia)
            });
        }

        [HttpPost("PostValidacao")]
        public IActionResult PostValidacao(Personagem novoPersonagem)
        {
            if (novoPersonagem.Defesa <= 10 || novoPersonagem.Inteligencia <= 30)
                return BadRequest("Defesa deve ser igual ou maior 10 e Inteligência maior ou igual a 30");

            personagens.Add(novoPersonagem);
            return Ok(novoPersonagem);
        }

        [HttpGet("GetByClasse/{classe}")]
        public IActionResult GetByClasse(ClasseEnum classe)
        {
            List<Personagem> personagensDaClasse = personagens.FindAll(p => p.Classe == classe);
            return Ok(personagensDaClasse);
        }

        [HttpPost("PostValidacaoMago")]
        public IActionResult PostValidacaoMago(Personagem novoPersonagem)
        {
            if (novoPersonagem.Classe != ClasseEnum.Mago)
                return BadRequest("Apenas Magos aqui");

            if (novoPersonagem.Inteligencia < 35)
                return BadRequest("Magos tem que ter Inteligência igual ou acima de 35");

            personagens.Add(novoPersonagem);
            return Ok(novoPersonagem);
        }
    }
}
