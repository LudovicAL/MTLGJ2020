using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeCollisionsManager : MonoBehaviour 
{
	public float axeDamage;
	void OnTriggerEnter2D(Collider2D collider) {
		FlammableObject flammableObject = collider.gameObject.GetComponent<FlammableObject>();
		if (flammableObject != null && flammableObject._flammableObjectData.isBreakable)
		{
			flammableObject._isOnFire = false;
			flammableObject._currentHitPoints -= axeDamage;
		}

	}
}
