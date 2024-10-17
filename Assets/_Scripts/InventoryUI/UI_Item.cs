using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.PackageManager.Requests;
using UnityEditor.Presets;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace InventorySystem.UI.Item
{
    public class UI_Item : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IPointerEnterHandler, IPointerExitHandler, IDragHandler, IEndDragHandler
    {
        [SerializeField]
        private Image itemImage;
        [SerializeField]
        private Image borderImage;
        [SerializeField]
        private TMP_Text quantityTxt;

        public event Action<UI_Item> OnItemHoverEnter, OnItemHoverExit, OnItemLeftClick, OnItemRightClick;

        private bool isEmpty = true;
        private bool isDragging = false;

        public void Awake()
        {
            ResetData();
        }

        public void ToggleBorder(bool value)
        {   
            borderImage.enabled = value;
        }


        public void ResetData()
        {
            itemImage.enabled = false;
            quantityTxt.enabled = false;
            isEmpty = true;
        }

        public void SetData(Sprite sprite, int quantity)
        {
            itemImage.enabled = true;
            quantityTxt.enabled = true;
            itemImage.sprite = sprite;
            quantityTxt.text = quantity + "";
            isEmpty = false;
        }

        //public void OnHoverEnter()
        //{
        //    if (isEmpty) return;
        //    OnItemHoverEnter?.Invoke(this);
        //}

        //public void OnHoverExit()
        //{
        //    OnItemHoverExit?.Invoke(this);
        //}

        //public void OnRightClick()
        //{
        //    if (isEmpty) return;
        //    OnItemRightClick?.Invoke(this);
        //}

        public void OnPointerClick(PointerEventData pointerData)
        {
            if (isDragging) return;
            if (pointerData.button == PointerEventData.InputButton.Right)
            {
                OnItemRightClick?.Invoke(this);
            }
            else
            {
                OnItemLeftClick?.Invoke(this);
            }
        }

        public void OnBeginDrag(PointerEventData pointerData)
        {
            isDragging = true;
            if (pointerData.button == PointerEventData.InputButton.Right)
            {
                OnItemRightClick?.Invoke(this);
            }
            else
            {
                OnItemLeftClick?.Invoke(this);
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (isEmpty) return;
            OnItemHoverEnter?.Invoke(this);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (isEmpty) return;
            OnItemHoverExit?.Invoke(this);
        }

        public void OnDrag(PointerEventData eventData)
        {

        }

        public void OnEndDrag(PointerEventData pointerData)
        {
            isDragging = false;
        }
    }
}