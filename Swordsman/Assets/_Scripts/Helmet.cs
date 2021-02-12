using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helmet : SphereHalves
{
    //[SerializeField]
    //private Collider _collider;
    [SerializeField]
    private GameObject _helemNotRB, _helemRb;
    [SerializeField]
    private float _forse;
    public void PushHelmet(Vector3 directonPush)
    {
        _helemNotRB.SetActive(false);
        _helemRb.SetActive(true);
        //_collider.enabled = true;
        _rbMain.isKinematic = false;
        Push(directonPush,_forse);
    }
}
