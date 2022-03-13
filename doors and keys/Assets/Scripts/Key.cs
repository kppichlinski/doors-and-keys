using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Key : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler
{ 
    public void OnPointerClick(PointerEventData eventData)
    {
        ActionPanel.instance.ShowWindowDialog(true);
        ActionPanel.instance.ShowYesNoButtons();
        ActionPanel.instance.SetActions(YesButtonAction, "Take?");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ActionPanel.instance.ChangeColor(gameObject, true);
    }

    public void YesButtonAction()
    {
        GameManagement.instance.hasPlayerKey = true; 
        ActionPanel.instance.ShowWindowDialog(false);
        Destroy(gameObject);
    }
}
