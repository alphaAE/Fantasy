using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankPanel : BasePanel {
    private Text _textNo1, _textNo2, _textNo3;
    private Button _btnBg;
    public RankPanel() : base(EventType.ShowRankPanel) { }

    protected override void Init() {
        //Text
        _textNo1 = transform.Find("Dialog/Item1").GetComponentInChildren<Text>();
        _textNo2 = transform.Find("Dialog/Item2").GetComponentInChildren<Text>();
        _textNo3 = transform.Find("Dialog/Item3").GetComponentInChildren<Text>();
        //Button
        _btnBg = transform.Find("Bg").GetComponent<Button>();
        _btnBg.onClick.AddListener(OnBgButtonClick);
    }

    protected override void ShowThisPanel() {
        base.ShowThisPanel();
        _textNo1.text = GameManager.Instance.Data.BestScoreArr[0].ToString();
        _textNo2.text = GameManager.Instance.Data.BestScoreArr[1].ToString();
        _textNo3.text = GameManager.Instance.Data.BestScoreArr[2].ToString();
    }

    private void OnBgButtonClick() {
        gameObject.SetActive(false);
    }
}