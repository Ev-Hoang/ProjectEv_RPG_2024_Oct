using InventorySystem.UI.Item;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
//using static UnityEditor.Progress;

namespace InventorySystem.UI.Inventory
{
    public class UI_Inventory : MonoBehaviour
    {
        [SerializeField]
        private UI_Item _UI_Item;
        [SerializeField]
        private RectTransform _Inv_Panel_Hand;
        [SerializeField]
        private RectTransform _Inv_Panel_Bag;
        [SerializeField]
        private MouseFollower mouseFollower;

        [SerializeField]
        List<UI_Item> _ItemsList = new List<UI_Item>();

        public bool isMouseHoldingItem;

        public event Action<int> OnItemSelected, OnItemActionRequested, OnItemHovered;
        public event Action<int, int> OnItemSwap;

        private void Awake()
        {
            Hide();
        }
        public void Initialize()
        {          
            mouseFollower.Initialize();
            isMouseHoldingItem = false;
            //Item in hand
            for (int i = 0; i < 8; i++)
            {
                UI_Item item = Instantiate(_UI_Item, Vector3.zero, Quaternion.identity);
                item.transform.SetParent(_Inv_Panel_Hand);
                item.transform.localScale = Vector3.one;
                _ItemsList.Add(item);

                item.OnItemHoverEnter += HandleHoverEnter;
                item.OnItemHoverExit += HandleHoverExit;
                item.OnItemLeftClick += HandleSelection;
            }

            //Item in bag
            for (int i = 0; i < 24; i++)
            {
                UI_Item item = Instantiate(_UI_Item, Vector3.zero, Quaternion.identity);
                item.transform.SetParent(_Inv_Panel_Bag);
                item.transform.localScale = Vector3.one;
                _ItemsList.Add(item);

                item.OnItemHoverEnter += HandleHoverEnter;
                item.OnItemHoverExit += HandleHoverExit;
                item.OnItemLeftClick += HandleSelection;
            }

            _ItemsList.Add(mouseFollower.mouseItem);
        }

        public void UpdateData(int itemIndex, Sprite itemImage, int itemQuantity)
        {
            if (_ItemsList.Count > itemIndex)
            {
                _ItemsList[itemIndex].SetData(itemImage, itemQuantity);
            } 
        }

        public void SetTooltip(string itemName, string itemType, string itemDescription)
        {
            mouseFollower.ToggleItemTooltip(true);
            mouseFollower.itemTooltip.setTooltip(itemName, itemType, itemDescription);
        }

        private void HandleSelection(UI_Item item)
        {
            if (!isMouseHoldingItem) mouseFollower.ToggleItemTooltip(false);
            else mouseFollower.ToggleItemTooltip(true);
            int index = _ItemsList.IndexOf(item);
            if (index == -1) return;
 
            OnItemSelected?.Invoke(index);
        }

        private void HandleHoverExit(UI_Item item)
        {
            if (isMouseHoldingItem) return;
            mouseFollower.ToggleItemTooltip(false);
        }

        private void HandleHoverEnter(UI_Item item)
        {
            if (isMouseHoldingItem) return;
            int index = _ItemsList.IndexOf(item);
            if (index == -1) return;
            OnItemHovered?.Invoke(index);
        }

        public void Show()
        {
            gameObject.SetActive(true);
            mouseFollower.Toggle(true);

        }

        public void Hide()
        {
            gameObject.SetActive(false);
            mouseFollower.Toggle(false);
        }

        public void ResetAllItems()
        {
            for(int i = 0 ; i < _ItemsList.Count; i++)
            {
                _ItemsList[i].ResetData();
            }
        }

        //public void CreateSelectedItem(Sprite itemImage, int quantity)
        //{
        //    if(!mouseFollower.isHoldingItem()) mouseFollower.ToggleItemTooltip(false);
        //    mouseFollower.Toggle(true);
        //    mouseFollower.SetData(itemImage, quantity);
        //}

        void OnDestroy()
        {
            foreach (UI_Item item in _ItemsList)
            {
                item.OnItemHoverEnter -= HandleHoverEnter;
                item.OnItemHoverExit -= HandleHoverExit;
                item.OnItemLeftClick -= HandleSelection;
            }
        }
    }
}