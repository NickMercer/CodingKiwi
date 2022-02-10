using System;
using UnityEngine;

namespace TinyZoo.Characters.Inputs.Commands
{
    [Serializable]
    public class EmptyInputCommand : IInputCommand
    {
        public bool IsComplete { get; private set; }

        public Vector3 Position { get; set; }

        public void Begin() => IsComplete = true;

        public bool GetJumpInput() => false;

        public Vector3 GetNormalizedMovementVector(Vector3 previousMovementVector) => Vector3.zero;
    }
}
