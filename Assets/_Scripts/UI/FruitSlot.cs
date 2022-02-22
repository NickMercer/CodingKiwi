using System.Collections;
using System.Collections.Generic;
using TinyZoo.FruitSpawning;
using UnityEngine;
using UnityEngine.UI;

namespace TinyZoo
{
    public class FruitSlot : MonoBehaviour
    {
        [Header("Dependencies")]
        [SerializeField]
        private Image _selectionOutline;

        [SerializeField]
        private FruitSpawner _fruitSpawner;

        private void Awake()
        {
            
        }
    }
}
