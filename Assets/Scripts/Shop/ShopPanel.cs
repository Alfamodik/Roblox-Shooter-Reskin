using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static Unity.VisualScripting.Antlr3.Runtime.Tree.TreeWizard;

public class ShopPanel : MonoBehaviour
{
    public event Action<ShopItemView> ItemViewClicked;

    private List<ShopItemView> _shopItems = new List<ShopItemView>();

    [SerializeField] private Transform _itemsParent;
    [SerializeField] private ShopItemViewFactory _shopItemViewFactory;

    private OpenSkinsChecker _openSkinsChecker;
    private SelectedSkinChecker _selectedSkinChecker;

    public void Initialize(OpenSkinsChecker openSkinsChecker, SelectedSkinChecker selectedSkinChecker)
    {
        _openSkinsChecker = openSkinsChecker;
        _selectedSkinChecker = selectedSkinChecker;
    }

    public void Show(IEnumerable<ShopItem> items, bool loadCurrency = true)
    {
        Clear();

        foreach (ShopItem item in items)
        {
            ShopItemView spawnedItem = _shopItemViewFactory.Get(item, _itemsParent);

            spawnedItem.Click += OnItemViewClick;
            spawnedItem.Unselect();
            spawnedItem.UnHighlight();

            _openSkinsChecker.Visit(spawnedItem.Item);

            if (_openSkinsChecker.IsOpened)
            {
                _selectedSkinChecker.Visit(spawnedItem.Item);

                if (_selectedSkinChecker.IsSelected)
                {
                    spawnedItem.Select();
                    spawnedItem.Highlight();

                    ItemViewClicked?.Invoke(spawnedItem);
                }

                spawnedItem.Unlock();
            }
            else
            {
                spawnedItem.Lock();

                if (loadCurrency)
                    spawnedItem.LoadCurrency();
            }

            _shopItems.Add(spawnedItem);
        }
        
        Sort();
    }
    public void Select(ShopItemView itemView)
    {
        foreach (var item in _shopItems)
            item.Unselect();

        itemView.Select();
    }

    private void Sort()
    {
        _shopItems = _shopItems
            .OrderBy(item => item.IsLock)
            .ThenBy(item => item.Item.SerialNumber)
            .ToList();

        for (int i = 0; i < _shopItems.Count; i++)
            _shopItems[i].transform.SetSiblingIndex(i);
    }

    private void OnItemViewClick(ShopItemView itemView)
    {
        Highlight(itemView);
        ItemViewClicked?.Invoke(itemView);
    }

    private void Highlight(ShopItemView shopItemView)
    {
        foreach (var item in _shopItems)
            item.UnHighlight();

        shopItemView.Highlight();
    }

    private void Clear()
    {
        foreach (ShopItemView item in _shopItems)
        {
            item.Click -= OnItemViewClick;
            Destroy(item.gameObject);
        }

        _shopItems.Clear();
    }
}
