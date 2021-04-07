using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(PolygonCollider2D))]

public class FallingOutSideWheel : MonoBehaviour
{
    [SerializeField] private UnityEvent _onFalling;

    private Collider2D _wheel;
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();

        _wheel = GameObject.FindObjectOfType<InsideWheelArea>().GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D _wheel)
    {
        _animator.SetBool("isFalling", false);
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider == _wheel)
        {
            _animator.SetBool("isFalling", true);
            _onFalling.Invoke();
        }
    }
}
