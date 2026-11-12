using System;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    [SerializeField] private Rigidbody2D Player;
    private Camera _mainCam;
    public static Vector2? CurrentCheckpoint { get; set; }


    void Awake()
    {
        CameraFollowPlayerYxis.ResetPlayer += SetPlayerPosition;
    }

    void Start()
    {
        // This is crossed out because player shouldn't reset if they have never been to a checkpoint.
        //CurrentCheckpoint = Player.position;
        _mainCam = Camera.main;
    }

    private void SetPlayerPosition()
    {
        // stop player
        Player.velocity = Vector2.zero;
        // place them at checkpoint position
        Player.position = (Vector2)CurrentCheckpoint;
        // place the camera at the checkpoint position
        _mainCam.transform.position = new Vector3(0, ((Vector2)CurrentCheckpoint).y, -10);
    }


}
