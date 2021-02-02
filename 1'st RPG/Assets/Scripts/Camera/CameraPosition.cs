using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPosition : MonoBehaviour
{
    private GameObject _player;

    [SerializeField]
    float rotateSpeed;

    [SerializeField]
    private float zOffset;
    [SerializeField]
    private float yOffset;

    float yRotate = 0;

    [SerializeField]
    GameObject _settingButton;

    private void Start()
    {
        _player = GameObject.Find("Player");
    }
    void Update()
    {
        if (_settingButton.activeSelf)
        {
            if(Input.GetMouseButton(0))
                yRotate += Input.GetAxis("Mouse X") * rotateSpeed * Time.deltaTime;

            transform.rotation = new Quaternion(0, yRotate, 0, 1);

            Camera.main.transform.position = _player.transform.position + (transform.up.normalized * yOffset) + (transform.forward.normalized * zOffset);

            Camera.main.transform.LookAt(_player.transform.position);
        }
    }



}
