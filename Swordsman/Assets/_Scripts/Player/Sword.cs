using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    [SerializeField]
    private PlayerMove _player;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Rock")
        {            
            _player.SpeedCut();
        }
        if (collision.collider.tag == "Wall")
        {
            _player.ChangeDirectionRotation();
        }
    }
}
