namespace mBitionChallengeStervan.models
{
    public class Event
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public required DateTime Date { get; set; }
        public string? Location { get; set; }
    }
}
