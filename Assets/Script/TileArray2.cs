using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class TileArray2 : MonoBehaviour
{

    public static TileArray2 Instance;

    public enum TileType { shadow, ground, edge, fire, wall };
    /*
     * -1 null
     * 0 그림자 타일
     * 1 빛 타일
     * 2 돌 타일
     * 3 일자방향 불
     * 4 updown통나무 // wall
     * 5 leftright통나무 // wall
     * 7 사냥꾼 입구 및 생성
     * 8 사냥꾼 출구
     * 9 어둑시니 입구 및 생성
     * 10 어둑시니 출구
     * 11 반딧불 // wall
     * 12 십자불 / wall
     * 13 도깨비불 //wall
    */
    [SerializeField]
    public int[,] tileMap = new int[,]
    {
        {-1,-1,-1,0,8,0,0,-1,-1,-1},
        {-1,-1,-1,0,0,0,0,-1,-1,-1},
        {-1,-1,-1,0,0,0,0,-1,-1,-1},
        {-1,-1,-1,0,0,0,2,-1,-1,-1},
        {-1,-1,-1,2,0,0,0,-1,-1,-1},
        {-1,-1,-1,0,0,0,0,-1,-1,-1},
        {-1,-1,-1,2,0,0,0,-1,-1,-1},
        {-1,-1,-1,0,0,0,2,-1,-1,-1},
        {-1,-1,-1,0,0,0,0,-1,-1,-1},
        {-1,-1,-1,0,0,7,0,-1,-1,-1}
    };

    public int[,] wallMap = new int[,]
    {
        {0,0,0,12,0,0,0,0,0,0},
        {0,0,0,12,5,0,0,0,0,0},
        {0,0,0,12,0,0,0,0,0,0},
        {0,0,0,12,0,0,0,0,0,0},
        {0,0,0,0,0,0,12,0,0,0},
        {0,0,0,0,0,5,12,0,0,0},
        {0,0,0,0,0,0,12,0,0,0},
        {0,0,0,12,5,0,0,0,0,0},
        {0,0,0,12,5,4,0,0,0,0},
        {0,0,0,12,0,0,0,0,0,0}
    };


    public float TileSize;

    public float wallSize;

    private int getWall_x, getWall_y;

    public Vector2 StartPoint;//top left corner of the map

    private Vector2 dragStartPosition;

    private GameObject get_Wall;

    //타일위치에 따라 프리펩을 저장해놓은 2차원 배열
    public GameObject[,] tilePrefab = new GameObject[10, 10];

    //public UnityEngine.Sprite[] shadowSprite;
    public Sprite shadowSprite;
    public Sprite groundSprite;

    int RLwall;

    public bool Touchable;

    private bool detectedWall = false;

    public bool isOnFenFire = true;

    private void Awake()
    {
        Instance = this;
    }

    // Use this for initialization
    void Start()
    {
        TileSize = 1;
        wallSize = 1;
        Touchable = true;
        PopulateTileMap();
        PopulatewallMap();
        
    }

    // Update is called once per frame
    void Update()
    {
        moveWall();
        makeShadow();
        MakeBright();
        //ShowMatrix();
    }


    void ShowMatrix()
    {
        for (int i = 0; i < tileMap.GetLength(0); i++)
        {
            for (int j = 0; j < tileMap.GetLength(1); j++)
            {
                Debug.Log(tileMap[i,j]);
            }
        }
        Debug.Log("-------------------------------");
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
                if (wallMap[i, j] == 4 || wallMap[i, j] == 5)
                {
                    GameObject prefab = Resources.Load("wall_" + wallMap[i, j].ToString()) as GameObject;
                    GameObject wall = Instantiate(prefab, Vector3.zero, Quaternion.identity) as GameObject;
                    wall.GetComponent<wall_Info>().Set(i, j);
                    if(wallMap[i, j] == 4)
                    {
                        tileMap[i, j] = (int)TileType.wall;
                    }
                    else if(wallMap[i, j] == 5)
                    {
                        tileMap[i, j] = 5;
                    }
                    wall.transform.position = new Vector2(StartPoint.x + (wallSize * j) + (wallSize / 2), StartPoint.y - (wallSize * i) - (wallSize / 2));

                }
                //일자불 생성
                if (wallMap[i, j] == 3)
                {
                    GameObject prefab = Resources.Load("tile_" + wallMap[i, j].ToString()) as GameObject;
                    GameObject tile = Instantiate(prefab, Vector3.zero, Quaternion.identity) as GameObject;
                    tile.transform.position = new Vector2(StartPoint.x + (TileSize * j) + (TileSize / 2), StartPoint.y - (TileSize * i) - (TileSize / 2));
                    tileMap[i, j] = (int)TileType.ground;
                    tilePrefab[i, j].GetComponent<SpriteRenderer>().sprite = groundSprite;
                }
                //십자불 생성
                if (wallMap[i, j] == 12)
                {
                    GameObject prefab = Resources.Load("tile_" + wallMap[i, j].ToString()) as GameObject;
                    GameObject tile = Instantiate(prefab, Vector3.zero, Quaternion.identity) as GameObject;
                    tile.transform.position = new Vector2(StartPoint.x + (TileSize * j) + (TileSize / 2), StartPoint.y - (TileSize * i) - (TileSize / 2));
                    //tile.GetComponent<wall_Info>().Set(i, j);
                    tileMap[i, j] = 12; //사냥꾼이 지나가지 못하게 타일맵을 1로 바꾸지 않고 그냥 스프라이트만 밝게함.
                    tilePrefab[i, j].GetComponent<SpriteRenderer>().sprite = groundSprite;

                }
                //반딧불 생성
                if (wallMap[i, j] == 11)
                {
                    GameObject prefab = Resources.Load("tile_" + wallMap[i, j].ToString()) as GameObject;
                    GameObject tile = Instantiate(prefab, Vector3.zero, Quaternion.identity) as GameObject;
                    tile.transform.position = new Vector2(StartPoint.x + (TileSize * j) + (TileSize / 2), StartPoint.y - (TileSize * i) - (TileSize / 2));
                    tile.GetComponent<wall_Info>().Set(i, j);
                    tileMap[i, j] = (int)TileType.ground;
                }

                //도깨비불 생성
                if (wallMap[i, j] == 13)
                {
                    GameObject prefab = Resources.Load("tile_" + wallMap[i, j].ToString()) as GameObject;
                    GameObject tile = Instantiate(prefab, Vector3.zero, Quaternion.identity) as GameObject;
                    tile.transform.position = new Vector2(StartPoint.x + (TileSize * j) + (TileSize / 2), StartPoint.y - (TileSize * i) - (TileSize / 2));
                    //tile.GetComponent<wall_Info>().Set(i, j);
                    tileMap[i, j] = 13; //사냥꾼이 지나가지 못하게 타일맵을 1로 바꾸지 않고 그냥 스프라이트만 밝게함.
                    tilePrefab[i, j].GetComponent<SpriteRenderer>().sprite = groundSprite;
                }
            }
        }
    }

    private void moveWall()
    {
        if (Input.GetMouseButtonDown(0) && Touchable)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("wall_0"))
                {
                    RLwall = 0;
                    get_Wall = hit.transform.gameObject;
                    getWall_x = get_Wall.transform.gameObject.GetComponent<wall_Info>().get_X();
                    getWall_y = hit.transform.gameObject.GetComponent<wall_Info>().get_Y();
                    detectedWall = true;
                }
                else if (hit.collider.CompareTag("wall_1"))
                {
                    RLwall = 1;
                    get_Wall = hit.transform.gameObject;
                    getWall_x = get_Wall.transform.gameObject.GetComponent<wall_Info>().get_X();
                    getWall_y = hit.transform.gameObject.GetComponent<wall_Info>().get_Y();
                    detectedWall = true;
                }
                else if (hit.collider.CompareTag("firefly"))
                {
                    RLwall = 2;
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
                    if (dragDirection.y > 0 && RLwall != 0) // UP
                    {
                        detectWallFunc(getWall_x, getWall_y, 'U');
                      
                    }
                    else if (dragDirection.y < 0 && RLwall != 0) // Down
                    {
                        detectWallFunc(getWall_x, getWall_y, 'D');
                    }
                }
                // 좌우 드래그 방향 감지
                else
                {
                    if (dragDirection.x > 0 && RLwall != 1) // Right
                    {
                        detectWallFunc(getWall_x, getWall_y, 'R');
                    }
                    else if (dragDirection.x < 0 && RLwall != 1) // Left
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
                    if ((tileMap[x - 1, y] == 0 || tileMap[x - 1, y] == 1) && wallMap[x - 1, y] == 0) 
                    {
                        if (tileMap[x, y] == 4)
                        {
                            wallMap[x - 1, y] = 4;
                            wallMap[x, y] = 0;
                            tileMap[x - 1, y] = 4;
                            tileMap[x, y] = 0;
                        }
                        else if (tileMap[x,y] == 5)
                        {
                            wallMap[x - 1, y] = 5;
                            wallMap[x, y] = 0;
                            tileMap[x - 1, y] = 5;
                            tileMap[x, y] = 0;
                        }
                        else if (wallMap[x, y] == 11)
                        {
                            wallMap[x - 1, y] = 11;
                            wallMap[x, y] = 0;
                            tileMap[x - 1, y] = 1;
                            tileMap[x, y] = 1;
                        }

                        get_Wall.GetComponent<wall_Info>().Set(x - 1, y);
                        get_Wall.transform.position = new Vector3(get_Wall.transform.position.x, get_Wall.transform.position.y + 1, 0);
                    }
                }
                break;
            case 'D':
                {
                    if ((tileMap[x + 1, y] == 0 || tileMap[x + 1, y] == 1) && wallMap[x + 1, y] == 0)
                    {
                        if (tileMap[x, y] == 4)
                        {
                            wallMap[x + 1, y] = 4;
                            wallMap[x, y] = 0;
                            tileMap[x + 1, y] = 4;
                            tileMap[x, y] = 0;
                        }
                        else if (tileMap[x, y] == 5)
                        {
                            wallMap[x + 1, y] = 5;
                            wallMap[x, y] = 0;
                            tileMap[x + 1, y] = 5;
                            tileMap[x, y] = 0;
                        }
                        else if (wallMap[x, y] == 11)
                        {
                            wallMap[x + 1, y] = 11;
                            wallMap[x, y] = 0;
                            tileMap[x + 1, y] = 1;
                            tileMap[x, y] = 1;
                        }
                        get_Wall.GetComponent<wall_Info>().Set(x + 1, y);
                        get_Wall.transform.position = new Vector3(get_Wall.transform.position.x, get_Wall.transform.position.y - 1, 0);
                    }
                }
                break;
            case 'R':
                {
                    if ((tileMap[x, y + 1] == 0 || tileMap[x, y + 1] == 1) && wallMap[x, y + 1] == 0)
                    {
                        if (tileMap[x, y] == 4)
                        {
                            wallMap[x, y + 1] = 4;
                            wallMap[x, y] = 0;
                            tileMap[x, y + 1] = 4;
                            tileMap[x, y] = 0;
                        }
                        else if (tileMap[x, y] == 5)
                        {
                            wallMap[x, y + 1] = 5;
                            wallMap[x, y] = 0;
                            tileMap[x, y + 1] = 5;
                            tileMap[x, y] = 0;
                        }
                        else if (wallMap[x, y] == 11)
                        {
                            wallMap[x, y + 1] = 11;
                            wallMap[x, y] = 0;
                            tileMap[x, y + 1] = 1;
                            tileMap[x, y] = 1;
                        }
                        get_Wall.GetComponent<wall_Info>().Set(x, y + 1);
                        get_Wall.transform.position = new Vector3(get_Wall.transform.position.x + 1, get_Wall.transform.position.y, 0);
                    }
                }
                break;
            case 'L':
                {
                    if ((tileMap[x, y - 1] == 0 || tileMap[x, y - 1] == 1) && wallMap[x, y - 1] == 0)
                    {
                        if (tileMap[x, y] == 4)
                        {
                            wallMap[x, y - 1] = 4;
                            wallMap[x, y] = 0;
                            tileMap[x, y - 1] = 4;
                            tileMap[x, y] = 0;
                        }
                        else if (tileMap[x, y] == 5)
                        {
                            wallMap[x , y - 1] = 5;
                            wallMap[x, y] = 0;
                            tileMap[x, y - 1] = 5;
                            tileMap[x, y] = 0;
                        }
                        else if (wallMap[x, y] == 11)
                        {
                            wallMap[x, y - 1] = 11;
                            wallMap[x, y] = 0;
                            tileMap[x, y - 1] = 1;
                            tileMap[x, y] = 1;
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

    //도깨비불 on / off
    //public void SwitchFenFire()
    //{
    //    if (Input.GetMouseButtonDown(0) && Touchable)
    //    {
    //        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //        RaycastHit hit;
    //        if (Physics.Raycast(ray, out hit))
    //        {
    //            if (isOnFenFire && hit.collider.CompareTag("fenfire"))
    //            {
    //                Debug.Log("turn off the fenfire");
    //                isOnFenFire = false;
    //            }
    //            else if(!isOnFenFire && hit.collider.CompareTag("fenfire"))
    //            {
    //                Debug.Log("turn on the fenfire");
    //                isOnFenFire = true;
    //            }
    //        }
    //    }
    //}



    //매 프레임마다 모든 길 타일을 어둡게 칠해놓는다.
    public void makeShadow()
    {
        for (int i = 1; i < wallMap.GetLength(0)-1; i++)  //row
        {
            for (int j = 1; j < wallMap.GetLength(1)-1; j++)  //column
            {
                //pass the objects in wallMap, null tile in tileMap
                if (/*wallMap[i,j] != 0 || */(tileMap[i,j] != 0 && tileMap[i,j] != 1))
                {
                    continue;
                }
                tileMap[i, j] = (int)TileType.shadow;
                tilePrefab[i, j].GetComponent<SpriteRenderer>().sprite = shadowSprite;
            }
        }
    }

    //불이 비추는 곳은 밝게 칠한다.
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
                            if (wallMap[k, j] == 12 || tileMap[k, j] == -1) 
                            {
                                continue;
                            }
                            if (tileMap[k, j] == (int)TileType.wall || tileMap[k, j] == 5)
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
                            if (wallMap[k, j] == 12 || tileMap[k, j] == -1)
                            {
                                continue;
                            }
                            if (tileMap[k, j] == (int)TileType.wall || tileMap[k, j] == 5)
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
                            if (wallMap[i, k] == 12 || tileMap[i, k] == -1)
                            {
                                continue;
                            }
                            if (tileMap[i, k] == (int)TileType.wall || tileMap[i, k] == 5)
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
                            if (wallMap[i, k] == 12 || tileMap[i, k] == -1)
                            {
                                continue;
                            }
                            if (tileMap[i, k] == (int)TileType.wall || tileMap[i, k] == 5)
                            {
                                break;
                            }
                            //불이 비추는 방향 밝게
                            tileMap[i, k] = (int)TileType.ground;
                            tilePrefab[i, k].GetComponent<SpriteRenderer>().sprite = groundSprite;
                        }

                    }

                }

                //반딧불이
                if (wallMap[i, j] == 11)
                {
                    //Debug.Log("도깨비불 위치" + i + ", " + j);
                    for(int n = -1; n <= 1; n++)
                    {
                        for(int m = -1; m <= 1; m++)
                        {
                            //pass null tile
                            if (tileMap[n,m] == -1)
                            {
                                continue;
                            }
                            //밝게할 타일이 타일맵의 0이나 1이 아니면(길 타일이 아니면) 건너뛴다. 길이 아니면 칠하면 안되니까.
                            if(tileMap[i + n, j + m] != 0 && tileMap[i + n, j + m] != 1)
                            {
                                continue;
                            }
                            //길타일이면 밝게 칠한다.
                            tileMap[i + n, j + m] = (int)TileType.ground;
                            tilePrefab[i + n, j + m].GetComponent<SpriteRenderer>().sprite = groundSprite;
                        }
                    }
                }

                //십자 불
                if (wallMap[i, j] == 12)
                {
                    //아래 방향
                    for (int k = i + 1; k < tileMap.GetLength(0) - 1; k++)
                    {
                        
                        
                        if (tileMap[k, j] != 0 && tileMap[k, j] != 1)
                        {
                            if (tileMap[k, j] == (int)TileType.wall || tileMap[k, j] == 5 || tileMap[k, j] == 2)
                            {
                                break;
                            }
                            continue;
                        }
                        //불이 비추는 방향 밝게
                        tileMap[k, j] = (int)TileType.ground;
                        tilePrefab[k, j].GetComponent<SpriteRenderer>().sprite = groundSprite;
                    }
                    //위 방향
                    for (int k = i - 1; k > 0; k--)
                    {
                        
                        
                        if (tileMap[k, j] != 0 && tileMap[k, j] != 1)
                        {
                            if (tileMap[k, j] == (int)TileType.wall || tileMap[k, j] == 5 || tileMap[k, j] == 2)
                            {
                                break;
                            }
                            continue;
                        }
                        //불이 비추는 방향 밝게
                        tileMap[k, j] = (int)TileType.ground;
                        tilePrefab[k, j].GetComponent<SpriteRenderer>().sprite = groundSprite;
                    }
                    //오른쪽 방향
                    for (int k = j + 1; k < tileMap.GetLength(1) - 1; k++)
                    {
                        
                        
                        if (tileMap[i, k] != 0 && tileMap[i, k] != 1)
                        {
                            if (tileMap[i, k] == (int)TileType.wall || tileMap[i, k] == 5 || tileMap[i, k] == 2)
                            {
                                break;
                            }
                            continue;
                        }
                        //불이 비추는 방향 밝게
                        tileMap[i, k] = (int)TileType.ground;
                        tilePrefab[i, k].GetComponent<SpriteRenderer>().sprite = groundSprite;
                    }
                    //왼쪽 방향
                    for (int k = j - 1; k >= 1; k--)
                    {
                        
                        
                        if (tileMap[i, k] != 0 && tileMap[i, k] != 1)
                        {
                            if (tileMap[i, k] == (int)TileType.wall || tileMap[i, k] == 5 || tileMap[i, k] == 2)
                            {
                                break;
                            }
                            continue;
                        }
                        //불이 비추는 방향 밝게
                        tileMap[i, k] = (int)TileType.ground;
                        tilePrefab[i, k].GetComponent<SpriteRenderer>().sprite = groundSprite;
                    }
                }

                //도깨비불
                //if (wallMap[i, j] == 13)
                //{
                //    if (isOnFenFire)
                //    {
                //        //아래 방향
                //        for (int k = i + 1; k < tileMap.GetLength(0) - 1; k++)
                //        {
                //            if (tileMap[k, j] == -1 || wallMap[k, j] == 12)
                //            {
                //                continue;
                //            }
                //            if (tileMap[k, j] == (int)TileType.wall)
                //            {
                //                break;
                //            }
                //            //불이 비추는 방향 밝게
                //            tileMap[k, j] = (int)TileType.ground;
                //            tilePrefab[k, j].GetComponent<SpriteRenderer>().sprite = groundSprite;
                //        }
                //        //위 방향
                //        for (int k = i - 1; k > 0; k--)
                //        {
                //            if (tileMap[k, j] == -1 || wallMap[k, j] == 12)
                //            {
                //                continue;
                //            }
                //            if (tileMap[k, j] == (int)TileType.wall)
                //            {
                //                break;
                //            }
                //            //불이 비추는 방향 밝게
                //            tileMap[k, j] = (int)TileType.ground;
                //            tilePrefab[k, j].GetComponent<SpriteRenderer>().sprite = groundSprite;
                //        }
                //        //오른쪽 방향
                //        for (int k = j + 1; k < tileMap.GetLength(1) - 1; k++)
                //        {
                //            if (tileMap[i, k] == -1 || wallMap[i, k] == 12)
                //            {
                //                continue;
                //            }
                //            if (tileMap[i, k] == (int)TileType.wall)
                //            {
                //                break;
                //            }
                //            //불이 비추는 방향 밝게
                //            tileMap[i, k] = (int)TileType.ground;
                //            tilePrefab[i, k].GetComponent<SpriteRenderer>().sprite = groundSprite;
                //        }
                //        //왼쪽 방향
                //        for (int k = j - 1; k >= 1; k--)
                //        {
                //            if (tileMap[i, k] == -1 || wallMap[i, k] == 12)
                //            {
                //                continue;
                //            }
                //            if (tileMap[i, k] == (int)TileType.wall)
                //            {
                //                break;
                //            }
                //            //불이 비추는 방향 밝게
                //            tileMap[i, k] = (int)TileType.ground;
                //            tilePrefab[i, k].GetComponent<SpriteRenderer>().sprite = groundSprite;
                //        }
                //    }
                    
                //}


            }
        }
    }

}