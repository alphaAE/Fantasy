using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OverPanel : BasePanel {
    private Text _textScore, _textMaxScore, _textDiamondCount;
    private Button _btnRank, _btnHome, _btnAgain;

    OverPanel() : base(EventType.ShowOverPanel) { }

    protected override void Init() {
        //Text
        _textScore = transform.Find("TextScore").GetComponent<Text>();
        _textMaxScore = transform.Find("MaxScore/TextMaxScore").GetComponent<Text>();
        _textDiamondCount = transform.Find("Diamond/TextDiamondCount").GetComponent<Text>();
        //Button
        _btnRank = transform.Find("Btns/BtnRank").GetComponent<Button>();
        _btnRank.onClick.AddListener(OnRankButtonClick);
        _btnHome = transform.Find("Btns/BtnHome").GetComponent<Button>();
        _btnHome.onClick.AddListener(OnHomeButtonClick);
        _btnAgain = transform.Find("BtnAgain").GetComponent<Button>();
        _btnAgain.onClick.AddListener(OnAgainButtonClick);
    }

    protected override void ShowThisPanel() {
        _textScore.text = GameManager.Instance.Score.ToString();
        _textMaxScore.text = GameManager.Instance.Score.ToString();
        _textDiamondCount.text = "+" + GameManager.Instance.Diamond;
        base.ShowThisPanel();
    }

    private void OnRankButtonClick() { }

    private void OnHomeButtonClick() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void OnAgainButtonClick() {
        GameTempData.isAgainGame = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}