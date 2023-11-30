using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class fastView : MonoBehaviour
{
    public static fastView Instance;
    public GameObject GoBtn;

    public GameObject FastBtn;

    private AudioSource audioSource;

    private void Start() {
        Instance = this;
    }

    public void switchBtn()
    {
        GoBtn.SetActive(false);
        FastBtn.SetActive(true);
    }

    public void fastScreen()
    {
        PathManager.Instance.walkSound.pitch = 4;
        Time.timeScale = 4;
    }

    public void originScreen()
    {
        Time.timeScale = 1;
    }

}
