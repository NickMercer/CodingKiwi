using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TinyZoo
{
    public class DespawnOutsideWorld : MonoBehaviour
    {
        void Update()
        {
            if(transform.position.y < -10)
            {
                Destroy(gameObject);
            }
        }
    }
}
