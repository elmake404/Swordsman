using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlyerLife : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            Destroy(transform.parent.gameObject);
        }
        if (other.tag == "Coin")
        {
            Destroy(other.gameObject);
        }
    }
}
