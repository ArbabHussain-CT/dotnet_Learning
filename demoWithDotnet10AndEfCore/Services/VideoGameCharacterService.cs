using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using demoWithDotnet10AndEfCore.Data;
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

        public async Task<Character?> GetCharacterByIdAsync(int id)
        {
            var result = await _context.Characters.FindAsync(id);
            return result;
        }

        public async Task<List<Character>> GetCharactersAsync()
        {
            return await _context.Characters.ToListAsync();
        }

        public Task<Character> UpdateCharacterAsync(int id, Character character)
        {
            throw new NotImplementedException();
        }
    }
}