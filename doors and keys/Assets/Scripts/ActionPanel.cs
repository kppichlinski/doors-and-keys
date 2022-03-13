using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ActionPanel : MonoBehaviour
{
    public static ActionPanel instance;

    [SerializeField] GameObject panel;
    [SerializeField] Button yesButton;
    [SerializeField] Button noButton;
    [SerializeField] Button okButton;
    [SerializeField] TextMeshProUGUI questionText;

    private void Start()
    {
        instance = this;
    }

    public void SetActions(UnityAction yesButtonAction, string text)
    {
        yesButton.onClick.AddListener(yesButtonAction);
        questionText.text = text;
    }

    public  void ShowWindowDialog(bool state)
    {
        panel.SetActive(state);
    }

    public  void ChangeColor(GameObject gameObject, bool state)
    {
        return;
    }

    public void CloseWindowDialog()
    {
        ShowWindowDialog(false);
    }

    public void ShowOkButton()
    {
        okButton.gameObject.SetActive(true);
        yesButton.gameObject.SetActive(false);
        noButton.gameObject.SetActive(false);
    }

    public void ShowYesNoButtons()
    {
        okButton.gameObject.SetActive(false);
        yesButton.gameObject.SetActive(true);
        noButton.gameObject.SetActive(true);
    }
}
