using UnityEngine;
using TinyZoo.Characters.PhysicsObjects;

namespace TinyZoo.Characters.Animals
{
    public class AnimalVisuals : MonoBehaviour
    {
        [Header("Dependencies")]
        [SerializeField]
        private FloatingCapsuleController _characterController;

        [SerializeField]
        private Animator _animator;

        [SerializeField]
        private ParticleSystem _dust;

        [Space, Header("Settings")]
        [SerializeField]
        private float _maxCatchupDistancePerFrame = 5f;

        private void Update()
        {
            if (_characterController.IsGrounded && _characterController.IsJumping == false)
            {
                UpdateVisualsOnGround();
            }
            else
            {
                UpdateVisualsInTheAir();
            }

            UpdateModelRotation();
        }

        private void UpdateModelRotation()
        {
            transform.rotation = _characterController.transform.rotation;
        }

        private void UpdateVisualsOnGround()
        {
            transform.position = Vector3.Slerp(
                transform.position,
                _characterController.GroundLocation,
                _maxCatchupDistancePerFrame * Time.deltaTime);

            var walking = _characterController.CurrentGoalVelocity != Vector3.zero;
            if (walking)
            {
                if (_dust.isPlaying == false)
                    _dust.Play();
            }
            else
            {
                _dust.Stop();
            }

            _animator.SetBool("isWalking", walking);
        }

        private void UpdateVisualsInTheAir()
        {
            transform.position = Vector3.Slerp(
                transform.position,
                _characterController.transform.position,
                _maxCatchupDistancePerFrame * Time.deltaTime);

            _dust.Stop();

            _animator.SetBool("isWalking", false);
        }

    }
}