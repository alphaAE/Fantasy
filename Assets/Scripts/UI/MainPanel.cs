using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainPanel : MonoBehaviour {
    private Button _btnStart, _btnShop, _btnRank, _btnSound;

    private void Awake() {
        Init();
    }

    private void Init() {
        _btnStart = transform.Find("BtnStart").GetComponent<Button>();
        _btnStart.onClick.AddListener(OnStartButtonClick);
        _btnShop = transform.Find("Btns/BtnShop").GetComponent<Button>();
        _btnShop.onClick.AddListener(OnShopButtonClick);
        _btnRank = transform.Find("Btns/BtnRank").GetComponent<Button>();
        _btnRank.onClick.AddListener(OnRankButtonClick);
        _btnSound = transform.Find("Btns/BtnSound").GetComponent<Button>();
        _btnSound.onClick.AddListener(OnSoundButtonClick);
    }

    private void OnStartButtonClick() {
        EventCenter.Broadcast(EventType.ShowGamePanel);
        gameObject.SetActive(false);
        GameManager.Instance.IsGameStarted = true;
    }

    private void OnShopButtonClick() { }
    private void OnRankButtonClick() { }
    private void OnSoundButtonClick() { }
}