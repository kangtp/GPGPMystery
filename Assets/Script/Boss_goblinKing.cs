using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boss_goblinKing : MonoBehaviour
{
    AudioSource audioSource;
    public int boss_count;
    public GameObject player;
    public GameObject boss;
    public Sprite goblinRock;

    private bool move = false;
    private float origin;

    static public Boss_goblinKing Instance;

    void Start()
    {
        Instance = this;
        audioSource = GetComponent<AudioSource>();

        player = FindObjectOfType<TileArray>().player;
        boss = FindObjectOfType<TileArray>().boss;
        origin = Vector3.Distance(boss.transform.position, player.transform.position);
        count.Instance.fixMaxValue(boss_count);
    }


    public void Boss_Die()
    {
            TileArray.Instance.Touchable = false;
            FindObjectOfType<count>().isOver = true;
            audioSource.Play();
            transform.gameObject.GetComponent<Animator>().enabled = false;
            transform.gameObject.GetComponent<SpriteRenderer>().sprite = goblinRock;
            move = true;
            StartCoroutine(GoblinAttack());
            boss_count = -2;
    }

    IEnumerator GoblinAttack()
    {
        while (true)
        {

            //Vector3 direction = player.transform.position - boss.transform.position;
            Vector3 direction = Vector3.left;
            direction.Normalize();
            yield return new WaitForSeconds(0.03f);
            float distance = Vector3.Distance(boss.transform.position, player.transform.position);

            boss.transform.Translate(direction * 100 * Time.deltaTime);

            if (distance < 1.5f)
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
