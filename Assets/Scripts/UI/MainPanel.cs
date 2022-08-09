using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainPanel : BasePanel {
    private ManagerVars _vars;
    private Button _btnStart, _btnShop, _btnRank;
    private LongClickButton _lBtnSound;
    MainPanel() : base(EventType.ShowMainPanel) { }

    private new void Awake() {
        base.Awake();
        EventCenter.AddListener<int>(EventType.SelectSkin, SkinChange);
    }

    private new void OnDestroy() {
        base.OnDestroy();
        EventCenter.RemoveListener<int>(EventType.SelectSkin, SkinChange);
    }

    protected override void Init() {
        _vars = ManagerVars.GetManagerVars();
        //Button
        _btnStart = transform.Find("BtnStart").GetComponent<Button>();
        _btnStart.onClick.AddListener(OnStartButtonClick);
        _btnShop = transform.Find("Btns/BtnShop").GetComponent<Button>();
        _btnShop.onClick.AddListener(OnShopButtonClick);
        _btnRank = transform.Find("Btns/BtnRank").GetComponent<Button>();
        _btnRank.onClick.AddListener(OnRankButtonClick);
        _lBtnSound = transform.Find("Btns/BtnSound").GetComponent<LongClickButton>();
        _lBtnSound.onClick.AddListener(OnSoundButtonClick);
        _lBtnSound.OnLongButtonClick.AddListener(OnSoundLongButtonClick);

        _btnShop.transform.GetChild(0).GetComponent<Image>().sprite =
            _vars.skinSprites[GameManager.Instance.Data.SelectSkin];
        _lBtnSound.transform.GetChild(0).GetComponent<Image>().sprite =
            GameManager.Instance.Data.IsMusicOn ? _vars.soundOn : _vars.soundOff;
    }

    private void SkinChange(int index) {
        _btnShop.transform.GetChild(0).GetComponent<Image>().sprite = _vars.skinSprites[index];
    }

    private void OnStartButtonClick() {
        EventCenter.Broadcast(EventType.PlayAudio, _vars.buttonClip);
        EventCenter.Broadcast(EventType.ShowGamePanel);
        gameObject.SetActive(false);
        GameManager.Instance.IsGameStarted = true;
    }

    private void OnShopButtonClick() {
        EventCenter.Broadcast(EventType.PlayAudio, _vars.buttonClip);
        EventCenter.Broadcast(EventType.ShowShopPanel);
    }

    private void OnRankButtonClick() {
        EventCenter.Broadcast(EventType.PlayAudio, _vars.buttonClip);
        EventCenter.Broadcast(EventType.ShowRankPanel);
    }

    private void OnSoundButtonClick() {
        EventCenter.Broadcast(EventType.PlayAudio, _vars.buttonClip);
        EventCenter.Broadcast(EventType.SetAudio, !GameManager.Instance.Data.IsMusicOn);
        _lBtnSound.transform.GetChild(0).GetComponent<Image>().sprite =
            GameManager.Instance.Data.IsMusicOn ? _vars.soundOn : _vars.soundOff;
    }

    private void OnSoundLongButtonClick() {
        EventCenter.Broadcast(EventType.ResetGame);
    }
}