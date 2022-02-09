using Cysharp.Threading.Tasks;
using UnityEngine;

namespace TinyZoo.Characters.Inputs.Commands
{
    [CreateAssetMenu(menuName = "Input Commands/Run")]
    public class RunInputCommand : InputCommand
    {
        [SerializeField]
        [Tooltip("The time in seconds that the run command is active before it completes.")]
        private float _runTimeInSeconds = 1f;

        [SerializeField]
        private Vector3 _moveVector;

        public override void Begin() => UniTask.Run(() => CompleteRunAsync()).Forget();

        private async UniTaskVoid CompleteRunAsync()
        {
            await UniTask.Delay((int)_runTimeInSeconds * 1000);
            IsComplete = true;
        }

        public override bool GetJumpInput() => false;

        public override Vector3 GetNormalizedMovementVector(Vector3 previousMovementVector) => _moveVector;

        public override InputCommand Copy()
        {
            var runCommand = CreateInstance<RunInputCommand>();
            runCommand._runTimeInSeconds = _runTimeInSeconds;
            runCommand._moveVector = _moveVector;

            return runCommand;
        }
    }
}
