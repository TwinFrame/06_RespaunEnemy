using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyHumster : MonoBehaviour
{
    private float _deathLevelYPosition;

    private void Start()
    {
        _deathLevelYPosition = GameObject.FindObjectOfType<DeathLinePosition>().
            transform.position.y;
    }

    private void FixedUpdate()
    {
        if (transform.position.y <= _deathLevelYPosition)
        {
            Destroy(gameObject);
        }
    }
}
