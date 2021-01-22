using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    CharacterController _characterController;

    Animator _animator;

    [SerializeField]
    float movePower;


    float moveAmount;
    void Start()
    {
        moveAmount = 0;

        _characterController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float xMove = Input.GetAxis("Horizontal");
        float yMove = Input.GetAxis("Vertical");

       

        Vector3 dir = Camera.main.transform.right * xMove + Camera.main.transform.forward * yMove;
        dir = new Vector3(dir.x, 0.0f, dir.z).normalized;
        if(!dir.Equals(new Vector3(0,0,0)))
        {
            _characterController.Move(dir * moveAmount * Time.deltaTime);
            transform.forward = dir;
            moveAmount = Mathf.Clamp(moveAmount + 3.0f * Time.deltaTime,0.0f,1.0f);           
        }
        else
            moveAmount = Mathf.Clamp(moveAmount - 5.0f * Time.deltaTime, 0.0f, 1.0f);

        _animator.SetFloat("Blend", moveAmount);
        _characterController.Move(dir * moveAmount * movePower * Time.deltaTime);
    }
}
