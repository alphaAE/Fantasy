using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Player : MonoBehaviour {
    private Transform _currentPlatform;
    private bool _isMoving = true;
    private ManagerVars _vars;

    void Start() {
        _vars = ManagerVars.GetManagerVars();
    }

    void Update() {
        if (!GameManager.Instance.IsGameStarted) {
            return;
        }

        if (Input.GetMouseButtonDown(0)) {
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
    }

    private void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.CompareTag("Platform")) {
            _currentPlatform = col.gameObject.transform;
            _isMoving = false;
        }
    }

    private void Jump(bool isLeft = false) {
        if (_isMoving) {
            return;
        }

        EventCenter.Broadcast(EventType.SpawnNextPlatform);
        _isMoving = true;
        Vector3 currentPlatformPos = _currentPlatform.position;
        if (isLeft) {
            transform.localScale = new Vector3(-1, 1, 1);
            transform.DOMoveX(currentPlatformPos.x - _vars.nextXPos, 0.2f);
            transform.DOMoveY(currentPlatformPos.y + _vars.nextYPos + 0.8f, 0.15f);
        }
        else {
            transform.localScale = new Vector3(1, 1, 1);
            transform.DOMoveX(currentPlatformPos.x + _vars.nextXPos, 0.2f);
            transform.DOMoveY(currentPlatformPos.y + _vars.nextYPos + 0.8f, 0.15f);
        }
    }
}