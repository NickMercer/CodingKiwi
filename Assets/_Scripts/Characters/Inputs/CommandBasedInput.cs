using System.Collections;
using System.Collections.Generic;
using TinyZoo.Characters.Inputs.Commands;
using UnityEngine;

namespace TinyZoo.Characters.Inputs
{
    public class CommandBasedInput : CharacterInputProvider
    {
        [Header("Dependencies")]
        [SerializeField]
        private CommandPickupsList _spawnedCommandPickups;

        [Space, Header("Settings")]
        [SerializeField]
        private float _jumpForce = 3000;
        public override float JumpForce 
        { 
            get { return _jumpForce; } 
            protected set { _jumpForce = value; } 
        }

        [Space, Header("Debug Values")]
        [SerializeField]
        private Vector3 _nearestCommandLocation = Vector3.zero;

        [SerializeField]
        private InputCommand _currentCommand;
        
        [SerializeField]
        private CommandPickup _nearestCommandPickup;

        private void Start()
        {
            SetNearestPickup();
        }

        private void OnCollisionEnter(Collision collision)
        {
            if(collision.transform.TryGetComponent(out CommandPickup pickup))
            {
                _nearestCommandPickup = null;
                _currentCommand = pickup.Command;
                _currentCommand.Begin();
                Destroy(pickup.gameObject);
            }
        }

        private void Update()
        {
            if (_currentCommand != null && _currentCommand.IsComplete)
            {
                _currentCommand = null;
                SetNearestPickup();
            }
        }

        private void SetNearestPickup()
        {
            _nearestCommandPickup = _spawnedCommandPickups.GetNearestOrDefault(transform.position);
            if(_nearestCommandPickup != null)
            {
                _nearestCommandLocation = _nearestCommandPickup.transform.position;
            }
        }

        public override bool GetJumpInput()
        {
            var result =  _currentCommand != null 
                ? _currentCommand.GetJumpInput() 
                : false;

            return result;
        }

        public override Vector3 GetNormalizedMovementVector(Vector3 previousMovementVector)
        {
            var result = Vector3.zero;

            if (_currentCommand != null)
            {
                result = _currentCommand.GetNormalizedMovementVector(previousMovementVector);
            }
            else if (_nearestCommandPickup != null)
            {
                var directionToCommand = transform.position.DirectionTo(_nearestCommandPickup.transform.position);
                result = new Vector3(directionToCommand.x, 0f, directionToCommand.z).normalized;
            }

            return result;
        }
    }
}
