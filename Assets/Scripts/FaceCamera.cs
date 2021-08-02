using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FaceCamera : MonoBehaviour
{
    public Camera camera;

    // Update is called once per frame
    void Update()
    {
        //transform.forward = Camera.main.transform.forward;
        transform.rotation = Quaternion.Euler(0, camera.transform.eulerAngles.y, 0);
    }
}
