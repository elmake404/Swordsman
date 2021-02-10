using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Delimiter : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem _bloodPS;

    [SerializeField]
    private SphereHalves[] _sphereHalves;

    [SerializeField]
    private float _foresePush;

    public void Separation(Vector3 PointContact)
    {
        gameObject.layer = 12;
        Vector3 direction = (transform.position - PointContact).normalized;

        foreach (var halves in _sphereHalves)
        {
            halves.gameObject.SetActive(true);
            halves.Push(direction, _foresePush+Random.Range(0,100));
        }

        Destruction();

    }
    private void Destruction()
    {
        PlayBlood();
        Destroy(gameObject);
    }
    private void PlayBlood()
    {
        if (_bloodPS != null)
        {
            _bloodPS.transform.SetParent(null);
            _bloodPS.Play();
            Destroy(_bloodPS, 1);
        }
    }


}
