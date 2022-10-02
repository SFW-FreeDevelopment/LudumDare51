using LD51.Unity.Models;
using LD51.Unity.Services;
using UnityEngine;

namespace LD51.Unity.Behaviors
{
    public class ApiTest : MonoBehaviour
    {
        public void Create()
        {
            PlayerService.Create(new Player
            {
                DisplayName = "TestUser"
            });
        }

        public void FetchAll()
        {
            PlayerService.FetchAll();
        }
        
        public void Fetch()
        {
            PlayerService.Fetch("2acb7b69-e022-4928-817b-ef84ee15771c");
        }
    }
}