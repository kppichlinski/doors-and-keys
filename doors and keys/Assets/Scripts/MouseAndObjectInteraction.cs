using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseAndObjectInteraction : MonoBehaviour
{
    public static MouseAndObjectInteraction instance;

    [SerializeField] Color onMouseEnterColor;

    private Color defaultColor = Color.white;

    private void Awake()
    {
        instance = this;
    }

    public void ChangeColor(GameObject gameObject, bool state)
    {
        if (state)
        {
            foreach (Renderer renderer in gameObject.GetComponentsInChildren<Renderer>())
            {
                renderer.material.color = onMouseEnterColor;
            }
        }
        else
        {
            foreach (Renderer renderer in gameObject.GetComponentsInChildren<Renderer>())
            {
                renderer.material.color = defaultColor;
            }
        }
    }
}
