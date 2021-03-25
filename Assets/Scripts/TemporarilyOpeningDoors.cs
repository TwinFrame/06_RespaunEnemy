using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class TemporarilyOpeningDoors : MonoBehaviour
{
	[SerializeField] private float _durationSwitchOffDoors;
	[SerializeField] private float _durationFadeOpacity;

	private SpriteRenderer _doors;
	private float _currentTime;
	//private bool _isDisable;
	private bool _isDisabling;
	private bool _isFadeOut;
	private bool _isFadeIn;

	private Color _colorDoorsNoVisible;
	private Color _colorDoorsOnStart;

	private Coroutine fadeInOpacity;
	private Coroutine fadeOutOpacity;
	private Coroutine _tempOpeningDoors;

	private void Start()
	{
		_doors = GetComponent<SpriteRenderer>();
		_colorDoorsOnStart = new Color(_doors.color.r, _doors.color.g, _doors.color.b, _doors.color.a);
		_colorDoorsNoVisible = new Color(_doors.color.r, _doors.color.g, _doors.color.b, 0);
	}

	public void StartDisablingDoors()
	{
		var disablingDoorsJob = StartCoroutine(DisablingDoors());
	}

	public IEnumerator DisablingDoors()
	{
		_currentTime = 0;

		while (_currentTime <= _durationFadeOpacity)
		{
			_currentTime += Time.deltaTime;

			float normalizeCurrentTime = _currentTime / _durationFadeOpacity;

			_doors.color = Color.Lerp(_colorDoorsOnStart, _colorDoorsNoVisible, normalizeCurrentTime);

			yield return null;
		}

		GetComponent<Rigidbody2D>().simulated = false;
		yield return new WaitForSecondsRealtime(_durationSwitchOffDoors);
		GetComponent<Rigidbody2D>().simulated = true;

		_currentTime = 0;

		while (_currentTime <= _durationFadeOpacity)
		{
			_currentTime += Time.deltaTime;

			float normalizeCurrentTime = 1 - _currentTime / _durationFadeOpacity;

			_doors.color = Color.Lerp(_colorDoorsOnStart, _colorDoorsNoVisible, normalizeCurrentTime);

			yield return null;
		}
	}
}
