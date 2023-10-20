using System.ComponentModel.DataAnnotations;

namespace MovieCreationAPI.Model.Dto
{
    public class addMovieRequestDTO
    {
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Description { get; set; }
        [Required]
        public DateTime ReleaseDate { get; set; }
        [Required]
        public string? Rating { get; set; }
        [Required]
        public decimal TicketPrice { get; set; }
        [Required]
        public string? Country { get; set; }
        [Required]
        public string? Genre { get; set; }
        [Required]
        //public string? Photo { get; set; }
        public string Photo { get; set; }
    }
}
