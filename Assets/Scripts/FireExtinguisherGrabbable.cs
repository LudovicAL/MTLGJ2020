using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireExtinguisherGrabbable : Grabbable
{
    public float MaxTank = 100f;
    public float DrainPerSecond = 5f;
    public Transform Stream;

    public float CurrentTank;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        CurrentTank = MaxTank;
        if (Stream) Stream.gameObject.SetActive(false);
    }

    public override void UpdateInternal()
    {
        if (owner == null)
            return;

        // drain
        bool isAiming = owner.IsAiming();
        if (owner.IsAiming())
        {
            CurrentTank -= DrainPerSecond * Time.deltaTime;
            if (CurrentTank <= 0f)
            {
                GetComponent<SpriteRenderer>().color = Color.gray;
                owner.DropGrabbedObject(false);
                gameObject.layer = 0; // default, no longer grabbable
                return;
            }
        }
        if (Stream) Stream.gameObject.SetActive(isAiming);

        // rotate
        rigidBody.MoveRotation(owner.transform.rotation * Quaternion.Euler(0, 0, -90));

        // teleport to position
        Vector2 attachPoint = owner.transform.position + owner.transform.rotation * (Vector3)attachOffset;
        rigidBody.position = attachPoint;
    }

    public override void Drop(bool intentional)
    {
        base.Drop(intentional);
        if (Stream) Stream.gameObject.SetActive(false);
    }
}
