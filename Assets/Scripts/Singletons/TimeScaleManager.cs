using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TimeScaleManager : MonoBehaviour {

	public static TimeScaleManager Instance { get; private set; }
	public UnityEvent TimeScaleFadingFinished;
	//public AnimationCurve plot = new AnimationCurve();

	private int currentId;

	private void Awake() {
		if (Instance == null) {
			Instance = this;
		} else if (Instance != this) {
			Destroy(gameObject);
		}
		DontDestroyOnLoad(gameObject);
	}

	// Start is called before the first frame update
	void Start() {
		currentId = 0;
		EventsManager.Instance.gameStateChanges.AddListener(OnGameStateChanges);
		OnGameStateChanges();
	}

	// Update is called once per frame
	void Update() {
		//plot.AddKey(Time.realtimeSinceStartup, Time.timeScale);
	}

	private void OnGameStateChanges() {
		switch (GameStatesManager.Instance.gameState) {
			case (GameStatesManager.AvailableGameStates.Menu):

				break;
			case (GameStatesManager.AvailableGameStates.Starting):

				break;
			case (GameStatesManager.AvailableGameStates.Playing):

				break;
			case (GameStatesManager.AvailableGameStates.Pausing):

				break;
			case (GameStatesManager.AvailableGameStates.Ending):

				break;
		}
	}

	/// <summary>
	/// Pauses the game
	/// </summary>
	[ContextMenu("Pause Game")]
	public void PauseGame() {
		SetTimeScale(0.0f, 0.0f, LerpManager.LerpMode.NormalLerp);
	}

	/// <summary>
	/// Pauses the game with a time scale fading
	/// </summary>
	public void PauseGame(float lerpDuration, LerpManager.LerpMode lerpMode) {
		SetTimeScale(0.0f, lerpDuration, lerpMode);
	}

	/// <summary>
	/// Resumes the game
	/// </summary>
	[ContextMenu("Resume Game")]
	public void ResumeGame() {
		SetTimeScale(1.0f, 0.0f, LerpManager.LerpMode.NormalLerp);
	}

	/// <summary>
	/// Resumes the game with a time scale fading
	/// </summary>
	public void ResumeGame(float lerpDuration, LerpManager.LerpMode lerpMode) {
		SetTimeScale(0.0f, lerpDuration, lerpMode);
	}

	/// <summary>
	/// Sets the time scale to the specified value
	/// </summary>
	public void SetTimeScale(float targetTimeScale, LerpManager.LerpMode lerpMode) {
		SetTimeScale(targetTimeScale, 0.0f, lerpMode);
	}

	private float timeScaleGetter() {
		return Time.timeScale;
	}

	private void timeScaleSetter(float value) {
		Time.timeScale = value;
	}

	private int currentIdGetter() {
		return currentId;
	}

	/// <summary>
	/// Fades the time scale to the specified value
	/// </summary>
	public void SetTimeScale(float targetTimeScale, float lerpDuration, LerpManager.LerpMode lerpMode) {
		if (lerpDuration > 0.0f) {  //With fading
			currentId++;
			switch (lerpMode) {
				case LerpManager.LerpMode.NormalLerp:
					LerpManager.Instance.StartLerp(currentIdGetter, timeScaleGetter, timeScaleSetter, targetTimeScale, lerpDuration, LerpManager.LerpMode.NormalLerp, TimeScaleFadingFinished, null);
					break;
				case LerpManager.LerpMode.EaseOutLerp:
					LerpManager.Instance.StartLerp(currentIdGetter, timeScaleGetter, timeScaleSetter, targetTimeScale, lerpDuration, LerpManager.LerpMode.EaseOutLerp, TimeScaleFadingFinished, null);
					break;
				case LerpManager.LerpMode.EaseInLerp:
					LerpManager.Instance.StartLerp(currentIdGetter, timeScaleGetter, timeScaleSetter, targetTimeScale, lerpDuration, LerpManager.LerpMode.EaseInLerp, TimeScaleFadingFinished, null);
					break;
				case LerpManager.LerpMode.SmoothLerp:
					LerpManager.Instance.StartLerp(currentIdGetter, timeScaleGetter, timeScaleSetter, targetTimeScale, lerpDuration, LerpManager.LerpMode.SmoothLerp, TimeScaleFadingFinished, null);
					break;
				case LerpManager.LerpMode.SmootherLerp:
					LerpManager.Instance.StartLerp(currentIdGetter, timeScaleGetter, timeScaleSetter, targetTimeScale, lerpDuration, LerpManager.LerpMode.SmootherLerp, TimeScaleFadingFinished, null);
					break;
			}
		} else { //Without fading
			Time.timeScale = targetTimeScale;
		}
	}
}