using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : SingletonMonobehaviour<InventorySystem>
{
    protected override void Awake()
    {
        base.Awake();
    }

    [SerializeField] GameObject InventoryScreenUI;
    [SerializeField] bool isOpen;
    [SerializeField] bool isFull;

    [SerializeField] List<GameObject> slotList = new List<GameObject>();
    [SerializeField] List<string> itemList = new List<string>();
    private GameObject itemToAdd;
    private GameObject whatSlotToEquip;


    private void Start()
    {
        InventoryScreenUI.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            isOpen = !isOpen;
            InventoryScreenUI.SetActive(isOpen);

            if (isOpen)
            {
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }

    public void AddToInventory(string itemName)
    {
        whatSlotToEquip = FindNextEmptySlot();
        itemToAdd = (GameObject)Instantiate(Resources.Load<GameObject>(itemName), whatSlotToEquip.transform.position, whatSlotToEquip.transform.rotation);
        itemToAdd.transform.SetParent(whatSlotToEquip.transform);

        itemList.Add(itemName);
    }

    private GameObject FindNextEmptySlot()
    {
        foreach (GameObject slot in slotList)
        {
            //Eğer içinde item olmayan bir slot bulursan gönder yoksa boş bir obje gönder
            if (slot.transform.childCount == 0)
            {
                return slot;
            }
        }
        return new GameObject();
    }

    public bool CheckIfFull()
    {
        int counter = 0;
        foreach (GameObject slot in slotList)
        {
            if (slot.transform.childCount > 0)
            {
                counter++;
            }
        }

        return counter == slotList.Count;
    }
}
