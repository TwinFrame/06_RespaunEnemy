using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Transform))]

public class WheelRotate : MonoBehaviour
{
    [SerializeField] private float _duration;

    private Transform _wheel;
    private Transform _pushButton;
    private Coroutine _wheelRotationJob;

    private void Start()
    {
        _wheel = GetComponent<Transform>();

        _pushButton = _wheel.GetComponentInChildren<PushButton>().transform;

        _wheelRotationJob = StartCoroutine(WheelRotation());
    }

    private IEnumerator WheelRotation()
    {
        while (true)
        {
            _wheel.transform.Rotate(new Vector3(0, 0, 1), -_duration * Time.deltaTime);

            _pushButton.transform.SetPositionAndRotation(_pushButton.position, Quaternion.Euler(0, 0, -_wheel.transform.rotation.z));

            yield return null;
        }
    }
}
