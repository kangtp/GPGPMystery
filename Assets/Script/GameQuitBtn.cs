using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameQuitBtn : MonoBehaviour
{
    // Start is called before the first frame update

    public void GameQuit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
