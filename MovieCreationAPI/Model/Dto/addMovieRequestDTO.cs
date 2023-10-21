using System.ComponentModel.DataAnnotations;

namespace MovieCreationAPI.Model.Dto
{
    public class addMovieRequestDTO
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string ReleaseDate { get; set; }
        [Required]
        public int Rating { get; set; }
        [Required]
        public decimal TicketPrice { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public List<string> Genres { get; set; } 
        //[Required]
        //public IFormFile Photo { get; set; }

    }
}
