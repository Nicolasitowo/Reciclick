using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class resolutionAdapter : MonoBehaviour
{
    public Canvas canv;
    // Start is called before the first frame update
    void Start()
    {
        double aspectRatio = (float)Screen.width / (float)Screen.height;
        Debug.Log(aspectRatio);


        if (aspectRatio >= 2.11f && aspectRatio < 2.33f)
        {
            canv.GetComponent<CanvasScaler>().matchWidthOrHeight = 0.375f;
        }
        else if (aspectRatio >= 2.33f) {
            canv.GetComponent<CanvasScaler>().matchWidthOrHeight = 0.492f;
        }
        else if (aspectRatio >= 2 && aspectRatio < 2.11f)
        {
            canv.GetComponent<CanvasScaler>().matchWidthOrHeight = 0.3f;
        }

    }

    
}
