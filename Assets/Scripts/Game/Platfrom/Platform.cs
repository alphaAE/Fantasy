using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour {
    private BoxCollider2D _boxCollider2D;
    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rigidbody2d;

    private float _fallTime;
    private bool _isFall;

    private void Awake() {
        _boxCollider2D = GetComponent<BoxCollider2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidbody2d = GetComponent<Rigidbody2D>();
    }

    private void OnEnable() {
        _fallTime = GameManager.Instance.FallTime;
        _isFall = false;
        ResetFromPool();
    }

    private void Update() {
        if (GameManager.Instance.IsGameOver || GameManager.Instance.IsGamePause ||
            !GameManager.Instance.IsGameStarted) {
            return;
        }

        if (!_isFall) {
            _fallTime -= Time.deltaTime;
            if (_fallTime <= 0) {
                ToFall();
                _isFall = true;
            }
        }
    }

    public void ResetFromPool() {
        _boxCollider2D.enabled = true;
        _spriteRenderer.sortingLayerName = "Platform";
        _rigidbody2d.bodyType = RigidbodyType2D.Static;
    }

    // 转变为跳跃后的平台
    public void ToOldPlatform() {
        _boxCollider2D.enabled = false;
    }

    public void ToFall() {
        _rigidbody2d.bodyType = RigidbodyType2D.Dynamic;
        StartCoroutine(ToHide());
    }

    private IEnumerator ToHide() {
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
    }
}