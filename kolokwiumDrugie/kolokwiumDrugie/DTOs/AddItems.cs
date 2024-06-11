using System.ComponentModel.DataAnnotations;

namespace kolokwiumDrugie.DTOs;

    public class AddItems
    {
        [Required]
        public List<int> ItemIds { get; set; } = new List<int>();
    }