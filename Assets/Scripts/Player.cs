using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Project: Colour
 * Code Author: Claudio Vertemara
*/

public class Player : MonoBehaviour
{
    public GameObject cam;
    Rigidbody rb;

    public float playerSpeed;
    public float jumpSpeed;
    //int controlScheme = 1;

    AudioSource jumpAudio;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        jumpAudio = GameObject.Find("JumpAudio").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //On Keypress 'C' switch the Controls
        /*if (Input.GetKeyDown(KeyCode.C)) {
            controlScheme *= -1;
        }*/

        //if (controlScheme == 1) {
            // Control Scheme 1
            Vector3 movement = transform.forward * Input.GetAxis("Vertical") * playerSpeed;
            movement.y = rb.velocity.y;
            rb.velocity = movement;

            if (Input.GetKey(KeyCode.A)) {
                rb.angularVelocity = Vector3.down * playerSpeed;
            }
            if (Input.GetKey(KeyCode.D)) {
                rb.angularVelocity = Vector3.up * playerSpeed;
            }
        /*} else {
            // Control Scheme 2
            if (Input.GetKey(KeyCode.W)) {
                transform.LookAt(2 * transform.position - new Vector3(cam.transform.position.x, transform.position.y, cam.transform.position.z));
                rb.velocity = transform.forward * playerSpeed;
            }
            if (Input.GetKey(KeyCode.S)) {
                transform.LookAt(new Vector3(cam.transform.position.x, transform.position.y, cam.transform.position.z));
                rb.velocity = transform.forward * playerSpeed;
            }
            if (Input.GetKey(KeyCode.A)) {
                transform.LookAt(2 * transform.position - new Vector3(cam.transform.position.x, transform.position.y, cam.transform.position.z));
                transform.Rotate(transform.up, -90);
                rb.velocity = transform.forward * playerSpeed;
            }
            if (Input.GetKey(KeyCode.D)) {
                transform.LookAt(2 * transform.position - new Vector3(cam.transform.position.x, transform.position.y, cam.transform.position.z));
                transform.Rotate(transform.up, 90);
                rb.velocity = transform.forward * playerSpeed;
            }
        }*/

        //Jump when spacebar is pressed
        if (Input.GetKeyDown(KeyCode.Space)) {
            Physics.Raycast(transform.position, -transform.up, out RaycastHit rh, 1.2f);

            if (rh.transform != null) {
                rb.AddForce(transform.up * jumpSpeed);
                jumpAudio.Play();
            }
        }

        //Resets player if they fall off map
        if (transform.position.y < -15) {
            transform.position = new Vector3(0, 15, 0);
        }
    }
}
