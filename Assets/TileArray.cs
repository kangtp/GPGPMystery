using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileArray : MonoBehaviour
{
    //90' rotate to left
    public int[,] tileMap = new int[,]
    {
         {2,2,2},   
         {2,2,0},
         {2,2,2}
    };

    public float TileSize;
    public Vector2 StartPoint;//top left corner of the map

    // Use this for initialization
    void Start()
    {
        TileSize = 1;
        StartPoint = new Vector2(0, 0);
        PopulateTileMap();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
            int tem = tileMap[1, 1];
            tileMap[1, 1] = tileMap[0, 0];
            tileMap[0, 0] = tem;
            PopulateTileMap();
        }
    }

    public void PopulateTileMap()
    {
        for (int i = 0; i < tileMap.GetLength(1); i++)
        {
            for (int j = 0; j < tileMap.GetLength(0); j++)
            {

                GameObject prefab = Resources.Load("tile_" + tileMap[i, j].ToString()) as GameObject;
                GameObject tile = Instantiate(prefab, Vector3.zero, Quaternion.identity) as GameObject;
                tile.transform.position = new Vector3(StartPoint.x + (TileSize * j) + (TileSize / 2), StartPoint.y - (TileSize * i) - (TileSize / 2), 0);

            }
        }
    }
}
