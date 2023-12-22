using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

[System.Serializable]

public class TileArray : MonoBehaviour
{

    public static TileArray Instance;

    public string tileMap_path;
    public string wallMap_path;

    AudioSource audioSource;
    public AudioClip dragWood;
    public AudioClip dragfirefly;
    public AudioClip fenfireOn;
    public AudioClip fenfireOff;
    public enum TileType
    {
        shadow, ground, rock, fire, upDownWood, leftRightWood, none, hunterStart, hunterGoal, monsterStart, monsterGoal,
        fireFly, crossFire, fenFire
    };
    /*
     * -1 null
     * 0 그림자 타일
     * 1 빛 타일
     * 2 돌 타일
     * 3 일자방향 불
     * 4 updown통나무 // wall
     * 5 leftright통나무 // wall
     * 6 temp
     * 
     * 7 사냥꾼 입구 및 생성
     * 8 사냥꾼 출구
     * 9 어둑시니 입구 및 생성
     * 10 어둑시니 출구
     * 11 반딧불 // wall
     * 12 십자불 / wall
     * 13 도깨비불 //wall
     * 
     * 20 호랑이 //boss
    */

    public int[,] tileMap = new int[,]
    {
        //boss
        {0,0,0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0,0,0}
    };

    public int[,] wallMap = new int[,]
    {
        {0,0,0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0,0,0}
    };


    public float TileSize;

    public float wallSize;

    private int getWall_x, getWall_y;

    public Vector2 StartPoint;//top left corner of the map

    private Vector2 dragStartPosition;

    private GameObject get_Wall;

    //타일위치에 따라 프리펩을 저장해놓은 2차원 배열
    public GameObject[,] tilePrefab = new GameObject[10, 10];
    public GameObject[,] fenfirePrefab = new GameObject[10, 10];

    //public UnityEngine.Sprite[] shadowSprite;
    public Sprite shadowSprite;
    public Sprite groundSprite;
    public Sprite offFenFireSprite;
    public Sprite onFenFireSprite;

    int RLwall;

    public bool Touchable;

    private bool detectedWall = false;

    public bool isOnFenFire = false;

    public GameObject boss;
    public GameObject player;

    public string bossType;

    public bool isBoss;
    private void Awake()
    {
        Instance = this;
        ReadtileMap();
        ReadwallMap();
        Time.timeScale = 1;
    }

    // Use this for initialization

    void Start()
    {  
        TileSize = 1;
        wallSize = 1;
        Touchable = true;
        PopulateTileMap();
        PopulatewallMap();

        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        moveWall();
        SwitchFenFire();
        makeShadow();
        MakeBright();
    }

    void ReadtileMap()
    {
        TextAsset textfile = Resources.Load(tileMap_path) as TextAsset;
        StringReader stringReader = new StringReader(textfile.text);
        int i = 0;

        while (stringReader != null)
        {
            string line = stringReader.ReadLine();
           

            if(line == null)
            break;

            for(int j = 0; j < 10; j++)
            {
                tileMap[i,j] = int.Parse(line.Split(',')[j]);
               
            }
            i++;
        }

        stringReader.Close();
    }

    void ReadwallMap()
    {
        TextAsset textfile = Resources.Load(wallMap_path) as TextAsset;
        StringReader stringReader = new StringReader(textfile.text);
        int i = 0; 

        while (stringReader != null)
        {
            string line = stringReader.ReadLine();

            if(line == null)
            break;

            for(int j = 0; j < 10; j++)
            {
                wallMap[i,j] = int.Parse(line.Split(',')[j]);
            }
            i++;
        }

        stringReader.Close();
    }
    void ShowMatrix()
    {
        for (int i = 0; i < tileMap.GetLength(0); i++)
        {
            for (int j = 0; j < tileMap.GetLength(1); j++)
            {
                Debug.Log(tileMap[i, j]);
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
                //hunter
                if (tileMap[i, j] == 7)
                {
                    player = tile;
                }
                if(tileMap[i,j] == 9)
                {
                    player = tile;
                }
            }
        }
    }

    public void PopulatewallMap()
    {
        for (int i = 0; i < wallMap.GetLength(0); i++)
        {
            for (int j = 0; j < wallMap.GetLength(1); j++)
            {
                //돌 생성
                if (wallMap[i, j] == 2)
                {
                    GameObject prefab = Resources.Load("tile_" + wallMap[i, j].ToString()) as GameObject;
                    GameObject tile = Instantiate(prefab, Vector3.zero, Quaternion.identity) as GameObject;
                    tile.transform.position = new Vector2(StartPoint.x + (TileSize * j) + (TileSize / 2), StartPoint.y - (TileSize * i) - (TileSize / 2));
                    tileMap[i, j] = (int)TileType.rock;
                }
                //통나무 생성
                if (wallMap[i, j] == 4 || wallMap[i, j] == 5)
                {
                    GameObject prefab = Resources.Load("wall_" + wallMap[i, j].ToString()) as GameObject;
                    GameObject wall = Instantiate(prefab, Vector3.zero, Quaternion.identity) as GameObject;
                    wall.GetComponent<wall_Info>().Set(i, j);
                    if (wallMap[i, j] == 4)
                    {
                        tileMap[i, j] = (int)TileType.upDownWood;
                    }
                    else if (wallMap[i, j] == 5)
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
                    tileMap[i, j] = 12;
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
                    tile.GetComponent<wall_Info>().Set(i, j);
                    tileMap[i, j] = 13;
                    fenfirePrefab[i, j] = tile;
                    tilePrefab[i, j].GetComponent<SpriteRenderer>().sprite = groundSprite;
                }
                //도깨비불2 생성
                if (wallMap[i, j] == 14)
                {
                    GameObject prefab = Resources.Load("tile_" + wallMap[i, j].ToString()) as GameObject;
                    GameObject tile = Instantiate(prefab, Vector3.zero, Quaternion.identity) as GameObject;
                    tile.transform.position = new Vector2(StartPoint.x + (TileSize * j) + (TileSize / 2), StartPoint.y - (TileSize * i) - (TileSize / 2));
                    tile.GetComponent<wall_Info>().Set(i, j);
                    tileMap[i, j] = 14;
                    fenfirePrefab[i, j] = tile;
                    tilePrefab[i, j].GetComponent<SpriteRenderer>().sprite = groundSprite;
                }
                //착취의 손아귀
                if (wallMap[i, j] == 15)
                {
                     GameObject prefab = Resources.Load("tile_" + wallMap[i, j].ToString()) as GameObject;
                    GameObject tile = Instantiate(prefab, Vector3.zero, Quaternion.identity) as GameObject;
                    tile.transform.position = new Vector2(StartPoint.x + (TileSize * j) + (TileSize / 2), StartPoint.y - (TileSize * i) - (TileSize / 2));
                    tileMap[i, j] = (int)TileType.rock;
                    tilePrefab[i, j].GetComponent<SpriteRenderer>().sprite = groundSprite;
                }

                //호랑이 생성
                if (wallMap[i, j] == 20)
                {
                    bossType = "T";
                    GameObject prefab = Resources.Load("tile_" + wallMap[i, j].ToString()) as GameObject;
                    GameObject tile = Instantiate(prefab, Vector3.zero, Quaternion.identity) as GameObject;
                    tile.transform.position = new Vector2(StartPoint.x + (TileSize * j) + (TileSize / 2), StartPoint.y - (TileSize * i) - (TileSize / 2));
                    tileMap[i, j] = 20;
                    tilePrefab[i, j].GetComponent<SpriteRenderer>().sprite = shadowSprite;
                    boss = tile;
                }

                if (wallMap[i, j] == 21)
                {
                    bossType = "L";
                    GameObject prefab = Resources.Load("tile_" + wallMap[i, j].ToString()) as GameObject;
                    GameObject tile = Instantiate(prefab, Vector3.zero, Quaternion.identity) as GameObject;
                    tile.transform.position = new Vector2(StartPoint.x + (TileSize * j) + (TileSize / 2), StartPoint.y - (TileSize * i) - (TileSize / 2));
                    tileMap[i, j] = 21;
                    tilePrefab[i, j].GetComponent<SpriteRenderer>().sprite = shadowSprite;
                    boss = tile;
                }

                if (wallMap[i, j] == 22)
                {
                    bossType = "A";
                    GameObject prefab = Resources.Load("tile_" + wallMap[i, j].ToString()) as GameObject;
                    GameObject tile = Instantiate(prefab, Vector3.zero, Quaternion.identity) as GameObject;
                    tile.transform.position = new Vector2(StartPoint.x + (TileSize * j) + (TileSize / 2), StartPoint.y - (TileSize * i) - (TileSize / 2));
                    tileMap[i, j] = 22;
                    tilePrefab[i, j].GetComponent<SpriteRenderer>().sprite = shadowSprite;
                    boss = tile;
                }

                if (wallMap[i, j] == 23)
                {
                    bossType = "G";
                    GameObject prefab = Resources.Load("tile_" + wallMap[i, j].ToString()) as GameObject;
                    GameObject tile = Instantiate(prefab, Vector3.zero, Quaternion.identity) as GameObject;
                    tile.transform.position = new Vector2(StartPoint.x + (TileSize * j) + (TileSize / 2), StartPoint.y - (TileSize * i) - (TileSize / 2));
                    tileMap[i, j] = 23;
                    tilePrefab[i, j].GetComponent<SpriteRenderer>().sprite = shadowSprite;
                    boss = tile;
                }

                if (wallMap[i, j] == 24)
                {
                    bossType = "C";
                    GameObject prefab = Resources.Load("tile_" + wallMap[i, j].ToString()) as GameObject;
                    GameObject tile = Instantiate(prefab, Vector3.zero, Quaternion.identity) as GameObject;
                    tile.transform.position = new Vector2(StartPoint.x + (TileSize * j) + (TileSize / 2), StartPoint.y - (TileSize * i) - (TileSize / 2));
                    tileMap[i, j] = 24;
                    tilePrefab[i, j].GetComponent<SpriteRenderer>().sprite = shadowSprite;
                    boss = tile;
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
                    if (x > 0 && (tileMap[x - 1, y] == 0 || tileMap[x - 1, y] == 1) && wallMap[x - 1, y] == 0)
                    {
                        if (tileMap[x, y] == 4)
                        {
                            wallMap[x - 1, y] = 4;
                            wallMap[x, y] = 0;
                            tileMap[x - 1, y] = 4;
                            tileMap[x, y] = 0;
                            tilePrefab[x -1, y].GetComponent<SpriteRenderer>().sprite = shadowSprite;
                            audioSource.clip = dragWood;
                            audioSource.Play();
                        }
                        else if (tileMap[x, y] == 5)
                        {
                            wallMap[x - 1, y] = 5;
                            wallMap[x, y] = 0;
                            tileMap[x - 1, y] = 5;
                            tileMap[x, y] = 0;
                            tilePrefab[x - 1, y].GetComponent<SpriteRenderer>().sprite = shadowSprite;
                            audioSource.clip = dragWood;
                            audioSource.Play();
                        }
                        else if (wallMap[x, y] == 11)
                        {
                            wallMap[x - 1, y] = 11;
                            wallMap[x, y] = 0;
                            tileMap[x - 1, y] = 1;
                            tileMap[x, y] = 1;
                            audioSource.clip = dragfirefly;
                            audioSource.Play();
                        }
                        get_Wall.GetComponent<wall_Info>().Set(x - 1, y);
                        get_Wall.transform.position = new Vector3(get_Wall.transform.position.x, get_Wall.transform.position.y + 1, 0);
                        if(isBoss)
                            count.Instance.fillBar();
                    }
                }
                break;
            case 'D':
                {
                    if (x < 9 && (tileMap[x + 1, y] == 0 || tileMap[x + 1, y] == 1) && wallMap[x + 1, y] == 0)
                    {
                        if (tileMap[x, y] == 4)
                        {
                            wallMap[x + 1, y] = 4;
                            wallMap[x, y] = 0;
                            tileMap[x + 1, y] = 4;
                            tileMap[x, y] = 0;
                            tilePrefab[x + 1, y].GetComponent<SpriteRenderer>().sprite = shadowSprite;
                            audioSource.clip = dragWood;
                            audioSource.Play();
                        }
                        else if (tileMap[x, y] == 5)
                        {
                            wallMap[x + 1, y] = 5;
                            wallMap[x, y] = 0;
                            tileMap[x + 1, y] = 5;
                            tileMap[x, y] = 0;
                            tilePrefab[x + 1, y].GetComponent<SpriteRenderer>().sprite = shadowSprite;
                            audioSource.clip = dragWood;
                            audioSource.Play();
                        }
                        else if (wallMap[x, y] == 11)
                        {
                            wallMap[x + 1, y] = 11;
                            wallMap[x, y] = 0;
                            tileMap[x + 1, y] = 1;
                            tileMap[x, y] = 1;
                            audioSource.clip = dragfirefly;
                            audioSource.Play();
                        }
                        get_Wall.GetComponent<wall_Info>().Set(x + 1, y);
                        get_Wall.transform.position = new Vector3(get_Wall.transform.position.x, get_Wall.transform.position.y - 1, 0);
                        if (isBoss)
                            count.Instance.fillBar();
                    }
                }
                break;
            case 'R':
                {
                    if (y < 9 && (tileMap[x, y + 1] == 0 || tileMap[x, y + 1] == 1) && wallMap[x, y + 1] == 0)
                    {
                        if (tileMap[x, y] == 4)
                        {
                            wallMap[x, y + 1] = 4;
                            wallMap[x, y] = 0;
                            tileMap[x, y + 1] = 4;
                            tileMap[x, y] = 0;
                            tilePrefab[x, y + 1].GetComponent<SpriteRenderer>().sprite = shadowSprite;
                            audioSource.clip = dragWood;
                            audioSource.Play();
                        }
                        else if (tileMap[x, y] == 5)
                        {
                            wallMap[x, y + 1] = 5;
                            wallMap[x, y] = 0;
                            tileMap[x, y + 1] = 5;
                            tileMap[x, y] = 0;
                            tilePrefab[x, y + 1].GetComponent<SpriteRenderer>().sprite = shadowSprite;
                            audioSource.clip = dragWood;
                            audioSource.Play();
                        }
                        else if (wallMap[x, y] == 11)
                        {
                            wallMap[x, y + 1] = 11;
                            wallMap[x, y] = 0;
                            tileMap[x, y + 1] = 1;
                            tileMap[x, y] = 1;
                            audioSource.clip = dragfirefly;
                            audioSource.Play();
                        }
                        get_Wall.GetComponent<wall_Info>().Set(x, y + 1);
                        get_Wall.transform.position = new Vector3(get_Wall.transform.position.x + 1, get_Wall.transform.position.y, 0);
                        if (isBoss)
                            count.Instance.fillBar();
                    }
                }
                break;
            case 'L':
                {
                    if (y > 0 && (tileMap[x, y - 1] == 0 || tileMap[x, y - 1] == 1) && wallMap[x, y - 1] == 0)
                    {
                        if (tileMap[x, y] == 4)
                        {
                            wallMap[x, y - 1] = 4;
                            wallMap[x, y] = 0;
                            tileMap[x, y - 1] = 4;
                            tileMap[x, y] = 0;
                            tilePrefab[x, y - 1].GetComponent<SpriteRenderer>().sprite = shadowSprite;
                            audioSource.clip = dragWood;
                            audioSource.Play();
                        }
                        else if (tileMap[x, y] == 5)
                        {
                            wallMap[x, y - 1] = 5;
                            wallMap[x, y] = 0;
                            tileMap[x, y - 1] = 5;
                            tileMap[x, y] = 0;
                            tilePrefab[x, y - 1].GetComponent<SpriteRenderer>().sprite = shadowSprite;
                            audioSource.clip = dragWood;
                            audioSource.Play();
                        }
                        else if (wallMap[x, y] == 11)
                        {
                            wallMap[x, y - 1] = 11;
                            wallMap[x, y] = 0;
                            tileMap[x, y - 1] = 1;
                            tileMap[x, y] = 1;
                            audioSource.clip = dragfirefly;
                            audioSource.Play();
                        }
                        get_Wall.GetComponent<wall_Info>().Set(x, y - 1);
                        get_Wall.transform.position = new Vector3(get_Wall.transform.position.x - 1, get_Wall.transform.position.y, 0);
                        if (isBoss)
                            count.Instance.fillBar();
                    }
                }
                break;

            default:
                break;
        }
        detectedWall = false;
    }

    //도깨비불 on / off
    public void SwitchFenFire()
    {
        if (Input.GetMouseButtonDown(0) && Touchable)
        {       
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("fenfire") && hit.transform.gameObject.GetComponent<FenFire>().isOnFenFire == true)
                {
                    if (isBoss)
                      count.Instance.fillBar();

                    Debug.Log("turn off the fenfire");
                    audioSource.clip = fenfireOff; audioSource.Play();

                    get_Wall = hit.transform.gameObject;
                    getWall_x = get_Wall.transform.gameObject.GetComponent<wall_Info>().get_X();
                    getWall_y = get_Wall.transform.gameObject.GetComponent<wall_Info>().get_Y();
                    tilePrefab[getWall_x, getWall_y].GetComponent<SpriteRenderer>().sprite = shadowSprite;
                    get_Wall.GetComponent<Animator>().runtimeAnimatorController = 
                        (RuntimeAnimatorController)RuntimeAnimatorController.Instantiate(Resources.Load("Ani\\goblinfire1off",typeof(RuntimeAnimatorController)));
                    get_Wall.GetComponent<FenFire>().isOnFenFire = false;
                }
                else if (hit.collider.CompareTag("fenfire") && hit.transform.gameObject.GetComponent<FenFire>().isOnFenFire == false )
                {
                    if (isBoss)
                      count.Instance.fillBar();
                    
                    Debug.Log("turn on the fenfire");
                    audioSource.clip = fenfireOn; audioSource.Play();

                    get_Wall = hit.transform.gameObject;
                    getWall_x = get_Wall.transform.gameObject.GetComponent<wall_Info>().get_X();
                    getWall_y = get_Wall.transform.gameObject.GetComponent<wall_Info>().get_Y();
                    tilePrefab[getWall_x, getWall_y].GetComponent<SpriteRenderer>().sprite = groundSprite;
                    if(SceneManager.GetActiveScene().buildIndex < 29)
                    {
                    get_Wall.GetComponent<Animator>().runtimeAnimatorController =
                        (RuntimeAnimatorController)RuntimeAnimatorController.Instantiate(Resources.Load("Ani\\goblinfire1", typeof(RuntimeAnimatorController)));
                    }
                    else
                    {
                        get_Wall.GetComponent<Animator>().runtimeAnimatorController =
                        (RuntimeAnimatorController)RuntimeAnimatorController.Instantiate(Resources.Load("Ani\\goblinfirefs1", typeof(RuntimeAnimatorController)));
                    }
                    get_Wall.GetComponent<FenFire>().isOnFenFire = true;
                }
            }
        }
    }



    //매 프레임마다 모든 길 타일을 어둡게 칠해놓는다.
    public void makeShadow()
    {
        for (int i = 0; i < wallMap.GetLength(0); i++)  //row
        {
            for (int j = 0; j < wallMap.GetLength(1); j++)  //column
            {
                //make shadow only 0, 1 tile
                if (tileMap[i,j] != 0 && tileMap[i,j] != 1)
                {
                    //but make shadow on the fenfire position if fenfire is off
                    if ((tileMap[i, j] == 13 || tileMap[i, j] == 14) && fenfirePrefab[i,j].GetComponent<FenFire>().isOnFenFire == false)
                    {
                        tilePrefab[i, j].GetComponent<SpriteRenderer>().sprite = shadowSprite;
                    }
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
                //반딧불이
                if (wallMap[i, j] == 11)
                {
                    for(int n = -1; n <= 1; n++)
                    {
                        for (int m = -1; m <= 1; m++)
                        {
                            //handling out of range
                            if((i + n < 0) || (i + n >= tileMap.GetLength(0)) || (j + m < 0) || (j + m >= tileMap.GetLength(1)))
                            {
                                continue;
                            }
                            //밝게할 타일이 타일맵의 0이나 1이 아니면(길 타일이 아니면) 건너뛴다. 길이 아니면 칠하면 안되니까.
                            if (tileMap[i + n, j + m] != 0 && tileMap[i + n, j + m] != 1)
                            {
                                //근데 off상태의 도깨비불이면 땅은 밝게 칠한다.
                                if((tileMap[i + n, j + m] == 13 || tileMap[i + n, j + m] == 14) && fenfirePrefab[i + n, j + m].GetComponent<FenFire>().isOnFenFire == false)
                                {
                                    tilePrefab[i + n, j + m].GetComponent<SpriteRenderer>().sprite = groundSprite;
                                }
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
                    for (int k = i + 1; k < tileMap.GetLength(0); k++)
                    {
                        if (tileMap[k, j] != 0 && tileMap[k, j] != 1)
                        {
                            if (tileMap[k, j] == 13 && fenfirePrefab[k, j].GetComponent<FenFire>().isOnFenFire == false)
                            {
                                tilePrefab[k, j].GetComponent<SpriteRenderer>().sprite = groundSprite;
                            }
                            else if (tileMap[k, j] == (int)TileType.upDownWood || tileMap[k, j] == 5 || tileMap[k, j] == 2 || tileMap[k, j] >= 20)
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
                    for (int k = i - 1; k >= 0; k--)
                    {
                        if (tileMap[k, j] != 0 && tileMap[k, j] != 1)
                        {
                            if (tileMap[k, j] == 13 && fenfirePrefab[k, j].GetComponent<FenFire>().isOnFenFire == false)
                            {
                                tilePrefab[k, j].GetComponent<SpriteRenderer>().sprite = groundSprite;
                            }
                            else if (tileMap[k, j] == (int)TileType.upDownWood || tileMap[k, j] == 5 || tileMap[k, j] == 2 || tileMap[k, j] >= 20)
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
                    for (int k = j + 1; k < tileMap.GetLength(1); k++)
                    {
                        if (tileMap[i, k] != 0 && tileMap[i, k] != 1)
                        {
                            if (tileMap[i, k] == 13 && fenfirePrefab[i, k].GetComponent<FenFire>().isOnFenFire == false)
                            {
                                tilePrefab[i, k].GetComponent<SpriteRenderer>().sprite = groundSprite;
                            }
                            else if (tileMap[i, k] == (int)TileType.upDownWood || tileMap[i, k] == 5 || tileMap[i, k] == 2 || tileMap[i, k] >= 20)
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
                    for (int k = j - 1; k >= 0; k--)
                    {
                        if (tileMap[i, k] != 0 && tileMap[i, k] != 1)
                        {
                            if (tileMap[i, k] == 13 && fenfirePrefab[i, k].GetComponent<FenFire>().isOnFenFire == false)
                            {
                                tilePrefab[i, k].GetComponent<SpriteRenderer>().sprite = groundSprite;
                            }
                            else if (tileMap[i, k] == (int)TileType.upDownWood || tileMap[i, k] == 5 || tileMap[i, k] == 2 || tileMap[i, k] >= 20)
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

            }
        }
    }

}
