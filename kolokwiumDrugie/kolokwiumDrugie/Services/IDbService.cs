using kolokwiumDrugie.DTOs;
using kolokwiumDrugie.Models;

namespace kolokwiumDrugie.Services;

public interface IDbService
{
    Task<CharacterDto?> GetCharacterByIdAsync(int characterId);
    Task<List<Backpacks>> AddItemsToBackpackAsync(int characterId, List<int> itemIds);
}