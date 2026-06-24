using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using demoWithDotnet10AndEfCore.Models;

namespace demoWithDotnet10AndEfCore.Services
{
    public class VideoGameCharacterService : IVideoGameCharacterService
    {
        static List<Character> characters = new List<Character>
        {
            new Character { Id = 1, Name = "Mario", Game = "Super Mario Bros.", Role = "Protagonist" },
            new Character { Id = 2, Name = "Link", Game = "The Legend of Zelda", Role = "Protagonist" },
            new Character { Id = 3, Name = "Master Chief", Game = "Halo", Role = "Protagonist" },
            new Character { Id = 4, Name = "Lara Croft", Game = "Tomb Raider", Role = "Protagonist" },
            new Character { Id = 5, Name = "Kratos", Game = "God of War", Role = "Protagonist" }
        };


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
            var result = characters.FirstOrDefault(c => c.Id == id);
            return await Task.FromResult(result);
        }

        public async Task<List<Character>> GetCharactersAsync()
        {
            return await Task.FromResult(characters);
        }

        public Task<Character> UpdateCharacterAsync(int id, Character character)
        {
            throw new NotImplementedException();
        }
    }
}