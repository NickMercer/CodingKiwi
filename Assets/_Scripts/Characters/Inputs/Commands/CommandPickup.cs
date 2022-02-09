using System;
using UnityEngine;

namespace TinyZoo.Characters.Inputs.Commands
{
    public class CommandPickup : MonoBehaviour
    {
        [Header("Dependencies")]
        [SerializeField]
        private CommandPickupsList _pickupsList;

        [Space, Header("Settings")]
        [SerializeField]
        private InputCommand _command;
        public InputCommand Command => _command;

        private void Awake()
        {
            _command = _command.Copy();
            _pickupsList.Add(this);
        }

        private void OnDestroy()
        {
            _pickupsList.Remove(this);
        }
    }
}
