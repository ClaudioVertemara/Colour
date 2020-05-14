using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Project: Colour
 * Code Author: Claudio Vertemara
*/

public class GrabColor : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // On Keypress 'Spacebar' Get the Color of the Gameobject in Front of Where the Player Object is Looking At
        if (Input.GetKey(KeyCode.E)) {
            Physics.Raycast(transform.position, transform.forward, out RaycastHit rh, 3f);

            if (rh.transform != null && rh.transform.gameObject.CompareTag("GrabableColor")) {
                gameObject.GetComponent<ShootPaint>().AddColor(rh.transform.gameObject.GetComponent<Renderer>().material.color);
            }
        }
    }
}
