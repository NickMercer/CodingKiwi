using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TinyZoo.Characters.Inputs.Commands
{
    [CreateAssetMenu(menuName = "Runtime Sets/Command Pickups List", fileName = "New Command Pickups List")]
    public class CommandPickupsList : ScriptableObject
    {
        [SerializeField]
        private List<CommandPickup> _commandPickups = new List<CommandPickup>();

        public void Add(CommandPickup commandPickup)
        {
            _commandPickups.Add(commandPickup);
        }

        public void Remove(CommandPickup commandPickup)
        {
            _commandPickups.Remove(commandPickup);
        }

        //Wouldn't use LINQ here if this were going to be called regularly in a hot path. But this only gets checked right when a command finishes.
        //If I needed to worry about performance more or it was being checked more frequently, I'd get rid of the linq and do this order check more manually
        //in a way that doesn't allocate garbage.
        public CommandPickup GetNearestOrDefault(Vector3 position)
        {
            var nearestCommand = _commandPickups
                .OrderBy(x => Vector3.Distance(x.transform.position, position))
                .FirstOrDefault();

            return nearestCommand;
        }
    }
}
