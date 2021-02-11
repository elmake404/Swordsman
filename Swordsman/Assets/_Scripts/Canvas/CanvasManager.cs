using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    #region StaticComponent
    public static bool IsStartGeme, IsGameFlow,IsWinGame,IsLoseGame;
    public static int QuantityEnemy;
    public static CanvasManager Instance;
    #endregion

    [SerializeField]
    private GameObject _menuUI, _inGameUI, _wimIU, _lostUI;
    [SerializeField]
    private Image _progresBar;
    [SerializeField]
    private Text _namberCoin, _levelNamberCurrent,_levelNamberTarget,_levelnamberWin;

    private float _progress, _addProgress;

    private void Awake()
    {
        Instance = this;
        IsWinGame = false;
        IsLoseGame = false;
    }
    private void Start()
    {
        PlyerLife.PlayerLife.onCoinTake += AddCoin;
        _levelNamberCurrent.text = PlayerPrefs.GetInt("Level").ToString(); 
        _levelNamberTarget.text = (PlayerPrefs.GetInt("Level")+1).ToString();
        _levelnamberWin.text = "Level " + PlayerPrefs.GetInt("Level");
        _namberCoin.text = PlayerPrefs.GetInt("Coin").ToString();
        _addProgress = 1f / QuantityEnemy;

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
            QuantityEnemy = 0;

            _inGameUI.SetActive(false);
            _wimIU.SetActive(true);
        }
        if (!_lostUI.activeSelf && IsLoseGame)
        {
            IsGameFlow = false;
            QuantityEnemy = 0;

            _inGameUI.SetActive(false);
            _lostUI.SetActive(true);
        }
    }
    private void FixedUpdate()
    {
        if (_progress>_progresBar.fillAmount)
        {
            _progresBar.fillAmount += 0.01f;
            if (QuantityEnemy<=0)
            {
                IsWinGame = true;
            }
        }

    }
    private void AddCoin(int namber)
    {
        PlayerPrefs.SetInt("Coin", PlayerPrefs.GetInt("Coin") + namber);
        _namberCoin.text = PlayerPrefs.GetInt("Coin").ToString();
    }
    public void AddProgress()
    {
        QuantityEnemy--;
        _progress += _addProgress;
    }
}
