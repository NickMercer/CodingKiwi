using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TinyZoo
{
    public class Cannon : MonoBehaviour
    {
        [Header("Dependencies")]
        [SerializeField]
        private Rigidbody _ammo;

        [SerializeField]
        private Transform _firePoint;

        [Space, Header("Settings")]
        [SerializeField]
        private float _launchingPower = 100f;

        [SerializeField]
        private float _secondsBetweenShots = 3f;

        private void Start()
        {
            StartCoroutine(Co_FireProjectile());
        }

        private IEnumerator Co_FireProjectile()
        {
            var projectile = Instantiate(_ammo, _firePoint.position, Quaternion.identity);
            projectile.AddForce(transform.up * _launchingPower, ForceMode.Impulse);
            yield return new WaitForSeconds(_secondsBetweenShots);
            StartCoroutine(Co_FireProjectile());
        }
    }
}
