using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OverPanel : BasePanel {
    private Text _textScore, _textMaxScore, _textDiamondCount;
    private Button _btnRank, _btnHome, _btnAgain;
    private GameObject _imgNew;

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
        //GameObject
        _imgNew = transform.Find("New").gameObject;
    }

    protected override void ShowThisPanel() {
        _textScore.text = GameManager.Instance.Score.ToString();
        _textMaxScore.text = GameManager.Instance.Data.BestScoreArr[0].ToString();
        _textDiamondCount.text = "+" + GameManager.Instance.Diamond;

        if (GameManager.Instance.Score == GameManager.Instance.Data.BestScoreArr[0]) {
            _imgNew.SetActive(true);
        }
        else {
            _imgNew.SetActive(false);
        }

        base.ShowThisPanel();
    }

    private void OnRankButtonClick() {
        EventCenter.Broadcast(EventType.ShowRankPanel);
    }

    private void OnHomeButtonClick() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnAgainButtonClick() {
        GameTempData.isAgainGame = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}