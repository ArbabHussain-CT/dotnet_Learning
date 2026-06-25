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

        public async Task<CharacterResponse> AddCharacterAsync(CreateCharacterRequest character)
        {
            var newCharacter = new Character
            {
                Name = character.Name,
                Game = character.Game,
                Role = character.Role
            };
            _context.Characters.Add(newCharacter);
            await _context.SaveChangesAsync();

            return new CharacterResponse
            {
                Id = newCharacter.Id,
                Name = newCharacter.Name,
                Game = newCharacter.Game,
                Role = newCharacter.Role
            };


        }

        public Task<bool> DeleteCharacterAsync(int id)
        {
            var existingCharacter = _context.Characters.FirstOrDefault(c => c.Id == id);

            if (existingCharacter is null)
            {
                return Task.FromResult(false);
            }

            _context.Characters.Remove(existingCharacter);
            _context.SaveChanges();

            return Task.FromResult(true);
        }

        public async Task<CharacterResponse?> GetCharacterByIdAsync(int id)
        {
            var result = await _context.Characters.Where(c => c.Id == id).Select(c => new CharacterResponse
            {
                Id = c.Id,
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
                Id = c.Id,
                Name = c.Name,
                Role = c.Role,
                Game = c.Game,
            }).ToListAsync();
        }

        public Task<bool> UpdateCharacterAsync(int id, UpdateCharacterRequest character)
        {

            var existingCharacter = _context.Characters.FirstOrDefault(c => c.Id == id);

            if (existingCharacter is null)
            {
                return Task.FromResult(false);
            }

            existingCharacter.Name = character.Name;
            existingCharacter.Game = character.Game;
            existingCharacter.Role = character.Role;

            _context.SaveChanges();

            return Task.FromResult(true);
        }
    }
}