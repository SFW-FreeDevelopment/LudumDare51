using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace LD51.Unity.Managers
{
    public class DecorManager : MonoBehaviour
    {
        private SpriteRenderer _playerSpriteRenderer;
        private Transform _playerTransform;
        private List<(Vector2 Position, SpriteRenderer SpriteRenderer)> _decorList;
        
        private void Start()
        {
            var playerObj = GameObject.FindGameObjectWithTag("Player");
            _playerTransform = playerObj.transform;
            _playerSpriteRenderer = playerObj.GetComponent<SpriteRenderer>();
            
            _decorList = GameObject.FindGameObjectsWithTag("Decor")
                .Select(x => new ValueTuple<Vector2, SpriteRenderer>(
                        x.transform.position, x.GetComponent<SpriteRenderer>()
                ))
                .ToList();
        }

        private void FixedUpdate()
        {
            foreach (var decor in _decorList)
            {
                try
                {
                    if (decor.Position.y > _playerTransform.position.y)
                    {
                        decor.SpriteRenderer.sortingOrder = _playerSpriteRenderer.sortingOrder - 1;
                    }
                    else if (decor.Position.y < _playerTransform.position.y)
                    {
                        decor.SpriteRenderer.sortingOrder = _playerSpriteRenderer.sortingOrder + 1;
                    }
                }
                catch(Exception e)
                {
                    Debug.LogError(e.Message);
                }
            }
        }
    }
}