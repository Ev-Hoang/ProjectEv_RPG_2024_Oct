using System.Collections;
using System.Collections.Generic;
using TMPro;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour
{
    [SerializeField] private Image tooltipBackground;
    [SerializeField] private TMP_Text itemName;
    [SerializeField] private TMP_Text itemType;
    [SerializeField] private TMP_Text itemDescription;
    void Awake()
    {
        hideTooltip();
    }

    public void hideTooltip()
    {
        tooltipBackground.gameObject.SetActive(false);
    }

    public void showToolTip()
    {
        tooltipBackground.gameObject.SetActive(true);
    }
}
