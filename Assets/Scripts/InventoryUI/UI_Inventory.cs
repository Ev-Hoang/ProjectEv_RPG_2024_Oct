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

        public event Action<int> OnItemSelected, OnItemActionRequested;
        public event Action<int, int> OnItemSwap;

        private void Awake()
        {
            Hide();
            mouseFollower.Toggle(true);
        }
        public void Initialize()
        {          
            mouseFollower.Initialize();
            //Item in hand
            for (int i = 0; i < 8; i++)
            {
                UI_Item item = Instantiate(_UI_Item, Vector3.zero, Quaternion.identity);
                item.transform.SetParent(_Inv_Panel_Hand);
                item.transform.localScale = Vector3.one;
                _ItemsList.Add(item);

                item.OnItemHoverEnter += HandleHoverEnter;
                item.OnItemHoverExit += HandleExit;
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
                item.OnItemHoverExit += HandleExit;
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

        private void HandleSwap(UI_Item item)
        {
        }

        private void HandleSelection(UI_Item item)
        {
            int index = _ItemsList.IndexOf(item);
            if (index == -1) return;
 
            OnItemSelected?.Invoke(index);
        }

        private void HandleExit(UI_Item item)
        {
        }

        private void HandleHoverEnter(UI_Item item)
        {
        }

        public void Show()
        {
            gameObject.SetActive(true);

        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void ResetAllItems()
        {
            for(int i = 0 ; i < _ItemsList.Count; i++)
            {
                _ItemsList[i].ResetData();
            }
        }

        public void CreateSelectedItem(Sprite itemImage, int quantity)
        {
            mouseFollower.Toggle(true);
            mouseFollower.SetData(itemImage, quantity);
        }

        void OnDestroy()
        {
            foreach (UI_Item item in _ItemsList)
            {
                item.OnItemHoverEnter -= HandleHoverEnter;
                item.OnItemHoverExit -= HandleExit;
                item.OnItemLeftClick -= HandleSelection;
            }
        }
    }
}