using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    private void Awake()
    {
        if (!gameObject.activeSelf)
            gameObject.SetActive(true);
    }

    public void StartGame()
    {
        gameObject.SetActive(false);
    }
}
