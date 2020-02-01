using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(PlayerControler))]
[RequireComponent(typeof(Collider2D))]
public class PlayerCollisionsManager : MonoBehaviour {

	private PlayerControler playerControler;
	private Collider2D currentCollider;

	void Awake() {
		playerControler = GetComponent<PlayerControler>();
		currentCollider = GetComponent<Collider2D>();
	}

    void Update() {

	}

	void OnTriggerEnter2D(Collider2D col) {
		if (col.CompareTag("Fire")) {
			playerControler.SetOnFire(true);
		}
	}

	private void OnTriggerExit2D(Collider2D col) {

	}

	void OnCollisionEnter2D(Collision2D col) {
		if (col.gameObject.CompareTag("Door")) {

		}
	}

	void OnCollisionExit2D(Collision2D col) {
		if (col.gameObject.CompareTag("Door")) {

		}
	}
}
