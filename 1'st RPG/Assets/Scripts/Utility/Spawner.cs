using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using common;
public class Spawner : MonoBehaviour
{
    [SerializeField]
    GameObject prefab;
    [SerializeField]
    int nSpawn;

    List<GameObject> prefabs;
    void Start()
    {

        EventManager.On("Respawn", Spawn); //리스폰 이벤트 발생시 존재하는 오브젝트가 유지해야 하는 수 미만일 경우 스폰이 실행  

        for(int i = 0; i<nSpawn;++i)
        {
            var go = Instantiate(prefab);
            prefab.transform.position = transform.position;
            prefab.transform.forward = transform.forward;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Spawn(object obj)
    {
        var founded = prefabs.FirstOrDefault(_ => !_.activeInHierarchy);
        if (founded != null)
        {
            founded.SetActive(true);
            founded.transform.position = transform.position;
            founded.transform.forward = transform.forward;
        }
    }

}
