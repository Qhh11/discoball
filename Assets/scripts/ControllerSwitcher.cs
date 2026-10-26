using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerSwitcher : MonoBehaviour
{
    public GameObject playerController;
    public GameObject ovrPlayerController;
    
    public void SwitchController()
    {
        if (ovrPlayerController.activeInHierarchy)
        {
            ovrPlayerController.SetActive(false);
            playerController.SetActive(true);
        }
        else
        {
            ovrPlayerController.SetActive(true);
            playerController.SetActive(false);
        }
    }
}