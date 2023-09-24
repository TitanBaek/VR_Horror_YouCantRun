using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    Vector3[] spawnPoints;
    int spawnCount;
    int lastSpawnPoint;

    private void Awake()
    {
        spawnCount = 0;
        lastSpawnPoint = 100;
    }
    public void InitSpawnArray()
    {
        GameObject[] spawnPointObjects = GameObject.FindGameObjectsWithTag("MonsterSpawnPoint");
        Debug.Log($"��������Ʈ �� {spawnPointObjects.Length}");
        spawnPoints = new Vector3[spawnPointObjects.Length];

        for(int i = 0; i < spawnPointObjects.Length; i++)
        {
            spawnPoints[i] = spawnPointObjects[i].transform.position;
        }
    }

    public void SpawnMonster(string path)
    {
        // �׽�Ʈ�� 2���̻� �����ȵǰ�
        if (spawnCount >= 2) { return; }
        int spawnPoint = Random.Range(0, spawnPoints.Length);
        // �ߺ��� ����Ʈ���� �������� �ʰ� �ϱ�
        if(spawnPoint == lastSpawnPoint)
        {
            SpawnMonster(path);
            return;
        }
        GameObject monster = GameManager.Resource.Load<GameObject>(path);        
        GameManager.Instantiate(monster, spawnPoints[Random.Range(0,spawnPoints.Length)],Quaternion.identity);
        lastSpawnPoint = spawnPoint;
        spawnCount++;
    }
}
