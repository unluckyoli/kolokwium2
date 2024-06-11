using kolokwiumDrugie.Models;
namespace kolokwiumDrugie.Services;
using Microsoft.EntityFrameworkCore;
using kolokwiumDrugie.Data;
using kolokwiumDrugie.DTOs;

public class DbService : IDbService 
{
    private readonly DatabaseContext _context;

    public DbService(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<List<Backpacks>> AddItemsToBackpackAsync(int characterId, List<int> itemIds)
    {
        var character = await _context.Characters
            .Include(c => c.Backpacks)
            .ThenInclude(bp => bp.Items)
            .FirstOrDefaultAsync(c => c.Id == characterId);

        if (character == null)
            throw new KeyNotFoundException("Character not found.");

        var items = await _context.Items
            .Where(i => itemIds.Contains(i.Id))
            .ToListAsync();

        if (items.Count != itemIds.Count)
            throw new KeyNotFoundException("One or more items not found.");

        var totalNewWeight = items.Sum(i => i.Weight);
        var newTotalWeight = character.CurrentWeight + totalNewWeight;

        if (newTotalWeight > character.MaxWeight)
            throw new InvalidOperationException("Character cannot carry that much weight.");

        foreach (var item in items)
        {
            var existingBackpackItem = character.Backpacks.FirstOrDefault(bp => bp.ItemId == item.Id);

            if (existingBackpackItem != null)
            {
                existingBackpackItem.Amount += 1;
            }
            else
            {
                character.Backpacks.Add(new Backpacks
                {
                    CharacterId = characterId,
                    ItemId = item.Id,
                    Amount = 1
                });
            }
        }

        character.CurrentWeight = newTotalWeight;

        await _context.SaveChangesAsync();

        return character.Backpacks.ToList();
    }
    
    
    
    
    public async Task<CharacterDto?> GetCharacterByIdAsync(int characterId)
    {
        var character = await _context.Characters
            .Include(c => c.Backpacks)
            .ThenInclude(bp => bp.Items)
            .Include(c => c.CharacterTitles)
            .ThenInclude(ct => ct.Titles)
            .FirstOrDefaultAsync(c => c.Id == characterId);

        if (character == null)
            return null;

        return new CharacterDto
        {
            FirstName = character.FirstName,
            LastName = character.LastName,
            CurrentWeight = character.CurrentWeight,
            MaxWeight = character.MaxWeight,
            BackpackItems = character.Backpacks.Select(bp => new BackpackItemDto
            {
                ItemName = bp.Items.Name,
                ItemWeight = bp.Items.Weight,
                Amount = bp.Amount
            }).ToList(),
            Titles = character.CharacterTitles.Select(ct => new TitleDto
            {
                Title = ct.Titles.Name,
                AcquiredAt = ct.AcquiredAt
            }).ToList()
        };
    }
}
