namespace Domain.Models
{
    public class VoteRecord
    {
        public int Id { get; set; }
        public required string UserId { get; set; }
        public int PollId { get; set; }
    }
}
