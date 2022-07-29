using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePanel : MonoBehaviour {
    private Button _btnPause, _btnPlay;
    private Text _textScore, _textDiamondCount;

    private void Awake() {
        EventCenter.AddListener(EventType.ShowGamePanel, ShowThisPanel);
        EventCenter.AddListener<int>(EventType.UpdateScoreText, UpdateScoreText);
        Init();
    }

    private void OnDestroy() {
        EventCenter.RemoveListener(EventType.ShowGamePanel, ShowThisPanel);
        EventCenter.RemoveListener<int>(EventType.ShowGamePanel, UpdateScoreText);
    }

    private void Init() {
        _btnPause = transform.Find("BtnPause").GetComponent<Button>();
        _btnPause.onClick.AddListener(OnPauseButtonClick);
        _btnPlay = transform.Find("BtnPlay").GetComponent<Button>();
        _btnPlay.onClick.AddListener(OnPlayButtonClick);
        _btnPlay.gameObject.SetActive(false);

        _textScore = transform.Find("TextScore").GetComponent<Text>();
        _textDiamondCount = transform.Find("Diamond/TextDiamondCount").GetComponent<Text>();

        gameObject.SetActive(false);
    }

    private void ShowThisPanel() {
        gameObject.SetActive(true);
    }

    private void OnPauseButtonClick() {
        _btnPause.gameObject.SetActive(false);
        _btnPlay.gameObject.SetActive(true);
        Time.timeScale = 0;
        GameManager.Instance.IsGamePause = true;
    }

    private void OnPlayButtonClick() {
        _btnPlay.gameObject.SetActive(false);
        _btnPause.gameObject.SetActive(true);
        Time.timeScale = 1;
        GameManager.Instance.IsGamePause = false;
    }

    private void UpdateScoreText(int score) {
        _textScore.text = score.ToString();
    }
}