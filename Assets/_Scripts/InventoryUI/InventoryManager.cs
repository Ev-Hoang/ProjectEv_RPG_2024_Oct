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
        private UI_Hotbar hotbarUI;

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
            inventoryData.OnInventoryUpdated += UpdateHotbarUI;
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
        private void UpdateHotbarUI(Dictionary<int, InventoryItem> inventoryState)
        {
            hotbarUI.ResetAllItems();
            foreach (var item in inventoryState)
            {
                hotbarUI.UpdateData(item.Key, item.Value.item.ItemImage, item.Value.quantity);
                if (item.Key > 8) return;
            }
        }

        private void PreapareUI()
        {
            inventoryUI.Initialize();
            inventoryUI.OnItemActionRequested += HandleItemActionRequest;
            inventoryUI.OnItemSelected += HandleItemSelection;
            inventoryUI.OnItemHovered += SetUITooltip;

            hotbarUI.Initialize();
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
            //Open and close inventory
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (!inventoryUI.isActiveAndEnabled)
                {
                    inventoryUI.Show();
                    foreach (var item in inventoryData.GetCurrentInventoryState())
                    {
                        inventoryUI.UpdateData(item.Key, item.Value.item.ItemImage, item.Value.quantity);
                    }
                    hotbarUI.Toggle(false);
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
                        //Update hotbarUI
                        hotbarUI.UpdateData(item.Key, item.Value.item.ItemImage, item.Value.quantity);
                        if (item.Key > 8) break;
                    }
                    hotbarUI.Toggle(true);
                }
            }
        }

        void OnDestroy()
        {
            inventoryData.OnInventoryUpdated -= UpdateInventoryUI;
            inventoryData.OnInventoryUpdated -= UpdateHotbarUI;

            inventoryUI.OnItemHovered -= SetUITooltip;
            inventoryUI.OnItemActionRequested -= HandleItemActionRequest;
            inventoryUI.OnItemSelected -= HandleItemSelection;
        }
    }
}