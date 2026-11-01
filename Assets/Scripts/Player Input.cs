using System;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private KeyCode _swapBall;
    [SerializeField] private KeyCode _stopBall;
    
    public static event Action onPlayerLaunch;
    public static event Action onPlayerSwapBall;
    public static event Action onPlayerBrakeStop;
    
    public Vector2 MoveInput { get; private set; }

    void Update()
    {
        if(Input.GetKeyDown(_swapBall))
        {
            onPlayerSwapBall?.Invoke();
        }
        
        if(Input.GetAxisRaw("Horizontal") != MoveInput.x)
            GetMoveInput();

        if(Input.GetMouseButtonUp(0))
        {
            onPlayerLaunch?.Invoke();
        }

        if(Input.GetKey(_stopBall))
        {
            onPlayerBrakeStop?.Invoke();
        }
    }

    private void GetMoveInput()
    {
        MoveInput = new Vector2(
            Input.GetAxisRaw("Horizontal"),
            0
        );
    }



}
