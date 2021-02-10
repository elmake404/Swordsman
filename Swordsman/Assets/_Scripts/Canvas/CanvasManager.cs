using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    #region StaticComponent
    public static bool IsStartGeme, IsGameFlow,IsWinGame,IsLoseGame;
    #endregion

    [SerializeField]
    private GameObject _menuUI, _inGameUI, _wimIU, _lostUI;
    [SerializeField]
    private Image _progresBar;
    [SerializeField]
    private Text _NamberCoin;

    private void Awake()
    {
        IsWinGame = false;
        IsLoseGame = false;
    }
    private void Start()
    {
        PlyerLife.PlayerLife.onCoinTake += AddCoin;
        _NamberCoin.text = PlayerPrefs.GetInt("Coin").ToString(); 
        if (!IsStartGeme)
        {
            _menuUI.SetActive(true);
        }
        else
        {
            IsGameFlow = true;
        }
    }

    private void Update()
    {
        if (!_inGameUI.activeSelf && IsStartGeme)
        {
            _menuUI.SetActive(false);
            _inGameUI.SetActive(true);
        }
        if (!_wimIU.activeSelf&& IsWinGame)
        {
            IsGameFlow = false;

            _inGameUI.SetActive(false);
            _wimIU.SetActive(true);
        }
        if (!_lostUI.activeSelf && IsLoseGame)
        {
            IsGameFlow = false;

            _inGameUI.SetActive(false);
            _lostUI.SetActive(true);
        }
    }
    private void AddCoin(int namber)
    {
        PlayerPrefs.SetInt("Coin", PlayerPrefs.GetInt("Coin") + namber);
        _NamberCoin.text = PlayerPrefs.GetInt("Coin").ToString();
    }
}
