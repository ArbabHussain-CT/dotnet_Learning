using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using demoWithDotnet10AndEfCore.Dtos;
using demoWithDotnet10AndEfCore.Models;
using demoWithDotnet10AndEfCore.Services;
using Microsoft.AspNetCore.Mvc;

namespace demoWithDotnet10AndEfCore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VideoGameCharactersController(IVideoGameCharacterService videoGameCharacterService) : ControllerBase
    {



        [HttpGet]
        public async Task<ActionResult<List<CharacterResponse>>> GetCharacters()
        {
            return Ok(await videoGameCharacterService.GetCharactersAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CharacterResponse?>> GetCharacterById(int id)
        {
            var character = await videoGameCharacterService.GetCharacterByIdAsync(id);

            if (character is null)
            {
                return NotFound();
            }

            return Ok(character);
        }

    }
}