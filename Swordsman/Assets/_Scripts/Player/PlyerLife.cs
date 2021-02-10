using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlyerLife : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Delimiter>()!=null)
        {
            Destroy(transform.parent.gameObject);
        }
        if (other.tag == "Coin")
        {
            Destroy(other.gameObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer==9)
        {
            CanvasManager.IsLoseGame = true;
        }
    }
}
