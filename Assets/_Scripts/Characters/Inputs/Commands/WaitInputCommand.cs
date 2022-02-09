using Cysharp.Threading.Tasks;
using UnityEngine;

namespace TinyZoo.Characters.Inputs.Commands
{
    [CreateAssetMenu(menuName = "Input Commands/Wait")]
    public class WaitInputCommand : InputCommand
    {
        [SerializeField]
        [Tooltip("The time in seconds that the wait command is active before it completes.")]
        private float _waitTimeInSeconds = 1f;

        public override void Begin() => UniTask.Run(() => CompleteWaitAsync()).Forget();

        private async UniTaskVoid CompleteWaitAsync()
        {
            await UniTask.Delay((int)_waitTimeInSeconds * 1000);
            IsComplete = true;
        }

        public override bool GetJumpInput() => false;

        public override Vector3 GetNormalizedMovementVector(Vector3 previousMovementVector) => Vector3.zero;

        public override InputCommand Copy()
        {
            var waitCommand = CreateInstance<WaitInputCommand>();
            waitCommand._waitTimeInSeconds = _waitTimeInSeconds;

            return waitCommand;
        }
    }
}
