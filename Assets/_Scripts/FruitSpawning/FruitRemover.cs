using System.Collections;
using System.Collections.Generic;
using TinyZoo.Characters.Inputs.Commands;
using UnityEngine;

namespace TinyZoo
{
    public class FruitRemover : MonoBehaviour
    {
        private Camera _cam;

        private void Awake()
        {
            _cam = Camera.main;
        }

        private void Update()
        {
            if (Input.GetMouseButtonUp(1))
            {
                var mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, _cam.transform.position.z * -1);
                var ray = _cam.ScreenPointToRay(mousePos);

                if(Physics.Raycast(ray, out var hitInfo))
                {
                    var command = hitInfo.transform.gameObject.GetComponent<CommandPickup>();
                    if(command != null)
                    {
                        Destroy(hitInfo.transform.gameObject);
                    }
                }
            }
        }
    }
}
