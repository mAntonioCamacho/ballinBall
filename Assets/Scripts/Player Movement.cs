using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private PlayerInput _input;
    [SerializeField] private PlayerDragInput _dragInput;
    [SerializeField] private Rigidbody2D _rBody;
    [SerializeField] private float _launchPower;
    [SerializeField] private float _speed;
    [SerializeField] private float _rayLength;
    [SerializeField] private LayerMask _groundLayer;
    void Awake()
    {
        PlayerInput.onPlayerMove += Move;
        PlayerInput.onPlayerLaunch += Launch;
    }


//
//      Movement Logic
//      
    private void Move()
    {
        // this variable is just testing some new movement speed
        float xVelocity = (!GroundedCheck()) ? _rBody.velocity.x : ((_rBody.velocity.x < _speed && Math.Abs(_rBody.velocity.x) < 0) ? _input.MoveInput.x * _rBody.velocity.x : _input.MoveInput.x * _speed);
        // xVelocity parameter is just _input.MoveInput.x * _speed
        Vector2 direction = new Vector2(xVelocity , _rBody.velocity.y);
        _rBody.velocity = direction;
    }

    
    private void Launch()
    {
        if(GroundedCheck())
        {
            _rBody.AddForce(_dragInput.FinalDragVector * _launchPower, ForceMode2D.Impulse);
        }
        _dragInput.FinalDragVector = Vector2.zero;
    }


//
//      Helper Logic
//      
    private bool GroundedCheck()
    {
        // shoot ray downwards and check if we hit something on the ground layer
        RaycastHit2D hit = Physics2D.Raycast(_rBody.position, Vector2.down, _rayLength , _groundLayer);
        // if we did not hit anything, return true otherwise false
        return hit.collider != null;
    }

    public void SetSpeed(float newSpeed)
    {
        _speed = newSpeed;    
    }

    public void SetLaunchPower(float newLaunchPower)
    {
        _launchPower = newLaunchPower;
    }
}
