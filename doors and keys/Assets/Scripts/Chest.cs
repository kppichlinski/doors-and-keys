using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class Chest : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
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
        if (!animator.GetBool("Open"))
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
        GetComponent<ParticleSystem>().Stop();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position, Vector3.Scale(GetComponent<BoxCollider>().size, transform.localScale));
    }

}
