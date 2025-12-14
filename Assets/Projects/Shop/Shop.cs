using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [SerializeField] private Transform _parent;
    [SerializeField] private ShopItemView _prefab;

    [SerializeField] private TMP_Text _cartPriceText;
    [SerializeField] private Button _cartButton;
    [SerializeField] private Button _nextDayButton;

    [SerializeField] private List<ShopItem> _shopItems = new();
    private readonly List<ShopItem> _cart = new();
    private readonly List<ShopItemView> _shopItemViews = new();

    private void Awake()
    {
        foreach (var i in _shopItems) AddView(i);
        _cartButton.onClick.AddListener(ShowCart);
        _nextDayButton.onClick.AddListener(NextDay);
        _cartPriceText.text = "0$";
    }

    private void AddView(ShopItem item)
    {
        var view = Instantiate(_prefab, _parent);
        view.ShopItem = item;
        view.OnBuyClick(() => AddToCart(item));
        _shopItemViews.Add(view);
    }

    private void AddToCart(ShopItem item)
    {
        _cart.Add(item);
        UpdateCartPrice();
    }

    private void NextDay()
    {
        foreach (var i in _shopItems) i.price *= 2;
        foreach (var v in _shopItemViews) v.UpdateView();
        UpdateCartPrice();
    }

    private void ShowCart() => _cart.ForEach(item => Debug.Log($"Item name: {item.name} price: {item.price}$"));
    private void UpdateCartPrice() => _cartPriceText.text = $"{_cart.Sum(item => item.price)}$";
}

[Serializable]
public class ShopItem
{
    public string name;
    public int price;
}