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
    private Rigidbody2D rigidBody;

    // Start is called before the first frame update
    public virtual void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (owner == null)
        {
            rigidBody.velocity = Vector2.zero;
            return;
        }

        Vector2 direction = ((Vector2)owner.transform.position - rigidBody.position);
        if (direction.sqrMagnitude > dropDistance * dropDistance)
        {

            owner.DropGrabbedObject(false);
            Drop(false);
            return;
        }

        direction = direction.normalized;
        rigidBody.velocity = direction * speed;
    }

    public virtual void Pickup(PlayerControler pickupPlayer)
    {
        owner = pickupPlayer;
    }

    public virtual void Drop(bool intentional)
    {
        owner = null;
        rigidBody.velocity = Vector2.zero;
    }

    public virtual void Aim(bool start) {}
}
