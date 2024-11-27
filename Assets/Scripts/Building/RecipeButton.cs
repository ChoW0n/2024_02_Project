using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RecipeButton : MonoBehaviour
{
    [Header("UI References")]
    public TextMeshProUGUI recipeName;      //������ �̸�
    public TextMeshProUGUI materialsText;   //�ʿ� ��� �ؽ�Ʈ
    public Button craftButton;              //���� ��ư

    private CraftingRecipe recipe;          //������ ������
    private BuildingCrafter crafter;        //�ǹ��� ���� �ý���
    private PlayerInventory playerInventory;    //�÷��̾� �κ��丮
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Setup(CraftingRecipe recipe, BuildingCrafter crafter)
    {
        this.recipe = recipe;
        this.crafter = crafter;
        playerInventory = FindObjectOfType<PlayerInventory>();

        recipeName.text = recipe.itemName;      //������ ���� ǥ��
        UpdateMaterialsText();

        craftButton.onClick.AddListener(OnCraftButtonClicked);  //���� ��ư�� ������ ����
    }

    private void UpdateMaterialsText()          //��� ���� ������Ʈ
    {
        string materials = "�ʿ� ��� :\n";
        for (int i = 0; i < recipe.requiredItems.Length; i++)
        {
            ItemType item = recipe.requiredItems[i];
            int required = recipe.requiredAmounts[i];
            int has = playerInventory.GetItemCount(item);
            materials += $"{item} : {has}/{required}\n";
        }
        materialsText.text = materials;
    }

    private void OnCraftButtonClicked()
    {
        crafter.TryCraft(recipe, playerInventory);      //���� ��ư Ŭ�� ó��
        UpdateMaterialsText();
    }
}
