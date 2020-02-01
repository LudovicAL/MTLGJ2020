using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LerpManager : MonoBehaviour {

	public static LerpManager Instance {
		get; private set;
	}
	[Tooltip("Frequency of coroutine updates in seconds")] public float coroutineUpdateFrequency = 0.05f;

	public enum LerpMode {
		NormalLerp,
		EaseOutLerp,
		EaseInLerp,
		SmoothLerp,
		SmootherLerp
	};

	private void Awake() {
		if (Instance == null) {
			Instance = this;
		} else if (Instance != this) {
			Destroy(gameObject);
		}
		DontDestroyOnLoad(gameObject);
	}

	private class LerpObject {
		public Func<int> currentIdGetter;
		public int instanceId;
		public Func<float> lerpedVariableGetter;
		public Action<float> lerpedVariableSetter;
		public float targetValue;
		public float lerpDuration;
		public Func<float, float, float> lerpFunction;
		public UnityEvent onFinishEvent;
		public Action onFinishFunction;

		public LerpObject(Func<int> currentIdGetter, Func<float> lerpedVariableGetter, Action<float> lerpedVariableSetter, float targetValue, float lerpDuration, Func<float, float, float> lerpFunction, UnityEvent onFinishEvent, Action onFinishFunction) { //The first n-1 arguments of a Func are the types of its parameters. The nth argument is its return type.
			this.currentIdGetter = currentIdGetter;
			this.instanceId = currentIdGetter();
			this.lerpedVariableGetter = lerpedVariableGetter;
			this.lerpedVariableSetter = lerpedVariableSetter;
			this.targetValue = targetValue;
			this.lerpDuration = lerpDuration;
			this.lerpFunction = lerpFunction;
			this.onFinishEvent = onFinishEvent;
			this.onFinishFunction = onFinishFunction;
		}
	}

	public void StartLerp(Func<int> currentIdGetter, Func<float> lerpedVariableGetter, Action<float> lerpedVariableSetter, float targetValue, float lerpDuration, LerpMode lerpMode, UnityEvent onFinishEvent, Action onFinishFunction) {
		if (lerpDuration > 0.0f) {
			switch (lerpMode) {
				case LerpMode.NormalLerp:
					StartCoroutine(CustomLerp(new LerpObject(currentIdGetter, lerpedVariableGetter, lerpedVariableSetter, targetValue, lerpDuration, NormalLerp, onFinishEvent, onFinishFunction)));
					break;
				case LerpMode.EaseOutLerp:
					StartCoroutine(CustomLerp(new LerpObject(currentIdGetter, lerpedVariableGetter, lerpedVariableSetter, targetValue, lerpDuration, EaseOutLerp, onFinishEvent, onFinishFunction)));
					break;
				case LerpMode.EaseInLerp:
					StartCoroutine(CustomLerp(new LerpObject(currentIdGetter, lerpedVariableGetter, lerpedVariableSetter, targetValue, lerpDuration, EaseInLerp, onFinishEvent, onFinishFunction)));
					break;
				case LerpMode.SmoothLerp:
					StartCoroutine(CustomLerp(new LerpObject(currentIdGetter, lerpedVariableGetter, lerpedVariableSetter, targetValue, lerpDuration, SmoothLerp, onFinishEvent, onFinishFunction)));
					break;
				case LerpMode.SmootherLerp:
					StartCoroutine(CustomLerp(new LerpObject(currentIdGetter, lerpedVariableGetter, lerpedVariableSetter, targetValue, lerpDuration, SmootherLerp, onFinishEvent, onFinishFunction)));
					break;
			}
		} else {
			lerpedVariableSetter(targetValue);
			if (onFinishEvent != null) {
				onFinishEvent.Invoke();
			}
			if (onFinishFunction != null) {
				onFinishFunction();
			}
		}
	}

	//Fades the time scale with a custom lerp
	private IEnumerator CustomLerp(LerpObject lerpObject) {
		float startingValue = lerpObject.lerpedVariableGetter();
		float valueDistance = lerpObject.targetValue - lerpObject.lerpedVariableGetter();
		float startingLerpTime = Time.realtimeSinceStartup;
		float endingLerpTime = Time.realtimeSinceStartup + lerpObject.lerpDuration;
		while (lerpObject.instanceId == lerpObject.currentIdGetter() && Time.realtimeSinceStartup <= endingLerpTime) {
			float currentLerpTime = (Time.realtimeSinceStartup - startingLerpTime);
			float lerpedPosition = lerpObject.lerpFunction(currentLerpTime, lerpObject.lerpDuration);
			lerpObject.lerpedVariableSetter((valueDistance * lerpedPosition) + startingValue);
			yield return new WaitForSecondsRealtime(coroutineUpdateFrequency);
		}
		yield return null;
		if (lerpObject.instanceId == lerpObject.currentIdGetter()) {
			lerpObject.lerpedVariableSetter(lerpObject.targetValue);
			if (lerpObject.onFinishEvent != null) {
				lerpObject.onFinishEvent.Invoke();
			}
			if (lerpObject.onFinishFunction != null) {
				lerpObject.onFinishFunction();
			}
		}
	}

	//Normal lerp
	private static float NormalLerp(float currentLerpTime, float lerpDuration) {
		return currentLerpTime / lerpDuration;
	}

	//Lerps with an easing out element
	private float EaseOutLerp(float currentLerpTime, float lerpDuration) {
		float t = currentLerpTime / lerpDuration;
		return Mathf.Sin(t * Mathf.PI * 0.5f);
	}

	//Lerps with an easing in element
	private float EaseInLerp(float currentLerpTime, float lerpDuration) {
		float t = currentLerpTime / lerpDuration;
		return 1f - Mathf.Cos(t * Mathf.PI * 0.5f);
	}

	//Lerps with both an easing in and an easing out element
	private float SmoothLerp(float currentLerpTime, float lerpDuration) {
		float t = currentLerpTime / lerpDuration;
		return t * t * (3f - 2f * t);
	}

	//Lerps event more smoothly with both an easing in and an easing out element
	private float SmootherLerp(float currentLerpTime, float lerpDuration) {
		float t = currentLerpTime / lerpDuration;
		return t * t * t * (t * (6f * t - 15f) + 10f);
	}
}
