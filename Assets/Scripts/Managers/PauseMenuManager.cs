using System;
using UnityEngine;

public class PauseMenuManager : MonoBehaviour
{
    public static event Action<bool> ShowPauseMenu;
    public static event Action DisableInputs;
    public static event Action EnableInputs;
    private bool _paused;
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Escape))
        {
            CheckIfPausedGame();
        }
    }

    private void CheckIfPausedGame()
    {
        if(!_paused)
        {
            Time.timeScale = 0;
            _paused = true;
            DisableInputs?.Invoke();
            ShowPauseMenu?.Invoke(_paused);
        }
        else
        {
            Time.timeScale = 1;
            _paused = false;
            EnableInputs?.Invoke();
            ShowPauseMenu?.Invoke(_paused);
        }
    }
}
