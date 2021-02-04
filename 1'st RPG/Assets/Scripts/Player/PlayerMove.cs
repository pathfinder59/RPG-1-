using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMove : MonoBehaviour
{

    CharacterController _characterController;

    PlayableFSM fsm;


    public CharacterController CharacterController => _characterController;
    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        fsm = GetComponent<PlayableFSM>();
    }

    // Update is called once per frame
    void Update()
    {
        Move(fsm.Status.MoveSpeed);
    }
    public void Move(float movePower)
    {
        float xMove = Input.GetAxis("Horizontal");
        float yMove = Input.GetAxis("Vertical");

        Vector3 dir = Camera.main.transform.right * xMove + Camera.main.transform.forward * yMove;

        dir = new Vector3(dir.x, 0.0f, dir.z).normalized;
        if (!dir.Equals(new Vector3(0, 0, 0)))
        {
            _characterController.Move(dir * movePower * Time.deltaTime);
            transform.forward = dir;
            fsm.IsMoving = true;
            return;
        }
        fsm.IsMoving = false;
     
    }
}
