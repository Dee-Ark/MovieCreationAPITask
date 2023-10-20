using System.ComponentModel.DataAnnotations.Schema;

namespace MovieCreationAPI.Model.Domain
{
    public class Movie
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime ReleaseDate { get; set; } = DateTime.UtcNow;
        public string? Rating { get; set; }
        public decimal TicketPrice { get; set; }
        public string? Country { get; set; }
        public genres? Genre { get; set; }
        [NotMapped]
        public IFormFile Photo { get; set; }
    }

    public class genres
    {
        public long Id { get; set; }
        public string Action { get; set; }
        public string Love { get; set; }
        public string Horror { get; set; }
        public string Drama { get; set; }
        public string Thriller { get; set; }
        public string Roman { get; set; }
        public string Cumedy { get; set; }
    }
}
