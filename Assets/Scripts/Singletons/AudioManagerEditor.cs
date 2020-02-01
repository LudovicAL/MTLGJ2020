using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(AudioManager))]
public class AudioManagerEditor : Editor {
	public override void OnInspectorGUI() {
		DrawDefaultInspector();

		AudioManager audioManager = (AudioManager)target;
		if (GUILayout.Button("PlayClip")) {
			audioManager.PlayClip(AudioManager.Instance.VictoryClip);
		}
	}
}