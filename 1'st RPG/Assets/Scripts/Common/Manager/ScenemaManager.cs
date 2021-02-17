using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using common;
public class ScenemaManager : Singleton<ScenemaManager> 
{
    [SerializeField]
    GameObject BossSpawn;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void CallCorutin(string name)
    {
        GameSceneManager.Instance.SetIsAct(true);
        StartCoroutine(name);
    }

    IEnumerator SpawnBoss()
    {
        BossSpawn.SetActive(true);
        yield return new WaitForSeconds(7);
        GameSceneManager.Instance.SetIsAct(false);
        yield break;
    }
}
