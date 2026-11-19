using System.Collections;
using UnityEngine;

public class CameraFollowPlayerYxis : MonoBehaviour
{
    [SerializeField] Camera _mainCam;
    [SerializeField] Rigidbody2D _playerPos;
    ///
    /// 
    /// 
    #region Camera Movement Variables
        private float _lowestCamPos = 0f;
        private float _cameraRiseSpeed = 1f; //was originally .5 -skye
        private float _cameraFallSpeed = 4f;
    #endregion
    

    void Awake()
    {
        PlayerMovement.onBowlBallHitGround += ShakeScreen;
    }


    void Update()
    {
        UpdateCameraPosition();
    }


    void UpdateCameraPosition()
    {
        // the position we want the camera to follow
        // Don't move horizontally, player position, -10 : the camera transform z position needs to stay the same other wise clipping issue
        Vector3 target = new Vector3(0, _playerPos.position.y, _mainCam.transform.position.z);
        
        // camera go down - camera doesn't go past lowest camera position possible
        if(_mainCam.transform.position.y > _playerPos.position.y && _mainCam.transform.position.y > _lowestCamPos)
        {
            _mainCam.transform.position = Vector3.Slerp(_mainCam.transform.position, target, Time.deltaTime * _cameraFallSpeed);
        }
        // camera go up
        if(_mainCam.transform.position.y < _playerPos.position.y)
        {
            _mainCam.transform.position = Vector3.Slerp(_mainCam.transform.position, target, Time.deltaTime * _cameraRiseSpeed);
        }

    }


    private void ShakeScreen()
    {
        //Debug.Log("I'm shook");
        StartCoroutine(Shook());
    }


    private IEnumerator Shook()
    {
        float duration = .2f;
        float elapsed = 0f;
        while(elapsed < duration)
        {
            // elasped * 80 is frequency
            // .5f is shake strength
            float z = Mathf.Sin(elapsed * 80f) * .5f;
            // twist the camera with sin zed value - sin wave goes up and down, so the value goes up and down - so the z value
            // goes up and down... giving it that... rotation effect...
            transform.localRotation = Quaternion.Euler(0,0,z);
            elapsed += Time.deltaTime;
            // happens every frame - probably not the best since framerate can change, but eh
            yield return null;
        }
        // set rotation back to normal position 0,0,0 and possibly another 0? Quaternions...
        transform.localRotation = Quaternion.identity;
    }


}
