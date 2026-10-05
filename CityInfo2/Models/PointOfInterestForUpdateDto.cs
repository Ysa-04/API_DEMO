using System.ComponentModel.DataAnnotations;

namespace CityInfo2.Models
{
    public class PointOfInterestForUpdateDto
    {
        [Required(ErrorMessage = "You should provide a name value")]
        [MaxLength(50)]

        public string Name { get; set; } = string.Empty;
        [MaxLength(200)]
        public string? Description { get; set; }
        // DTO voor een PUT-request: Hiermee vang je de nieuwe waarden op 
        // en valideer je de invoer voordat je een bestaand object in de database bijwerkt.
    }
}
