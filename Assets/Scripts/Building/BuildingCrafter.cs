using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingCrafter : MonoBehaviour
{
    public BuildingType buildingType;               //�ǹ� Ÿ��
    public CraftingRecipe[] recipes;                //��� ������ ������ �迭
    private SurivivalStats surivivalStats;          //���� ���� ����
    private ConstructibleBuilding building;         //�ǹ� ���� ����

    // Start is called before the first frame update
    void Start()
    {
        surivivalStats = FindObjectOfType<SurivivalStats>();
        building = GetComponent<ConstructibleBuilding>();

        switch (buildingType)               //�ǹ� Ÿ�Կ� ���� ������ ����
        {
            case BuildingType.Kitchen:
                recipes = RecipeList.KitchenRecipes;
                break;
            case BuildingType.CraftingTable:
                recipes = RecipeList.WorkbenchRecipes;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TryCraft(CraftingRecipe recipe, PlayerInventory inventory)      //������ ���� �õ�
    {
        if (!building.isConstructed)            //�Ǽ��� �Ϸ���� �ʾҴٸ� ���� �Ұ�
        {
            FloatingTextManager.instance?.Show("�Ǽ��� �Ϸ���� �ʾҽ��ϴ�!", transform.position + Vector3.up);
            return;
        }

        for (int i = 0; i < recipe.requiredItems.Length; i++)       //��� üũ
        {
            if (inventory.GetItemCount(recipe.requiredItems[i]) < recipe.requiredAmounts[i])
            {
                FloatingTextManager.instance?.Show("��ᰡ �����մϴ�. !", transform.position + Vector3.up);
                return;
            }
        }

        for (int i = 0; i < recipe.requiredItems.Length; i++)       //��� �Һ�
        {
            inventory.RemoveItem(recipe.requiredItems[i], recipe.requiredAmounts[i]);
        }

        surivivalStats.DamageCrafting();        //���ֺ� ������ ����

        inventory.AddItem(recipe.resultItem, recipe.resultAmount);      //������ ����
        FloatingTextManager.instance?.Show($"{ recipe.itemName } ���� �Ϸ�!", transform.position + Vector3.up);
    }
}
