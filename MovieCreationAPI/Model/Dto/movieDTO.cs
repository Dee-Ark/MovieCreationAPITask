using System.ComponentModel.DataAnnotations.Schema;

namespace MovieCreationAPI.Model.Dto
{
    public class movieDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ReleaseDate { get; set; }
        public int Rating { get; set; }
        public decimal TicketPrice { get; set; }
        public string Country { get; set; }
        public List<string> Genres { get; set; } 
        //public string Photo { get; set; }
    }
}
