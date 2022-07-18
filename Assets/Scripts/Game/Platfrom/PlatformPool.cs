using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlatformPool : MonoBehaviour {
    public static PlatformPool Instance;
    private ManagerVars _vars;

    public int defaultCount = 1;

    // 主题字典
    private Dictionary<PlatfromTheme, GameObject> _platformDict = new();
    private Dictionary<PlatfromTheme, List<GameObject>> _obstacleDict = new();

    // 池子
    private Dictionary<PlatfromTheme, List<GameObject>> _platformPoolDict = new();
    private Dictionary<PlatfromTheme, List<GameObject>> _obstaclePoolDict = new();

    private void Awake() {
        Instance = this;
        _vars = ManagerVars.GetManagerVars();
        Init();
    }

    private void Init() {
        //构造主题字典
        InitWithTheme(PlatfromTheme.Normal, _vars.normalPlatformPre, _vars.normalObstaclePres);
        InitWithTheme(PlatfromTheme.Ice, _vars.icePlatformPre, _vars.iceObstaclePres);
        InitWithTheme(PlatfromTheme.Grass, _vars.grassPlatformPre, _vars.grassObstaclePres);
        InitWithTheme(PlatfromTheme.Fire, _vars.firePlatformPre, _vars.fireObstaclePres);
    }

    private void InitWithTheme(PlatfromTheme theme, GameObject platform, List<GameObject> obstacles) {
        _platformDict.Add(theme, platform);
        _obstacleDict.Add(theme, obstacles);
        List<GameObject> platformPool = new();
        List<GameObject> obstaclePool = new();
        for (int i = 0; i < defaultCount; i++) {
            var platformObj = Instantiate(platform);
            platformObj.SetActive(false);
            platformPool.Add(platformObj);

            var rdIndex = Random.Range(0, obstacles.Count);
            var obstacleObj = Instantiate(obstacles[rdIndex]);
            obstacleObj.SetActive(false);
            obstaclePool.Add(obstacleObj);
        }

        _platformPoolDict.Add(theme, platformPool);
        _obstaclePoolDict.Add(theme, obstaclePool);
    }

    public GameObject GetPlatformByTheme(PlatfromTheme theme) {
        var pool = _platformPoolDict[theme];
        foreach (var item in pool) {
            if (!item.activeSelf) {
                item.SetActive(true);
                return item;
            }
        }

        var platformObj = Instantiate(_platformDict[theme]);
        // platformObj.SetActive(false);
        pool.Add(platformObj);

        return platformObj;
    }

    public GameObject GetObstacleByTheme(PlatfromTheme theme) {
        var pool = _obstaclePoolDict[theme];
        foreach (var item in pool) {
            if (!item.activeSelf) {
                item.SetActive(true);
                return item;
            }
        }

        var rdIndex = Random.Range(0, _obstacleDict[theme].Count);
        var obstacleObj = Instantiate(_obstacleDict[theme][rdIndex]);
        // obstacleObj.SetActive(false);
        pool.Add(obstacleObj);

        return obstacleObj;
    }
}