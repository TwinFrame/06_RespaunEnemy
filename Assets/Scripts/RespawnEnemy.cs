using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Transform))]

public class RespawnEnemy : MonoBehaviour
{
	[SerializeField] private GameObject _enemy;
	[SerializeField] private float _durationRespawn;
	[SerializeField] ContactFilter2D _filter;

	private GameObject _currentEnemy;
	private RespawnPoint[] _respawnPoints;
	private int _currentPoint;
	private Collider2D _wheelFrame;
	private Collider2D[] _colliders = new Collider2D[1];
	private Vector3 _offsetInstantiatePosition;
	private WaitForSecondsRealtime _waitForSecondsRealtime;

	private void Start()
	{
		_waitForSecondsRealtime = new WaitForSecondsRealtime(_durationRespawn);

		_wheelFrame = GameObject.FindObjectOfType<WheelFrame>().GetComponent<PolygonCollider2D>();

		_respawnPoints = GetComponentsInChildren<RespawnPoint>();

		var creatingEnemiesJob = StartCoroutine(CreatingEnemiesByPoints(_respawnPoints));
	}

	private IEnumerator CreatingEnemiesByPoints(RespawnPoint[] _points)
	{
		_currentPoint = 0;

		while (true)
		{
			_colliders = Physics2D.OverlapBoxAll(new Vector2(_points[_currentPoint].transform.position.x, _points[_currentPoint].transform.position.y),
				new Vector2(0.5f, 0.5f), 0);

			foreach (var colider in _colliders)
			{
				if (colider == _wheelFrame)
				{
					if (_points[_currentPoint].transform.position.y > 0)
					{
						_offsetInstantiatePosition = new Vector3(0, -1f, 0);
					}
					else
					{
						_offsetInstantiatePosition = new Vector3(0, 1f, 0);
					}

					break;
				}

				_offsetInstantiatePosition = new Vector3(0, 0, 0);
			}

			_currentEnemy = Instantiate(_enemy, _points[_currentPoint].transform.position + _offsetInstantiatePosition, Quaternion.identity);

			_currentPoint++;

			if (_currentPoint >= _points.Length)
			{
				_currentPoint = 0;
			}

			yield return _waitForSecondsRealtime;
		}
	}
}
