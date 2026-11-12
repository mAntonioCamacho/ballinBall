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
    
        public static event Action onBowlBallHitGround;
        public static event Action<bool> onBallReadyForLaunch;

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
        private float _downwardYvelocity;

        /// <summary>
        /// This value is for BOTH balls y velocity - since gravity changes between the two, y velocity is changed too.
        /// </summary>
        private float _trueGroundedValue = .00016f;

    #endregion
    /// 
    /// 
    /// 
    #region All Functions 

        #region Unity Functions
            void Awake()
            {
                BallFormManager.changeValues += ChangeValues;
                PlayerInput.onPlayerLaunch += Launch;
                PlayerInput.onPlayerBrakeStop += BrakeStop;
            }


            void Update()
            {
                PlayerInput.CanLaunch = (Mathf.Abs(_rBody.velocity.y) < _trueGroundedValue && Mathf.Abs(_rBody.velocity.x) < _trueGroundedValue && GroundedCheck()) ? true : false;
                onBallReadyForLaunch?.Invoke(PlayerInput.CanLaunch);
                BowlinBallHitGround();
            }


            void FixedUpdate()
            {
                Move();
                FallVelocityCap();
            }

        #endregion
    /// 
    /// 
    /// 
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
                if(PlayerInput.CanLaunch)
                {
                    _rBody.AddForce(_dragInput.FinalDragVector * _launchPower, ForceMode2D.Impulse);
                }
                _dragInput.FinalDragVector = Vector2.zero;
            }


            private void BrakeStop()
            {
                if(GroundedCheck())
                {
                    _rBody.velocity = (_rBody.velocity != Vector2.zero) ? Vector2.Lerp(Vector2.zero, _rBody.velocity, _brakePower) : _rBody.velocity;
                    Debug.Log(_rBody.velocity);
                    // these are random values right now - this has to do with the rotation of the ball - with it being unlocked
                    // the x velocity is affected by the rotation so trying to tune it so that the launch can started sooner 
                    // but not to soon - also trying to get rotation to slow down too with angular velocity
                    // when ball slows down - roll should slow down too.
                    if(Mathf.Abs(_rBody.velocity.x) < .045f)
                    {
                        _rBody.velocity = _rBody.velocity/1.1f;
                        _rBody.angularVelocity = -_rBody.velocity.x/2;
                    }
                             
                }
                    
            }

        #endregion
    ///
    /// 
    /// 
        #region Helper Logic Functions 
            /// <summary>
            /// Simple ground check - mostly for not allowing the player to launch in air or to allow braking while rolling around.
            /// </summary>
            /// <returns>Returns true when we are grounded, otherwise returns false</returns>
            private bool GroundedCheck()
            {
                // shoot ray downwards and check if we hit something on the ground layer
                RaycastHit2D hit = Physics2D.Raycast(_rBody.position, Vector2.down, _groundCheckRayLength , _groundLayer);
                // if we did not hit objects of type ground, return true otherwise false
                return hit.collider != null;
            }


            private void BowlinBallHitGround()
            {
                if(_rBody.velocity.y < 0)
                {
                    _downwardYvelocity = _rBody.velocity.y;
                }
                if(GroundedCheck() && _downwardYvelocity < -20f && BallFormManager.currentBall == BallFormManager.BallType.BowlBall)
                {
                    onBowlBallHitGround?.Invoke();
                    _downwardYvelocity = 0;
                }
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