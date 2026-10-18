using System;
using UnityEngine;

public class CameraFollowPlayerYxis : MonoBehaviour
{
    [SerializeField] Camera _mainCam;
    [SerializeField] Rigidbody2D _playerPos;
    private float _lowestCamPos = 0f;
    private float _deadZone = 1.5f;
    private Vector3 _cameraVelocity = Vector3.zero;
    private float _cameraRiseSpeed = .5f;
    private float _cameraFallSpeed = 3f;

    void Update()
    {
        UpdateCameraPosition();
    }


    void UpdateCameraPosition()
    {
        // the position we want the camera to follow
        // Don't move horizontally, player position, -10, the camera transform position needs to stay the same other wise clipping issue
        Vector3 target = new Vector3(0, _playerPos.position.y, _mainCam.transform.position.z);
        // camera go down
        if(_mainCam.transform.position.y - _deadZone > _playerPos.position.y && _mainCam.transform.position.y > _lowestCamPos)
        {
            _mainCam.transform.position = Vector3.Slerp(_mainCam.transform.position, target, Time.deltaTime * _cameraFallSpeed);
            // another way to move camera, different FEEL to it
            //_mainCam.transform.position = Vector3.SmoothDamp(_mainCam.transform.position, target, ref cameraVelocity, .2f);
        }


        // camera go up
        if(_mainCam.transform.position.y + _deadZone < _playerPos.position.y)
        {
            _mainCam.transform.position = Vector3.Slerp(_mainCam.transform.position, target, Time.deltaTime * _cameraRiseSpeed);
        }

    }

}
