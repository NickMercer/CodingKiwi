using System;
using UnityEngine;

namespace TinyZoo.Characters.Inputs
{
    public abstract class CharacterInputProvider : MonoBehaviour
    {
        public abstract float JumpForce { get; protected set; }

        public abstract Vector3 GetNormalizedMovementVector(Vector3 previousMovementVector);

        public abstract bool GetJumpInput();
    }
}
