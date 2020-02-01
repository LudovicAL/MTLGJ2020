using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(PlayerHealthBar))]
public class PlayerHealthBarEditor : Editor {
	public override void OnInspectorGUI() {
		DrawDefaultInspector();

		PlayerHealthBar playerHealthBar = (PlayerHealthBar)target;
		if (GUILayout.Button("OnTakingDamage")) {
			playerHealthBar.OnTakingDamage(playerHealthBar.player.playerId, (float)playerHealthBar.player.playerId.currentHealth / (float) playerHealthBar.player.playerId.maxHealth);
		}
	}
}