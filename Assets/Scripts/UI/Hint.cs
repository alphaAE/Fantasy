using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Hint : MonoBehaviour {
    private Text _text;
    private Vector3 _startPos = new Vector3(0, 0);

    private void Awake() {
        EventCenter.AddListener<String>(EventType.ShowHint, Show);
        Init();
        gameObject.SetActive(false);
    }

    private void OnDestroy() {
        EventCenter.RemoveListener<String>(EventType.ShowHint, Show);
    }


    private void OnEnable() {
        transform.localPosition = new Vector3(_startPos.x, _startPos.y - 100);
        transform.DOLocalMove(_startPos, 1f).OnComplete(() => { gameObject.SetActive(false); });
    }


    private void Init() {
        _text = GetComponentInChildren<Text>();
    }

    private void Show(String content) {
        _text.text = content;
        gameObject.SetActive(true);
    }
}