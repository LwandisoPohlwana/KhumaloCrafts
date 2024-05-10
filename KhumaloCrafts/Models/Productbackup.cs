 using Microsoft.AspNetCore.Mvc.ApplicationParts;
using System.Xml.Linq;

namespace KhumaloCrafts.Models
{
    public class Productbackup
    {
            public int Id { get; set; }
            public string Title { get; set; }
            public string ArtForm { get; set; }
            public string Artist { get; set; }
            public string Description { get; set; }
            public decimal Price { get; set; }
            public string ImageUrl { get; set; }
            // Common properties and methods

            // Constructor
            public Productbackup(int id, string name, string artForm, string artist, string description, decimal price, string imageUrl)
            {
            Id = id;
            Title = name;
            Artist = artist;
            Description = description;
            Price = price;
            ImageUrl = imageUrl;
            //types of the artForms avalable
            if (artForm.ToLower() == "painting" || artForm.ToLower() == "sculpture" || artForm.ToLower() == "pottery" || artForm.ToLower() == "wood carving" || artForm.ToLower() == "beadwork" || artForm.ToLower() == "glass")
            {
                ArtForm = artForm;
            }
            else
            {
                ArtForm = "Unknown";
            }
        }
    }
}

