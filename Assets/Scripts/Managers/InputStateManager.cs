using UnityEngine;

public class InputStateManager : MonoBehaviour
{
    [SerializeField] PlayerInput inputScript;
    [SerializeField] TransitionManager inputTransitionScript;
    [SerializeField] PauseMenuManager inputPauseScript;


    void Awake()
    {
        PauseMenuManager.DisableInputs += DisableInput;
        PauseMenuManager.DisableInputs += DisableTransitionInput;
        PauseMenuManager.EnableInputs += EnableInput;
        PauseMenuManager.EnableInputs += EnableTransitionInput;

        TransitionManager.DisableInput += DisableInput;
        TransitionManager.DisableInput += DisablePauseInput;
        TransitionManager.EnableInput += EnableInput;
        TransitionManager.EnableInput += EnablePauseInput;
    }


    #region Player Input Enable/Disable
    private void EnableInput()
        {
            Debug.Log("Player Input enabled");
            inputScript.enabled = true;
        }
        private void DisableInput()
        {
            Debug.Log("Player Input disabled");
            inputScript.enabled = false;
        }


    #endregion

    
    #region Transition Input Enable/Disable
        private void EnableTransitionInput()
        {
            Debug.Log("Transition Input enabled");
            inputTransitionScript.enabled = true;
        }
        private void DisableTransitionInput()
        {
            Debug.Log("Transition Input disabled");
            inputTransitionScript.enabled = false;
        }


    #endregion


    #region Pause Input Enable/Disable
        private void EnablePauseInput()
        {
            Debug.Log("Pause Input enabled");
            inputPauseScript.enabled = true;
        }
        private void DisablePauseInput()
        {
            Debug.Log("Pause Input disabled");
            inputPauseScript.enabled = false;
        }


    #endregion


}
