using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlatformSpawner : MonoBehaviour {
    private ManagerVars _vars;
    private bool _isLeftSpawn = true;
    private Vector3 _startSpawnPos;

    private Vector3 _currentSpawnPos;

    // 可见长度
    private int _showCount = 6;
    private int _groupMaxCount = 8;
    private int _groupMinCount = 3;
    private int _currentGroupCount;

    private void Awake() {
        EventCenter.AddListener(EventType.SpawnNextPlatform, DecidePath);
    }

    private void OnDestroy() {
        EventCenter.RemoveListener(EventType.SpawnNextPlatform, DecidePath);
    }

    void Start() {
        _vars = ManagerVars.GetManagerVars();
        _startSpawnPos = transform.position;
        _currentSpawnPos = _startSpawnPos;

        Instantiate(_vars.normalPlatformPre, _startSpawnPos, Quaternion.identity);
        for (int i = 0; i < _showCount - 1; i++) {
            DecidePath();
        }
    }

    // 生成下一个平台  
    private void DecidePath() {
        if (_currentGroupCount == 0) {
            _currentGroupCount = Random.Range(_groupMinCount, _groupMaxCount);
            _isLeftSpawn = !_isLeftSpawn;
        }

        Spawn();
        _currentGroupCount--;
    }

    // 生成单个平台
    private void Spawn() {
        if (_isLeftSpawn) {
            _currentSpawnPos = new Vector3(_currentSpawnPos.x - _vars.nextXPos, _currentSpawnPos.y + _vars.nextYPos,
                _currentSpawnPos.z + 0.1f);
        }
        else {
            _currentSpawnPos = new Vector3(_currentSpawnPos.x + _vars.nextXPos, _currentSpawnPos.y + _vars.nextYPos,
                _currentSpawnPos.z + 0.1f);
        }

        Instantiate(_vars.normalPlatformPre, _currentSpawnPos, Quaternion.identity);
    }
}