namespace PMG.Domain
{
    public abstract class Fact
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string? Author { get; set; }
    }
}