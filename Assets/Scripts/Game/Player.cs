using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour {
    private Transform _currentPlatform;
    private bool _isMoving = true;
    private ManagerVars _vars;
    private SpriteRenderer _spriteRenderer;
    private BoxCollider2D _boxCollider2D;
    private CircleCollider2D _circleCollider2D;

    private void Awake() {
        EventCenter.AddListener<int>(EventType.SelectSkin, SkinChange);
    }

    private void OnDestroy() {
        EventCenter.RemoveListener<int>(EventType.SelectSkin, SkinChange);
    }

    void Start() {
        _vars = ManagerVars.GetManagerVars();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _boxCollider2D = GetComponent<BoxCollider2D>();
        _circleCollider2D = GetComponent<CircleCollider2D>();

        _spriteRenderer.sprite = _vars.inGameSkinSprites[GameManager.Instance.Data.SelectSkin];
    }

    void Update() {
        if (!GameManager.Instance.IsGameStarted || GameManager.Instance.IsGamePause) {
            return;
        }

        if (Input.GetMouseButtonDown(0) && !IsPointerOverGameObject(Input.mousePosition)) {
            Vector3 mousePos = Input.mousePosition;
            if (mousePos.x < (float)Screen.width / 2) {
                //向左跳跃
                Jump(true);
            }
            else {
                //向右跳跃
                Jump();
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            Jump(true);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow)) {
            Jump();
        }
    }

    private void OnTriggerEnter2D(Collider2D col) {
        if (GameManager.Instance.IsGameOver) {
            return;
        }

        if (col.gameObject.CompareTag("Platform")) {
            if (_currentPlatform && col.gameObject.transform != _currentPlatform) {
                _currentPlatform.GetComponent<Platform>().ToOldPlatform();
                // var pos = transform.position;
                // transform.position = new Vector3(pos.x, pos.y,
                //     _currentPlatform.position.z - 0.1f);
                EventCenter.Broadcast(EventType.AddScore);
            }

            _currentPlatform = col.gameObject.transform;
            _isMoving = false;
        }
        else if (col.gameObject.CompareTag("Pickup")) {
            Destroy(col.gameObject);
            Instantiate(_vars.pickupEffect, col.gameObject.transform.position, Quaternion.identity);
            EventCenter.Broadcast(EventType.AddDiamond);
            EventCenter.Broadcast(EventType.PlayAudio, _vars.diamondClip);
        }
        else if (col.gameObject.CompareTag("Obstacle")) {
            EventCenter.Broadcast(EventType.PlayAudio, _vars.hitClip);
            Dead();
        }
        else if (col.gameObject.CompareTag("DeadZone")) {
            EventCenter.Broadcast(EventType.PlayAudio, _vars.fallClip);
            Dead();
        }
    }

    private void SkinChange(int index) {
        _spriteRenderer.sprite = _vars.inGameSkinSprites[index];
    }

    private void Jump(bool isLeft = false) {
        if (_isMoving) {
            return;
        }


        EventCenter.Broadcast(EventType.PlayAudio, _vars.jumpClip);
        EventCenter.Broadcast(EventType.SpawnNextPlatform);
        _isMoving = true;
        Vector3 currentPlatformPos = _currentPlatform.position;
        if (isLeft) {
            transform.localScale = new Vector3(-1, 1, 1);
            transform.DOMoveX(currentPlatformPos.x - _vars.nextXPos, 0.2f);
            transform.DOMoveY(currentPlatformPos.y + _vars.nextYPos + 0.4f, 0.15f);
            transform.DOMoveZ(currentPlatformPos.z + 1f - 0.5f, 0.15f);
        }
        else {
            transform.localScale = new Vector3(1, 1, 1);
            transform.DOMoveX(currentPlatformPos.x + _vars.nextXPos, 0.2f);
            transform.DOMoveY(currentPlatformPos.y + _vars.nextYPos + 0.4f, 0.15f);
            transform.DOMoveZ(currentPlatformPos.z + 1f - 0.5f, 0.15f);

        }
    }

    private void Dead() {
        GameManager.Instance.IsGameOver = true;
        Debug.Log("GameOver");
        EventCenter.Broadcast(EventType.GameOver);
        Instantiate(_vars.deathEffect, transform.position, Quaternion.identity);
        StartCoroutine(ShowGameOverPanel());
        _spriteRenderer.color = new Color(0, 0, 0, 0);
        _circleCollider2D.enabled = false;
        _boxCollider2D.enabled = false;
    }

    private IEnumerator ShowGameOverPanel() {
        yield return new WaitForSeconds(1f);
        EventCenter.Broadcast(EventType.ShowOverPanel);
        Destroy(gameObject);
    }

    //判断是否点击到UI （兼容移动平台）
    private bool IsPointerOverGameObject(Vector2 mousePosition) {
        //创建点击事件
        PointerEventData eventData = new(EventSystem.current);
        eventData.position = mousePosition;
        List<RaycastResult> raycastResults = new();
        //向点击位置发射射线
        EventSystem.current.RaycastAll(eventData, raycastResults);
        return raycastResults.Count > 0;
    }
}