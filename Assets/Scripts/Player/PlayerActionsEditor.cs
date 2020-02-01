using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(PlayerActions))]
public class PlayerActionsEditor : Editor {
	public override void OnInspectorGUI() {
		DrawDefaultInspector();

		PlayerActions playerActions = (PlayerActions)target;
		if (GUILayout.Button("Attack")) {
			playerActions.Attack();
		}
	}
}