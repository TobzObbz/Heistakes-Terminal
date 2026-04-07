using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraAdjust : MonoBehaviour
{
    private float targetWidth = 1920f;
    private float targetHeight = 1080f;

    private Camera cam;

    private void Start()
    {
        cam = GetComponent<Camera>();

        float targetAspect = targetWidth / targetHeight;
        float windowAspect = (float)Screen.width / Screen.height;

        float baseSize = targetHeight / 200f;

        if (windowAspect > targetAspect)
        {
            cam.orthographicSize = baseSize;
        }
        else
        {
            float scaleHeight = targetAspect / windowAspect;
            cam.orthographicSize = baseSize * scaleHeight;
        }
    }
}