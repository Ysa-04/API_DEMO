using CityInfo2.Models;

namespace CityInfo2
{
    public class CitiesDataStore
    {
        public static CitiesDataStore Current { get; } = new(); //Static property waarmee je altijd dezelfde instantie van de class kunt gebruiken.
        public List<CityDto> Cities { get; set; }
        public CitiesDataStore()
        {
            Cities = [
                new CityDto()
                {
                    Id = 1,
                    Name = "New York City",
                    Description = "The one with that big park.",
                    PointsOfInterest = [ 
                        new() 
                        {
                            Id = 1,
                            Name = "Central Park",
                            Description = "The most visited urban park in the United States."
                        },
                        new() 
                        {
                            Id = 2,
                            Name = "Empire State Building",
                            Description = "A 102-story skyscraper located in Midtown Manhattan."
                        }
                    ]
                },
                new CityDto()
                {
                    Id = 2,
                    Name = "Antwerp",
                    Description = "The one with the cathedral",
                    PointsOfInterest  = [
                        new()
                        {
                            Id = 1,
                            Name = "Cathedral of Our Lady",
                            Description = "A Gothic style cathedral, conceived by architects Jan and Pieter Appelmans."
                        },
                        new()
                        {
                            Id = 2,
                            Name = "Antwerp Central Station",
                            Description = "The the finest example of railway architecture in Belgium."
                        }
                    ]
                },
                new CityDto()
                {
                    Id = 3,
                    Name = "Paris",
                    Description = "The one with ladybug and chat noir",
                    PointsOfInterest  = [
                        new()
                        {
                            Id = 1,
                            Name = "Eiffel tower",
                            Description = "A Gothic style cathedral, conceived by architects Jan and Pieter Appelmans."
                        },
                        new()
                        {
                            Id = 2,
                            Name = "Bakery",
                            Description = "The the finest example of railway architecture in Belgium."
                        }
                    ]
                }
            ];
        }


        
    }
}
