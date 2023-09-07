using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ǻ��/���� Ȱ��ȭ/��Ȱ��ȭ �� ���������� �� ���� ���� ��ũ��Ʈ -> �̸� ���� �� �� ����
public class ElevatorController : MonoBehaviour
{
    public bool fuseActive;     // ǻ�� Ȱ��ȭ ����
    public bool leverActive;    // ���� Ȱ��ȭ ����

    public bool fuse21Active;   // ǻ�� 2-1 Ȱ��ȭ ����
    public bool fuse22Active;   // ǻ�� 2-2 Ȱ��ȭ ����
        
    public bool open;

    // ���� leftDoor, rightDoor �� ���� ���������ִµ� �� ������ ���� ���� �ʿ�
    public GameObject leftDoor;
    public GameObject rightDoor;

    public void EnableFuse()
    {
        Debug.Log("Enable Fuse");

        fuseActive = true;
        if (fuseActive && leverActive) Debug.Log("Elevator Active");
    }

    public void DisableFuse()
    {
        Debug.Log("Disable Fuse");

        fuseActive = false;
    }

    public void EnableLever()
    {
        Debug.Log("Enable Lever");

        leverActive = true;
        if (fuseActive && leverActive) Debug.Log("Elevator Active");
    }

    public void DisableLever()
    {
        Debug.Log("Disable Lever");

        leverActive = false;
    }

    public void SetFuseActive(bool active)
    {
        // It's the same state?
        if (active == fuseActive)
            return;

        // Change the machine state
        fuseActive = active;
        if (fuseActive)
            EnableFuse();
        else
            DisableFuse();
    }

    public void SetLeverActive(bool active)
    {
        if (active == leverActive)
            return;

        leverActive = active;
        if (leverActive)
            EnableLever();
        else
            DisableLever();
    }

    public void DoubleFuseActive1(bool active)
    {
        fuse21Active = active;
        if (fuse21Active && fuse22Active) fuseActive = true;
        else fuseActive = false;
    }

    public void DoubleFuseActive2(bool active)
    {
        fuse22Active = active;
        if (fuse21Active && fuse22Active) fuseActive = true;
        else fuseActive = false;
    }

    public void OpenDoor()  
    {
        // ǻ��� ������ ��� Ȱ��ȭ �Ǿ������� ����
        if (fuseActive && leverActive)
        {
            // StartCoroutine(OpenElevatorRoutine());
            leftDoor.transform.position = Vector3.Lerp(leftDoor.transform.position, leftDoor.transform.position + Vector3.back, 1f);
            rightDoor.transform.position = Vector3.Lerp(rightDoor.transform.position, rightDoor.transform.position + Vector3.forward, 1f);
        }
        else
        {
            Debug.Log("Not Active");
        }
    }

    // �ڷ�ƾ���� �ε巯�� ������ x -> ���� �ʿ�
    IEnumerator OpenElevatorRoutine()
    {
        Debug.Log("OpenDoor");
        open = true;
        leftDoor.transform.position = Vector3.Lerp(leftDoor.transform.position, leftDoor.transform.position + Vector3.back, 1f);
        rightDoor.transform.position = Vector3.Lerp(rightDoor.transform.position, rightDoor.transform.position + Vector3.forward, 1f);

        yield return new WaitForSeconds(5f);

        Debug.Log("CloseDoor");
        leftDoor.transform.position = Vector3.Lerp(leftDoor.transform.position, leftDoor.transform.position + Vector3.forward, 1f);
        rightDoor.transform.position = Vector3.Lerp(rightDoor.transform.position, rightDoor.transform.position + Vector3.back, 1f);
        open = false;

        yield return null;
    }
}
