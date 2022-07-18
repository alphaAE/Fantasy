using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BgTheme : MonoBehaviour {
    private SpriteRenderer _spriteRenderer;
    private ManagerVars _vars;

    private void Awake() {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _vars = ManagerVars.GetManagerVars();
        int ranValue = Random.Range(0, _vars.backgroundThemeSprites.Count);
        _spriteRenderer.sprite = _vars.backgroundThemeSprites[ranValue];
    }
}