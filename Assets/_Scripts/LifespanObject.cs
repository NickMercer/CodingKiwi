using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TinyZoo
{
    public class LifespanObject : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField]
        private float _lifespanInSeconds = 5f;

        private void Awake()
        {
            StartCoroutine(Co_Destroy());
        }

        private IEnumerator Co_Destroy()
        {
            yield return new WaitForSeconds(_lifespanInSeconds);
            Destroy(gameObject);
        }
    }
}
