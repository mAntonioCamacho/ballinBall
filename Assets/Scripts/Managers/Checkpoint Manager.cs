using System;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    [SerializeField] private Rigidbody2D Player;
    private Camera _mainCam;
    private Checkpoint _currentCheckpoint;
    public static event Action StartTransition;

    void Awake()
    {
        Checkpoint.NotifyCheckpointManager += UpdateCurrentCheckpoint;
        TransitionManager.BeginTransition += CanResetPosition;
        TransitionManager.ResetPlayer += SetPosition;
    }

    void Start()
    {
        // This is crossed out because player shouldn't reset if they have never been to a checkpoint.
        //CurrentCheckpoint = Player.position;
        _mainCam = Camera.main;
    }

    private void CanResetPosition()
    {
        if(_currentCheckpoint == null) return;
        StartTransition?.Invoke();
    }


    private void UpdateCurrentCheckpoint(Checkpoint checkpoint)
    {
        // basically first checkpoint
        if(_currentCheckpoint == null)
        {
            _currentCheckpoint = checkpoint;
        }
        // update the checkpoint if new checkpoint is better
        if(checkpoint.CheckpointID > _currentCheckpoint.CheckpointID)
        {
            _currentCheckpoint = checkpoint;
        }
    }


    private void SetPosition()
    {
        // stop player
        Player.velocity = Vector2.zero;
        // place them at checkpoint position
        Player.position = _currentCheckpoint.GetPosition();
        // place the camera at the checkpoint position
        _mainCam.transform.position = new Vector3(0, _currentCheckpoint.GetPositionY(), -10);
    }


}
