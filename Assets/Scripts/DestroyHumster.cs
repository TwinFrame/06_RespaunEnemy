using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyHumster : MonoBehaviour
{
    private BoxCollider2D _deathZone;

    private void Start()
    {
        _deathZone = GameObject.FindObjectOfType<DeathZone>().GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == _deathZone)
        {
            Destroy(gameObject);
        }
    }
}
