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

    // 主题相关
    private PlatfromTheme _theme;
    private int _nextThemeCount;

    // 障碍相关
    private bool _generateObs = false;
    private List<NextObstacleInfo> _nextObstacleInfos = new();

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

        // 生成初始平台
        RandomTheme();
        var obj = PlatformPool.Instance.GetPlatformByTheme(_theme);
        obj.transform.position = _startSpawnPos;
        for (int i = 0; i < _showCount - 1; i++) {
            DecidePath();
        }

        // 初始平台生成后开始生成障碍
        _generateObs = true;
    }

    // 生成下一个平台  
    private void DecidePath() {
        bool isStartGroup = false;
        if (_currentGroupCount == 0) {
            _currentGroupCount = Random.Range(_groupMinCount, _groupMaxCount);
            _isLeftSpawn = !_isLeftSpawn;
            isStartGroup = true;
        }

        if (_nextThemeCount == 0) {
            RandomTheme();
        }

        SpawnPlatform();
        SpawnObstacleLater();
        if (_generateObs && !isStartGroup && _currentGroupCount > 1 && Random.Range(1, 101) > 70) {
            SpawnObstacle();
        }


        _currentGroupCount--;
        _nextThemeCount--;
    }

    // 生成单个平台
    private void SpawnPlatform() {
        if (_isLeftSpawn) {
            _currentSpawnPos = new Vector3(_currentSpawnPos.x - _vars.nextXPos, _currentSpawnPos.y + _vars.nextYPos,
                _currentSpawnPos.z + 1f);
        }
        else {
            _currentSpawnPos = new Vector3(_currentSpawnPos.x + _vars.nextXPos, _currentSpawnPos.y + _vars.nextYPos,
                _currentSpawnPos.z + 1f);
        }

        var obj = PlatformPool.Instance.GetPlatformByTheme(_theme);
        obj.transform.position = _currentSpawnPos;

        // 概率生成钻石
        if (Random.Range(1, 101) < 20) {
            var pos = new Vector3(_currentSpawnPos.x, _currentSpawnPos.y + 0.44f, _currentSpawnPos.z);
            Instantiate(_vars.diamondPre, pos, Quaternion.identity);
        }
    }

    // 生成障碍平台
    private void SpawnObstacle() {
        Vector3 obstaclePos;
        if (!_isLeftSpawn) {
            obstaclePos = new Vector3(_currentSpawnPos.x - _vars.nextXPos, _currentSpawnPos.y + _vars.nextYPos,
                _currentSpawnPos.z + 1f);
        }
        else {
            obstaclePos = new Vector3(_currentSpawnPos.x + _vars.nextXPos, _currentSpawnPos.y + _vars.nextYPos,
                _currentSpawnPos.z + 1f);
        }

        var obj = PlatformPool.Instance.GetObstacleByTheme(_theme);
        obj.transform.position = obstaclePos;

        _nextObstacleInfos.Add(new(obstaclePos, Random.Range(0, 6), !_isLeftSpawn));
    }

    // 生成障碍后续假平台
    private void SpawnObstacleLater() {
        List<NextObstacleInfo> copy = new(_nextObstacleInfos);
        _nextObstacleInfos.Clear();

        foreach (var item in copy) {
            NextObstacleInfo info = new();
            info.isLeft = item.isLeft;
            info.count = item.count - 1;
            if (info.isLeft) {
                info.pos = new Vector3(item.pos.x - _vars.nextXPos, item.pos.y + _vars.nextYPos, item.pos.z + 1f);
            }
            else {
                info.pos = new Vector3(item.pos.x + _vars.nextXPos, item.pos.y + _vars.nextYPos, item.pos.z + 1f);
            }

            var obj = PlatformPool.Instance.GetPlatformByTheme(_theme);
            obj.GetComponent<BoxCollider2D>().enabled = false;
            obj.transform.position = info.pos;
            if (info.count > 0) {
                _nextObstacleInfos.Add(info);
            }
        }
    }

    private void RandomTheme() {
        _theme = (PlatfromTheme)Random.Range(0, 4);
        _nextThemeCount = Random.Range(20, 50);
    }
}