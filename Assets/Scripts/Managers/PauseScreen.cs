using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class PauseScreen : MonoBehaviour
{
	[SerializeField] private string _pauseButton;
	[SerializeField] private Canvas _pauseCanvas;
	private bool _paused;

	private void Update()
	{
		if (Input.GetButtonDown(_pauseButton))
		{
			_paused = !_paused;
		}
		if (_paused)
		{
			Pause();
		}
		else Resume();

	}

	public bool Paused
	{
		get => _paused;
		set => _paused = value;

	}


	public void Pause()
	{
		Time.timeScale = 0;
		_pauseCanvas.enabled = true;
		_paused = true;
	}

	public void Resume()
	{
		_paused = false;
		Time.timeScale = 1;
		_pauseCanvas.enabled = false;


	}

}
