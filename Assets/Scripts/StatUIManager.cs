using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatUIManager : MonoBehaviour
{
    public static StatUIManager Instance { get; private set; }

    [Header("UI References")]
    public Slider hungerSliger;         //허기 게이지
    public Slider suitDurabilitySlider; //우주복 내구도 게이지
    public TextMeshProUGUI hungerText;  //허기 수치 텍스트
    public TextMeshProUGUI durabilityText;  //내구도 수치 텍스트

    private SurivivalStats surivivalStats;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        surivivalStats = FindObjectOfType<SurivivalStats>();
        hungerSliger.maxValue = surivivalStats.maxHunger;       //슬라이더 최대값 설정
        suitDurabilitySlider.maxValue = surivivalStats.maxSuitDurability;   //슬라이더 최대값 설정
    }

    // Update is called once per frame
    void Update()
    {
        UpdateStatUI();
    }

    private void UpdateStatUI()
    {
        //슬라이더 값 업데이트
        hungerSliger.value = surivivalStats.currentHunger;
        suitDurabilitySlider.value = surivivalStats .currentSuitDurability;

        //텍스트 업데이트 (퍼센트 표시)
        hungerText.text = $"허기 {surivivalStats.GetHungerPercentage():F0}%";
        durabilityText.text = $"우주복 {surivivalStats.GetSuitDuravilityPercentage():F0}%";

        //위험 상태일 때 색상 변경
        hungerSliger.fillRect.GetComponent<Image>().color =
            surivivalStats.currentHunger < surivivalStats.maxHunger * 0.3f ? Color.red : Color.green;
        suitDurabilitySlider.fillRect.GetComponent<Image>().color =
            surivivalStats.currentSuitDurability < surivivalStats.maxSuitDurability * 0.3f ? Color.red : Color.blue;
    }
}
