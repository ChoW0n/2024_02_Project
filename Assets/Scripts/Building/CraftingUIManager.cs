using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CraftingUIManager : MonoBehaviour
{
    public static CraftingUIManager Instance { get; private set; }     //�̱��� �ν��Ͻ�

    [Header("UI References")]
    public GameObject craftingPanel;        //���� UI �г�
    public TextMeshProUGUI buildingNameText;    //�ǹ� �̸� �ؽ�Ʈ
    public Transform recipeContainer;           //������ ��ư�� �� �����̳�
    public Button closeButton;                  //�ݱ� ��ư
    public GameObject recipeButtonPrefabs;      //������ ��ư ������

    private BuildingCrafter currentCrafter;     //���� ���õ� �ǹ��� ���� �ý���

    private void Awake()
    {
        if (Instance == null) Instance = this;      //�̱��� ����
        else Destroy(gameObject);

        craftingPanel.SetActive(false);             //���۽� UI �����
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void RefreshRecipeList()        //������ ��� ���ΰ�ħ
    {
        //���� ������ ��ư ����
        foreach (Transform child in recipeContainer)
        {
            Destroy(child.gameObject);
        }

        //�� ������ ��ư ����
        if (currentCrafter != null && currentCrafter.recipes != null)
        {
            foreach (CraftingRecipe recipe in currentCrafter.recipes)
            {
                GameObject buttonObj = Instantiate(recipeButtonPrefabs, recipeContainer);
                RecipeButton recipeButton = buttonObj.GetComponent<RecipeButton>();
                recipeButton.Setup(recipe, currentCrafter);
                //RecipeButton Ŭ���� �۾� ����
            }
        }
    }

    public void ShowUI(BuildingCrafter crafter) //UI ǥ��
    {
        currentCrafter = crafter;
        craftingPanel.SetActive(true);          //�г��� ���ش�

        Cursor.visible = true;                  //���콺 Ŀ�� ǥ�� �� ��� ����
        Cursor.lockState = CursorLockMode.None;

        if (crafter != null)
        {
            buildingNameText.text = crafter.GetComponent<ConstructibleBuilding>().buildingName;
            RefreshRecipeList();
        }
    }

    public void HideUI()
    {
        craftingPanel.SetActive(false);
        currentCrafter = null;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}