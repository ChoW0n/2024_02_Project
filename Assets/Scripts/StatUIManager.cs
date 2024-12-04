using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatUIManager : MonoBehaviour
{
    public static StatUIManager Instance { get; private set; }

    [Header("UI References")]
    public Slider hungerSliger;         //��� ������
    public Slider suitDurabilitySlider; //���ֺ� ������ ������
    public TextMeshProUGUI hungerText;  //��� ��ġ �ؽ�Ʈ
    public TextMeshProUGUI durabilityText;  //������ ��ġ �ؽ�Ʈ

    private SurivivalStats surivivalStats;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        surivivalStats = FindObjectOfType<SurivivalStats>();
        hungerSliger.maxValue = surivivalStats.maxHunger;       //�����̴� �ִ밪 ����
        suitDurabilitySlider.maxValue = surivivalStats.maxSuitDurability;   //�����̴� �ִ밪 ����
    }

    // Update is called once per frame
    void Update()
    {
        UpdateStatUI();
    }

    private void UpdateStatUI()
    {
        //�����̴� �� ������Ʈ
        hungerSliger.value = surivivalStats.currentHunger;
        suitDurabilitySlider.value = surivivalStats .currentSuitDurability;

        //�ؽ�Ʈ ������Ʈ (�ۼ�Ʈ ǥ��)
        hungerText.text = $"��� {surivivalStats.GetHungerPercentage():F0}%";
        durabilityText.text = $"���ֺ� {surivivalStats.GetSuitDuravilityPercentage():F0}%";

        //���� ������ �� ���� ����
        hungerSliger.fillRect.GetComponent<Image>().color =
            surivivalStats.currentHunger < surivivalStats.maxHunger * 0.3f ? Color.red : Color.green;
        suitDurabilitySlider.fillRect.GetComponent<Image>().color =
            surivivalStats.currentSuitDurability < surivivalStats.maxSuitDurability * 0.3f ? Color.red : Color.blue;
    }
}
