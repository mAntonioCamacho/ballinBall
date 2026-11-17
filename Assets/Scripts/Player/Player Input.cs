using System;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    #region Player Input Keys
        [SerializeField] private KeyCode _swapBall;
        [SerializeField] private KeyCode _stopBall;
    #endregion
    /// 
    /// 
    #region Player Movement Events
        public static event Action onPlayerLaunch;
        public static event Action onPlayerSwapBall;
        public static event Action onPlayerBrakeStop;
    #endregion
    /// 
    /// 
    #region Player Mouse Events & Variable(s)
        public static event Action onMouseDown;
        public static event Action onMouseDrag;
        public static event Action onMouseUp;
        private bool _wasPlayerClicked;
    #endregion
    ///
    /// 
    private static Vector2 _moveInput = Vector2.zero;
    public static float PlayerInputDirection { get {return _moveInput.x;} }


    void Update()
    {
        CheckBallSwapInput();
        CheckMovementInput();
        CheckBrakeInput();
        CheckMouseInput();
    }


    private void GetMoveInput()
    {
        _moveInput = new Vector2(
            Input.GetAxisRaw("Horizontal"),
            0
        );
    }


    private void CheckBrakeInput()
    {
        if(Input.GetKey(_stopBall))
        {
            onPlayerBrakeStop?.Invoke();
        }
    }


    private void CheckMouseInput()
    {
        CheckMouseInputDown();
        if(!_wasPlayerClicked) return;

        if(Input.GetMouseButton(0))
        {
            onMouseDrag?.Invoke();
        }
        CheckMouseInputUp();
    }


    private void CheckMouseInputUp()
    {
        if(Input.GetMouseButtonUp(0))
        {
            _wasPlayerClicked = false;
            onMouseUp?.Invoke();
            onPlayerLaunch?.Invoke();
        }
    }


    private void CheckMouseInputDown()
    {
        if(Input.GetMouseButtonDown(0))
        {
            _wasPlayerClicked = MouseClickHitPlayer();
            if(!_wasPlayerClicked) return;
            onMouseDown?.Invoke();
        }
    }


    private bool MouseClickHitPlayer()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
        if(hit.collider == null) return false;
        return  hit.collider.CompareTag("Player");
    }


    private void CheckMovementInput()
    {
        if(Input.GetAxisRaw("Horizontal") != _moveInput.x)
        {
            GetMoveInput();
        }
    }


    private void CheckBallSwapInput()
    {
        if(Input.GetKeyDown(_swapBall))
        {
            onPlayerSwapBall?.Invoke();
        }
    }

}
