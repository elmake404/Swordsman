using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helmet : SphereHalves
{
    //[SerializeField]
    //private Collider _collider;
    [SerializeField]
    private float _forse;
    public void PushHelmet(Vector3 directonPush)
    {
        //_collider.enabled = true;
        _rbMain.isKinematic = false;
        Push(directonPush,_forse);
    }
}
