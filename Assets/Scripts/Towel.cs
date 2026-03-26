using UnityEngine;

public class Towel : MonoBehaviour
{
    private bool followMouse = false;
    private Vector3 mousePos;

    private void OnMouseDown()
    {
        followMouse = true;

        var gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        gm.towelSelected = true;

        // 不要禁用 Collider2D，否则 Dirt 无法检测到 IsTouching
        // 改为：切到 Ignore Raycast 层，避免再次点到毛巾
        gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
    }

    private void Update()
    {
        if (!followMouse) return;

        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        transform.position = mousePos;
    }
}