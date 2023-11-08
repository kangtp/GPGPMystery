using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fadeinout : MonoBehaviour
{

    public CanvasGroup canvasGroup;
    public bool fadein = false;
    public bool fadeout = false;

    public float timetofade;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(fadein)
        {
            if(canvasGroup.alpha < 1)
            {
                canvasGroup.alpha += timetofade * Time.deltaTime;
                if(canvasGroup.alpha >= 1)
                {
                    fadein = false;
                }
            }
        }
        if(fadeout)
        {
            if(canvasGroup.alpha >= 0)
            {
                canvasGroup.alpha -= timetofade * Time.deltaTime;
                if(canvasGroup.alpha == 0)
                {
                    fadein = false;
                }
            }
        }
        
    }

    public void fadeIn()
    {
        fadein = true;
    }
    public void fadeOut()
    {
        fadeout = true;
    }
}
