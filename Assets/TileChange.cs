using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileChange : MonoBehaviour
{
    //Grid grid;
    public Tilemap tilemap;
    public TileBase tileBase;
    private Vector3 wallPosition;
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
            Debug.Log("Screen : " + Input.mousePosition);
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); // ȭ�� ��ǥ�� ���� ��ǥ�� ��ȯ
            Debug.Log("World : " + mousePosition);
            //Vector3Int cellPosition = WorldToCell(mousePosition); // ���� ��ǥ�� �׸��� �� ��ǥ�� ��ȯ
            //Debug.Log(cellPosition);
            //ChangeTile(new Vector3Int((int)wallPosition.x, (int)wallPosition.y,0));

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
            //Debug.Log(hit.collider.transform.position);
            wallPosition = hit.collider.transform.position;
            //�� Ÿ�ϸ� Int ��ǥ
            Vector3Int wallPos = new Vector3Int((int)(wallPosition.x - 0.5), (int)(wallPosition.y - 0.5), 0);

            //�� �� Ÿ�� ��ǥ
            Vector3Int[] wallBackPos = new Vector3Int[5];
            for(int i = 1; i < wallBackPos.Length + 1; i++)
            {
                ChangeTile(new Vector3Int (wallPos.x - i, wallPos.y, 0));
            }
        }
    }
}
