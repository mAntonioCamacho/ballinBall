using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("References")]
        [SerializeField] private PlayerInput _input;
        [SerializeField] private PlayerDragInput _dragInput;
        [SerializeField] private Rigidbody2D _rBody;
        [SerializeField] private LayerMask _groundLayer;
    [Header("Ray for Ground Check")]
        [SerializeField] private float _groundCheckRayLength;
        
    #region Player Movement Variables
    /// <summary>
    /// These values are changed from the scriptable objects in Unity. Each Ball type has its own unique values - some values
    /// will be the same.
    /// </summary>
        private float _launchPower;
        private float _brakePower;
        private float _speed;
        private float _inAirSpeed;
        private float _fallSpeedCap;

    /// <summary>
    /// This vector belongs to SmoothDamp function - don't use it
    /// </summary>
        private Vector2 _smoothDampVec;
    #endregion

    #region All Functions
        #region Unity Functions
        void Awake()
        {
            BallFormManager.changeValues += ChangeValues;
            PlayerInput.onPlayerLaunch += Launch;
            PlayerInput.onPlayerBrakeStop += BrakeStop;
        }


        void FixedUpdate()
        {
            Move();
            FallVelocityCap();
        }
        #endregion

        #region Player Movement Private Functions     
        private void Move()
        {
            // if grounded or no input - ignore the following code and return
            if(GroundedCheck() || _input.MoveInput.x == 0) return;

            float xVelocity = _input.MoveInput.x * _speed;
            Vector2 direction = new Vector2(xVelocity , _rBody.velocity.y);
            _rBody.velocity = Vector2.SmoothDamp(_rBody.velocity, direction, ref _smoothDampVec, _inAirSpeed);
        }

        
        private void Launch()
        {
            if(GroundedCheck())
            {
                _rBody.AddForce(_dragInput.FinalDragVector * _launchPower, ForceMode2D.Impulse);
            }
            _dragInput.FinalDragVector = Vector2.zero;
        }


        private void BrakeStop()
        {
            if(GroundedCheck())
                _rBody.velocity = (_rBody.velocity != Vector2.zero) ? Vector2.Lerp(Vector2.zero, _rBody.velocity, _brakePower) : _rBody.velocity;
        }
        #endregion

        #region Helper Logic Functions 
        /// <summary>
        /// Simple ground check - mostly for not allowing the player to launch in air or to allow braking while rolling around.
        /// </summary>
        /// <returns>Returns true when we are grounded, otherwise returns false</returns>
        private bool GroundedCheck()
        {
            // shoot ray downwards and check if we hit something on the ground layer
            RaycastHit2D hit = Physics2D.Raycast(_rBody.position, Vector2.down, _groundCheckRayLength , _groundLayer);
            // if we did not hit anything, return true otherwise false
            return hit.collider != null;
        }


        /// <summary>
        /// Caps the downward y velocity to the value of _fallSpeedCap to not allow the player to bounce higher and higher 
        /// upon switching balls. Momentum is carried from ball to ball without this code snippet.
        /// </summary>
        private void FallVelocityCap()
        {
            _rBody.velocity = (_rBody.velocity.y < -(_fallSpeedCap)) ? new Vector2(_rBody.velocity.x, -(_fallSpeedCap)) : _rBody.velocity;
        }


        private void ChangeValues(float launch, float speed, float brake, float airSpeed, float speedCap)
        {
            _launchPower = launch;
            _speed = speed;
            _brakePower = brake;
            _inAirSpeed = airSpeed;
            _fallSpeedCap = speedCap;
        }
        #endregion

    #endregion

}