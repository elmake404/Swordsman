using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlyerLife : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        var Enemy = other.GetComponent<Enemy>();

        if (Enemy != null)
        {
            Destroy(transform.parent.gameObject);
        }
    }
}
