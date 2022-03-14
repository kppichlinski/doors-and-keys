using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagement : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);    }
}
