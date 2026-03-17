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

        if (windowAspect >= targetAspect)
        {
            cam.orthographicSize = targetHeight / 200f;
        }
        else
        {
            float scaleHeight = targetAspect / windowAspect;
            cam.orthographicSize = (targetHeight / 200f) * scaleHeight;
        }
    }
}