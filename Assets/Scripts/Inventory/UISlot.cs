using System;
using Inventory;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UISlot : MonoBehaviour
{
    [SerializeField] private Image _itemImage;
    [SerializeField] private GameObject _selectIndicator;
    [SerializeField] private TMP_Text _itemAmountText;
    private Button _button;
    private Item _item;
    public Item Item => _item;
    
    public bool IsSelected;
    public event Action<int> Selected; 

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    public bool IsEmpty()
    {
        return Item == null;
    }

    public void UpdateItem(Item item, int getItemCount)
    {
        _item = item;
        _itemImage.enabled = true;
        _itemImage.sprite = item.Data.ItemIcon;
        _itemAmountText.text = getItemCount.ToString();
    }
    public void SelectState(bool state)
    {
        IsSelected = state;
        _selectIndicator.SetActive(state);
    }

    private void OnSelect()
    {
        Selected?.Invoke(transform.GetSiblingIndex());
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(OnSelect);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnSelect);
    }

    public void SetEmpty()
    {
        _itemImage.enabled = false;
        _itemAmountText.text = String.Empty;
    }
}