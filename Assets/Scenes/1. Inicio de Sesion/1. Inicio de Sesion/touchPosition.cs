using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class touchPosition : MonoBehaviour
{
    public Text texto;
    public float x;
    public float y;


    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            
            Debug.Log("Touch Position : " + touch.position);
            texto.text = "Touch Position : " + touch.position;
            x = touch.position.x;
            y = touch.position.y;
        }
    }
}
