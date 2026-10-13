using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private PlayerInput _input;
    [SerializeField] private Rigidbody2D _rBody;
    [SerializeField] private float _slamPower;
    [SerializeField] private float _speed;
    [SerializeField] private float _rayLength = .8f;
    [SerializeField] private LayerMask _groundLayer;
    void Awake()
    {
        PlayerInput.onPlayerSlam += GroundSlam;
        PlayerInput.onPlayerMove += Move;
    }


    void Update()
    {
        
    }


    private void Move()
    {
        Vector2 direction = new Vector2(_input.MoveInput.x * _speed, _rBody.velocity.y);
        _rBody.velocity = direction;
    }


    private void GroundSlam()
    {   
        if(!GroundedCheck())
            _rBody.AddForce(Vector2.down * _slamPower, ForceMode2D.Impulse);
    }

    private bool GroundedCheck()
    {
        RaycastHit2D hit = Physics2D.Raycast(_rBody.position, _rBody.position + Vector2.down, _rayLength , _groundLayer);
        return hit.collider != null;
    }
}
