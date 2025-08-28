using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    private void OnEnable()
    {
        EventManager.OnChangeButton += ChangeUIState;
    }

    private void OnDisable()
    {
        EventManager.OnChangeButton -= ChangeUIState;
    }

    private void ChangeUIState(GameObject UIElement)
    {
        if (UIElement != null)
        {
            if (UIElement.activeInHierarchy)
            {
                UIElement.SetActive(false);
            }
            else
            {
                UIElement.SetActive(true);
            }
        }
    }
}
