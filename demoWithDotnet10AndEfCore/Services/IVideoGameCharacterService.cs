using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using demoWithDotnet10AndEfCore.Dtos;
using demoWithDotnet10AndEfCore.Models;

namespace demoWithDotnet10AndEfCore.Services
{
    public interface IVideoGameCharacterService
    {
        Task<List<CharacterResponse>> GetCharactersAsync();
        Task<CharacterResponse?> GetCharacterByIdAsync(int id);

        Task<Character> AddCharacterAsync(Character character);
        Task<Character> UpdateCharacterAsync(int id, Character character);
        Task<Character> DeleteCharacterAsync(int id);
    }
}