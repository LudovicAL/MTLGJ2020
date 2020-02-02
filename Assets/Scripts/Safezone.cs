using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Safezone : MonoBehaviour
{
    public float radius = 12f;

    private GameObject[] victims;

    void OnValidate()
    {
        transform.localScale = new Vector3(radius, radius, 1);
    }

    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = new Vector3(radius, radius, 1);

        victims = GameObject.FindGameObjectsWithTag("Victim");
    }

    // Update is called once per frame
    void Update()
    {
        if (victims.Length == 0)
            return;

        Vector2 position = transform.position;
        float sqrRadius = radius * radius * 0.8f; // 80% of size so we have to get the victim inside
        for (int i = 0; i < victims.Length; ++i)
        {
            GameObject victim = victims[i];
            if (victim == null)
                continue;

            Vector2 toVictim = (Vector2)victim.transform.position - position;
            if (toVictim.sqrMagnitude <= sqrRadius)
            {
                GameObject parent = victim.transform.parent ? victim.transform.parent.gameObject : victim;

                // turn off collisions
                Collider2D[] colliders = parent.GetComponents<Collider2D>();
                foreach (Collider2D col in colliders)
                    col.isTrigger = true;

                Grabbable[] grabbables = parent.GetComponentsInChildren<Grabbable>();
                foreach (Grabbable grabbable in grabbables)
                {
                    grabbable.gameObject.layer = 0;
                    if (grabbable.owner) grabbable.owner.DropGrabbedObject(true);
                }
    
                Debug.Log($"Saved {parent.name}!");

                // remove from list
                victims[i] = null;
            }
        }
    }
}
