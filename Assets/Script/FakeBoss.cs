using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FakeBoss : MonoBehaviour
{
    AudioSource audioSource;
    public int boss_count;
    private float origin;


    static public FakeBoss Instance;
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        count.Instance.fixMaxValue(boss_count);
    }


    public void Boss_Die()
    {
        TileArray.Instance.Touchable = false;
        FindObjectOfType<count>().isOver = true;
        //audioSource.Play();
        StartCoroutine(Done());
    }

    IEnumerator Done()
    {
        ShakeScreen.Instance.Callshake(); // 화면 흔들림 함수 호출
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
