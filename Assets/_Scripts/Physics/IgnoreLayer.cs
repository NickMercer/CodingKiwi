using UnityEngine;

namespace TinyZoo
{
    public class IgnoreLayer : MonoBehaviour
    {
        [SerializeField]
        [Header("Settings")]
        private LayerMask _layer1;

        [SerializeField]
        [Header("Settings")]
        private LayerMask _layer2;

        private void Start()
        {
            Physics.IgnoreLayerCollision(_layer1, _layer2);
        }
    }
}
