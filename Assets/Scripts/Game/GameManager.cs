using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public static GameManager Instance;
    private ManagerVars _vars;

    public bool IsGameStarted { get; set; }
    public bool IsGamePause { get; set; }
    public bool IsGameOver { get; set; }

    public float FallTime { get; set; }
    public int Score { get; set; }
    public int Diamond { get; set; }

    public GameData Data { get; set; }

    private void Awake() {
        Instance = this;
        _vars = ManagerVars.GetManagerVars();
        FallTime = _vars.defaultPlatformFallTime;
        EventCenter.AddListener(EventType.AddScore, AddScore);
        EventCenter.AddListener(EventType.AddDiamond, AddDiamond);
        EventCenter.AddListener(EventType.ResetGame, ResetGame);
        EventCenter.AddListener<int>(EventType.SelectSkin, SelectSkin);

        //GameData
        LoadData();
        if (Data is null) {
            InitGameData();
            SaveData();
        }
    }

    private void OnDestroy() {
        EventCenter.RemoveListener(EventType.AddScore, AddScore);
        EventCenter.RemoveListener(EventType.AddDiamond, AddDiamond);
        EventCenter.RemoveListener(EventType.ResetGame, ResetGame);
        EventCenter.RemoveListener<int>(EventType.SelectSkin, SelectSkin);

        //GameData
        SaveData();
    }

    private void Start() {
        if (GameTempData.isAgainGame) {
            GameTempData.isAgainGame = false;
            EventCenter.Broadcast(EventType.ShowGamePanel);
            Instance.IsGameStarted = true;
            return;
        }

        EventCenter.Broadcast(EventType.ShowMainPanel);
    }


    private void AddScore() {
        Score++;
        EventCenter.Broadcast(EventType.UpdateScoreText, Score);

        // 缩减掉落时间
        if (Score % 50 == 0) {
            FallTime /= 2;
            if (FallTime < 0.4) {
                FallTime = 0.4f;
            }
        }
    }

    private void AddDiamond() {
        Diamond++;
        EventCenter.Broadcast(EventType.UpdateDiamondText, Diamond);
    }

    //Shop
    public bool BuySkin(int index) {
        if (Buy(_vars.skinPrices[index])) {
            Data.SkinUnlocker[index] = true;
            SaveData();
            return true;
        }

        return false;
    }

    public bool Buy(int prices) {
        if (prices > Data.DiamondsCount) {
            return false;
        }

        Data.DiamondsCount -= prices;
        SaveData();
        return true;
    }

    //Skin
    private void SelectSkin(int index) {
        Data.SelectSkin = index;
        SaveData();
    }

    //GameData
    private void InitGameData() {
        Data = new();
        Data.IsMusicOn = true;
        Data.DiamondsCount = 100;
        Data.BestScoreArr = new int[10];
        Data.SelectSkin = 0;
        Data.SkinUnlocker = new bool[_vars.skinSprites.Count];
        Data.SkinUnlocker[0] = true;
    }

    private void SaveData() {
        try {
            BinaryFormatter bf = new();
            using (FileStream fs = File.Create(Application.persistentDataPath + "/GameData.data")) {
                bf.Serialize(fs, Data);
            }
        }
        catch (Exception e) {
            Debug.Log(e);
        }
    }

    private void LoadData() {
        try {
            BinaryFormatter bf = new();
            using (FileStream fs = File.Open(Application.persistentDataPath + "/GameData.data", FileMode.Open)) {
                Data = (GameData)bf.Deserialize(fs);
            }
        }
        catch (Exception e) {
            Debug.Log(e);
        }
    }

    private void ResetGame() {
        InitGameData();
        SaveData();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Debug.Log("游戏已重置");
    }
}