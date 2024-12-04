using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUIManager : MonoBehaviour
{
    public static InventoryUIManager Instance { get; private set; }  //�̱��� ȭ

    [Header("UI References")]           
    public GameObject inventoryPanel;           //�κ��丮 �г�
    public Transform itemContainer;             //������ ���Ե��� �� �����̳�
    public GameObject itemSlotPrefab;           //������ ���� ������
    public Button closeButton;                  //�ݱ� ��ư

    private PlayerInventory playerInventory;
    private SurivivalStats surivivalStats;

    private void Awake()
    {
        Instance = this;
        inventoryPanel.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        playerInventory = FindObjectOfType<PlayerInventory>();
        surivivalStats = FindObjectOfType<SurivivalStats>();
        closeButton.onClick.AddListener(HideUI);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (inventoryPanel.activeSelf)
            {
                HideUI();
            }
            else
            {
                ShowUI();
            }
        }
    }

    public void ShowUI()
    {
        inventoryPanel.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        RefreshInventory();
    }

    public void HideUI()
    {
        inventoryPanel?.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void RefreshInventory()
    {
        //���� ������ ���Ե��� ����
        foreach (Transform child in itemContainer)
        {
            Destroy(child.gameObject);
        }
        CreateItemSlot(ItemType.Crystal);
        CreateItemSlot(ItemType.Plant);
        CreateItemSlot(ItemType.Bush);
        CreateItemSlot(ItemType.Tree);
        CreateItemSlot(ItemType.VegetableStew);
        CreateItemSlot(ItemType.FruitSalad);
        CreateItemSlot(ItemType.RepairKit);
    }

    private void CreateItemSlot(ItemType type)
    {
        GameObject slotObj = Instantiate(itemSlotPrefab, itemContainer);
        ItemSlot slot = slotObj.GetComponent<ItemSlot>();
        slot.Setup(type, playerInventory.GetItemCount(type));           //�÷��̾� �κ��丮�� ���ڸ� �޾ƿͼ� ���Կ� �ִ´�
    }
}