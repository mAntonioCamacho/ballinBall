using System;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private KeyCode _swapBall;
    public static event Action onPlayerMove;
    public static event Action onPlayerLaunch;
    public static event Action onPlayerSwapBall;
    
    public Vector2 MoveInput { get; private set; }

    void Update()
    {
        if(Input.GetKeyDown(_swapBall))
        {
            onPlayerSwapBall?.Invoke();
        }
        GetMoveInput();
        if(MoveInput != Vector2.zero)
        {
            onPlayerMove?.Invoke();
        }

        if(Input.GetMouseButtonUp(0))
        {
            onPlayerLaunch?.Invoke();
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
