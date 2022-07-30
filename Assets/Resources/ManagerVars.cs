using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(menuName = "Manager Vars Container")]
public class ManagerVars : ScriptableObject {
    public static ManagerVars GetManagerVars() {
        return Resources.Load<ManagerVars>("ManagerVarsContainer");
    }

    public List<Sprite> backgroundThemeSprites = new();

    public GameObject normalPlatformPre;
    public List<GameObject> normalObstaclePres;
    public GameObject icePlatformPre;
    public List<GameObject> iceObstaclePres;
    public GameObject grassPlatformPre;
    public List<GameObject> grassObstaclePres;
    public GameObject firePlatformPre;
    public List<GameObject> fireObstaclePres;

    public GameObject deathEffect;

    public float nextXPos = 0.554f;
    public float nextYPos = 0.645f;

    public float defaultPlatformFallTime = 3f;
}