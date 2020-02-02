using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillTheParent : MonoBehaviour
{
    void OnDestroy() {
        Destroy(transform.parent.gameObject);
    }
}
