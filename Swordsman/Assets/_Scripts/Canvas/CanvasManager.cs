using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    #region StaticComponent
    public static bool IsStartGeme, IsGameFlow,IsWinGame,IsLoseGame;
    #endregion

    [SerializeField]
    private GameObject _menuUI, _inGameUI, _wimIU, _lostUI;
    private void Awake()
    {
        IsWinGame = false;
        IsLoseGame = false;
    }
    private void Start()
    {
        if (!IsStartGeme)
        {
            _menuUI.SetActive(true);
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
            _inGameUI.SetActive(false);
            _wimIU.SetActive(true);
        }
        if (!_lostUI.activeSelf && IsLoseGame)
        {
            _inGameUI.SetActive(false);
            _lostUI.SetActive(true);
        }
    }
}
