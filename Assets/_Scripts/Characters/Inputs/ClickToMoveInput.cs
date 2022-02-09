using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TinyZoo.Characters.Inputs
{
    public class ClickToMoveInput : CharacterInputProvider
    {
        private Camera _camera;

        private Vector3? _goalPosition = null;

        public override float JumpForce { get; protected set; }

        private void Awake()
        {
            _camera = Camera.main;
        }

        public void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                var ray = _camera.ScreenPointToRay(Input.mousePosition);
                if(Physics.Raycast(ray, out var rayHitInfo))
                {
                    _goalPosition = rayHitInfo.point;
                }
            }
        }

        public override Vector3 GetNormalizedMovementVector(Vector3 previousMovementVector)
        {
            if(_goalPosition.HasValue == false)
            {
                return Vector3.zero.normalized;
            }

            var currentPosition = transform.position;
            
            var goalPosition = new Vector3(
                _goalPosition.Value.x, 
                transform.position.y, 
                _goalPosition.Value.z);

            if(Vector3.Distance(currentPosition, goalPosition) < 0.5f)
            {
                _goalPosition = null;
            }

            return currentPosition.DirectionTo(goalPosition);
        }

        public override bool GetJumpInput()
        {
            return false;
        }
    }
}
