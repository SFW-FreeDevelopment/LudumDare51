using System;
using System.Collections.Generic;
using LD51.Unity.Clients;
using LD51.Unity.Models;
using UnityEditor;

namespace LD51.Unity.Services
{
    public static class PlayerService
    {
        public static Player Player { get; set; } = new();
        public static List<Player> Players { get; set; } = new();
        public static DateTime? LastFetched { get; set; }

        public static void Create(Player newPlayer)
        {
            PlayerClient.Create(newPlayer, player => { Player = player; });
        }
        
        public static void FetchAll(Action<List<Player>> action = null)
        {
            if (LastFetched == null || LastFetched.Value.AddMinutes(1) < DateTime.Now)
            {
                PlayerClient.FetchAll(players =>
                {
                    Players = players;
                    action?.Invoke(Players);
                });
            }
            else
            {
                action?.Invoke(Players);
            }
        }

        public static void Fetch(string id, Action<Player> action = null)
        {
            if (LastFetched == null || LastFetched.Value.AddMinutes(1) < DateTime.Now)
            {
                PlayerClient.Fetch(id, player =>
                {
                    Player = player;
                    action?.Invoke(Player);
                });
            }
            else
            {
                action?.Invoke(Player);
            }
        }
    }
}