using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class TileArray : MonoBehaviour
{

    public static TileArray Instance;

    public enum TileType { shadow, ground, edge, fire, wall };
    /*
     * 7 사냥꾼
     * 8 사냥꾼 출구
     * 9 어둑시니
     * 10 어둑시니 출구
     * 12 십자불
    */
    [SerializeField]
    public int[,] tileMap = new int[,]
    {
         {2,9,2,2,7,2,2,2,2,2},
         {2,0,0,0,0,0,0,0,0,1},
         {2,0,0,0,0,0,0,0,0,1},
         {2,0,0,0,0,0,0,0,0,1},
         {2,0,0,0,0,0,0,0,0,1},
         {2,0,0,0,0,0,0,0,0,1},
         {2,0,0,0,0,0,0,0,0,1},
         {2,0,0,0,0,0,0,0,0,1},
         {10,0,0,0,0,0,0,0,0,1},
         {2,2,2,2,2,2,2,8,2,2}
    };

    public int[,] wallMap = new int[,]
    {
         {0,0,0,0,0,0,0,0,0,0},
         {0,0,0,0,1,0,0,0,0,3},
         {0,0,0,0,1,0,0,0,0,3},
         {0,0,0,0,2,0,0,0,0,3},
         {0,0,0,0,2,0,0,0,0,3},
         {0,0,0,0,1,0,0,0,0,3},
         {0,0,0,0,1,0,0,0,0,3},
         {0,0,0,0,1,0,0,0,0,3},
         {0,0,0,0,1,0,0,0,0,3},
         {0,0,0,0,0,0,0,0,0,0}
    };


    public float TileSize;

    public float wallSize;

    private int getWall_x, getWall_y;

    public Vector2 StartPoint;//top left corner of the map

    private Vector2 dragStartPosition;

    private GameObject get_Wall;

    public GameObject[,] tilePrefab = new GameObject[10, 10];
    public Sprite shadowSprite;
    public Sprite groundSprite;

    private bool detectedWall = false;

    private void Awake()
    {
        Instance = this;
    }

    // Use this for initialization
    void Start()
    {
        TileSize = 1;
        wallSize = 1;
        PopulateTileMap();
        PopulatewallMap();
    }

    // Update is called once per frame
    void Update()
    {
        moveWall();
        makeShadow();
        MakeBright();
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
                tilePrefab[i, j] = tile;

            }
        }
    }

    public void PopulatewallMap()
    {
        for (int i = 0; i < wallMap.GetLength(0); i++)
        {
            for (int j = 0; j < wallMap.GetLength(1); j++)
            {
                //통나무 생성
                if (wallMap[i, j] == 1 || wallMap[i, j] == 2)
                {
                    GameObject prefab = Resources.Load("wall_" + wallMap[i, j].ToString()) as GameObject;
                    GameObject wall = Instantiate(prefab, Vector3.zero, Quaternion.identity) as GameObject;
                    wall.GetComponent<wall_Info>().Set(i, j);
                    tileMap[i, j] = (int)TileType.wall;
                    wall.transform.position = new Vector2(StartPoint.x + (wallSize * j) + (wallSize / 2), StartPoint.y - (wallSize * i) - (wallSize / 2));

                }
                //일자불 생성
                if (wallMap[i, j] == 3)
                {
                    GameObject prefab = Resources.Load("tile_" + wallMap[i, j].ToString()) as GameObject;
                    GameObject tile = Instantiate(prefab, Vector3.zero, Quaternion.identity) as GameObject;
                    tile.transform.position = new Vector2(StartPoint.x + (TileSize * j) + (TileSize / 2), StartPoint.y - (TileSize * i) - (TileSize / 2));
                    //tile.GetComponent<wall_Info>().Set(i, j);
                    //tileMap[i, j] = 4;
                }
                //십자불 생성
                if (wallMap[i, j] == 12)
                {
                    GameObject prefab = Resources.Load("tile_" + wallMap[i, j].ToString()) as GameObject;
                    GameObject tile = Instantiate(prefab, Vector3.zero, Quaternion.identity) as GameObject;
                    tile.transform.position = new Vector2(StartPoint.x + (TileSize * j) + (TileSize / 2), StartPoint.y - (TileSize * i) - (TileSize / 2));
                    //tile.GetComponent<wall_Info>().Set(i, j);
                    //tileMap[i, j] = 4;
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
                        if (tileMap[x, y] == 4)
                        {
                            wallMap[x - 1, y] = 1;
                            wallMap[x, y] = 0;
                            tileMap[x - 1, y] = 4;
                            tileMap[x, y] = 0;
                        }
                        else if (tileMap[x, y] == 12)
                        {
                            wallMap[x - 1, y] = 1;
                            wallMap[x, y] = 0;
                            tileMap[x - 1, y] = 12;
                            tileMap[x, y] = 0;
                        }

                        get_Wall.GetComponent<wall_Info>().Set(x - 1, y);
                        get_Wall.transform.position = new Vector3(get_Wall.transform.position.x, get_Wall.transform.position.y + 1, 0);
                    }
                }
                break;
            case 'D':
                {
                    if (tileMap[x + 1, y] == 0 || tileMap[x + 1, y] == 1)
                    {
                        if (tileMap[x, y] == 4)
                        {
                            wallMap[x + 1, y] = 1;
                            wallMap[x, y] = 0;
                            tileMap[x + 1, y] = 4;
                            tileMap[x, y] = 0;
                        }
                        else if (tileMap[x, y] == 12)
                        {
                            wallMap[x + 1, y] = 1;
                            wallMap[x, y] = 0;
                            tileMap[x + 1, y] = 12;
                            tileMap[x, y] = 0;
                        }
                        get_Wall.GetComponent<wall_Info>().Set(x + 1, y);
                        get_Wall.transform.position = new Vector3(get_Wall.transform.position.x, get_Wall.transform.position.y - 1, 0);
                    }
                }
                break;
            case 'R':
                {
                    if ((tileMap[x, y + 1] == 0 || tileMap[x, y + 1] == 1) && wallMap[x, y + 1] != 3)
                    {
                        if (tileMap[x, y] == 4)
                        {
                            wallMap[x, y + 1] = 1;
                            wallMap[x, y] = 0;
                            tileMap[x, y + 1] = 4;
                            tileMap[x, y] = 0;
                        }
                        else if (tileMap[x, y] == 12)
                        {
                            wallMap[x, y + 1] = 1;
                            wallMap[x, y] = 0;
                            tileMap[x, y + 1] = 12;
                            tileMap[x, y] = 0;
                        }
                        get_Wall.GetComponent<wall_Info>().Set(x, y + 1);
                        get_Wall.transform.position = new Vector3(get_Wall.transform.position.x + 1, get_Wall.transform.position.y, 0);
                    }
                }
                break;
            case 'L':
                {
                    if (tileMap[x, y - 1] == 0 || tileMap[x, y - 1] == 1)
                    {
                        if (tileMap[x, y] == 4)
                        {
                            wallMap[x, y - 1] = 1;
                            wallMap[x, y] = 0;
                            tileMap[x, y - 1] = 4;
                            tileMap[x, y] = 0;
                        }
                        else if (tileMap[x, y] == 12)
                        {
                            wallMap[x, y - 1] = 1;
                            wallMap[x, y] = 0;
                            tileMap[x, y - 1] = 12;
                            tileMap[x, y] = 0;
                        }
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

                if (wallMap[i, j] == (int)TileType.fire)
                {
                    //맨 윗줄에 불이 있을 때
                    if (i == 0)
                    {
                        for (int k = 1; k < wallMap.GetLength(0) - 1; k++)
                        {
                            if (wallMap[k, j] == 12)
                            {
                                continue;
                            }

                            if (tileMap[k, j] == (int)TileType.wall)
                            {
                                findWall = true;

                            }

                            else if (findWall)  //벽 찾으면 그림자로 
                            {
                                tileMap[k, j] = (int)TileType.shadow;
                                tilePrefab[k, j].GetComponent<SpriteRenderer>().sprite = shadowSprite;
                            }

                        }

                    }
                    //빛이 맨 아래 있을 때
                    else if (i == wallMap.GetLength(0) - 1) //5,3 불 3,3 벽
                    {
                        for (int k = wallMap.GetLength(0) - 2; k >= 1; k--)//다음 칸부터 탐색
                        {
                            if (wallMap[k, j] == 12)
                            {
                                continue;
                            }

                            if (tileMap[k, j] == (int)TileType.wall)
                            {
                                findWall = true;

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
                        for (int k = 1; k < wallMap.GetLength(1) - 1; k++)
                        {
                            if (wallMap[i, k] == 12)
                            {
                                continue;
                            }

                            if (tileMap[i, k] == (int)TileType.wall)
                            {
                                findWall = true;

                            }

                            else if (findWall)  //벽 찾으면 그림자로 
                            {
                                tileMap[i, k] = (int)TileType.shadow;
                                tilePrefab[i, k].GetComponent<SpriteRenderer>().sprite = shadowSprite;
                            }

                        }

                    }
                    //빛이 맨 오른쪽
                    else if (j == wallMap.GetLength(1) - 1)//2,6불 2,4벽
                    {
                        for (int k = wallMap.GetLength(1) - 2; k >= 1; k--)
                        {
                            if (wallMap[i, k] == 12)
                            {
                                continue;
                            }

                            if (tileMap[i, k] == (int)TileType.wall)
                            {
                                findWall = true;

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

                //십자 불
                if (wallMap[i, j] == 12)
                {
                    //아래 방향
                    for (int k = i + 1; k < tileMap.GetLength(0) - 1; k++)
                    {
                        if (tileMap[k, j] == (int)TileType.wall)
                        {
                            findWall = true;
                        }
                        else if (findWall)  //벽 찾으면 그림자로
                        {
                            tileMap[k, j] = (int)TileType.shadow;
                            tilePrefab[k, j].GetComponent<SpriteRenderer>().sprite = shadowSprite;
                        }
                    }
                    //위 방향
                    for (int k = i - 1; k >= 1; k--)
                    {
                        if (tileMap[k, j] == (int)TileType.wall)
                        {
                            findWall = true;
                        }
                        else if (findWall)  //벽 찾으면 그림자로
                        {
                            tileMap[k, j] = (int)TileType.shadow;
                            tilePrefab[k, j].GetComponent<SpriteRenderer>().sprite = shadowSprite;
                        }
                    }
                    //오른쪽 방향
                    for (int k = j + 1; k < tileMap.GetLength(1) - 1; k++)
                    {
                        if (tileMap[i, k] == (int)TileType.wall)
                        {
                            findWall = true;
                        }
                        else if (findWall)  //벽 찾으면 그림자로
                        {
                            tileMap[i, k] = (int)TileType.shadow;
                            tilePrefab[i, k].GetComponent<SpriteRenderer>().sprite = shadowSprite;
                        }
                    }
                    //왼쪽 방향
                    for (int k = j - 1; k >= 1; k--)
                    {
                        if (tileMap[i, k] == (int)TileType.wall)
                        {
                            findWall = true;
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


    public void MakeBright()
    {
        for (int i = 0; i < wallMap.GetLength(0); i++)  //row
        {
            for (int j = 0; j < wallMap.GetLength(1); j++)  //column
            {

                if (wallMap[i, j] == (int)TileType.fire)
                {
                    //맨 윗줄에 불이 있을 때
                    if (i == 0)
                    {
                        for (int k = 1; k < wallMap.GetLength(0) - 1; k++)
                        {
                            //십자불은 스프라이트 바꾸면 안되니까 패스
                            if (wallMap[k, j] == 12)
                            {
                                continue;
                            }
                            if (tileMap[k, j] == (int)TileType.wall)
                            {
                                break;
                            }
                            //불이 비추는 방향 밝게
                            tileMap[k, j] = (int)TileType.ground;
                            tilePrefab[k, j].GetComponent<SpriteRenderer>().sprite = groundSprite;
                        }

                    }
                    //빛이 맨 아래 있을 때
                    else if (i == wallMap.GetLength(0) - 1) //5,3 불 3,3 벽
                    {
                        for (int k = wallMap.GetLength(0) - 2; k >= 1; k--)//다음 칸부터 탐색
                        {
                            if (wallMap[k, j] == 12)
                            {
                                continue;
                            }
                            if (tileMap[k, j] == (int)TileType.wall)
                            {
                                break;
                            }
                            //불이 비추는 방향 밝게
                            tileMap[k, j] = (int)TileType.ground;
                            tilePrefab[k, j].GetComponent<SpriteRenderer>().sprite = groundSprite;
                        }
                    }

                    //빛이 맨 왼쪽에 있을 때
                    else if (j == 0)
                    {
                        for (int k = 1; k < wallMap.GetLength(1) - 1; k++)
                        {
                            if (wallMap[i, k] == 12)
                            {
                                continue;
                            }
                            if (tileMap[i, k] == (int)TileType.wall)
                            {
                                break;
                            }
                            //불이 비추는 방향 밝게
                            tileMap[i, k] = (int)TileType.ground;
                            tilePrefab[i, k].GetComponent<SpriteRenderer>().sprite = groundSprite;
                        }

                    }
                    //빛이 맨 오른쪽
                    else if (j == wallMap.GetLength(1) - 1)//2,6불 2,4벽
                    {
                        for (int k = wallMap.GetLength(1) - 2; k >= 1; k--)
                        {
                            if (wallMap[i, k] == 12)
                            {
                                continue;
                            }
                            if (tileMap[i, k] == (int)TileType.wall)
                            {
                                break;
                            }
                            //불이 비추는 방향 밝게
                            tileMap[i, k] = (int)TileType.ground;
                            tilePrefab[i, k].GetComponent<SpriteRenderer>().sprite = groundSprite;
                        }

                    }

                }

                //십자 불
                if (wallMap[i, j] == 12)
                {
                    for (int k = i + 1; k < tileMap.GetLength(0) - 1; k++)
                    {
                        if (tileMap[k, j] == (int)TileType.wall)
                        {
                            break;
                        }
                        //불이 비추는 방향 밝게
                        tileMap[k, j] = (int)TileType.ground;
                        tilePrefab[k, j].GetComponent<SpriteRenderer>().sprite = groundSprite;
                    }
                    //위 방향
                    for (int k = i - 1; k > 0; k--)
                    {
                        if (tileMap[k, j] == (int)TileType.wall)
                        {
                            break;
                        }
                        //불이 비추는 방향 밝게
                        tileMap[k, j] = (int)TileType.ground;
                        tilePrefab[k, j].GetComponent<SpriteRenderer>().sprite = groundSprite;
                    }
                    //오른쪽 방향
                    for (int k = j + 1; k < tileMap.GetLength(1) - 1; k++)
                    {
                        if (tileMap[i, k] == (int)TileType.wall)
                        {
                            break;
                        }
                        //불이 비추는 방향 밝게
                        tileMap[i, k] = (int)TileType.ground;
                        tilePrefab[i, k].GetComponent<SpriteRenderer>().sprite = groundSprite;
                    }
                    //왼쪽 방향
                    for (int k = j - 1; k >= 1; k--)
                    {
                        if (tileMap[i, k] == (int)TileType.wall)
                        {
                            break;
                        }
                        //불이 비추는 방향 밝게
                        tileMap[i, k] = (int)TileType.ground;
                        tilePrefab[i, k].GetComponent<SpriteRenderer>().sprite = groundSprite;
                    }
                }
            }
        }
    }

}
