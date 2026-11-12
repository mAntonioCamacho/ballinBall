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
    /// 
    #region Player Movement Events
        public static event Action onPlayerLaunch;
        public static event Action onPlayerSwapBall;
        public static event Action onPlayerBrakeStop;
    #endregion
    /// 
    /// 
    /// 
    #region Player Mouse Events
        public static event Action onMouseDown;
        public static event Action onMouseDrag;
        public static event Action onMouseUp;
    #endregion
    /// 
    ///
    /// 
    #region Pause Event Info
        public static event Action<bool> onPlayerPause;
        private bool _isPaused = false;
    #endregion
    ///
    /// 
    ///
    public static event Action onPlayerWantReset;
    public static bool IsTransitioning { get; set ; } = false;
    private bool HasCheckpoint => CheckpointManager.CurrentCheckpoint != null;
    public Vector2 MoveInput { get; private set; }
    public static bool CanLaunch { get; set; }
    private bool _didPlayerClick = false;
    private bool CanGetInput => !_isPaused && !IsTransitioning;

    void Update()
    {
        CheckBallSwapInput();
        CheckMovementInput();
        CheckBrakeInput();
        CheckIfPausedGame();
        GoToLastCheckpointCheck();
    }


    void OnMouseDown()
    {
        CheckMouseInputDown();
    }


    void OnMouseDrag()
    {
        CheckMouseInputDrag();
    }


    void OnMouseUp()
    {
        CheckMouseInputUp();   
    }


    private void GetMoveInput()
    {
        MoveInput = new Vector2(
            Input.GetAxisRaw("Horizontal"),
            0
        );
    }


    private void CheckIfPausedGame()
    {
        if(IsTransitioning) return;
        if(Input.GetKeyUp(KeyCode.Escape))
        {
            onPlayerPause?.Invoke(_isPaused);   
        }
        _isPaused = (Time.timeScale == 0) ? true : false;
    }


    private void CheckBrakeInput()
    {
        if(Input.GetKey(_stopBall))
        {
            onPlayerBrakeStop?.Invoke();
        }
    }


    private void CheckMouseInputDown()
    {
        if(Input.GetMouseButtonDown(0) && CanGetInput && CanLaunch)
        {
            onMouseDown?.Invoke();
            _didPlayerClick = true;
        }
    }


    private void CheckMouseInputDrag()
    {
        if(Input.GetMouseButton(0) && !_isPaused && _didPlayerClick)
        {
            onMouseDrag?.Invoke();
        }
    }


    private void CheckMouseInputUp()
    {
        if(Input.GetMouseButtonUp(0) && !_isPaused && _didPlayerClick)
        {
            onMouseUp?.Invoke();
            onPlayerLaunch?.Invoke();
            _didPlayerClick = false;
        }
    }


    private void CheckMovementInput()
    {
        if(Input.GetAxisRaw("Horizontal") != MoveInput.x && CanGetInput)
        {
            GetMoveInput();
        }
    }


    private void CheckBallSwapInput()
    {
        if(Input.GetKeyDown(_swapBall) && CanGetInput)
        {
            onPlayerSwapBall?.Invoke();
        }
    }


    private void GoToLastCheckpointCheck()
    {
        if(Input.GetKeyUp(KeyCode.R) && CanGetInput && HasCheckpoint)
        {
            IsTransitioning = true;
            onPlayerWantReset?.Invoke();
        }
    }


}
