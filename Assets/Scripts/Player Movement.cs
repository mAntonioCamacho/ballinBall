using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private PlayerInput _input;
    [SerializeField] private PlayerDragInput _dragInput;
    [SerializeField] private Rigidbody2D _rBody;
    [SerializeField] private float _slamPower;
    [SerializeField] private float _launchPower;
    [SerializeField] private float _speed;
    [SerializeField] private float _rayLength;
    [SerializeField] private LayerMask _groundLayer;
    void Awake()
    {
        PlayerInput.onPlayerSlam += GroundSlam;
        PlayerInput.onPlayerMove += Move;
        PlayerInput.onPlayerLaunch += Launch;
    }


    private void Move()
    {
        Vector2 direction = new Vector2(_input.MoveInput.x * _speed, _rBody.velocity.y);
        _rBody.velocity = direction;
    }


    private void GroundSlam()
    {   
        // if WE DID in fact hit SOMETHING from our raycast, don't ground slam, just ignore this function call and do nothing 
        if(!GroundedCheck())
            _rBody.AddForce(Vector2.down * _slamPower, ForceMode2D.Impulse);
    }

    private bool GroundedCheck()
    {
        // shoot ray downwards and check if we hit something on the ground layer
        RaycastHit2D hit = Physics2D.Raycast(_rBody.position, Vector2.down, _rayLength , _groundLayer);
        // if we did not hit anything, return true otherwise false
        return hit.collider != null;
    }

    private void Launch()
    {
        if(GroundedCheck())
        {
            _rBody.AddForce(_dragInput.FinalDragVector * _launchPower, ForceMode2D.Impulse);
        }
    }
}
