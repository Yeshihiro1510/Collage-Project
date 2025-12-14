using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ShopItemView : MonoBehaviour
{
    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private TMP_Text _priceText;
    [SerializeField] private Button _buyButton;

    private ShopItem _shopItem;

    public ShopItem ShopItem
    {
        get => _shopItem;
        set
        {
            _shopItem = value;
            UpdateView();
        }
    }

    public void OnBuyClick(UnityAction action) => _buyButton.onClick.AddListener(action);

    public void UpdateView()
    {
        _nameText.text = _shopItem.name;
        _priceText.text = $"{_shopItem.price}$";
    }
}