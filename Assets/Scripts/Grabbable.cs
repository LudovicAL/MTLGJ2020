using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabbable : MonoBehaviour
{
    // TODO: change owner speed
    public float speed = 3f;
    public float dropDistance = 1f;
    
    [HideInInspector]
    public PlayerControler owner;
    protected Rigidbody2D rigidBody;
    
    public Vector2 attachOffset;

    private float attachDropImmuninity = 1f;

    // Start is called before the first frame update
    public virtual void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
		UpdateInternal();
	}


	public virtual void UpdateInternal() {
        if (owner == null) 
			return;

        Vector2 attachPoint = owner.transform.position + owner.transform.rotation * (Vector3)attachOffset;
		Vector2 direction = attachPoint - rigidBody.position;
        float distanceSqr = direction.sqrMagnitude;

        // accidental drop immunity when just pickup
        if (attachDropImmuninity > 0f)
            attachDropImmuninity -= Time.deltaTime;
        bool canDrop = attachDropImmuninity <= 0f;
		if (canDrop && distanceSqr > dropDistance * dropDistance) {

			owner.DropGrabbedObject(false);
			Drop(false);
			return;
		}

        // rotate
        rigidBody.MoveRotation(owner.transform.rotation * Quaternion.Euler(0, 0, -90));

        // close enough, stop moving (avoid jitter)
        if (distanceSqr < 0.15f)
        {
            rigidBody.velocity = Vector2.zero;
            return;
        }

		direction = direction.normalized;
		rigidBody.velocity = direction * speed * 1.2f; // 20% extra speed to better match player
	}	

	public virtual void Pickup(PlayerControler pickupPlayer)
    {
        owner = pickupPlayer;
        attachDropImmuninity = 0f;
    }

    public virtual void Drop(bool intentional)
    {
        owner = null;
        rigidBody.velocity = Vector2.zero;
    }

    public virtual void Aim(bool start) {}
}
