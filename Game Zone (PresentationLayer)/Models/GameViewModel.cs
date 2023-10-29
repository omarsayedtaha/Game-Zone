using Core_Layer;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;
using System.ComponentModel.DataAnnotations;

namespace Game_Zone__PresentationLayer_.Models
{
    public class GameViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is Required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description is Required")]
        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }


        public string? Poster { get; set; }

        public IFormFile? PosterImage { get; set; }

        [Required(ErrorMessage = "CategoryId is Required")]
        public int? CategoryId { get; set; }
        public Category? Category { get; set; }

        [Required(ErrorMessage = "DevicesIds is Required")]
        public List<int>? DevicesIds { get; set; } 
        //public List<Device>? Devices { get; set; } 
        public List<GameDevices>? GameDevices { get; set; } 
    }
}
