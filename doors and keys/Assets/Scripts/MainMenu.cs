using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        gameObject.SetActive(false);
        Camera.main.GetComponent<CameraController>().enabled = true;
        Timer.instance.TimerState(true);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
