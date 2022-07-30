using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OverPanel : MonoBehaviour {
    private Text _textScore, _textMaxScore, _textDiamondCount;

    private Button _btnRank, _btnHome, _btnAgain;

    private void Awake() {
        Init();
        EventCenter.AddListener(EventType.GameOver, ShowGameOverPanel);
    }

    private void OnDestroy() {
        EventCenter.RemoveListener(EventType.GameOver, ShowGameOverPanel);
    }

    private void Init() {
        _textScore = transform.Find("TextScore").GetComponent<Text>();
        _textMaxScore = transform.Find("MaxScore/TextMaxScore").GetComponent<Text>();
        _textDiamondCount = transform.Find("Diamond/TextDiamondCount").GetComponent<Text>();

        _btnRank = transform.Find("Btns/BtnRank").GetComponent<Button>();
        _btnRank.onClick.AddListener(OnRankButtonClick);
        _btnHome = transform.Find("Btns/BtnHome").GetComponent<Button>();
        _btnHome.onClick.AddListener(OnHomeButtonClick);
        _btnAgain = transform.Find("BtnAgain").GetComponent<Button>();
        _btnAgain.onClick.AddListener(OnAgainButtonClick);

        gameObject.SetActive(false);
    }

    private void ShowGameOverPanel() {
        _textScore.text = GameManager.Instance.Score.ToString();
        _textMaxScore.text = GameManager.Instance.Score.ToString();
        _textDiamondCount.text = "+" + 0;
        gameObject.SetActive(true);
    }

    private void OnRankButtonClick() { }

    private void OnHomeButtonClick() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void OnAgainButtonClick() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}