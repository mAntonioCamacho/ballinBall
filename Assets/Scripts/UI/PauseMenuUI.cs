using UnityEngine;

public class PauseMenuUI : MonoBehaviour
{
    [SerializeField] private GameObject PauseMenu;

    void Awake()
    {
        PauseMenuManager.ShowPauseMenu += PauseGame;
    }

    private void PauseGame(bool isGamePaused)
    {
        if(isGamePaused)
        {
            PauseMenu.SetActive(isGamePaused);
        }
        else
        {
            PauseMenu.SetActive(isGamePaused);
        }
            
    }


}
