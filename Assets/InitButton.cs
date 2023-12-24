using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitButton : MonoBehaviour
{
    public void Init()
    {
        PlayerPrefs.DeleteKey("Stage");
        PlayerPrefs.DeleteKey("Exist");
    }
}
