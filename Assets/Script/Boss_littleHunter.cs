using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boss_littleHunter : MonoBehaviour
{
    AudioSource audioSource;
    public int boss_count;
    public GameObject player;
    public GameObject boss;
    public GameObject fire;
    public Sprite hunterSide;

    private bool move = false;
    private float origin;

    static public Boss_littleHunter Instance;

    void Start()
    {
        Instance = this;
        audioSource = GetComponent<AudioSource>();

        GameObject prefab = Resources.Load("fireThrow") as GameObject;
        fire = Instantiate(prefab, Vector3.zero, Quaternion.identity) as GameObject;
        fire.transform.position = transform.position;
        player = FindObjectOfType<TileArray>().player;
        boss = FindObjectOfType<TileArray>().boss;
        //target = FindObjectOfType<TileArray>().player.transform;
        origin = Vector3.Distance(fire.transform.position, player.transform.position);
        count.Instance.fixMaxValue(boss_count);
    }


    public void Boss_Die()
    {

            TileArray.Instance.Touchable = false;
            FindObjectOfType<count>().isOver = true;
            audioSource.Play();
            transform.gameObject.GetComponent<Animator>().enabled = false;
            transform.gameObject.GetComponent<SpriteRenderer>().sprite = hunterSide;
            move = true;
            StartCoroutine(ThrowFire());
            Debug.Log("Throw");
            GameOver();
            boss_count = -2;
    }

    void GameOver()
    {
        Debug.Log("GameOver!!!");
    }

    IEnumerator ThrowFire()
    {
        while (true)
        {
            
            Debug.Log("Throw");
            Vector3 direction = player.transform.position - fire.transform.position;
            direction.Normalize();
            yield return new WaitForSeconds(0.03f);
            float distance = Vector3.Distance(fire.transform.position, player.transform.position);
            fire.transform.localScale = new Vector3(1, 1, 1);
            
            fire.transform.Translate(direction * 100 * Time.deltaTime);

            if (distance < 1)
            {
                move = false;
                ShakeScreen.Instance.Callshake(); // ȭ�� ��鸲 �Լ� ȣ��
                yield return new WaitForSeconds(2f);
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            yield return null;

        }
    }

}
