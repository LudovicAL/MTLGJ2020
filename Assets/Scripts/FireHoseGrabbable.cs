using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireHoseGrabbable : Grabbable
{
    public Transform stream;
    private ConstantForce2D constantForce;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        constantForce = GetComponent<ConstantForce2D>();
        if (constantForce) constantForce.enabled = false;
        if (stream) stream.gameObject.SetActive(false);
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
