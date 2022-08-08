using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LongClickButton : Button {
    [Serializable]
    public class LongButtonEvent : UnityEvent { }

    [SerializeField] private LongButtonEvent _onLongButtonClick = new LongButtonEvent();

    public LongButtonEvent OnLongButtonClick {
        get { return _onLongButtonClick; }

        set { _onLongButtonClick = value; }
    }

    private DateTime m_FirstTime;
    private DateTime m_SecondTime;

    void ResetTime() {
        m_FirstTime = default(DateTime);
        m_SecondTime = default(DateTime);
    }

    void Press() {
        if (OnLongButtonClick != null)
            OnLongButtonClick.Invoke();
        else
            ResetTime();
    }

    public override void OnPointerDown(PointerEventData eventData) {
        base.OnPointerDown(eventData);
        if (m_FirstTime.Equals(default(DateTime))) {
            m_FirstTime = DateTime.Now;
        }
    }

    public override void OnPointerUp(PointerEventData eventData) {
        base.OnPointerUp(eventData);
        if (!m_FirstTime.Equals(default(DateTime))) {
            m_SecondTime = DateTime.Now;
        }

        if (!m_FirstTime.Equals(default(DateTime)) && !m_SecondTime.Equals(default(DateTime))) {
            var intervalTime = m_SecondTime - m_FirstTime;
            float milliTime = intervalTime.Seconds * 1000 + intervalTime.Milliseconds; //毫秒
            if (milliTime > 600) {
                Press();
            }
            else
                ResetTime();
        }
    }

    public override void OnPointerExit(PointerEventData eventData) {
        base.OnPointerExit(eventData);
        ResetTime();
    }
}