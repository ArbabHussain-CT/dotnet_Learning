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

        [HttpPost]
        public async Task<ActionResult<CharacterResponse>> AddCharacter(CreateCharacterRequest request)
        {


            var createdCharacter = await videoGameCharacterService.AddCharacterAsync(request);



            return CreatedAtAction(nameof(GetCharacterById), new { id = createdCharacter.Id }, createdCharacter);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCharacter(int id, UpdateCharacterRequest request)
        {


            var updatedCharacter = await videoGameCharacterService.UpdateCharacterAsync(id, request);

            if (!updatedCharacter)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCharacter(int id)
        {
            var deletedCharacter = await videoGameCharacterService.DeleteCharacterAsync(id);

            if (!deletedCharacter)
            {
                return NotFound();
            }

            return NoContent();
        }

    }
}