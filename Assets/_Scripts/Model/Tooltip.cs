using System.Collections;
using System.Collections.Generic;
using TMPro;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour
{
    public RectTransform contentRectTransform;

    [SerializeField] private Image tooltipBackground;
    [SerializeField] private TMP_Text itemName;
    [SerializeField] private TMP_Text itemType;
    [SerializeField] private TMP_Text itemDescription;
    void Awake()
    {
        ResetTooltip();
        hideTooltip();
    }

    private void ResetTooltip()
    {
        itemName.text = string.Empty;
        itemType.text = string.Empty;
        itemDescription.text = string.Empty;
    }

    public void setTooltip(string itemName, string itemType, string itemDescription)
    {
        this.itemName.text = itemName;
        this.itemType.text = itemType;
        this.itemDescription.text = itemDescription;
    }

    public void hideTooltip()
    {
        gameObject.SetActive(false);
    }

    public void showToolTip()
    {
        gameObject.SetActive(true);
    }
}
