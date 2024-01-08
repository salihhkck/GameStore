using System.ComponentModel.DataAnnotations;

namespace GameStore.Api.Entitites;

public class Game
{
    public int Id { get; set; }

    [Required]
    [StringLength(50)]
    public required string Name { get; set; }

    [Required]
    [StringLength(30)]
    public required string Genre { get; set; }


    [Range(1, 200)]
    public decimal Price { get; set; }

    [Required]
    public DateTime ReleaseDate { get; set; }

    [Required]
    [Url]
    [StringLength(100)]
    public required string ImageUri { get; set; }
}