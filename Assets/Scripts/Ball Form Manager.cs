using UnityEngine;

public class BallFormManager : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _playerSprite;
    [SerializeField] private Rigidbody2D _playerRb;
    [SerializeField] private BallForm[] _allBallForms;
    [SerializeField] private GameObject _player;
    public BallForm currentScriptObjBallForm { get; private set; }
    private int _ballIndex = 0;
    private PlayerMovement _playerMovement;


    void Awake()
    {
        PlayerInput.onPlayerSwapBall += SwapBall;   
        _playerMovement = _player.GetComponent<PlayerMovement>(); 
        currentScriptObjBallForm = _allBallForms[_ballIndex]; 
        ChangeValues(currentScriptObjBallForm); 
    }

    private void SwapBall()
    {

        _ballIndex = (_ballIndex < _allBallForms.Length - 1) ? ++_ballIndex : 0;
        currentScriptObjBallForm = _allBallForms[_ballIndex];
        ChangeValues(currentScriptObjBallForm);
    }

    private void ChangeValues(BallForm newForm)
    {
        _playerSprite.sprite = newForm.sprite;
        _playerSprite.color = newForm.spriteColor;
        _playerRb.sharedMaterial = newForm.ballPhysics;
        _playerRb.gravityScale = newForm.playerGravity;
        _playerMovement.SetSpeed(newForm.moveSpeed);
        _playerMovement.SetLaunchPower(newForm.launchPower);
    }


}
