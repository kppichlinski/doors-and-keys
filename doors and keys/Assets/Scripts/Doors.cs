using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Doors : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (animator.GetBool("Open"))
            return;

        ActionPanel.instance.ShowWindowDialog(true);

        if (GameManagement.instance.hasPlayerKey)
        {
            ActionPanel.instance.SetActions(YesButtonAction, "Open?");
            ActionPanel.instance.ShowYesNoButtons();
        }
        else
        {
            ActionPanel.instance.SetActions(YesButtonAction, "You need a key!");
            ActionPanel.instance.ShowOkButton();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        MouseAndObjectInteraction.instance.ChangeColor(gameObject, true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        MouseAndObjectInteraction.instance.ChangeColor(gameObject, false);
    }

    public void YesButtonAction()
    {
        animator.SetBool("Open", true);
        ActionPanel.instance.ShowWindowDialog(false);

        StartCoroutine(EndGame.instance.GameOver());
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Collider collider = GetComponent<Collider>();
        Gizmos.DrawWireCube(collider.bounds.center, collider.bounds.size * 2f);
    }

}
