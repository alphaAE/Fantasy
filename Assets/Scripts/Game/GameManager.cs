using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager Instance;

    private void Awake() {
        Instance = this;
    }

    public bool IsGameStarted { get; set; }
    public bool IsGameOver { get; set; }
}