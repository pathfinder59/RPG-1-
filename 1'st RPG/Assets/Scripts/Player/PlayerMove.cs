using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMove : MonoBehaviour
{

    CharacterController _characterController;

    Animator _animator;

    NavMeshAgent _navMeshAgent;
    
    float movePower;
    void Start()
    {
        movePower = 10;

        _characterController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
        _navMeshAgent = gameObject.AddComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool Move()
    {
        //navmesh로 움직일땐 플레이어의 look을 스크린으로 바꿔서 x,y에 적용시켜보자 
        float xMove = Input.GetAxis("Horizontal");
        float yMove = Input.GetAxis("Vertical");



        Vector3 dir = Camera.main.transform.right * xMove + Camera.main.transform.forward * yMove;
        dir = new Vector3(dir.x, 0.0f, dir.z).normalized;
        if (!dir.Equals(new Vector3(0, 0, 0)))
        {
            _characterController.Move(dir * movePower * Time.deltaTime);
            transform.forward = dir;
            return true;
        }
        else
            return false;

    }
}
