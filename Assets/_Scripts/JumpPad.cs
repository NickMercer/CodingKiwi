using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TinyZoo
{
    public class JumpPad : MonoBehaviour
    {
        [SerializeField]
        private float _jumpForce;

        private void OnTriggerEnter(Collider other)
        {
            other.attachedRigidbody.AddForce(Vector3.up * _jumpForce);
            other.attachedRigidbody.AddTorque(Vector3.forward * Random.Range(200f, 800f));
            other.attachedRigidbody.AddTorque(Vector3.right * Random.Range(200f, 800f));
        }
    }
}
