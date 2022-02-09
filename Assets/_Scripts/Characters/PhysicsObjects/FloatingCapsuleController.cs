using TinyZoo.Characters.Inputs;
using UnityEngine;

namespace TinyZoo.Characters.PhysicsObjects
{
    public class FloatingCapsuleController : MonoBehaviour
    {
        [Header("Dependencies")]
        [SerializeField]
        private Rigidbody _rigidbody;

        [SerializeField]
        private CharacterInputProvider _input;

        [Space, Header("Settings")]
        [Header("Movement")]
        [SerializeField]
        private float _maxSpeed = 4f;

        [SerializeField]
        private float _acceleration = 200f;

        [SerializeField]
        private AnimationCurve _accelerationFactorFromDot;

        [SerializeField]
        private float _maxAccelerationForce = 150f;

        [SerializeField]
        private AnimationCurve _maxAccelerationForceFactorFromDot;

        [SerializeField]
        private Vector3 _forceScale = new Vector3(1, 0, 1);

        [SerializeField]
        private float _rotationDegreesPerFrame = 5f;

        [Header("Floating Spring")]
        [SerializeField]
        private float _desiredFloatingHeight = 0.5f;

        [SerializeField]
        private float _floorRaycastDistance = 0.5f;

        [SerializeField]
        private float _floatingSpringStrength = 2000;

        [SerializeField]
        private float _floatingSpringDampening = 100;

        [SerializeField]
        private int _jumpFramesToIgnoreFloating = 30;

        [Space, Header("Upright Spring")]
        [SerializeField]
        private float _uprightSpringStrength = 2000;

        [SerializeField]
        private float _uprightSpringDampening = 100;

        public bool IsGrounded { get; private set; }
        public Vector3 GroundLocation { get; private set; }
        public Vector3 CurrentGoalVelocity { get; private set; }
        public bool IsJumping { get; private set; }

        private Quaternion _targetRotation;
        private Vector3 _targetVelocity;
        private Vector3 _previousMovementVector;
        private int _jumpFramesRemaining;

        private void Start()
        {
            _previousMovementVector = _rigidbody.transform.forward;
        }

        private void FixedUpdate()
        {
            UpdateJumping();
            UpdateFloatingSpringForce();
            UpdateStabilizationSpringForce();
            UpdateMovement();
        }
        
        private void UpdateMovement()
        {
            var moveVector = IsGrounded 
                ? _input.GetNormalizedMovementVector(_previousMovementVector)
                : _previousMovementVector;

            UpdateVelocity(moveVector);
        }

        private void UpdateVelocity(Vector3 moveVector)
        {
            var unitVelocity = _targetVelocity.normalized;

            var velocityDot = Vector3.Dot(moveVector, unitVelocity);
            var acceleration = _acceleration * _accelerationFactorFromDot.Evaluate(velocityDot);

            var newGoalVelocity = moveVector * _maxSpeed;

            _targetVelocity = Vector3.MoveTowards(
                _targetVelocity,
                newGoalVelocity,
                acceleration * Time.fixedDeltaTime);

            var neededAcceleration = (_targetVelocity - _rigidbody.velocity) / Time.fixedDeltaTime;

            var maxAccel = _maxAccelerationForce * _maxAccelerationForceFactorFromDot.Evaluate(velocityDot);

            neededAcceleration = Vector3.ClampMagnitude(neededAcceleration, maxAccel);

            _rigidbody.AddForce(Vector3.Scale(neededAcceleration * _rigidbody.mass, _forceScale));

            if(_targetVelocity.magnitude > 0)
            {
                _previousMovementVector = _targetVelocity.normalized;
            }

            CurrentGoalVelocity = newGoalVelocity;
        }

        private void UpdateJumping()
        {
            if(IsJumping == false && IsGrounded)
            {
                if (_input.GetJumpInput())
                {
                    _rigidbody.AddForce(Vector3.up * _input.JumpForce);
                    IsJumping = true;
                    IsGrounded = false;
                    _jumpFramesRemaining = _jumpFramesToIgnoreFloating;
                }
            }
        }

        private void UpdateFloatingSpringForce()
        {
            if (Physics.Raycast(transform.position, Vector3.down, out var rayHitInfo, _floorRaycastDistance))
            {
                if (IsJumping == false)
                {
                    var velocity = _rigidbody.velocity;
                    var rayDirection = transform.TransformDirection(Vector3.down);

                    var otherVelocity = Vector3.zero;
                    var hitBody = rayHitInfo.rigidbody;
                    if (hitBody != null)
                    {
                        otherVelocity = hitBody.velocity;
                    }

                    var rayDirectionVelocity = Vector3.Dot(rayDirection, velocity);
                    var otherDirectionVelocity = Vector3.Dot(rayDirection, otherVelocity);

                    var relativeVelocity = rayDirectionVelocity - otherDirectionVelocity;

                    var x = rayHitInfo.distance - _desiredFloatingHeight;

                    var springForce = x * _floatingSpringStrength - relativeVelocity * _floatingSpringDampening;

                    _rigidbody.AddForce(rayDirection * springForce);

                    if (hitBody != null)
                    {
                        hitBody.AddForceAtPosition(rayDirection * -springForce, rayHitInfo.point);
                    }
                }
                else
                {
                    _jumpFramesRemaining--;
                    if(_jumpFramesRemaining <= 0)
                    {
                        IsJumping = false;
                    }
                }

                IsGrounded = true;
                GroundLocation = rayHitInfo.point;
            }
            else
            {
                IsGrounded = false;
                GroundLocation = new Vector3(transform.position.x, 0, transform.position.z);
            }
        }

        private void UpdateStabilizationSpringForce()
        {
            var currentRotation = transform.rotation;
            var goalRotation = _targetRotation.ShortestRotation(currentRotation);

            goalRotation.ToAngleAxis(out var rotationDegrees, out var rotationAxis);
            rotationAxis.Normalize();

            var rotationRadians = rotationDegrees * Mathf.Deg2Rad;

            _rigidbody.AddTorque(rotationAxis * (rotationRadians * _uprightSpringStrength) - _rigidbody.angularVelocity * _uprightSpringDampening);

            var targetRotation = Quaternion.LookRotation(_previousMovementVector, Vector3.up); 

            _targetRotation = Quaternion.Slerp(transform.rotation, targetRotation, _rotationDegreesPerFrame * Time.fixedDeltaTime);
            transform.rotation = _targetRotation;
        }
    }
}