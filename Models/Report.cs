namespace kodmeydan.Models
{
    public class Report
    {
        public int Id { get; set; }
        public string? Reason { get; set; }
        public DateTime CreatedAt { get; set; }
        public int? ArticleId { get; set; }
        public int? ProjectId { get; set; }
    }
}