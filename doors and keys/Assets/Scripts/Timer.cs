using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public static Timer instance;

    [SerializeField] GameObject timer;

    [HideInInspector] public float TimeFromStart { get { return time; } set { time = value; } }
    private float time = 0;

    private bool isTimerEnabled;
    private TextMeshProUGUI text;

    private void Awake()
    {
        instance = this;
        text = timer.GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Update()
    {
        if (isTimerEnabled)
        {
            time += Time.deltaTime;
            text.text = TimeToString(time);
        }
    }

    public void TimerState(bool state)
    {
        isTimerEnabled = state;
        timer.SetActive(state);
    }

    public static string TimeToString(float time)
    {
        float minutes = Mathf.FloorToInt(time / 60);
        float seconds = Mathf.FloorToInt(time % 60);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
