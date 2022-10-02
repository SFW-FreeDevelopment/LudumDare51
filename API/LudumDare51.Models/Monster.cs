namespace LudumDare51.Models
{
    public class Monster
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public ushort Health { get; set; }

        public byte Damage { get; set; }

        public float Speed { get; set; }
    }
}
