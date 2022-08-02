using System;
using UnityEngine;

public abstract class BasePanel : MonoBehaviour {
    private EventType _showEvent;

    protected BasePanel(EventType showEvent) {
        _showEvent = showEvent;
    }

    protected void Awake() {
        EventCenter.AddListener(_showEvent, ShowThisPanel);
        Init();
        gameObject.SetActive(false);
    }

    protected void OnDestroy() {
        EventCenter.RemoveListener(_showEvent, ShowThisPanel);
    }

    protected virtual void ShowThisPanel() {
        gameObject.SetActive(true);
    }

    protected abstract void Init();
}