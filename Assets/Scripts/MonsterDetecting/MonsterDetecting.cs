using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class MonsterDetecting : MonoBehaviour
{
    [SerializeField] Mannequin mannequin;
    [SerializeField] Camera cam;
    [SerializeField] LayerMask obstacleMask;
    [SerializeField] float plusY;
    Collider collider;
    Plane[] cameraFrustum;

    private void Awake()
    {
        mannequin = GetComponentInParent<Mannequin>();
        collider = GetComponentInParent<Collider>();
    }

    /*
    private void OnBecameVisible()
    {            
        if (mannequin.CurState != Mannequin_State.Dormant)
        mannequin.MannequinBecameVisible();
    }

    private void OnBecameInvisible()
    {
        if (mannequin.CurState != Mannequin_State.Dormant)
            mannequin.MannequinBecameInvisible();
    }
    */

    void LateUpdate()
    {
        // ī�޶� �þ� ���� ���� Collider�� �����ȴٸ�
        cameraFrustum = GeometryUtility.CalculateFrustumPlanes(cam);
        if (GeometryUtility.TestPlanesAABB(cameraFrustum, collider.bounds))
        {
            Debug.Log($"X :{transform.position.x} Y : {transform.position.y + plusY} Z : { transform.position.z}");
            // 1.�����ɽ�Ʈ
            Vector3 dirTarget = (transform.position - cam.transform.position).normalized; // �� ������Ʈ���� ī�޶��� ���� �Ϲ�ȭ

            // 2. ��ֹ� Ȯ��
            float distToTarget = Vector3.Distance(transform.position, cam.transform.position); // �� ������Ʈ���� ī�޶���� �Ÿ� 
            Debug.Log($"���Ϳ� ī�޶� �Ÿ� {distToTarget}");
            if (Physics.Linecast(transform.position, cam.transform.position, out var hit, obstacleMask)) // �� ������Ʈ���� ī�޶� ��������, distToTarget ���� RayCast ��� 
            {
                Debug.Log("���� �����Ұ�");
                if (mannequin.CurState != Mannequin_State.Dormant)
                    mannequin.MannequinBecameInvisible();
                return;
            }

            Debug.Log("���� ����");
            if (mannequin.CurState != Mannequin_State.Dormant)
                mannequin.MannequinBecameVisible();
        } else
        {
            Debug.Log("���� �����Ұ�");
            if (mannequin.CurState != Mannequin_State.Dormant)
                mannequin.MannequinBecameInvisible();

        }
    }    
}
