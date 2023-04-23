using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
/// IPointerDownHandler - Следит за нажатиями мышки по объекту на котором висит этот скрипт
/// IPointerUpHandler - Следит за отпусканием мышки по объекту на котором висит этот скрипт
/// IDragHandler - Следит за тем не водим ли мы нажатую мышку по объекту
public class DragAndDropItem : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public InventorySlot oldSlot;
    private Transform player;

    private void Start()
    {
        //ПОСТАВЬТЕ ТЭГ "PLAYER" НА ОБЪЕКТЕ ПЕРСОНАЖА!
        player = GameObject.FindGameObjectWithTag("Player").transform;
        // Находим скрипт InventorySlot в слоте в иерархии
        oldSlot = transform.GetComponentInParent<InventorySlot>();
    }
    public void OnDrag(PointerEventData eventData)
    {
        // Если слот пустой, то мы не выполняем то что ниже return;
        if (oldSlot.IsEmpty)
            return;
        GetComponent<RectTransform>().position += new Vector3(eventData.delta.x, eventData.delta.y);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (oldSlot.IsEmpty)
            return;
        //Делаем картинку прозрачнее
        GetComponentInChildren<Image>().color = new Color(1, 1, 1, 0.75f);
        // Делаем так чтобы нажатия мышкой не игнорировали эту картинку
        GetComponentInChildren<Image>().raycastTarget = false;
        // Делаем наш DraggableObject ребенком InventoryPanel чтобы DraggableObject был над другими слотами инвенторя
        transform.SetParent(transform.parent.parent);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (oldSlot.IsEmpty)
            return;
        // Делаем картинку опять не прозрачной
        GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1f);
        // И чтобы мышка опять могла ее засечь
        GetComponentInChildren<Image>().raycastTarget = true;

        //Поставить DraggableObject обратно в свой старый слот
        transform.SetParent(oldSlot.transform);
        transform.position = oldSlot.transform.position;
        //Если мышка отпущена над объектом по имени UIPanel, то...
        if (eventData.pointerCurrentRaycast.gameObject.name == "UIPanel")
        {
            // Выброс объектов из инвентаря - Спавним префаб обекта перед персонажем
            GameObject itemObject = Instantiate(oldSlot.Item.itemPrefab, player.position + Vector3.up + player.forward, Quaternion.identity);
            // Устанавливаем количество объектов такое какое было в слоте
            itemObject.GetComponent<Item>().Amount = oldSlot.Amount;
            // убираем значения InventorySlot
            NullifySlotData();
        }
        else if(eventData.pointerCurrentRaycast.gameObject.transform.parent.parent.GetComponent<InventorySlot>() != null)
        {
            //Перемещаем данные из одного слота в другой
            ExchangeSlotData(eventData.pointerCurrentRaycast.gameObject.transform.parent.parent.GetComponent<InventorySlot>());
        }
       
    }
    void NullifySlotData()
    {
        // убираем значения InventorySlot
        oldSlot.Item = null;
        oldSlot.Amount = 0;
        oldSlot.IsEmpty = true;
        oldSlot.IconGO.color = new Color(1, 1, 1, 0);
        oldSlot.IconGO.sprite = null;
        oldSlot.ItemAmountText.text = "";
    }
    void ExchangeSlotData(InventorySlot newSlot)
    {
        // Временно храним данные newSlot в отдельных переменных
        ItemScriptableObject item = newSlot.Item;
        int amount = newSlot.Amount;
        bool isEmpty = newSlot.IsEmpty;
        Image iconGO = newSlot.IconGO;
        TMP_Text itemAmountText = newSlot.ItemAmountText;

        // Заменяем значения newSlot на значения oldSlot
        newSlot.Item = oldSlot.Item;
        newSlot.Amount = oldSlot.Amount;
        if (oldSlot.IsEmpty == false)
        {
            newSlot.SetIcon(oldSlot.IconGO.sprite);
            newSlot.ItemAmountText.text = oldSlot.Amount.ToString();
        }
        else
        {
            newSlot.IconGO.color = new Color(1, 1, 1, 0);
            newSlot.IconGO.sprite = null;
            newSlot.ItemAmountText.text = "";
        }
        
        newSlot.IsEmpty = oldSlot.IsEmpty;

        // Заменяем значения oldSlot на значения newSlot сохраненные в переменных
        oldSlot.Item = item;
        oldSlot.Amount = amount;
        if (isEmpty == false)
        {
            oldSlot.SetIcon(iconGO.GetComponent<Image>().sprite);
            oldSlot.ItemAmountText.text = amount.ToString();
        }
        else
        {
            oldSlot.IconGO.GetComponent<Image>().color = new Color(1, 1, 1, 0);
            oldSlot.IconGO.GetComponent<Image>().sprite = null;
            oldSlot.ItemAmountText.text = "";
        }
        
        oldSlot.IsEmpty = isEmpty;
    }
}
