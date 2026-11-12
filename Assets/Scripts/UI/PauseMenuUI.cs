using UnityEngine;

public class PauseMenuUI : MonoBehaviour
{
    [SerializeField] private GameObject PauseMenu;

    void Awake()
    {
        PlayerInput.onPlayerPause += PauseGame;
    }

    private void PauseGame(bool isGamePaused)
    {
        Debug.Log(isGamePaused);
        if(isGamePaused)
        {
            Debug.Log("unpaused");
            PauseMenu.SetActive(!isGamePaused);
            Time.timeScale = 1;
        }
        else
        {
            Debug.Log("paused");
            PauseMenu.SetActive(!isGamePaused);
            Time.timeScale = 0;
        }
            
    }

    public void PrintOnButtonPress()
    {
        Debug.Log("Pressed");
    }
}
