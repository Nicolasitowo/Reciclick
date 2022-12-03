using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class url : MonoBehaviour
{
    public void OpenURL()
    {
        Application.OpenURL("http://www.ulearnet.org");
        Debug.Log("is this working?");
    }
}
