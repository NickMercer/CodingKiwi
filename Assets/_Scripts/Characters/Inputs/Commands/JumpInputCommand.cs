using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

namespace TinyZoo.Characters.Inputs.Commands
{
    [Serializable]
    public class JumpInputCommand : IInputCommand
    {
        [SerializeField, Header("Settings")]
        [Tooltip("The time in seconds that the jump command is active before it completes.")]
        private float _jumpBufferTimeInSeconds = 0.1f;

        public bool IsComplete { get; private set; }

        public Vector3 Position { get; set; }

        private bool _jumpBufferStarted = false;

        public void Begin() { }

        public bool GetJumpInput()
        {
            if (_jumpBufferStarted == false)
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

        public Vector3 GetNormalizedMovementVector(Vector3 previousMovementVector) => previousMovementVector;
    }
}
