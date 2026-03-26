using UnityEngine;

public class ImageFollowMouse : MonoBehaviour
{
    [Tooltip("如果不为空，则使用该相机把屏幕坐标转换为世界坐标；否则使用 Camera.main。")]
    [SerializeField] private Camera targetCamera;

    [Tooltip("物体在世界坐标中的 Z 值（2D 场景一般为 0）。")]
    [SerializeField] private float zPosition = 0f;

    private void Awake()
    {
        if (targetCamera == null)
        {
            targetCamera = Camera.main;
        }
    }

    private void Update()
    {
        if (targetCamera == null) return;

        var worldPos = targetCamera.ScreenToWorldPoint(Input.mousePosition);
        worldPos.z = zPosition;
        transform.position = worldPos;
    }
}