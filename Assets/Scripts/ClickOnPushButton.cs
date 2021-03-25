using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ClickOnPushButton : MonoBehaviour
{
    [SerializeField] private UnityEvent _clickOnButton;

    private void OnMouseDown()
    {
        _clickOnButton.Invoke();
    }
}
