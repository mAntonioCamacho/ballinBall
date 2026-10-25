using UnityEngine;

public class BallFormManager : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _playerSprite;
    [SerializeField] private Rigidbody2D _playerRb;
    [SerializeField] private BallForm[] _allBallForms;
    [SerializeField] private GameObject _player;
    private BallForm currentForm;
    private int _ballIndex = 0;
    private PlayerMovement _playerMovement;


    void Awake()
    {
        PlayerInput.onPlayerSwapBall += SwapBall;   
        _playerMovement = _player.GetComponent<PlayerMovement>(); 
        currentForm = _allBallForms[_ballIndex]; 
        ChangeValues(currentForm); 
    }

    private void SwapBall()
    {
        if(_ballIndex == _allBallForms.Length - 1)
        {
            _ballIndex = 0;
        }
        else
        {
            _ballIndex++;
        }
        currentForm = _allBallForms[_ballIndex];
        ChangeValues(currentForm);
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
