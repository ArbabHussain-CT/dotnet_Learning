using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using demoWithDotnet10AndEfCore.Data;
using demoWithDotnet10AndEfCore.Dtos;
using demoWithDotnet10AndEfCore.Models;
using Microsoft.EntityFrameworkCore;

namespace demoWithDotnet10AndEfCore.Services
{
    public class VideoGameCharacterService(AppDbContext _context) : IVideoGameCharacterService
    {

        public Task<Character> AddCharacterAsync(Character character)
        {
            throw new NotImplementedException();
        }

        public Task<Character> DeleteCharacterAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<CharacterResponse?> GetCharacterByIdAsync(int id)
        {
            var result = await _context.Characters.Where(c => c.Id == id).Select(c => new CharacterResponse
            {
                Name = c.Name,
                Role = c.Role,
                Game = c.Game,
            }).FirstOrDefaultAsync();

            return result;
        }

        public async Task<List<CharacterResponse>> GetCharactersAsync()
        {
            return await _context.Characters.Select(c => new CharacterResponse
            {
                Name = c.Name,
                Role = c.Role,
                Game = c.Game,
            }).ToListAsync();
        }

        public Task<Character> UpdateCharacterAsync(int id, Character character)
        {
            throw new NotImplementedException();
        }
    }
}