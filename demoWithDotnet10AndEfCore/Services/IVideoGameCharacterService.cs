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

        Task<CharacterResponse> AddCharacterAsync(CreateCharacterRequest character);
        Task<bool> UpdateCharacterAsync(int id, UpdateCharacterRequest character);
        Task<bool> DeleteCharacterAsync(int id);
    }
}