using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMove : MonoBehaviour
{

    CharacterController _characterController;

    Animator _animator;

    NavMeshAgent _navMeshAgent;
    public NavMeshAgent Agent => _navMeshAgent;

    PlayableFSM fsm;


    public CharacterController CharacterController => _characterController;
    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        fsm = GetComponent<PlayableFSM>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Vector3 ValueInputMove()
    {
        float xMove = Input.GetAxis("Horizontal");
        float yMove = Input.GetAxis("Vertical");

        Vector3 dir = Camera.main.transform.right * xMove + Camera.main.transform.forward * yMove;
        return dir;
    }
    public bool Move(float movePower, Vector3 dir)
    {
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

    public void Chase(Vector3 target,float distance)
    {
        _navMeshAgent.isStopped = true;
        _navMeshAgent.ResetPath();
        _navMeshAgent.stoppingDistance = distance;
        _navMeshAgent.destination = target;
    }

    public void StopChase()
    {
        _navMeshAgent.isStopped = true;
        _navMeshAgent.ResetPath();
    }
}
