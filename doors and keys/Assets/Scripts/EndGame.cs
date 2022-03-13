using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        ScreenShake.instace.enabled = false;

        endGamePanel.SetActive(true);
        Timer.instance.TimerState(false);

        float time = Timer.instance.TimeFromStart;
        currentScore.text = "Current score: " +  Timer.TimeToString(time);

        float bestScore = PlayerPrefs.GetFloat("BestScore", -1);

        if (bestScore == -1 || time < bestScore)
        {
            bestScore = time;
            PlayerPrefs.SetFloat("BestScore", bestScore);
            PlayerPrefs.Save();
        }

        highScore.text = "High score: " + Timer.TimeToString(bestScore);
    }

    public void TryAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public IEnumerator GameOver()
    {
        yield return new WaitForSeconds(timeToEnd);
        OnGameEnd();
    }
}
