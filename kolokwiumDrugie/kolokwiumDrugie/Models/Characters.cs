﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace kolokwiumDrugie.Models;

[Table("characters")]
public class Characters
{
    [Key]
    public int Id { get; set; }
    [MaxLength(50)]
    public string FirstName { get; set; } = string.Empty;
    [MaxLength(120)]
    public string LastName { get; set; }= string.Empty;
    public int CurrentWeight { get; set; }
    public int MaxWeight { get; set; }



    public ICollection<Backpacks> Backpacks { get; set; } = new HashSet<Backpacks>();
    public ICollection<Character_titles> CharacterTitles { get; set; } = new HashSet<Character_titles>();

}
