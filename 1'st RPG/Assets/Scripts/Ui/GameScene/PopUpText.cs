using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpText : MonoBehaviour
{
    void Start()
    {
        
    }
    private void OnEnable()
    {
        StartCoroutine(LifeRoutine());
    }
    void Update()
    {
        
    }

    public IEnumerator LifeRoutine()
    {
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
    }
}
