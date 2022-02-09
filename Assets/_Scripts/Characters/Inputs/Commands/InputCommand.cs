using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TinyZoo.Characters.Inputs.Commands
{
    //This class originally was an interface IInputCommand. However, this meant that I couldn't surface debug information into the inspector at runtime
    //because interfaces can't be serialized. So I couldn't see the list of spawned command pickups, see the character's current command information, and generally
    //just had to fight Unity at every turn. I'm currently looking for a good way to show debug information for interface reference implementations in the inspector. 
    //Even Odin Inspector has a tough time with it.
    //For now, I just have to live with the subclassed, ScriptableObject route. I don't want to use abstract classes like this, but at least this way designers don't have to deal with 
    //a lack of debug tooling or information available to them.
    public abstract class InputCommand : ScriptableObject
    {
        public bool IsComplete { get; protected set; }

        public Vector3 Position { get; set; }

        public abstract void Begin();

        public abstract Vector3 GetNormalizedMovementVector(Vector3 previousMovementVector);

        public abstract bool GetJumpInput();

        //Need to let each command call CreateInstance on it's own with it's type information to preserve the subclass.
        public abstract InputCommand Copy();
    }
}
