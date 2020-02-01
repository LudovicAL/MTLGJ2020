using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Player))]
public class PlayerActions : MonoBehaviour {

	[HideInInspector] public Player player;
	public PlayerAttacking playerAttacking = new PlayerAttacking();

	[System.Serializable]
	public class PlayerAttacking : UnityEvent<PlayerId> {}

	// Use this for initialization
	void Start () {
		player = GetComponent<Player>();
	}
	
	// Update is called once per frame
	void Update () {
		if (player.playerId.controls.GetButtonXDown()) {
			Attack();
		}
	}

	public void Attack() {
		playerAttacking.Invoke(player.playerId);
	}
}
