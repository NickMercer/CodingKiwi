using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

namespace TinyZoo.Characters.Inputs.Commands
{
    [Serializable]
    public class WaitInputCommand : IInputCommand
    {
        [SerializeField, Header("Settings")]
        [Tooltip("The time in seconds that the wait command is active before it completes.")]
        private float _waitTimeInSeconds = 1f;

        public bool IsComplete { get; private set; }
        public Vector3 Position { get; set; }

        public void Begin() => UniTask.Run(() => CompleteWaitAsync()).Forget();

        private async UniTaskVoid CompleteWaitAsync()
        {
            await UniTask.Delay((int)_waitTimeInSeconds * 1000);
            IsComplete = true;
        }

        public bool GetJumpInput() => false;

        public Vector3 GetNormalizedMovementVector(Vector3 previousMovementVector) => Vector3.zero;
    }
}
