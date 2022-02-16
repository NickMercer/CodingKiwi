using System;
using TinyZoo.Characters.Inputs.Commands;
using UnityEngine;

namespace TinyZoo.FruitSpawning
{
    [Serializable]
    public class KeyFruitBinding
    {
        [SerializeField]
        private KeyCode _keyCode;
        public KeyCode Key => _keyCode;

        [SerializeField]
        private CommandPickup _commandFruit;
        public CommandPickup CommandFruit => _commandFruit;
    }
}
