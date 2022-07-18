using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
    private Transform _target;
    private Vector2 _v;
    private Vector3 _offset;

    void Update() {
        if (_target is null && GameObject.FindWithTag("Player")) {
            _target = GameObject.FindWithTag("Player").transform;
            _offset = _target.position - transform.position;
        }
    }
    
    private void FixedUpdate() {
        if (_target) {
            Vector3 targetPos = _target.position;
            Vector3 pos = transform.position;
            float newX = Mathf.SmoothDamp(pos.x, targetPos.x - _offset.x, ref _v.x, 0.05f);
            float newY = Mathf.SmoothDamp(pos.y, targetPos.y - _offset.y, ref _v.y, 0.05f);
            if (newY < pos.y) {
                newY = pos.y;
            }
            transform.position = new Vector3(newX, newY, pos.z);

        }
    }
}