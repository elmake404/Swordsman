using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    #region StaticComponent
    public static bool IsStartGeme, IsGameFlow, IsWinGame, IsLoseGame;
    public static int QuantityEnemy;
    public static CanvasManager Instance;
    #endregion

    [SerializeField]
    private GameObject _menuUI, _inGameUI, _wimIU, _lostUI;
    [SerializeField]
    private Image _progresBar, _rageBar, _face;
    [SerializeField]
    private Text _namberCoin, _levelNamberCurrent, _levelNamberTarget, _levelnamberWin;
    [SerializeField]
    private Color _faceRageColor;

    private float _progress, _addProgress, _rage;

    private void Awake()
    {
        Instance = this;
        IsWinGame = false;
        IsLoseGame = false;
    }
    private void Start()
    {
        if (PlayerPrefs.GetInt("Level") <= 0)
        {
            PlayerPrefs.SetInt("Level", 1);
        }

        PlyerLife.PlayerLife.onCoinTake += AddCoin;
        _addProgress = 1f / QuantityEnemy;
        TextLevel();

        if (!IsStartGeme)
        {
            FacebookManager.Instance.GameStart();
            _menuUI.SetActive(true);
        }
        else
        {
            FacebookManager.Instance.LevelStart(PlayerPrefs.GetInt("Level"));
            IsGameFlow = true;
        }
    }

    private void Update()
    {

        if (!_inGameUI.activeSelf && IsStartGeme && IsGameFlow)
        {
            _menuUI.SetActive(false);
            _inGameUI.SetActive(true);
        }
        if (!_wimIU.activeSelf && IsWinGame)
        {
            IsGameFlow = false;
            QuantityEnemy = 0;
            FacebookManager.Instance.LevelWin(PlayerPrefs.GetInt("Level"));

            PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level") + 1);
            PlayerPrefs.SetInt("Scenes", PlayerPrefs.GetInt("Scenes") + 1);
            _inGameUI.SetActive(false);
            _wimIU.SetActive(true);
        }
        if (!_lostUI.activeSelf && IsLoseGame)
        {
            IsGameFlow = false;
            QuantityEnemy = 0;
            FacebookManager.Instance.LevelFail(PlayerPrefs.GetInt("Level"));

            _inGameUI.SetActive(false);
            _lostUI.SetActive(true);
        }
    }
    private void FixedUpdate()
    {
        if (_progress > _progresBar.fillAmount)
        {
            _progresBar.fillAmount += 0.01f;
            if (QuantityEnemy <= 0)
            {
                if (!IsLoseGame)
                    IsWinGame = true;
            }
        }
        _rageBar.fillAmount = Mathf.Lerp(_rageBar.fillAmount, _rage, 0.1f);
    }
    private void AddCoin(int namber)
    {
        PlayerPrefs.SetInt("Coin", PlayerPrefs.GetInt("Coin") + namber);
        _namberCoin.text = PlayerPrefs.GetInt("Coin").ToString();
    }
    private void TextLevel()
    {
        _levelNamberCurrent.text = PlayerPrefs.GetInt("Level").ToString();
        _levelNamberTarget.text = (PlayerPrefs.GetInt("Level") + 1).ToString();
        _levelnamberWin.text = "Level " + PlayerPrefs.GetInt("Level");
        _namberCoin.text = PlayerPrefs.GetInt("Coin").ToString();
    }
    public void Rage(float namber)
    {
        _rage = namber;
        if (_rage == 1 && _face.color != _faceRageColor)
        {
            _face.color = _faceRageColor;
        }
        else if (_rage < 1 && _face.color != Color.white)
        {
            _face.color = Color.white;

        }
    }
    public void AddProgress()
    {
        QuantityEnemy--;
        _progress += _addProgress;
    }
}
