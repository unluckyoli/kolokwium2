using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace kolokwiumDrugie.Models;

[Table("titles")]
public class Titles
{
    [Key]
    public int Id { get; set; }
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;
    
    public ICollection<Character_titles> CharacterTitles { get; set; } = new HashSet<Character_titles>();

}
