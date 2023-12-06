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
    private bool move = false;
    private float origin;

    static public Boss Instance;
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        audioSource = GetComponent<AudioSource>();
        boss = FindObjectOfType<TileArray>().boss;
        player = FindObjectOfType<TileArray>().player;
        origin = Vector3.Distance(boss.transform.position, player.transform.position);
        count.Instance.fixMaxValue(boss_count);
    }


    public void Boss_Die()
    {
            TileArray.Instance.Touchable = false;
            FindObjectOfType<count>().isOver = true;
            audioSource.Play();
            move = true;
            StartCoroutine(MoveBoss());
            GameOver();
    }

    void GameOver()
    {
        Debug.Log("GameOver!!!");
    }

    IEnumerator MoveBoss()
    {
        while (true)
        {
            Vector3 direction = player.transform.position - boss.transform.position;
            direction.Normalize();
            yield return new WaitForSeconds(0.03f);
            float distance = Vector3.Distance(boss.transform.position, player.transform.position);
            if (distance > (origin * 0.5))
            {
                boss.transform.localScale *= 1.1f;
            }
            else
            {
                boss.transform.localScale *= 0.9f;
            }
            boss.transform.Translate(direction * 100 * Time.deltaTime);
            
            if (distance < 1)
            {
                move = false;
                ShakeScreen.Instance.Callshake(); // 화면 흔들림 함수 호출
                yield return new WaitForSeconds(2f);
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            yield return null;
        }
    }

}
