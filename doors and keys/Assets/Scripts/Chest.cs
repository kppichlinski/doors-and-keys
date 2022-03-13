using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class Chest : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!animator.GetBool("Open"))
        {
            ActionPanel.instance.ShowWindowDialog(true);
            ActionPanel.instance.ShowYesNoButtons();
            ActionPanel.instance.SetActions(YesButtonAction, "Open?");
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ActionPanel.instance.ChangeColor(gameObject, true);
    }

    public void YesButtonAction()
    {
        animator.SetBool("Open", true);
        ActionPanel.instance.ShowWindowDialog(false);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position, GetComponent<BoxCollider>().bounds.size);
    }

}
