using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TinyZoo.Characters.Inputs.Commands;
using UnityEngine;
using UnityEngine.EventSystems;

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
            foreach (var fruitOption in _fruitKeys)
            {
                if (Input.GetKeyDown(fruitOption.Key))
                {
                    _selectedFruit = fruitOption.CommandFruit;
                }
            }
        }

        public void SelectFruit(string fruitId)
        {
            var fruitOption = _fruitKeys.FirstOrDefault(x => x.FruitId == fruitId);
            if(fruitOption != null)
            {
                _selectedFruit = fruitOption.CommandFruit;
            }
            else
            {
                Debug.Log($"Tried to select Fruit with invalid id {fruitId}", gameObject);
            }
        }

        private void SpawnFruitOnClick()
        {
            if (Input.GetMouseButtonUp(0) && EventSystem.current.IsPointerOverGameObject() == false)
            {
                var mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, _cam.transform.position.z * -1);
                var mouseWorldPosition = _cam.ScreenToWorldPoint(mousePos);
                mouseWorldPosition.z = _fixedZLevel;

                var maxAttempts = 5;
                var attempts = 0;

                while(attempts < maxAttempts)
                {
                    var maxColliders = 1;
                    Collider[] sphereCollisions = new Collider[maxColliders];
                    var sphereCollisionCount = Physics.OverlapSphereNonAlloc(mouseWorldPosition, 1f, sphereCollisions);

                    var downwardHit = Physics.Raycast(mouseWorldPosition, Vector3.down, out var downwardHitInfo, 10f);

                    if(sphereCollisionCount > 0 || downwardHit == false)
                    {
                        mouseWorldPosition.y += 2f;
                        attempts++;
                    }
                    else
                    {
                        break;
                    }
                }

                Instantiate(_selectedFruit, mouseWorldPosition, Quaternion.identity);
            }
        }
    }
}
