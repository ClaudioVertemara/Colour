using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Project: Colour
 * Code Author: Claudio Vertemara
 * 
 * Paint Code Source: https://github.com/lukakostic/UnySplat 
*/

public class PaintBall : MonoBehaviour
{
    SphereCollider sc;

    int index;
    Color color;

    // Use this for initialization
    void Start() {
        Destroy(gameObject, 3f);
        sc = GetComponent<SphereCollider>();
    }

    public void SetColor(Color color) {
        this.color = color;
    }

    private void OnCollisionEnter(Collision collision) {

        sc.enabled = false;
        //Debug.Log(gameObject.name + "-" + collision.gameObject.name);

        if (collision.transform.parent != null && collision.transform.parent.CompareTag("Paintable")) {
            /*for (int i = 0; i < collision.transform.parent.transform.childCount; i++) {
                if (collision.transform.parent.transform.GetChild(i).name == collision.gameObject.name) {
                    index = i;
                    break;
                }
            }*/
            index = ShootPaint.surfaceNames[collision.gameObject.name];

            for (int i = 0; i < 25; i++) {
                var dir = new Vector3((float)(ShootPaint.r.NextDouble() * 2.0 - 1.0), (float)(ShootPaint.r.NextDouble() * 2.0 - 1.0), (float)(ShootPaint.r.NextDouble() * 2.0 - 1.0));
                var dir2 = collision.GetContact(0).point - transform.position;

                PaintRay(dir * 0.4f + dir2);
                PaintRay(dir * 0.2f + dir2);
            }

            ShootPaint.texArr[index].Apply();

            collision.gameObject.GetComponent<MeshRenderer>().material.SetTexture("_MainTex", ShootPaint.texArr[index]);

            GameObject.Find("SplatAudio").GetComponent<AudioSource>().Play();
        }

        Destroy(gameObject);
    }

    public void PaintRay(Vector3 dir) {
        Physics.Raycast(transform.position, dir, out RaycastHit rh, 0.7f);

        if (rh.transform != null) {
            int x = (int)(rh.textureCoord.x * ShootPaint.texArr[index].width);
            int y = (int)(rh.textureCoord.y * ShootPaint.texArr[index].height);

            RandomBrush(x, y, 3, 4);
        }
    }

    public void RandomBrush(int x, int y, int taps, int radius) {
        for (int i = 0; i < (taps + 1); i++) {
            float rx = (float)ShootPaint.r.NextDouble() * (float)radius;

            Vector3 point = new Vector3(rx, 0, rx);
            int ry = ShootPaint.r.Next(360);
            point = Quaternion.AngleAxis(ry, Vector3.up) * point;


            ShootPaint.texArr[index].SetPixel(x + (int)point.x, y + (int)point.z, color);
        }

    }
}
