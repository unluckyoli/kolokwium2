using Microsoft.AspNetCore.Mvc;
using kolokwiumDrugie.DTOs;
using kolokwiumDrugie.Services;

namespace kolokwiumDrugie.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CharactersController : ControllerBase
    {
        private readonly IDbService _characterService;

        public CharactersController(IDbService dbService)
        {
            _characterService = dbService;
        }

        [HttpPost("{characterId}/backpacks")]
        public async Task<IActionResult> AddItemsToBackpack(int characterId, [FromBody] AddItems request)
        {
            if (request == null || request.ItemIds.Count == 0)
                return BadRequest("ItemIds list is required.");

            try
            {
                var backpackItems = await _characterService.AddItemsToBackpackAsync(characterId, request.ItemIds);

                return Ok(backpackItems.Select(bp => new
                {
                    bp.Amount,
                    bp.ItemId,
                    bp.CharacterId
                }));
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        
        [HttpGet("{characterId}")]
        public async Task<IActionResult> GetCharacter(int characterId)
        {
            var character = await _characterService.GetCharacterByIdAsync(characterId);
            if (character == null)
                return NotFound($"Character with ID {characterId} not found.");

            return Ok(character);
        }
    }
}

