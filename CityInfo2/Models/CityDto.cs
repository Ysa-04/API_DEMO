namespace CityInfo2.Models
{
    public class CityDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty; 
        public string? Description { get; set; }
        public int NumberOfInterest => PointsOfInterest.Count;
        public ICollection<PointOfInterestDto> PointsOfInterest { get; set; } = new List<PointOfInterestDto>();


        // als ge een naam niet invult gaat da leeg zijn, een description kan null zijn. 
        // lege string is een lege string en niet null, string is een referencetype
        // referencetype: je kunt de waarde niet veranderen, maar je kunt wel een nieuwe waarde toewijzen.
        // string is een reference type dat zich gedraagt als een value type
        // string.isnullorempty kan je gebruiken om te checken of een string leeg is of null.



    }
}
