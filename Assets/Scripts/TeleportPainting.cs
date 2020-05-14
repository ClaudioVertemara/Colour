using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Project: Colour
 * Code Author: Claudio Vertemara
*/

public class TeleportPainting : MonoBehaviour
{
    public float x;
    public float z;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.name == "Node") {
            collision.transform.position = new Vector3(x, 2.2f, z);
        }
    }
}
