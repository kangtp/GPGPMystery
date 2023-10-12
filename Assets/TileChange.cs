using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileChange : MonoBehaviour
{
    Grid grid;
    public Tilemap tilemap;
    public TileBase tileBase;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetMouseButtonDown(0))
        //{
            
        //    Vector3Int point = new Vector3Int((int)Input.mousePosition.x, (int)Input.mousePosition.y, 0);
        //    ChangeTile(point);
        //    Debug.Log("Click : " + point);
        //}
        if (Input.GetMouseButtonDown(0)) // ���콺 ���� ��ư Ŭ���� ����
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); // ȭ�� ��ǥ�� ���� ��ǥ�� ��ȯ
            Vector3Int cellPosition = grid.WorldToCell(mousePosition); // ���� ��ǥ�� �׸��� �� ��ǥ�� ��ȯ
                                                                       // cellPosition�� ����Ͽ� ������ ���� ��ġ ������ ���� �� ����
        }

    }

    public void ChangeTile(Vector3Int tilePosition) //�� tilePosition�� ��� �������İ� ���� ������ǥ�� �׸��� ��ǥ�� �ٸ��� ����
    {
        tilemap.SetTile(tilePosition, tileBase);
    }

    private void FixedUpdate()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.left, 10, LayerMask.GetMask("wall"));
        if (hit.collider != null)
        {
            //Debug.Log(hit.collider.name);
        }
    }
}
