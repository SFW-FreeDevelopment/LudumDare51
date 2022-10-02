using System;
using System.Collections.Generic;
using LD51.Unity.Extensions;
using LD51.Unity.Models;
using Newtonsoft.Json;
using Proyecto26;
using UnityEngine;

namespace LD51.Unity.Clients
{
    public class PlayerClient
    {
        public static void Create(Player player, Action<Player> successCallback)
        {
            var json = JsonConvert.SerializeObject(player, Formatting.Indented);
            Debug.Log(json);
        
            var url = $"{Constants.ApiUrl}/players";
            Debug.Log(url);
        
            RestClient.Post(url, json)
                .Then(response =>
                {
                    Debug.Log("Request successful");
                    var deserializedPlayer = JsonConvert.DeserializeObject<Player>(response.Text);
                    successCallback(deserializedPlayer);
                })
                .Catch(error =>
                {
                    Debug.Log("Request failed");
                    Debug.Log(error?.InnerException?.Message ?? "");
                    Debug.Log($"Could not save the player.{Environment.NewLine}{error?.Message}");
                });
        }
        
        public static void FetchAll(Action<List<Player>> successCallback)
        {
            var url = $"{Constants.ApiUrl}/players";
            Debug.Log(url);
        
            RestClient.Get(url)
                .Then(response =>
                {
                    Debug.Log("Request successful");
                    var deserializedPlayers = JsonConvert.DeserializeObject<List<Player>>(response.Text);
                    successCallback(deserializedPlayers);
                    Debug.Log(JsonConvert.SerializeObject(deserializedPlayers, Formatting.Indented));
                })
                .Catch(error =>
                {
                    Debug.Log("Request failed");
                    Debug.Log(error?.InnerException?.Message ?? "");
                    Debug.Log($"Could not fetch the players.{Environment.NewLine}{error?.Message}");
                });
        }

        public static void Fetch(string id, Action<Player> successCallback)
        {
            var url = $"{Constants.ApiUrl}/players/{id}";
            Debug.Log(url);
        
            RestClient.Get(url)
                .Then(response =>
                {
                    Debug.Log("Request successful");
                    var deserializedPlayer = JsonConvert.DeserializeObject<Player>(response.Text);
                    successCallback(deserializedPlayer);
                    Debug.Log(JsonConvert.SerializeObject(deserializedPlayer, Formatting.Indented));
                })
                .Catch(error =>
                {
                    Debug.Log("Request failed");
                    Debug.Log(error?.InnerException?.Message ?? "");
                    Debug.Log($"Could not fetch the player.{Environment.NewLine}{error?.Message}");
                });
        }
        
        public static void ProcessGameResults(string id, GameResults gameResults, Action<Player> successCallback)
        {
            var url = $"{Constants.ApiUrl}/players/{id}/processGameResults";
            Debug.Log(url);
        
            RestClient.Patch(url, gameResults)
                .Then(response =>
                {
                    Debug.Log("Request successful");
                    var deserializedPlayer = JsonConvert.DeserializeObject<Player>(response.Text);
                    successCallback(deserializedPlayer);
                    Debug.Log(JsonConvert.SerializeObject(deserializedPlayer, Formatting.Indented));
                })
                .Catch(error =>
                {
                    Debug.Log("Request failed");
                    Debug.Log(error?.InnerException?.Message ?? "");
                    Debug.Log($"Could not fetch the player.{Environment.NewLine}{error?.Message}");
                });
        }
    }
}