using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchReadyUI : MonoBehaviour
{
    [SerializeField] private GameObject _launchSignifier;

    void Awake()
    {
        PlayerMovement.onBallReadyForLaunch += ShowLaunchSignifier;
    }

    private void ShowLaunchSignifier(bool isReadyForLaunch)
    {
        if(isReadyForLaunch) { _launchSignifier.SetActive(true); }
        else if(!isReadyForLaunch) { _launchSignifier.SetActive(false); }
    }
}
