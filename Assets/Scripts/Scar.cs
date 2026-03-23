using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scar : MonoBehaviour
{
    private void OnMouseDown()
    {
        if (GameObject.Find("GameManager").GetComponent<GameManager>().canDestroyScar)
        {
            Destroy(gameObject);
        }
        
    }
}
