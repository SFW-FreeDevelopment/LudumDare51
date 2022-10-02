using LD51.Unity.Services;
using UnityEngine;

namespace LD51.Unity.Behaviors
{
    public class ApiTest : MonoBehaviour
    {
        public void Save()
        {
            PlayerService.Save();
        }

        public void FetchAll()
        {
            PlayerService.FetchAll();
        }
        
        public void Fetch()
        {
            PlayerService.Fetch();
        }
    }
}