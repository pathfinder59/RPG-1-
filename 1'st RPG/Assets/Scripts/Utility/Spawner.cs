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
        prefabs = new List<GameObject>();

        for (int i = 0; i<nSpawn;++i)
        {
            var go = Instantiate(prefab);
            go.transform.position = transform.position + new Vector3(Random.Range(-0.3f, 0.3f), 0f, Random.Range(-0.3f, 0.3f));
            go.transform.forward = transform.forward;
            go.gameObject.SetActive(true);
            prefabs.Add(go);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Spawn(object obj = null)
    {
        var founded = prefabs.FirstOrDefault(_ => !_.activeInHierarchy);
        if (founded != null)
        {
            founded.SetActive(true);
            founded.transform.position = transform.position + new Vector3(Random.Range(-0.3f,0.3f),0f, Random.Range(-0.3f, 0.3f));
            founded.transform.forward = transform.forward;
            founded.gameObject.SetActive(true);
        }
    }

}
