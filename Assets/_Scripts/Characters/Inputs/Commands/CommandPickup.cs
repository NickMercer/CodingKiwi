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
        [SerializeReference, SubclassSelector]
        private IInputCommand _command = new EmptyInputCommand();
        public IInputCommand Command => _command;

        private void Awake()
        {
            _pickupsList.Add(this);
        }

        private void OnDestroy()
        {
            _pickupsList.Remove(this);
        }
    }
}
