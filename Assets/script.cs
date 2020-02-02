using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class script : MonoBehaviour
{
    void OnParticleCollision(GameObject collision) {
        Debug.Log(collision);
    }
}
