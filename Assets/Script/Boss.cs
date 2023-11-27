using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    AudioSource audioSource;
    public int boss_count;
    public GameObject boss;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        boss = FindObjectOfType<TileArray>().boss;
        player = FindObjectOfType<TileArray>().player;

        count.Instance.fixMaxValue(boss_count);
    }


    public void reduceCount()
    {
        boss_count -= 1;
        count.Instance.fillBar();
        CheckCount();
    }

    void CheckCount()
    {
        if (boss_count == -1)
        {
            TileArray.Instance.Touchable = false;
            FindObjectOfType<count>().isOver = true;
            //ȣ���� ����
            audioSource.Play();
            StartCoroutine(MoveBoss());
            GameOver();
            boss_count = -2;
        }
    }

    void GameOver()
    {
        Debug.Log("GameOver!!!");
    }

    IEnumerator MoveBoss()
    {
        float origin = Vector3.Distance(boss.transform.position, player.transform.position);
        while (true)
        {
            Vector3 direction = player.transform.position - boss.transform.position;
            direction.Normalize();
            float distance = Vector3.Distance(boss.transform.position, player.transform.position);
            yield return new WaitForSeconds(0.05f);
            if(distance > (origin * 0.85))
            {
                boss.transform.localScale *= 1.2f;
            }
            else
            {
                boss.transform.localScale *= 0.93f;
            }
            boss.transform.Translate(direction * 100 * Time.deltaTime);
            if (distance < 1)
            {
                ShakeScreen.Instance.Callshake(); // 화면 흔들림 함수 호출
                yield return new WaitForSeconds(2f);
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }
}
