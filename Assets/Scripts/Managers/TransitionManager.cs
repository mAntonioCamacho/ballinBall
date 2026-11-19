using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TransitionManager : MonoBehaviour
{
    #region Camera Fade Variables
        [SerializeField] private Image _fadeScreenImage;
        [SerializeField] private GameObject _imageObject;
        private float _fadeTimer;
        private float _fadeDuration = 1f;
    #endregion

    public static event Action ResetPlayer;
    public static event Action BeginTransition;

    public static event Action DisableInput;
    public static event Action EnableInput;
    private bool _isTransitioning = false;

    void Awake()
    {
        CheckpointManager.StartTransition += FadeToBlack;
    }

    void Update()
    {
        if(Input.GetKeyUp(KeyCode.R))
        {
            BeginTransition?.Invoke();
        }
    }

   
    private void FadeToBlack()
    {
        if(_isTransitioning)
            return;
        DisableInput?.Invoke();
        StartCoroutine(FadeScreenToBlack());
    }

    #region Camera Fade Logic
        IEnumerator FadeScreenToBlack()
        {
            _isTransitioning = true;
            StartCoroutine(FadeIn());
            yield return new WaitForSeconds(2.5f);
            ResetPlayer?.Invoke();
            StartCoroutine(FadeOut());
        }


        IEnumerator FadeOut()
        {
            _fadeTimer = 0;
            Color c = _fadeScreenImage.color;
            while(_fadeTimer < _fadeDuration)
            {
                yield return null;
                _fadeTimer += .008f;
                c.a = 1.0f - Mathf.Clamp01(_fadeTimer/_fadeDuration);
                _fadeScreenImage.color = c;
            }
            _imageObject.SetActive(false);
            _isTransitioning = false;
            EnableInput?.Invoke();
        }


        IEnumerator FadeIn()
    {
        _imageObject.SetActive(true);
        _fadeTimer = 0;
        Color c = _fadeScreenImage.color;
        while(_fadeTimer < _fadeDuration)
        {
            yield return null;
            _fadeTimer += .008f;
            c.a = Mathf.Clamp01(_fadeTimer/_fadeDuration);
            _fadeScreenImage.color = c;
        }
        
    }
    #endregion
}
