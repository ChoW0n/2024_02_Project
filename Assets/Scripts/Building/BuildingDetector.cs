using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingDetector : MonoBehaviour
{
    public float checkRadius = 3.0f;            //아이템 감지 범위
    public Vector3 lastPosition;                 //플레이어의 마지막 위치 (플레이어 이동이 감지 될 경우 주변을 찾기 위한 변수)
    public float moveThreshold = 0.1f;          //이동 감지 임계값
    public ConstructibleBuilding currentNearbyBuilding;   //가장 가까이 있는 수집 가능한 아이템
    public BuildingCrafter currentBuildingCrafter;          //추가: 현재 건물의 제작 시스템
    // Start is called before the first frame update
    void Start()
    {
        lastPosition = transform.position;  //시작 시 현재 위치를 마지막 위치로 설정
        CheckForBuilding();                    //초기 아이템 체크 수정
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(lastPosition, transform.position) > moveThreshold)     //플레이어가 일정 거리 이상 이동했는지 체크
        {
            CheckForBuilding();                                            //이동시 아이템 체크
            lastPosition = transform.position;                          //현재 위치를 마지막 위치로 업데이트
        }

        //가까운 아이템이 있고 F키를 눌렀을 때 건설
        if (currentNearbyBuilding != null && Input.GetKeyDown(KeyCode.F))
        {
            if (!currentNearbyBuilding.isConstructed)
            {
                currentNearbyBuilding.StartConstruction(GetComponent<PlayerInventory>());     //플레이어 인벤토리를 참조하여 아이템 수집
            }
            else if (currentBuildingCrafter != null)
            {
                Debug.Log($"{currentNearbyBuilding.buildingName}의 제작 메뉴 열기");
                CraftingUIManager.Instance?.ShowUI(currentBuildingCrafter);     //싱글톤으로 접근해서 UI 표시를 한다.
            }
        }
    }
    //주변의 수집 가능한 아이템을 감지하는 함수

    private void CheckForBuilding()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, checkRadius);       //감지 범위 내의 모든 콜라이더를 찾아옴

        float closestDistance = float.MaxValue;                     //가장 가까운 거리의 초기값
        ConstructibleBuilding closestBuilding = null;                         //가장 가까운 아이템이 초기값
        BuildingCrafter closestCrafter = null;          //추가

        foreach (Collider collider in hitColliders)
        {
            ConstructibleBuilding building = collider.GetComponent<ConstructibleBuilding>();        //건물 감지
            if (building != null)       //모든 건물 감지로 변경
            {
                float distance = Vector3.Distance(transform.position, building.transform.position);     //거리 계산
                if (distance < closestDistance)                                                     //더 가까운 아이템을 발견 시 업데이트
                {
                    closestDistance = distance;
                    closestBuilding = building;
                    closestCrafter = building.GetComponent<BuildingCrafter>();          //여기서 크래프터 가져오기
                }
            }
        }
        if (closestBuilding != currentNearbyBuilding)        //가장 가까운 건물이 변경되었을 때 메세지 표시
        {
            currentNearbyBuilding = closestBuilding;        //가장 가까운 건물 업데이트
            currentBuildingCrafter = closestCrafter;        //추가
            if (currentNearbyBuilding != null && !currentNearbyBuilding.isConstructed)          
            {
                if (FloatingTextManager.instance != null)
                {
                    Vector3 textPostion = transform.position + Vector3.up * 0.5f;
                    FloatingTextManager.instance.Show(
                        $"[F] 키로 {currentNearbyBuilding.buildingName} 건설 (나무 {currentNearbyBuilding.requiredTree} 개 필요)"
                        , currentNearbyBuilding.transform.position + Vector3.up
                        );
                }
            }
        }
    }
}
