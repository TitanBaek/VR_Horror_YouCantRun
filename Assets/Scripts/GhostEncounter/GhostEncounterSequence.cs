using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class GhostEncounterSequence : MonoBehaviour
{
    [SerializeField] GhostEncounterAction owner;
    [SerializeField] Transform[] ghostSpawnZone;
    //[SerializeField] int ghostTotalShowCount;
    private Light[] lights;
    private bool isStarted;
    public bool IsStarted { get { return isStarted; } set { isStarted = value; } }

    private void Awake()
    {
        isStarted = false;
        owner = GetComponent<GhostEncounterAction>();
        LightArrayInit();
    }

    private void Update()
    {
        if (!isStarted)
            return;

        owner.EncounterTime -= Time.deltaTime;
        if (owner.EncounterTime <= 0)
            isStarted = false;
    }

    public void LightArrayInit()
    {
        lights = new Light[owner.BlinkingLights.Length];
        for (int i = 0;i < owner.BlinkingLights.Length;i++)
        {
            lights[i] = owner.BlinkingLights[i].GetComponentInChildren<Light>();
        }
    }

    public void GhostEncounterStart()
    {
        Debug.Log("������� �����Լ� ����");
        if (isStarted)
            return;
        // ������� ������ ����

        Debug.Log("������� �����Լ� ����");
        isStarted = true;
        SetDoor(false);
        owner.EncounterCoroutine = StartCoroutine(EncounterCoroutine());
    }

    public void GhostEncounterEnds()
    {
        Debug.Log("������� ��");
        owner.EndSequence();
        SetDoor(true);
    }

    public void GhostSetJumpScarePose()
    {
        owner.Ghost.JumpScarePose();
    }

    public IEnumerator EncounterCoroutine()
    {
        while (isStarted)
        {
            SwitchGhost(false);
            SwitchLight(false);
            // ���� �Ⱥ��̰�
            yield return new WaitForSeconds(Random.Range(0.2f,0.6f));
            SwitchLight(true);
            // ���� ���̰�
            if(Random.Range(0,101) > 10)    // 70%�� Ȯ���� ���� ����
            {
                SwitchGhost(true);
                SpawnGhost();
            }
            // �����̰�
            for(int i = 0; i < Random.Range(1,6); i++)
            {
                //SwitchGhost(false);
                SwitchLight(false);
                yield return new WaitForSeconds(Random.Range(0.05f, 0.15f));
                //SwitchGhost(true);
                SwitchLight(true);
                yield return new WaitForSeconds(Random.Range(0.05f, 0.15f));
            }
            yield return new WaitForSeconds(Random.Range(0.6f,1.2f));
        }

        owner.JumpScareCoroutine = StartCoroutine(JumpScareCoroutine());
        yield return null;
    }  

    public IEnumerator JumpScareCoroutine()
    {
        SwitchLight(false);
        SwitchGhost(false);
        yield return new WaitForSeconds(1f);
        //SwitchLight(true);
        SwitchGhost(true);
        SpawnGhost(true);
        GhostSetJumpScarePose();
        yield return new WaitForSeconds(2f);
        SwitchLight(false);
        SwitchGhost(false);
        GhostEncounterEnds();
        yield return new WaitForSeconds(0.1f);
        SwitchLight(true);
        yield return null;
    }

    public void SwitchLight(bool state)
    {
        for (int i = 0; i < owner.BlinkingLights.Length; i++)
        {
            MeshRenderer meshRenderer = owner.BlinkingLights[i].gameObject.GetComponent<MeshRenderer>(); // Material �� EMISSION  Enable/Disable�� ���� lightObject�� MeshRenderer ������Ʈ
            Material[] material = meshRenderer.materials;                                                // ���� �޽÷������� ���׸���
            lights[i].enabled = state;                                                                   // ���� ����ġ
            if (state)                                                                                   // State�� ���� EMISSION Switch
            {
                material[1].EnableKeyword("_EMISSION");
            }
            else
            {
                material[1].DisableKeyword("_EMISSION");
            }
        }
    }

    public void SwitchGhost(bool state)
    {
        owner.GhostObject.gameObject.SetActive(state);
    }

    public void SpawnGhost(bool isJumpScare = false)
    {
        Vector3 spawnTransform = new Vector3();
        if (isJumpScare)
        {
            spawnTransform = new Vector3(Camera.main.transform.position.x, 0, Camera.main.transform.position.z) + new Vector3(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z) * 1.7f;
        } else
        {
            spawnTransform = ghostSpawnZone[Random.Range(0, ghostSpawnZone.Length)].position;
        }

        owner.GhostObject.transform.position = spawnTransform;


    }

    // true : ���� , false : �ݱ� 
    public void SetDoor(bool state)
    {
        if (state)
        {
            // �� �� �� �ְ�
        }
        else
        {
            // �� �ݰ�
            owner.Door.transform.localRotation = Quaternion.Euler(0, 0, 0);
            // �� ���� ����

        }
    }
}
