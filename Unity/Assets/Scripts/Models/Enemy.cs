using System;
using UnityEngine;

namespace LD51.Unity.Models
{
    [CreateAssetMenu(menuName = "LD51/Monster")]
    public class Enemy : ScriptableObject
    {
        public string Id = Guid.NewGuid().ToString();
        public string Name => name;
        public ushort Health = 10;
        public byte Damage = 10;
        public float Speed = 1;
        public string SoundName;
    }
}