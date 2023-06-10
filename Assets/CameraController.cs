using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Camera cam;

    void Update()
    {
        var touches = Input.touches;

        if(touches.Length>=2)
            Zoom(touches);
    }

    private void Drag(Touch[] touches)
    {
        var touch = Input.GetTouch(0);
        cam.transform.position -= (Vector3) touch.deltaPosition * 0.001f;
    }

    private void Zoom(Touch[] touches)
    {
        var prevPos0 = touches[0].position - touches[0].deltaPosition;
        var prevPos1 = touches[1].position - touches[1].deltaPosition;
        var previousDistance = Vector2.Distance(prevPos0, prevPos1);
        var currentDistance = Vector2.Distance(touches[0].position, touches[1].position);
        var deltaDistance = currentDistance - previousDistance;
    
        if (cam.orthographic)
        {
            cam.orthographicSize -= deltaDistance*0.1f;
            cam.orthographicSize = Mathf.Clamp(cam.orthographicSize, 2, 15);
        }
        else
        {
            cam.transform.position -= Vector3.up * deltaDistance * 0.01f;
            var y = Mathf.Clamp(cam.transform.position.y, 10, 100);
            cam.transform.position = new Vector3(cam.transform.position.x, y, cam.transform.position.z);
        }
    }
}
