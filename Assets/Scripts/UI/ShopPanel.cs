using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ShopPanel : BasePanel {
    private Text _textDiamondCount, _textSkinName, _textBuyDiamondCount;
    private Button _btnBack, _btnSelected, _btnSelect, _btnBuy;
    private Transform _parent;

    private ManagerVars _vars;

    private float _itemWidth = 360f;

    private int _currentSkinIndex = 0;
    ShopPanel() : base(EventType.ShowShopPanel) { }

    protected override void Init() {
        _vars = ManagerVars.GetManagerVars();

        //Text
        _textDiamondCount = transform.Find("Diamond/TextDiamondCount").GetComponent<Text>();
        _textSkinName = transform.Find("TextSkinName").GetComponent<Text>();
        _textBuyDiamondCount = transform.Find("BtnBuy/Text").GetComponent<Text>();
        //Button
        _btnBack = transform.Find("BtnBack").GetComponent<Button>();
        _btnBack.onClick.AddListener(OnBackButtonClick);
        _btnSelected = transform.Find("BtnSelected").GetComponent<Button>();
        _btnSelect = transform.Find("BtnSelect").GetComponent<Button>();
        _btnSelect.onClick.AddListener(OnSelectButtonClick);
        _btnBuy = transform.Find("BtnBuy").GetComponent<Button>();
        _btnBuy.onClick.AddListener(OnBuyButtonClick);

        //Scroll Skins
        _parent = transform.Find("ScrollSkins/Parent");
        _parent.GetComponent<RectTransform>().sizeDelta = new Vector2((_vars.skinSprites.Count + 2) * _itemWidth, 500);
        for (int i = 0; i < _vars.skinSprites.Count; i++) {
            var obj = Instantiate(_vars.itemSkinPre, _parent);
            var img = obj.GetComponentInChildren<Image>();
            img.sprite = _vars.skinSprites[i];
            if (!GameManager.Instance.Data.SkinUnlocker[i]) {
                img.color = Color.gray;
            }

            obj.transform.localPosition = new Vector3((i + 1) * _itemWidth, 0);
        }
    }

    private void Start() {
        _textDiamondCount.text = GameManager.Instance.Data.DiamondsCount.ToString();
        //设置按钮
        _btnSelected.gameObject.SetActive(true);
        _btnSelect.gameObject.SetActive(false);
        _btnBuy.gameObject.SetActive(false);
        _parent.DOLocalMoveX(-_itemWidth * 1.5f + GameManager.Instance.Data.SelectSkin * -_itemWidth, 0.2f);
    }

    private void Update() {
        int index = (int)Math.Round((_parent.localPosition.x + _itemWidth / 2) / -_itemWidth) - 1;
        index = Math.Clamp(index, 0, _vars.skinSprites.Count - 1);
        //选中的皮肤发生变更
        if (_currentSkinIndex != index) {
            _currentSkinIndex = index;
            //图标缩小
            for (int i = 0; i < _vars.skinSprites.Count; i++) {
                if (_currentSkinIndex == i) {
                    _parent.GetChild(i).GetChild(0).GetComponent<RectTransform>().sizeDelta =
                        new Vector2(_itemWidth, _itemWidth);
                }
                else {
                    _parent.GetChild(i).GetChild(0).GetComponent<RectTransform>().sizeDelta =
                        new Vector2(_itemWidth / 2, _itemWidth / 2);
                }
            }

            //设置名字
            _textSkinName.text = _vars.skinNames[_currentSkinIndex];
            //设置价格
            _textBuyDiamondCount.text = _vars.skinPrices[_currentSkinIndex].ToString();
            //设置按钮
            if (_currentSkinIndex == GameManager.Instance.Data.SelectSkin) {
                _btnSelected.gameObject.SetActive(true);
                _btnSelect.gameObject.SetActive(false);
                _btnBuy.gameObject.SetActive(false);
            }
            else if (!GameManager.Instance.Data.SkinUnlocker[_currentSkinIndex]) {
                _btnSelected.gameObject.SetActive(false);
                _btnSelect.gameObject.SetActive(false);
                _btnBuy.gameObject.SetActive(true);
            }
            else {
                _btnSelected.gameObject.SetActive(false);
                _btnSelect.gameObject.SetActive(true);
                _btnBuy.gameObject.SetActive(false);
            }
        }

        //对齐动画
        if (Input.GetMouseButtonUp(0)) {
            _parent.DOLocalMoveX(-_itemWidth * 1.5f + index * -_itemWidth, 0.2f);
        }
    }

    private void OnBackButtonClick() {
        gameObject.SetActive(false);
    }

    private void OnSelectButtonClick() {
        EventCenter.Broadcast(EventType.SelectSkin, _currentSkinIndex);
        _btnSelected.gameObject.SetActive(true);
        _btnSelect.gameObject.SetActive(false);
        _btnBuy.gameObject.SetActive(false);
    }

    private void OnBuyButtonClick() {
        if (GameManager.Instance.BuySkin(_currentSkinIndex)) {
            EventCenter.Broadcast(EventType.SelectSkin, _currentSkinIndex);
            _btnSelected.gameObject.SetActive(true);
            _btnSelect.gameObject.SetActive(false);
            _btnBuy.gameObject.SetActive(false);
            _textDiamondCount.text = GameManager.Instance.Data.DiamondsCount.ToString();
            _parent.GetChild(_currentSkinIndex).GetComponentInChildren<Image>().color = Color.white;
        }
        else {
            EventCenter.Broadcast(EventType.ShowHint, "钻石不足");
        }
    }
}