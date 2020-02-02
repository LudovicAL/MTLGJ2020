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
    private ConstantForce2D constantForce;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        constantForce = GetComponent<ConstantForce2D>();
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

            owner.DropGrabbedObject();
            Drop();
            return;
        }

        direction = direction.normalized;
        rigidBody.velocity = direction * speed;
    }

    public void Pickup(PlayerControler pickupPlayer)
    {
        owner = pickupPlayer;
        if (constantForce) constantForce.enabled = false;
    }

    public void Drop()
    {
        owner = null;
        if (constantForce) constantForce.enabled = true;
        rigidBody.velocity = Vector2.zero;
    }
}
