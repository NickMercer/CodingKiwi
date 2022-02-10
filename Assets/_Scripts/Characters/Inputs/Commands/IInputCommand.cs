using System;
using System.Collections.Generic;
using UnityEngine;

namespace TinyZoo.Characters.Inputs.Commands
{
    public interface IInputCommand
    {
        public bool IsComplete { get; }

        public Vector3 Position { get; set; }

        public abstract void Begin();

        public abstract Vector3 GetNormalizedMovementVector(Vector3 previousMovementVector);

        public abstract bool GetJumpInput();
    }
}
