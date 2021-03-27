using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]

public class TemporarilyOpeningDoors : MonoBehaviour
{
	[SerializeField] private float _durationOpenDoors;
	[SerializeField] private float _durationFadeOpacity;

	private SpriteRenderer _doors;
	private float _currentTime;
	private Coroutine _openingDoorsJob;
	private Coroutine _fadingOpacityJob;
	private bool _isOpeningDoors;

	private Color _colorDoorsNoVisible;
	private Color _colorDoorsOnStart;

	private void Start()
	{
		_doors = GetComponent<SpriteRenderer>();
		_colorDoorsOnStart = new Color(_doors.color.r, _doors.color.g, _doors.color.b, _doors.color.a);
		_colorDoorsNoVisible = new Color(_doors.color.r, _doors.color.g, _doors.color.b, 0);
	}

	public void StartOpeningDoors()
	{
		if (!_isOpeningDoors)
		{
			_openingDoorsJob = StartCoroutine(OpeningDoors());
		}
	}

	public IEnumerator OpeningDoors()
	{
		_isOpeningDoors = true;

		if (_fadingOpacityJob != null)
		{
			StopCoroutine(_fadingOpacityJob);
		}

		_fadingOpacityJob = StartCoroutine(FadingOpacity(_colorDoorsOnStart, _colorDoorsNoVisible, _durationFadeOpacity));

		GetComponent<Rigidbody2D>().simulated = false;
		yield return new WaitForSecondsRealtime(_durationOpenDoors);
		GetComponent<Rigidbody2D>().simulated = true;

		if (_fadingOpacityJob != null)
		{
			StopCoroutine(_fadingOpacityJob);
		}

		_fadingOpacityJob = StartCoroutine(FadingOpacity(_colorDoorsNoVisible, _colorDoorsOnStart, _durationFadeOpacity));

		_isOpeningDoors = false;
	}

	private IEnumerator FadingOpacity(Color startColor, Color endColor, float time)
	{
		_currentTime = 0;

		while (_currentTime <= time)
		{
			_currentTime += Time.deltaTime;

			float normalizeCurrentTime = _currentTime / time;

			_doors.color = Color.Lerp(startColor, endColor, normalizeCurrentTime);

			yield return null;
		}
	}
}
