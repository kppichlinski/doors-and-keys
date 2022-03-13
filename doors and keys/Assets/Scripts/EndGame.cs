using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    public static EndGame instance;

    [SerializeField] GameObject endGamePanel;
    [SerializeField] TextMeshProUGUI currentScore;
    [SerializeField] TextMeshProUGUI highScore;

    [SerializeField] float timeToEnd;

    private void Awake()
    {
        instance = this;
    }

    private void OnGameEnd()
    {
        endGamePanel.SetActive(true);
        currentScore.text = Timer.TimeToString(Timer.instance.TimeFromStart);

        float time = PlayerPrefs.GetFloat("BestTime", -1);

        if (time == -1 || time < Timer.instance.TimeFromStart)
            PlayerPrefs.SetFloat("BestTime", Timer.instance.TimeFromStart);

        highScore.text = Timer.TimeToString(PlayerPrefs.GetFloat("BestTime"));
    }

    public void TryAgain()
    {

    }

    public IEnumerator GameOver()
    {
        yield return new WaitForSeconds(timeToEnd);
        OnGameEnd();
    }
}
