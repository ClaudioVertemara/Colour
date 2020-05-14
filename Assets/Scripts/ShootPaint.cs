using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * Project: Colour
 * Code Author: Claudio Vertemara
 * 
 * Paint Code Source: https://github.com/lukakostic/UnySplat 
*/

public class ShootPaint : MonoBehaviour
{
    //public static Texture2D tex;
    public static System.Random r;
    public GameObject paintBall;

    public GameObject paintLevel;
    public Transform spawnPoint;
    public static Texture2D[] texArr;

    public static Dictionary<string, int> surfaceNames;

    List<Color> colArr;
    List<float> colAmount;
    int colIndex;

    float shootTimer = 0.2f;

    public Image colorImg;
    public int maxFillAmount;

    Material leftEye;
    Material rightEye;

    // Use this for initialization
    void Start() {
        r = new System.Random();
        leftEye = transform.GetChild(0).GetComponent<MeshRenderer>().materials[8];
        rightEye = transform.GetChild(0).GetComponent<MeshRenderer>().materials[9];
        //tex = new Texture2D(1200, 1200);

        surfaceNames = new Dictionary<string, int>();
        texArr = new Texture2D[paintLevel.transform.childCount];
        for (int i = 0; i < paintLevel.transform.childCount; i++) {
            //texArr[i] = new Texture2D(100, 100);
            //texArr[i] = new Texture2D((int)(paintLevel.transform.GetChild(i).localScale.x * 100), (int)(paintLevel.transform.GetChild(i).localScale.z * 100));
            surfaceNames.Add(paintLevel.transform.GetChild(i).name, i);

            Collider childCol = paintLevel.transform.GetChild(i).GetComponent<Collider>();
            int scale = 30;

            if (childCol.CompareTag("Floor")) {
                texArr[i] = new Texture2D((int)(childCol.bounds.size.x * scale), (int)(childCol.bounds.size.z * scale));
            } else if (childCol.CompareTag("XYWall")) {
                texArr[i] = new Texture2D((int)(childCol.bounds.size.x * scale), (int)(childCol.bounds.size.y * scale));
            } else if (childCol.CompareTag("ZYWall")) {
                texArr[i] = new Texture2D((int)(childCol.bounds.size.z * scale), (int)(childCol.bounds.size.y * scale));
            }

            // Sets all Textures to White
            if (texArr[i].width > 0 && texArr[i].height > 0) {
                Color[] texColors = new Color[texArr[i].width * texArr[i].height];
                for (int j = 0; j < texColors.Length; j++) {
                    texColors[j] = new Color(1, 1, 1);
                }

                texArr[i].SetPixels(texColors);
                texArr[i].Apply();
            }

            //texArr[i] = new Texture2D((int)(1000), (int)(1000));
        }

        colIndex = 0;
        colArr = new List<Color>();
        colAmount = new List<float>();
    }

    private void Update() {
        shootTimer -= Time.deltaTime;

        if (colArr.Count > 0) {

            // On Left Mouse Click, Shoot the Paint Ball
            if (Input.GetMouseButton(0) && shootTimer < 0 && colAmount[colIndex] > 0) {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                GameObject ball = Instantiate(paintBall, spawnPoint.position, transform.rotation);

                if (Physics.Raycast(ray, out RaycastHit rh)) {
                    ball.transform.LookAt(rh.point);
                } else {
                    ball.transform.LookAt(ray.origin + (ray.direction * 10f));
                }

                ball.GetComponent<Renderer>().material.color = colArr[colIndex];
                ball.GetComponent<PaintBall>().SetColor(colArr[colIndex]);

                ball.GetComponent<Rigidbody>().velocity = ball.transform.forward * 15f;

                shootTimer = 0.2f;
                colAmount[colIndex]--;
                colorImg.fillAmount = colAmount[colIndex] / maxFillAmount;
            }

            // On Right Mouse Click, Go through Color List
            if (Input.GetMouseButtonDown(1)) {
                colIndex = (colIndex + 1) % colArr.Count;

                colorImg.color = colArr[colIndex];

                colorImg.fillAmount = colAmount[colIndex] / maxFillAmount;

                leftEye.SetColor("_Color", colArr[colIndex]);
                rightEye.SetColor("_Color", colArr[colIndex]);
            }
        }
    }

    // Adds Color to Player's Color List
    public void AddColor(Color color) {
        if (!colArr.Contains(color)) {
            colArr.Add(color);
            colAmount.Add(maxFillAmount);
        } else {
            colAmount[colArr.IndexOf(color)] = maxFillAmount;
        }

        colIndex = colArr.IndexOf(color);

        colorImg.color = color;
        colorImg.fillAmount = colAmount[colIndex] / maxFillAmount;
        leftEye.SetColor("_Color", color);
        rightEye.SetColor("_Color", color);
    }

    //Reset Wall Color to White (Used by Enemies)
    public void ResetWall(Transform wall) {
        int index = 0;

        for (int i = 0; i < paintLevel.transform.childCount; i++) {
            if (paintLevel.transform.GetChild(i).name == wall.name) {
                index = i;
                break;
            }
        }

        Color[] texColors = new Color[texArr[index].width * texArr[index].height];
        for (int j = 0; j < texColors.Length; j++) {
            texColors[j] = new Color(1, 1, 1);
        }

        texArr[index].SetPixels(texColors);
        texArr[index].Apply();

        wall.GetComponent<MeshRenderer>().material.SetTexture("_MainTex", texArr[index]);
    }
}
