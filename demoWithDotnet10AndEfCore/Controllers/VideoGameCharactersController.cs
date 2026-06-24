using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using demoWithDotnet10AndEfCore.Models;
using Microsoft.AspNetCore.Mvc;

namespace demoWithDotnet10AndEfCore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VideoGameCharactersController : ControllerBase
    {

        static List<Character> characters = new List<Character>
        {
            new Character { Id = 1, Name = "Mario", Game = "Super Mario Bros.", Role = "Protagonist" },
            new Character { Id = 2, Name = "Link", Game = "The Legend of Zelda", Role = "Protagonist" },
            new Character { Id = 3, Name = "Master Chief", Game = "Halo", Role = "Protagonist" },
            new Character { Id = 4, Name = "Lara Croft", Game = "Tomb Raider", Role = "Protagonist" },
            new Character { Id = 5, Name = "Kratos", Game = "God of War", Role = "Protagonist" }
        };

        [HttpGet]
        public async Task<ActionResult<List<Character>>> GetCharacters()
        {
            return Ok(characters);
        }


    }
}