using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireHoseGrabbable : Grabbable
{
    public Transform stream;
    private ConstantForce2D constantForce;
    private float initTimer = 1f;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        constantForce = GetComponent<ConstantForce2D>();
    }

    public override void UpdateInternal()
    {
        base.UpdateInternal();

        // so that it messes up our straight line a bit at the start
        if (initTimer > 0f)
        {
            initTimer -= Time.deltaTime;
            if (initTimer <= 0f)
            {
                if (constantForce) constantForce.enabled = false;
                if (stream) stream.gameObject.SetActive(false);
                rigidBody.velocity = Vector2.zero;
            }
        }
    }

    public override void Pickup(PlayerControler pickupPlayer)
    {
        base.Pickup(pickupPlayer);
        if (constantForce) constantForce.enabled = false;
        if (stream) stream.gameObject.SetActive(false);
    }

    public override void Drop(bool intentional)
    {
        base.Drop(intentional);
        if (constantForce) constantForce.enabled = !intentional;
        if (stream) stream.gameObject.SetActive(!intentional);
    }
    public override void Aim(bool start)
    {
        base.Aim(start);
        if (stream) stream.gameObject.SetActive(start);
    }
}
