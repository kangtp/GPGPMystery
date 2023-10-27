using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class TileArray : MonoBehaviour
{

    enum TileType { ground, shadow, edge, fire, wall};

    //90' rotate to left
    public int[,] tileMap = new int[,]
    {
         {2,2,2,2,3,2,2,2,2,2},
         {2,0,0,0,0,0,0,0,0,2},
         {2,0,0,0,0,0,0,0,0,2},
         {2,0,0,0,0,0,0,0,0,2},
         {2,0,0,0,0,0,0,0,0,3},
         {2,0,0,0,0,0,0,0,0,2},
         {3,0,0,0,0,0,0,0,0,2},
         {2,0,0,0,0,0,0,0,0,2},
         {2,0,0,0,0,0,0,0,0,2},
         {2,2,2,2,2,2,2,3,2,2}

    };

    public int[,] wallMap = new int[,]
    {
         {0,0,0,0,0,0,0,0,0,0},
         {0,0,0,0,0,0,0,0,0,0},
         {0,0,0,0,0,0,0,0,0,0},
         {0,1,0,0,0,0,0,0,1,0},
         {0,0,0,0,0,0,0,0,0,0},
         {0,0,1,0,0,0,0,0,0,0},
         {0,0,0,0,0,0,0,0,1,0},
         {0,0,0,0,0,0,0,0,0,0},
         {0,0,0,0,1,0,0,0,0,0},
         {0,0,0,0,0,0,0,0,0,0}
    };


    public float TileSize;

    public float wallSize;

    private int getWall_x, getWall_y;

    public Vector2 StartPoint;//top left corner of the map

    private Vector2 dragStartPosition;

    private GameObject get_Wall;

    public GameObject[,] tilePrefab = new GameObject[10,10];
    public Sprite shadowSprite;
    public Sprite groundSprite;

    private bool detectedWall = false;


    // Use this for initialization
    void Start()
    {
        TileSize = 1;
        wallSize = 1;
        PopulateTileMap();
        PopulatewallMap();
        makeShadow();

    }

    // Update is called once per frame
    void Update()
    {
        moveWall();
        makeShadow();
    }


    public void PopulateTileMap()
    {
        for (int i = 0; i < tileMap.GetLength(0); i++)
        {
            for (int j = 0; j < tileMap.GetLength(1); j++)
            {

                GameObject prefab = Resources.Load("tile_" + tileMap[i, j].ToString()) as GameObject;
                GameObject tile = Instantiate(prefab, Vector3.zero, Quaternion.identity) as GameObject;
                tile.transform.position = new Vector2(StartPoint.x + (TileSize * j) + (TileSize / 2), StartPoint.y - (TileSize * i) - (TileSize / 2));
                tilePrefab[i,j] = tile;

            }
        }
    }

    public void PopulatewallMap()
    {
        for (int i = 0; i < wallMap.GetLength(0); i++)
        {
            for (int j = 0; j < wallMap.GetLength(1); j++)
            {
                if (wallMap[i, j] == 1)
                {
                    GameObject prefab = Resources.Load("wall") as GameObject;
                    GameObject wall = Instantiate(prefab, Vector3.zero, Quaternion.identity) as GameObject;
                    wall.GetComponent<wall_Info>().Set(i, j);
                    tileMap[i, j] = 4;
                    wall.transform.position = new Vector2(StartPoint.x + (wallSize * j) + (wallSize / 2), StartPoint.y - (wallSize * i) - (wallSize / 2));

                }

            }
        }
    }

    private void moveWall()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("wall"))
                {
                    get_Wall = hit.transform.gameObject;
                    getWall_x = get_Wall.transform.gameObject.GetComponent<wall_Info>().get_X();
                    getWall_y = hit.transform.gameObject.GetComponent<wall_Info>().get_Y();
                    detectedWall = true;
                }
            }
            dragStartPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.GetMouseButtonUp(0))
        {
            Vector2 dragEndPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 dragDirection = dragEndPosition - dragStartPosition;

            // 상하 드래그 방향 감지
            if (detectedWall)
            {
                if (Mathf.Abs(dragDirection.y) > Mathf.Abs(dragDirection.x))
                {
                    if (dragDirection.y > 0) // UP
                    {
                        detectWallFunc(getWall_x, getWall_y, 'U');
                    }
                    else if (dragDirection.y < 0) // Down
                    {
                        detectWallFunc(getWall_x, getWall_y, 'D');
                    }
                }
                // 좌우 드래그 방향 감지
                else
                {
                    if (dragDirection.x > 0) // Right
                    {
                        detectWallFunc(getWall_x, getWall_y, 'R');
                    }
                    else if (dragDirection.x < 0) // Left
                    {
                        detectWallFunc(getWall_x, getWall_y, 'L');
                    }
                }
            }
        }
    }

    private void detectWallFunc(int x, int y, char v)
    {
        //Debug.Log(getWall_x + "," + getWall_y);
        switch (v)
        {
            case 'U':
                {
                    if (tileMap[x - 1, y] == 0 || tileMap[x - 1, y] == 1)
                    {
                        wallMap[x - 1, y] = 1;
                        wallMap[x, y] = 0;
                        tileMap[x - 1, y] = 4;
                        tileMap[x, y] = 0;
                        get_Wall.GetComponent<wall_Info>().Set(x - 1, y);
                        get_Wall.transform.position = new Vector3(get_Wall.transform.position.x, get_Wall.transform.position.y + 1, 0);
                    }
                }
                break;
            case 'D':
                {
                    if (tileMap[x + 1, y] == 0 || tileMap[x + 1, y] == 1)
                    {
                        wallMap[x + 1, y] = 1;
                        wallMap[x, y] = 0;
                        tileMap[x + 1, y] = 4;
                        tileMap[x, y] = 0;
                        get_Wall.GetComponent<wall_Info>().Set(x + 1, y);
                        get_Wall.transform.position = new Vector3(get_Wall.transform.position.x, get_Wall.transform.position.y - 1, 0);
                    }
                }
                break;
            case 'R':
                {
                    if (tileMap[x, y + 1] == 0 || tileMap[x, y + 1] == 0)
                    {
                        wallMap[x, y + 1] = 1;
                        wallMap[x, y] = 0;
                        tileMap[x, y + 1] = 4;
                        tileMap[x, y] = 0;
                        get_Wall.GetComponent<wall_Info>().Set(x, y + 1);
                        get_Wall.transform.position = new Vector3(get_Wall.transform.position.x + 1, get_Wall.transform.position.y, 0);
                    }
                }
                break;
            case 'L':
                {
                    if (tileMap[x, y - 1] == 0 || tileMap[x, y - 1] == 1)
                    {
                        wallMap[x, y - 1] = 1;
                        wallMap[x, y] = 0;
                        tileMap[x, y - 1] = 4;
                        tileMap[x, y] = 0;
                        get_Wall.GetComponent<wall_Info>().Set(x, y - 1);
                        get_Wall.transform.position = new Vector3(get_Wall.transform.position.x - 1, get_Wall.transform.position.y, 0);
                    }
                }
                break;

            default:
                break;
        }
        detectedWall = false;
        
    }

    public void makeShadow()
    {
        bool findWall = false;
        for (int i = 0; i < wallMap.GetLength(0); i++)  //row
        {
            for (int j = 0; j < wallMap.GetLength(1); j++)  //column
            {
                if (tileMap[i, j] == (int)TileType.fire)
                {
                    //맨 윗줄에 불이 있을 때
                    if (i == 0)
                    {
                        for (int k = 1; k < tileMap.GetLength(0) - 1; k++)
                        {
                            if (tileMap[k, j] == (int)TileType.wall)
                            {
                                findWall = true;
                                continue;
                            }

                            if (!findWall)
                            {
                                //불이 비추는 방향 밝게
                                tileMap[k, j] = (int)TileType.ground;
                                tilePrefab[k, j].GetComponent<SpriteRenderer>().sprite = groundSprite;
                            }
                            
                            else if (findWall)  //벽 찾으면 그림자로 
                            {
                                tileMap[k, j] = (int)TileType.shadow;
                                tilePrefab[k, j].GetComponent<SpriteRenderer>().sprite = shadowSprite;
                            }

                        }

                    }
                    //빛이 맨 아래 있을 때
                    else if (i == tileMap.GetLength(0) - 1) //5,3 불 3,3 벽
                    {
                        for (int k = tileMap.GetLength(0) - 2; k >= 1; k--)//다음 칸부터 탐색
                        {
                            if (tileMap[k, j] == (int)TileType.wall)
                            {
                                findWall = true;
                                continue;
                            }

                            if (!findWall)
                            {
                                //불이 비추는 방향 밝게
                                tileMap[k, j] = (int)TileType.ground;
                                tilePrefab[k, j].GetComponent<SpriteRenderer>().sprite = groundSprite;
                            }

                            else if (findWall)  //벽 찾으면 그림자로 
                            {
                                tileMap[k, j] = (int)TileType.shadow;
                                tilePrefab[k, j].GetComponent<SpriteRenderer>().sprite = shadowSprite;
                            }
                        }
                    }

                    //빛이 맨 왼쪽에 있을 때
                    else if (j == 0)
                    {
                        for (int k = 1; k < tileMap.GetLength(1) - 1; k++)
                        {
                            
                            if (tileMap[i, k] == (int)TileType.wall)
                            {
                                findWall = true;
                                continue;
                            }

                            if (!findWall)
                            {
                                //불이 비추는 방향 밝게
                                tileMap[i, k] = (int)TileType.ground;
                                tilePrefab[i, k].GetComponent<SpriteRenderer>().sprite = groundSprite;
                            }

                            else if (findWall)  //벽 찾으면 그림자로 
                            {
                                tileMap[i, k] = (int)TileType.shadow;
                                tilePrefab[i, k].GetComponent<SpriteRenderer>().sprite = shadowSprite;
                            }

                        }

                    }
                    //빛이 맨 오른쪽
                    else if (j == tileMap.GetLength(1) - 1)//2,6불 2,4벽
                    {
                        for (int k = tileMap.GetLength(1) - 2; k >= 1; k--)
                        {
                            
                            if (tileMap[i, k] == (int)TileType.wall)
                            {
                                findWall = true;
                                continue;
                            }

                            if (!findWall)
                            {
                                //불이 비추는 방향 밝게
                                tileMap[i, k] = (int)TileType.ground;
                                tilePrefab[i, k].GetComponent<SpriteRenderer>().sprite = groundSprite;
                            }

                            else if (findWall)  //벽 찾으면 그림자로 
                            {
                                tileMap[i, k] = (int)TileType.shadow;
                                tilePrefab[i, k].GetComponent<SpriteRenderer>().sprite = shadowSprite;
                            }

                        }

                    }
                    findWall = false;
                }
            }
        }
    }


}
