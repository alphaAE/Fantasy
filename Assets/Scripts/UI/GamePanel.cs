using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePanel : BasePanel {
    private ManagerVars _vars;
    private Text _textScore, _textDiamondCount;
    private Button _btnPause, _btnPlay;

    GamePanel() : base(EventType.ShowGamePanel) { }

    private new void Awake() {
        base.Awake();
        EventCenter.AddListener<int>(EventType.UpdateScoreText, UpdateScoreText);
        EventCenter.AddListener<int>(EventType.UpdateDiamondText, UpdateDiamondText);
    }

    private new void OnDestroy() {
        base.OnDestroy();
        EventCenter.RemoveListener<int>(EventType.UpdateScoreText, UpdateScoreText);
        EventCenter.RemoveListener<int>(EventType.UpdateDiamondText, UpdateDiamondText);
    }

    protected override void Init() {
        _vars = ManagerVars.GetManagerVars();
        //Text
        _textScore = transform.Find("TextScore").GetComponent<Text>();
        _textDiamondCount = transform.Find("Diamond/TextDiamondCount").GetComponent<Text>();
        //Button
        _btnPause = transform.Find("BtnPause").GetComponent<Button>();
        _btnPause.onClick.AddListener(OnPauseButtonClick);
        _btnPlay = transform.Find("BtnPlay").GetComponent<Button>();
        _btnPlay.onClick.AddListener(OnPlayButtonClick);
        _btnPlay.gameObject.SetActive(false);
    }

    private void OnPauseButtonClick() {
        EventCenter.Broadcast(EventType.PlayAudio, _vars.buttonClip);
        _btnPause.gameObject.SetActive(false);
        _btnPlay.gameObject.SetActive(true);
        Time.timeScale = 0;
        GameManager.Instance.IsGamePause = true;
    }

    private void OnPlayButtonClick() {
        EventCenter.Broadcast(EventType.PlayAudio, _vars.buttonClip);
        _btnPlay.gameObject.SetActive(false);
        _btnPause.gameObject.SetActive(true);
        Time.timeScale = 1;
        GameManager.Instance.IsGamePause = false;
    }

    private void UpdateScoreText(int score) {
        _textScore.text = score.ToString();
    }

    private void UpdateDiamondText(int count) {
        _textDiamondCount.text = count.ToString();
    }
}