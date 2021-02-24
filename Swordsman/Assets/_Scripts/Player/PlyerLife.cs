using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlyerLife : MonoBehaviour
{
    public static PlyerLife PlayerLife;

    private List<GameObject> _ether = new List<GameObject>();
    [SerializeField]
    private MeshRenderer[] _meshes;
    [SerializeField]
    private Material _rageMaterialPlayer, _rageMaterialSword;
    private Material _materialPlayer, _materialSword;

    [SerializeField]
    private int _health;
    [SerializeField]
    private float _timeInvulnerability, _blinkRate;
    private bool _isInvulnerability = false;

    public delegate void AddCoin(int namber);
    public event AddCoin onCoinTake;

    public delegate void LossOfLife();
    public event LossOfLife lossOfLife;

    private void Awake()
    {
        _materialPlayer = _meshes[0].material;
        _materialSword = _meshes[1].material;
        PlayerLife = this;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Delimiter>() != null && !_isInvulnerability)
        {
            if (_health > 0)
            {
                lossOfLife?.Invoke();
                StartCoroutine(TemporaryImmortality());
            }
            else
            {
                if (!CanvasManager.IsWinGame)
                    CanvasManager.IsLoseGame = true;

                Destroy(transform.parent.gameObject);
            }
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
    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.gameObject.layer == 9)
    //    {
    //        _ether.Remove(other.gameObject);
    //        if (_ether.Count <= 0 && !CanvasManager.IsWinGame)
    //            CanvasManager.IsLoseGame = true;
    //    }
    //}
    private IEnumerator TemporaryImmortality()
    {
        _isInvulnerability = true;
        _health--;
        float time = _timeInvulnerability;
        while (true)
        {
            for (int i = 0; i < _meshes.Length; i++)
            {
                _meshes[i].enabled = false;
            }

            yield return new WaitForSeconds(_blinkRate/2);

            for (int i = 0; i < _meshes.Length; i++)
            {
                _meshes[i].enabled = true;
            }

            yield return new WaitForSeconds(_blinkRate/2);

            time -= _blinkRate;

            if (time <= 0)
                break;
        }
        _isInvulnerability = false;
    }
    public void ActivationRage()
    {
        if (_rageMaterialPlayer!= _meshes[0].material)
        {
            _meshes[0].material = _rageMaterialPlayer;
            _meshes[1].material = _rageMaterialSword;
        }
    }
    public void DeactivationRage()
    {
        if (_rageMaterialPlayer!= _meshes[0].material)
        {
            _meshes[0].material = _materialPlayer;
            _meshes[1].material = _materialSword;
        }
    }

}
