using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class fastView : MonoBehaviour
{

    public void fastScreen()
    {
        Time.timeScale = 2;
    }

    public void originScreen()
    {
        Time.timeScale = 1;
    }

}
