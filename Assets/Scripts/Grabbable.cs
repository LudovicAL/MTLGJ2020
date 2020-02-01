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
    void Start()
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

            owner.DropGrabbedObject();
            rigidBody.velocity = Vector2.zero;
            return;
        }

        direction = direction.normalized;
        rigidBody.velocity = direction * speed;
    }
}
