using System;
using UnityEngine;

namespace TinyZoo.Characters.Inputs
{
    public class AnalogInput : CharacterInputProvider
    {

        [field: SerializeField]
        public override float JumpForce { get; protected set; }

        private bool _bufferedJump;

        private float _jumpBufferTimespan = 0.25f;
        private float _jumpLastBuffered;

        private void Update()
        {
            if(_bufferedJump == false)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    _bufferedJump = true;
                    _jumpLastBuffered = Time.time;
                }
            }
            else
            {
                if(_jumpLastBuffered + _jumpBufferTimespan > Time.time)
                {
                    _bufferedJump = false;
                    _jumpLastBuffered = 0f;
                }
            }

        }

        public override bool GetJumpInput()
        {
            var jumping = _bufferedJump;
            if (jumping)
            {
                _bufferedJump = false;
            }
            return jumping;
        }

        public override Vector3 GetNormalizedMovementVector(Vector3 previousMovementVector)
        {
            var xInput = Input.GetAxis("Horizontal");
            var zInput = Input.GetAxis("Vertical");
            var moveVector = new Vector3(xInput, 0f, zInput);

            if (moveVector.magnitude > 1.0f)
            {
                moveVector.Normalize();
            }

            return moveVector;
        }
    }
}
