public enum EventType {
    //Panel
    ShowMainPanel,
    ShowGamePanel,
    ShowOverPanel,
    ShowShopPanel,
    ShowRankPanel,
    ShowHint,

    //Score
    AddScore,
    UpdateScoreText,

    //Pickup
    AddDiamond,
    UpdateDiamondText,

    //Skin
    BuySkin,
    SelectSkin,

    //Cycle
    GameOver,

    //Other
    SpawnNextPlatform,
    ResetGame,
    SetAudio,
    PlayAudio,
}