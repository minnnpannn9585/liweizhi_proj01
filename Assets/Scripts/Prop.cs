using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prop : MonoBehaviour
{
    bool followMouse = false;
    private Vector3 mousePos;
    
    private void OnMouseDown()
    {
        followMouse = true;
        GameObject.Find("GameManager").GetComponent<GameManager>().canDestroyScar = true;
        GetComponent<Collider2D>().enabled = false;
    }

    private void Update()
    {
        if (followMouse)
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            transform.position = mousePos;
        }
    }
}
