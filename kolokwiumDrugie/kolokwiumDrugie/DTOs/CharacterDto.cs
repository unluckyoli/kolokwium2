namespace kolokwiumDrugie.DTOs;

public class CharacterDto
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public int CurrentWeight { get; set; }
        public int MaxWeight { get; set; }
        public List<BackpackItemDto> BackpackItems { get; set; } = new List<BackpackItemDto>();
        public List<TitleDto> Titles { get; set; } = new List<TitleDto>();
    }

    public class BackpackItemDto
    {
        public string ItemName { get; set; } = string.Empty;
        public int ItemWeight { get; set; }
        public int Amount { get; set; }
    }

    public class TitleDto
    {
        public string Title { get; set; } = string.Empty;
        public DateTime AcquiredAt { get; set; }
    }

