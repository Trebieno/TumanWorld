using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlot : MonoBehaviour
{
    public ItemScriptableObject Item;
    public int Amount;
    public bool IsEmpty = true;
    public Image IconGO;
    public TMP_Text ItemAmountText;

    private void Start()
    {
        IconGO = transform.GetChild(0).GetChild(0).GetComponent<Image>();
        ItemAmountText = transform.GetChild(0).GetChild(1).GetComponent<TMP_Text>();
    }

    public void SetIcon(Sprite icon)
    {
        IconGO.sprite = icon;
        IconGO.gameObject.SetActive(true);
    }

    public void SetText(int amount)
    {
        ItemAmountText.text = amount.ToString();
        ItemAmountText.gameObject.SetActive(true);
    }
}
