using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FocusDirection : MonoBehaviour
{
    [SerializeField] JumpScareBase owner;
    [SerializeField] LayerMask obstacleMask;
    Camera cam;
    Collider collider;
    Plane[] cameraFrustum;
    float nowFocusTime;

    private bool isRunning;

    public bool IsRunning { get { return isRunning; } set { isRunning = value; } }

    private void Awake()
    {
        cam = Camera.main;
        isRunning = false;
        owner = GetComponentInParent<JumpScareBase>();
        collider = GetComponentInParent<Collider>();
    }

    public void PlayerIn()
    {
        if (isRunning)
            return;
        isRunning = true;
    }

    public void PlayerOut()
    {
        if (!isRunning)
            return;
        isRunning = false;
    }

    void LateUpdate()
    {
        if (!isRunning)
            return;

        cameraFrustum = GeometryUtility.CalculateFrustumPlanes(cam);
        if (GeometryUtility.TestPlanesAABB(cameraFrustum, collider.bounds))
        {
            //if (Physics.Linecast(transform.position, cam.transform.position, out var hit, obstacleMask)) // �� ������Ʈ���� ī�޶� ��������, distToTarget ���� RayCast ��� 
            if (Physics.Linecast(transform.position, cam.transform.position, out var hit, obstacleMask)) // �� ������Ʈ���� ī�޶� ��������, distToTarget ���� RayCast ��� 
            {
                // ���ع��� �����Ѵ�.
                Debug.Log($"���ع� ���� : {hit.collider.gameObject.name}");
                return;
            }

            // �þ߿� FocusObject�� �ִ�.
            // JumpScareBase�� FocusTime ���� �����Ѵٸ� SpawnObejct �Լ� ȣ��
            Debug.Log("�÷��̾� ����");
            if (nowFocusTime >= owner.FocusTime)
            {
                owner.SpawnObejct();
            }
            nowFocusTime += Time.deltaTime;
        }
        else
        {
            // �÷��̾� �þ߿� FocusObject�� ����.
            Debug.Log("����X");
        }
    }
}
