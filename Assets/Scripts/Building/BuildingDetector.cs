using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingDetector : MonoBehaviour
{
    public float checkRadius = 3.0f;            //������ ���� ����
    public Vector3 lastPosition;                 //�÷��̾��� ������ ��ġ (�÷��̾� �̵��� ���� �� ��� �ֺ��� ã�� ���� ����)
    public float moveThreshold = 0.1f;          //�̵� ���� �Ӱ谪
    public ConstructibleBuilding currentNearbyBuilding;   //���� ������ �ִ� ���� ������ ������
    public BuildingCrafter currentBuildingCrafter;          //�߰�: ���� �ǹ��� ���� �ý���
    // Start is called before the first frame update
    void Start()
    {
        lastPosition = transform.position;  //���� �� ���� ��ġ�� ������ ��ġ�� ����
        CheckForBuilding();                    //�ʱ� ������ üũ ����
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(lastPosition, transform.position) > moveThreshold)     //�÷��̾ ���� �Ÿ� �̻� �̵��ߴ��� üũ
        {
            CheckForBuilding();                                            //�̵��� ������ üũ
            lastPosition = transform.position;                          //���� ��ġ�� ������ ��ġ�� ������Ʈ
        }

        //����� �������� �ְ� FŰ�� ������ �� �Ǽ�
        if (currentNearbyBuilding != null && Input.GetKeyDown(KeyCode.F))
        {
            if (!currentNearbyBuilding.isConstructed)
            {
                currentNearbyBuilding.StartConstruction(GetComponent<PlayerInventory>());     //�÷��̾� �κ��丮�� �����Ͽ� ������ ����
            }
            else if (currentBuildingCrafter != null)
            {
                Debug.Log($"{currentNearbyBuilding.buildingName}�� ���� �޴� ����");
                CraftingUIManager.Instance?.ShowUI(currentBuildingCrafter);     //�̱������� �����ؼ� UI ǥ�ø� �Ѵ�.
            }
        }
    }
    //�ֺ��� ���� ������ �������� �����ϴ� �Լ�

    private void CheckForBuilding()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, checkRadius);       //���� ���� ���� ��� �ݶ��̴��� ã�ƿ�

        float closestDistance = float.MaxValue;                     //���� ����� �Ÿ��� �ʱⰪ
        ConstructibleBuilding closestBuilding = null;                         //���� ����� �������� �ʱⰪ
        BuildingCrafter closestCrafter = null;          //�߰�

        foreach (Collider collider in hitColliders)
        {
            ConstructibleBuilding building = collider.GetComponent<ConstructibleBuilding>();        //�ǹ� ����
            if (building != null)       //��� �ǹ� ������ ����
            {
                float distance = Vector3.Distance(transform.position, building.transform.position);     //�Ÿ� ���
                if (distance < closestDistance)                                                     //�� ����� �������� �߰� �� ������Ʈ
                {
                    closestDistance = distance;
                    closestBuilding = building;
                    closestCrafter = building.GetComponent<BuildingCrafter>();          //���⼭ ũ������ ��������
                }
            }
        }
        if (closestBuilding != currentNearbyBuilding)        //���� ����� �ǹ��� ����Ǿ��� �� �޼��� ǥ��
        {
            currentNearbyBuilding = closestBuilding;        //���� ����� �ǹ� ������Ʈ
            currentBuildingCrafter = closestCrafter;        //�߰�
            if (currentNearbyBuilding != null && !currentNearbyBuilding.isConstructed)          
            {
                if (FloatingTextManager.instance != null)
                {
                    Vector3 textPostion = transform.position + Vector3.up * 0.5f;
                    FloatingTextManager.instance.Show(
                        $"[F] Ű�� {currentNearbyBuilding.buildingName} �Ǽ� (���� {currentNearbyBuilding.requiredTree} �� �ʿ�)"
                        , currentNearbyBuilding.transform.position + Vector3.up
                        );
                }
            }
        }
    }
}
