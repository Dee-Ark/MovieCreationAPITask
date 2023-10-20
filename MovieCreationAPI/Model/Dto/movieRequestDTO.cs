namespace MovieCreationAPI.Model.Dto
{
    public class movieRequestDTO
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string? Rating { get; set; }
        public decimal TicketPrice { get; set; }
        public string? Country { get; set; }
        public string? Genre { get; set; }
        //public string? Photo { get; set; }
        public string ImageBase64 { get; set; }
    }
}
