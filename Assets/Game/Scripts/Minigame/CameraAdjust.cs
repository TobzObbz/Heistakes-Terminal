using UnityEngine;

public class CameraAdjust : MonoBehaviour
{
    private float targetAspect = 16f / 9f;
    private Camera cam;

    private void Start()
    {
        cam = GetComponent<Camera>();

        float windowAspect = (float)Screen.width / Screen.height;
        float scaleHeight = windowAspect / targetAspect;

        if (scaleHeight < 1.0f)
        {
            cam.orthographicSize = cam.orthographicSize / scaleHeight;
        }
    }
}