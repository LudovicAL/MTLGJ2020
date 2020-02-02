using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeCollisionsManager : MonoBehaviour {

	public float axeDamage;

	void Start() {
        
    }

    void Update() {
        
    }

	void OnTriggerEnter2D(Collider2D col) {
		if (col.CompareTag("Wall")) {
			col.gameObject.GetComponent<FlammableObject>()._currentHitPoints -= axeDamage;
		}
	}
}
