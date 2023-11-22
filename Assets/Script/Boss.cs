using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    public int boss_count;
    public GameObject boss;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        boss = FindObjectOfType<TileArray>().boss;
        player = FindObjectOfType<TileArray>().player;
    }

    // Update is called once per frame
    void Update()
    {
        CheckCount();
    }

    void CheckCount()
    {
        if (boss_count == -1)
        {
            //호랑이 공격
            StartCoroutine(MoveBoss());
            GameOver();
            //게임 오버
            boss_count = -2;
        }
    }

    void GameOver()
    {
        Debug.Log("GameOver!!!!!!!!!!!!!");
        //
    }

    IEnumerator MoveBoss()
    {
        float distance;
        while (true)
        {
            Vector3 direction = player.transform.position - boss.transform.position;
            direction.Normalize();
            distance = Vector3.Distance(boss.transform.position, player.transform.position);
            yield return new WaitForSeconds(0.05f);
            boss.transform.Translate(direction * 100 * Time.deltaTime);
            Debug.Log("ing~");
            if (distance < 0.5)
            {
                Debug.Log("break!!!!!");
                yield return new WaitForSeconds(2f);
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
        


    }
}
