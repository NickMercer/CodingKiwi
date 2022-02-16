using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TinyZoo.Characters.Inputs.Commands;
using UnityEngine;

namespace TinyZoo.FruitSpawning
{
    public class FruitSpawner : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField]
        private List<KeyFruitBinding> _fruitKeys;

        [SerializeField]
        [Tooltip("The Z Level to spawn fruits at.")]
        private float _fixedZLevel;

        private Camera _cam;
        private CommandPickup _selectedFruit;

        private void Awake()
        {
            _cam = Camera.main;
        }

        private void Start()
        {
            _selectedFruit = _fruitKeys.First().CommandFruit;
        }

        private void Update()
        {
            UpdateFruitSelection();
            SpawnFruitOnClick();
        }

        private void UpdateFruitSelection()
        {
            foreach (var pair in _fruitKeys)
            {
                if (Input.GetKeyDown(pair.Key))
                {
                    _selectedFruit = pair.CommandFruit;
                }
            }
        }
        private void SpawnFruitOnClick()
        {
            if (Input.GetMouseButtonUp(0))
            {
                var mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, _cam.transform.position.z * -1);
                var mouseWorldPosition = _cam.ScreenToWorldPoint(mousePos);
                mouseWorldPosition.z = _fixedZLevel;

                Instantiate(_selectedFruit, mouseWorldPosition, Quaternion.identity);
            }
        }
    }
}
