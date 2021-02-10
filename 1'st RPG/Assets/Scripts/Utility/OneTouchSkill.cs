using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneTouchSkill : MonoBehaviour
{
    [SerializeField]
    float reactTime;
    public int Atk { get; set; }
    public Transform Caster { get; set; }
    public string target { get; set; }
        
    bool isOn;
    void Start()
    {
        //Atk = 10;
    }
    private void OnEnable()
    {
        isOn = true;
        StartCoroutine(Act());
    }
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (!isOn)
            return;
        if (other.gameObject.layer == LayerMask.NameToLayer(target))
        {
            if(target == "Player")
                other.gameObject.GetComponent<PlayableFSM>().Damaged(Atk, Caster);
            else if (target == "Enemy")
                other.gameObject.GetComponent<EnemyFSM>().Damaged(Atk, Caster);
        }
    }
    
    IEnumerator Act()
    {
        yield return new WaitForSeconds(reactTime);
        isOn = false;
    }
}
