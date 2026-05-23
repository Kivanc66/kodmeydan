namespace kodmeydan.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        
        
        public string? GitHubUrl { get; set; }
        public string? LiveDemoUrl { get; set; }
        public string? UserId { get; set; }
        public ApplicationUser? User { get; set; }
        public string? Ticket { get; set; }
        public string? Code { get; set; }
        public string? Content { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}