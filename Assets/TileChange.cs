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
        if (Input.GetMouseButtonDown(0)) // 마우스 왼쪽 버튼 클릭을 감지
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); // 화면 좌표를 월드 좌표로 변환
            Vector3Int cellPosition = grid.WorldToCell(mousePosition); // 월드 좌표를 그리드 셀 좌표로 변환
                                                                       // cellPosition을 사용하여 선택한 셀의 위치 정보를 얻을 수 있음
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
            //Debug.Log(hit.collider.name);
        }
    }
}
