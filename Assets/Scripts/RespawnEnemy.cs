using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Transform))]
public class RespawnEnemy : MonoBehaviour
{
    [SerializeField] private GameObject _enemy;
    [SerializeField] private float _durationRespawn;

    private Transform[] _respawnPointsWithParrent;
    private Transform[] _respawnPoints;
    private int _currentPoint;

    private void Start()
    {
        _respawnPointsWithParrent = GetComponentsInChildren<Transform>();

        _respawnPoints = new Transform[_respawnPointsWithParrent.Length - 1];

        for (int i = 0; i < _respawnPointsWithParrent.Length - 1; i++)
        {
            _respawnPoints[i] = _respawnPointsWithParrent[i + 1];
        }

        var creatingEnemiesJob = StartCoroutine(CreatingEnemiesByPoints(_respawnPoints));
    }

    private IEnumerator CreatingEnemiesByPoints(Transform[] _points)
    {
        while (true)
        {
            Instantiate(_enemy, _points[_currentPoint].transform.position, Quaternion.identity);

            _currentPoint++;

            if (_currentPoint >= _points.Length)
            {
                _currentPoint = 0;
            }

            yield return new WaitForSecondsRealtime(_durationRespawn);
        }


    }
}
