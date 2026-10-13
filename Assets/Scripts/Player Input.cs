using System;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    //public static event Action onPlayerJump;
    public static event Action onPlayerSlam;
    public static event Action onPlayerMove;
    
    public Vector2 MoveInput { get; private set; }

    public void Update()
    {

        // Ground Slam
        if(Input.GetKeyDown(KeyCode.Space))
        {
            onPlayerSlam?.Invoke();
        }

        Move();
        Debug.Log($"Input Vector: {MoveInput}");
        if(MoveInput != Vector2.zero)
        {
            onPlayerMove?.Invoke();
        }
    }

    private void Move()
    {
        MoveInput = new Vector2(
            Input.GetAxisRaw("Horizontal"),
            0
        );
    }



}
