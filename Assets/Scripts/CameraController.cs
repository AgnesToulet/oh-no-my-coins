using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public static CameraController instance = null;

    public int waitingZoomTime = 2;
    private int zoomSize = 7;
    private int normalSize = 4;

    public bool isZoomed;
    
    private Camera _camera;

    private float zoomTime = 1f / 0.2f;

    void Awake () {
        if (instance == null) {
            instance = this;
        } else if (instance != this) {
            Destroy(gameObject);
        }
        _camera = GetComponent<Camera>();
    }

    // void Update () {
    //     if (isZoomed) {
    //         if (up) {
    //             elapsed += Time.deltaTime / 1.0f;
    //             _camera.orthographicSize = normalSize + 2 * elapsed;
    //             if (elapsed > 1.0f) {
    //                 up = false;
    //                 elapsed = 0;
    //             }
    //         } 
            
    //         if (!up) {
    //             elapsed += Time.deltaTime / 1.0f;
    //             _camera.orthographicSize = zoomSize - 2 * elapsed;

    //             if (elapsed > 1.0f) {
    //                 up = true;
    //                 isZoomed = false;
    //                 elapsed = 0;
    //             } 
    //         }
    //     }
    // }

    protected IEnumerator SmoothZoom (float orthographicSize) {
        isZoomed = true;
        float distance = Mathf.Abs(_camera.orthographicSize - orthographicSize);
        while (distance > float.Epsilon) {
            float newZoomSize = Mathf.MoveTowards(_camera.orthographicSize, orthographicSize, zoomTime * Time.deltaTime);
            // Debug.Log("new zoom: " + newZoomSize);
            _camera.orthographicSize = newZoomSize;

            distance = Mathf.Abs(_camera.orthographicSize - orthographicSize);
            yield return null;
        }

        if (orthographicSize != normalSize) {
            StartCoroutine(SmoothZoom(normalSize));
        } else {
            isZoomed = false;
        }
    }

    public void Zoom() {
        isZoomed = true;
        StartCoroutine(SmoothZoom(zoomSize));
    }

}
