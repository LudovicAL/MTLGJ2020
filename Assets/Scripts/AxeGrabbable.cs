using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeGrabbable : Grabbable {

	// Start is called before the first frame update
	public override void Start() {
	}

	public override void UpdateInternal() {
	}

	public override void Pickup(PlayerControler pickupPlayer) {
		base.Pickup(pickupPlayer);
		pickupPlayer.isAxing = true;
		GetComponent<SpriteRenderer>().enabled = false;
	}

	public override void Drop(bool intentional) {
		GetComponent<SpriteRenderer>().enabled = true;
		transform.position = owner.transform.position;
		transform.rotation = Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f));
		owner.isAxing = false;
		owner = null;
	}
	public override void Aim(bool start) {
		base.Aim(start);
	}
}
