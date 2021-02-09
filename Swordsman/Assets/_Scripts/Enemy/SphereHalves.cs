using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereHalves : MonoBehaviour
{
    [SerializeField]
    private Rigidbody _rbMain;
    [SerializeField]
    private float _timeBeforeDestroy =2;

    public void Push(Vector3 pushDirection, float forse)
    {
        transform.SetParent(null);
        _rbMain.constraints = RigidbodyConstraints.None;
        _rbMain.AddForce(pushDirection * forse);
        Destroy(gameObject, _timeBeforeDestroy);
    }
}
