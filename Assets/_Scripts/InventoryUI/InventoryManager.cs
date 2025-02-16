using InventorySystem.Model.Inventory;
using InventorySystem.UI.Inventory;
using InventorySystem.UI.Item;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace InventorySystem.Controller
{
    public class InventoryManager : MonoBehaviour
    {
        [SerializeField]
        private UI_Inventory inventoryUI;

        [SerializeField]
        private InventorySO inventoryData;

        [SerializeField]
        private ScreenItem_UI screenitemUI;

        public List<InventoryItem> initialItems = new List<InventoryItem>();

        private void Start()
        {
            PreapareUI();
            PrepareInventoryData();
        }

        private void PrepareInventoryData()
        {
            inventoryData.Initialize();
            inventoryData.OnInventoryUpdated += UpdateInventoryUI;
            inventoryData.OnInventoryUpdated += UpdateScreenItemUI;
            foreach (InventoryItem item in initialItems)
            {
                if (item.IsEmpty) continue;
                inventoryData.AddItem(item);
            }
        }

        private string PrepareDescription(InventoryItem item)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(item.item.Description);
            sb.AppendLine();
            for (int i = 0; i < item.itemState.Count; i++)
            {
                sb.Append($"{item.itemState[i].itemParameter.ParameterName}" +
                    $": {item.itemState[i].value} / " +
                    $" {item.item.ParametersList[i].value}");
            }
            return sb.ToString();
        }

        private void SetUITooltip(int itemIndex)
        {
            InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);
            inventoryUI.SetTooltip(inventoryItem.item.Name, "fruit", PrepareDescription(inventoryItem));
        }

        private void UpdateInventoryUI(Dictionary<int, InventoryItem> inventoryState)
        {
            inventoryUI.ResetAllItems();

            inventoryUI.isMouseHoldingItem = false;
            foreach (var item in inventoryState)
            {
                inventoryUI.UpdateData(item.Key, item.Value.item.ItemImage, item.Value.quantity);
                if (item.Key == 32) inventoryUI.isMouseHoldingItem = true;
            }

        }
        private void UpdateScreenItemUI(Dictionary<int, InventoryItem> inventoryState)
        {
            screenitemUI.ResetAllItems();
            foreach (var item in inventoryState)
            {
                screenitemUI.UpdateData(item.Key, item.Value.item.ItemImage, item.Value.quantity);
                if (item.Key > 8) return;
            }
        }

        private void PreapareUI()
        {
            inventoryUI.Initialize();
            inventoryUI.OnItemActionRequested += HandleItemActionRequest;
            inventoryUI.OnItemSelected += HandleItemSelection;
            inventoryUI.OnItemHovered += SetUITooltip;

            screenitemUI.Initialize();
        }

        private void HandleItemSelection(int itemIndex)
        {
            InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);
            if (inventoryItem.IsEmpty && inventoryData.GetItemAt(32).IsEmpty) return;
            inventoryData.CreateMouseSelectItem(itemIndex);
        }

        private void HandleItemActionRequest(int itemIndex)
        {

        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (!inventoryUI.isActiveAndEnabled)
                {
                    inventoryUI.Show();
                    foreach (var item in inventoryData.GetCurrentInventoryState())
                    {
                        inventoryUI.UpdateData(item.Key, item.Value.item.ItemImage, item.Value.quantity);
                    }
                    screenitemUI.Toggle(false);
                }
                else
                {
                    inventoryUI.Hide();
                    //Check if mouse is holding anyitem
                    if (!inventoryData.GetItemAt(32).IsEmpty)
                    {
                        inventoryData.AddItem(inventoryData.GetItemAt(32));
                        inventoryData.resetMouseData();
                    }
                    foreach (var item in inventoryData.GetCurrentInventoryState())
                    {
                        //Update ScreenItemUI
                        screenitemUI.UpdateData(item.Key, item.Value.item.ItemImage, item.Value.quantity);
                        if (item.Key > 8) break;
                    }
                    screenitemUI.Toggle(true);
                }
            }
        }

        void OnDestroy()
        {
            inventoryData.OnInventoryUpdated -= UpdateInventoryUI;
            inventoryData.OnInventoryUpdated -= UpdateScreenItemUI;

            inventoryUI.OnItemHovered -= SetUITooltip;
            inventoryUI.OnItemActionRequested -= HandleItemActionRequest;
            inventoryUI.OnItemSelected -= HandleItemSelection;
        }
    }
}