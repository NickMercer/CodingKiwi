using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using TinyZoo.Characters.Inputs.Commands;
using UnityEngine;

namespace TinyZoo
{
    [CreateAssetMenu(menuName = "Input Commands/Jump")]
    public class JumpInputCommand : InputCommand
    {
        [SerializeField]
        [Tooltip("The time in seconds that the jump command is active before it completes.")]
        private float _jumpBufferTimeInSeconds = 0.1f;

        private bool _jumpBufferStarted = false;

        public override void Begin() { }

        public override bool GetJumpInput()
        {
            if(_jumpBufferStarted == false)
            {
                UniTask.Run(() => CompleteJumpBufferAsync()).Forget();
                _jumpBufferStarted = true;
            }

            return true;
        }

        private async UniTaskVoid CompleteJumpBufferAsync()
        {
            await UniTask.Delay((int)_jumpBufferTimeInSeconds * 1000);
            IsComplete = true;
        }

        public override Vector3 GetNormalizedMovementVector(Vector3 previousMovementVector) => previousMovementVector;

        public override InputCommand Copy()
        {
            var jumpCommand = CreateInstance<JumpInputCommand>();
            jumpCommand._jumpBufferTimeInSeconds = _jumpBufferTimeInSeconds;

            return jumpCommand;
        }
    }
}
