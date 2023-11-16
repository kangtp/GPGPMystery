using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameStartBtn : MonoBehaviour
{

    public Image[] buttons;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameStart()
    {
        for(int i = 0; i < buttons.Length; i++) 
        {
            buttons[i].enabled = false;
        }
        GameObject.Find("Ani").transform.GetChild(0).gameObject.SetActive(true);
        StartCoroutine(StartGame());
        
    }

    IEnumerator StartGame()
    {
        //1.5�� �ڿ� �����޴��� �̵�
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("Level Menu");
    }
}
