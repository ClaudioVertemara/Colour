using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Project: Colour Prototype
 * Code Author: Claudio Vertemara
*/

public class FollowingCamera : MonoBehaviour
{
    public Transform playerTransform;
    public Transform cameraPosition;

    Vector3 offset;

    public float sensitivity;

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - playerTransform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!PauseMenu.pause) {
            //transform.position = playerTransform.position + offset;
            //transform.rotation = Quaternion.Euler(25, playerTransform.rotation.y, 0);

            //transform.position = cameraPosition.position;
            transform.position = playerTransform.position + offset;

            //transform.rotation = Quaternion.Euler(transform.eulerAngles.x, playerTransform.eulerAngles.y, transform.eulerAngles.z);

            //transform.LookAt(new Vector3(playerTransform.position.x, playerTransform.position.y, playerTransform.position.z));
            transform.RotateAround(playerTransform.position, Vector3.up, Input.GetAxis("Mouse X") * sensitivity);
            //transform.RotateAround(playerTransform.position, transform.right, -Input.GetAxis("Mouse Y") * sensitivity);
            transform.Rotate(Vector3.right * -Input.GetAxis("Mouse Y") * (sensitivity / 2));

            offset = transform.position - playerTransform.position;

            Cursor.lockState = CursorLockMode.Confined;
        }
    }
}
