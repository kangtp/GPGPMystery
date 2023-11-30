using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    Image img;
    Vector3 origin;
    float timer = 0;
    // Start is called before the first frame update
    void Start()
    {
        img = GetComponent<Image>();
        StartCoroutine(appear());
        origin = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("wall_0") || hit.collider.CompareTag("wall_1"))
                {
                    StopAllCoroutines();
                    FindObjectOfType<clickTuto>().appear();
                    transform.gameObject.SetActive(false);
                }
            }
        }
    }
    IEnumerator appear()
    {
        yield return new WaitForSeconds(4f);
        img.color = new Color(img.color.r, img.color.g, img.color.b, 1f);
        yield return new WaitForSeconds(1f);
        timer = 0;
        while (timer < 1.5f)
        {
            yield return new WaitForSeconds(0.05f);
            transform.Translate(Vector3.left * 1000 * Time.deltaTime);
            timer += Time.deltaTime;
        }
        yield return new WaitForSeconds(1f);
        transform.position = origin;
        StartCoroutine(moving());
    }

    IEnumerator moving()
    {
        yield return new WaitForSeconds(1f);
        timer = 0;
        while (timer < 1.5f)
        {
            yield return new WaitForSeconds(0.05f);
            transform.Translate(Vector3.left * 1000 * Time.deltaTime);
            timer += Time.deltaTime;
        }
        yield return new WaitForSeconds(1f);
        transform.position = origin;
        StartCoroutine(moving());
    }
}
