using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostEncounterSequence : MonoBehaviour
{
    [SerializeField] GhostEncounterBase owner;
    [SerializeField] Transform[] ghostSpawnZone;
    //[SerializeField] int ghostTotalShowCount;
    private Light[] lights;
    private bool isStarted;
    public bool IsStarted { get { return isStarted; } set { isStarted = value; } }

    private void Awake()
    {
        isStarted = false;
        owner = GetComponent<GhostEncounterBase>();
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

    public void GhostEncounterJumpScare()
    {
        Debug.Log("�������ɾ�");
        SwitchLight(true);
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
                yield return new WaitForSeconds(Random.Range(0.05f, 0.2f));
                //SwitchGhost(true);
                SwitchLight(true);
                yield return new WaitForSeconds(Random.Range(0.05f, 0.2f));
            }
            yield return new WaitForSeconds(Random.Range(0.6f,1.2f));
        }

        SwitchLight(false);
        GhostEncounterJumpScare();
        yield return new WaitForSeconds(2f);
        GhostEncounterEnds();
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

    public void SpawnGhost()
    {
        owner.GhostObject.transform.position = ghostSpawnZone[Random.Range(0, ghostSpawnZone.Length)].position;
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
            // �� �ݰ� �� �� ����
            owner.Door.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
