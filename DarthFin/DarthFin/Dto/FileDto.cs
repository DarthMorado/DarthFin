namespace DarthFin.Dto
{
    public class FileDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte[] Content { get; set; }
        public int UserId { get; set; }
        public bool IsProcessed { get; set; }
    }
}
