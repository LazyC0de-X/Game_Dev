using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{ //Variable
    [SerializeField] private float MouseSensitivity;



    //Reference
    private Transform parent;
    private void Start()
    {
        parent = transform.parent;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        Rotate();
    }
    private void Rotate()
    {
        float MouseX = Input.GetAxis("Mouse X") * MouseSensitivity * Time.deltaTime;
        parent.Rotate(Vector3.up, MouseX);
    }
}
