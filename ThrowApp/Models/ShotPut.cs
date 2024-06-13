namespace ThrowApp.Models
{
    public class ShotPut
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Country { get; set; }
        public double Result { get; set; }
        public int Position { get; set; }

        public ShotPut()
        {
        }
    }
}
