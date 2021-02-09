using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField]
    private Shell _shell;
    [SerializeField]
    private Transform _shootPos;

    [SerializeField]
    private float _timeShoot;

    void Start()
    {
        StartCoroutine(Fire());   
    }

    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Sword")
        {
            Destroy(gameObject);

            //gameObject.layer = 12;
            //Vector3 direction = (transform.position - collision.GetContact(0).point).normalized;

            //foreach (var halves in _sphereHalves)
            //{
            //    halves.gameObject.SetActive(true);
            //    halves.Push(direction, _foresePushHalves);
            //}

            //Destruction();
        }
    }
    //private void Destruction()
    //{
    //    PlayBlood();
    //    Destroy(gameObject);
    //}
    //private void PlayBlood()
    //{
    //    if (_bloodPS != null)
    //    {
    //        _bloodPS.transform.SetParent(null);
    //        _bloodPS.Play();
    //        Destroy(_bloodPS, 1);
    //    }
    //}

    private IEnumerator Fire()
    {
        if (_timeShoot<=0)
        {
            _timeShoot = 1;
        }
        while (true)
        {
            Instantiate(_shell,_shootPos.position,_shootPos.rotation);
            yield return new WaitForSeconds(_timeShoot);
        }
    }
}
