using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class RotateWheel : MonoBehaviour
{
    [SerializeField] private float _speed;

    private Transform _wheel;
    private Transform _pushButton;

    void Start()
    {
        _wheel = GetComponent<Transform>();

        _pushButton = _wheel.GetComponentInChildren<PushButton>().transform;
    }

    void Update()
    {
        _wheel.transform.SetPositionAndRotation(_wheel.position, Quaternion.Euler(0, 0, -_speed * Time.time));
        _pushButton.transform.SetPositionAndRotation(_pushButton.position, Quaternion.Euler(0, 0, -_wheel.transform.rotation.z));
    }
}
