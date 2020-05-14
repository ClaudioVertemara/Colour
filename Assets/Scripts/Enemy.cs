using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Project: Colour
 * Code Author: Claudio Vertemara
*/

public class Enemy : MonoBehaviour
{
    Rigidbody rb;
    ShootPaint sp;

    public GameObject waypoints;

    public int currentWP;
    Transform currentT;

    public float enemySpeed;

    bool reset;
    bool resetting;
    float rTimer;
    Transform side;

    bool frozen;
    float fTimer;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        sp = GameObject.Find("Node").GetComponent<ShootPaint>();

        reset = false;
        resetting = false;
        rTimer = 3f;

        frozen = false;
        fTimer = 5f;
    }

    // Update is called once per frame
    void Update()
    {
        if (frozen == false) {
            if (resetting == false) {
                currentT = waypoints.transform.GetChild(currentWP);

                transform.LookAt(new Vector3(currentT.position.x, transform.position.y, currentT.position.z));
                rb.velocity = transform.forward * enemySpeed;

                if (Mathf.Abs(transform.position.x - currentT.position.x) < 0.2f && Mathf.Abs(transform.position.z - currentT.position.z) < 0.2f) {
                    if (currentT.CompareTag("ScanPoint") && reset == false) {
                        resetting = true;
                        ResetWall();
                    } else {
                        reset = false;
                        currentWP = (currentWP + 1) % waypoints.transform.childCount;
                    }
                }
            } else {
                if (rTimer <= 0) {
                    if (reset == true) {
                        resetting = false;
                    }

                    rTimer = 3f;
                } else {
                    if (rTimer <= 1.5f && side != null) {
                        sp.ResetWall(side);
                    }

                    rTimer -= Time.deltaTime;
                }
            }
        } else {
            if (fTimer <= 0) {
                frozen = false;
                fTimer = 5f;
            } else {
                transform.Rotate(0, 5, 0);

                fTimer -= Time.deltaTime;
            }
        }
    }

    void ResetWall() {
        Physics.Raycast(transform.position, -transform.right, out RaycastHit lrh, 5f);
        Physics.Raycast(transform.position, transform.right, out RaycastHit rrh, 5f);

        if (lrh.transform != null) {
            transform.Rotate(0, -90, 0);
            side = lrh.transform;
        } else if (rrh.transform != null) {
            transform.Rotate(0, 90, 0);
            side = rrh.transform;
        } else {
            side = null;
        }

        reset = true;
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.name.Contains("PaintBall")) {
            frozen = true;
        }
    }
}
