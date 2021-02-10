using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlyerLife : MonoBehaviour
{
    public static PlyerLife PlayerLife;
    private List<GameObject> _ether = new List<GameObject>();

    public delegate void AddCoin(int namber);
    public event AddCoin onCoinTake;

    private void Awake()
    {
        PlayerLife = this;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Delimiter>() != null)
        {
            CanvasManager.IsLoseGame = true;

            Destroy(transform.parent.gameObject);
        }

        if (other.tag == "Coin")
        {
            onCoinTake?.Invoke(1);
            Destroy(other.gameObject);
        }

        if (other.gameObject.layer == 9)
        {
            _ether.Add(other.gameObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 9)
        {
            _ether.Remove(other.gameObject);
            if(_ether.Count<=0)
            CanvasManager.IsLoseGame = true;
        }
    }
}
