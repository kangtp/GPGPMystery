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
        if (Input.GetMouseButtonDown(0)) // 마우스 왼쪽 버튼 클릭을 감지
        {
            Debug.Log("Screen : " + Input.mousePosition);
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); // 화면 좌표를 월드 좌표로 변환
            Debug.Log("World : " + mousePosition);
            //Vector3Int cellPosition = WorldToCell(mousePosition); // 월드 좌표를 그리드 셀 좌표로 변환
            //Debug.Log(cellPosition);
            //ChangeTile(new Vector3Int((int)wallPosition.x, (int)wallPosition.y,0));

        }

    }

    public void ChangeTile(Vector3Int tilePosition) //이 tilePosition을 어떻게 가져오냐가 문제 월드좌표와 그리드 좌표는 다르기 때문
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
            //벽 타일맵 Int 좌표
            Vector3Int wallPos = new Vector3Int((int)(wallPosition.x - 0.5), (int)(wallPosition.y - 0.5), 0);

            //벽 뒤 타일 좌표
            Vector3Int[] wallBackPos = new Vector3Int[5];
            for(int i = 1; i < wallBackPos.Length + 1; i++)
            {
                ChangeTile(new Vector3Int (wallPos.x - i, wallPos.y, 0));
            }
        }
    }
}
