using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

namespace TinyZoo.Characters.Inputs.Commands
{
    [Serializable]
    public class RunInputCommand : IInputCommand
    {
        [SerializeField, Header("Settings")]
        [Tooltip("The time in seconds that the run command is active before it completes.")]
        private float _runTimeInSeconds = 1f;

        [SerializeField]
        private Vector3 _moveVector;

        public bool IsComplete { get; private set; }

        public Vector3 Position { get; set; }

        public void Begin() => UniTask.Run(() => CompleteRunAsync()).Forget();

        private async UniTaskVoid CompleteRunAsync()
        {
            await UniTask.Delay((int)_runTimeInSeconds * 1000);
            IsComplete = true;
        }

        public bool GetJumpInput() => false;

        public Vector3 GetNormalizedMovementVector(Vector3 previousMovementVector) => _moveVector;

    }
}
