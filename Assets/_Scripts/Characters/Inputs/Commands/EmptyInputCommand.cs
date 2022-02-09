using UnityEngine;

namespace TinyZoo.Characters.Inputs.Commands
{
    [CreateAssetMenu(menuName = "Input Commands/Empty")]
    public class EmptyInputCommand : InputCommand
    {
        public override void Begin() => IsComplete = true;

        public override bool GetJumpInput() => false;

        public override Vector3 GetNormalizedMovementVector(Vector3 previousMovementVector) => Vector3.zero;

        public override InputCommand Copy() => CreateInstance<EmptyInputCommand>();
    }
}
