namespace kodmeydan.Models
{
    public class Article
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        
        public DateTime CreatedAt { get; set; }
        public string? UserId{get; set;}
        public ApplicationUser? User { get; set; }
        public string? Ticket { get; internal set; }
        public string? Link { get; internal set; }
    }
}