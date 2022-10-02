using System;
using LD51.Unity.Services;

namespace LD51.Unity.Models
{
    public class Player
    {
        public string Id { get; set; }
        public string DisplayName { get; set; }
        public string ShirtColor { get; set; }
        public string PantColor { get; set; }
        public ushort Score { get; set; }
        public ushort Waves { get; set; }

        public Player()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}