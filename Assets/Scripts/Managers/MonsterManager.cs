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
        Debug.Log($"스폰포인트 수 {spawnPointObjects.Length}");
        spawnPoints = new Vector3[spawnPointObjects.Length];

        for(int i = 0; i < spawnPointObjects.Length; i++)
        {
            spawnPoints[i] = spawnPointObjects[i].transform.position;
        }
    }

    public void SpawnMonster(string path)
    {
        // 테스트용 2번이상 스폰안되게
        if (spawnCount >= 2) { return; }
        int spawnPoint = Random.Range(0, spawnPoints.Length);
        // 중복된 포인트에서 생성되지 않게 하기
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
