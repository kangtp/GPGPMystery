using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TileArray;
using UnityEngine.Tilemaps;

public class FenFire : MonoBehaviour
{
    public bool isOnFenFire = false;
    GameObject[,] tilePrefab;
    GameObject[,] fenfirePrefab;
    int i, j;

    public Sprite groundSprite;
    public Sprite shadowSprite;
    // Start is called before the first frame update
    void Start()
    {
        tilePrefab = FindObjectOfType<TileArray>().tilePrefab;
        fenfirePrefab = FindObjectOfType<TileArray>().fenfirePrefab;
        i = GetComponent<wall_Info>().get_X();
        j = GetComponent<wall_Info>().get_Y();
    }

    // Update is called once per frame
    void Update()
    {
        FenfireBright();
    }

    
    public void FenfireBright()
    {
        //도깨비불 on일때만 실행
        if (isOnFenFire)
        {
            //아래 방향
            for (int k = i + 1; k < TileArray.Instance.tileMap.GetLength(0); k++)
            {
                if (TileArray.Instance.tileMap[k, j] != 0 && TileArray.Instance.tileMap[k, j] != 1)
                {
                    if ((TileArray.Instance.tileMap[k,j] == 13 || TileArray.Instance.tileMap[k, j] == 14) && fenfirePrefab[k,j].GetComponent<FenFire>().isOnFenFire == false)
                    {
                        tilePrefab[k, j].GetComponent<SpriteRenderer>().sprite = groundSprite;
                    }
                    if (TileArray.Instance.tileMap[k, j] == (int)TileType.upDownWood || TileArray.Instance.tileMap[k, j] == 5 || TileArray.Instance.tileMap[k, j] == 2 || TileArray.Instance.tileMap[k, j] >= 20)
                    {
                        break;
                    }
                    continue;
                }

                //불이 비추는 방향 밝게
                TileArray.Instance.tileMap[k, j] = (int)TileType.ground;
                tilePrefab[k, j].GetComponent<SpriteRenderer>().sprite = groundSprite;
            }
            //위 방향
            for (int k = i - 1; k >= 0; k--)
            {
                if (TileArray.Instance.tileMap[k, j] != 0 && TileArray.Instance.tileMap[k, j] != 1)
                {
                    if ((TileArray.Instance.tileMap[k, j] == 13 || TileArray.Instance.tileMap[k, j] == 14) && fenfirePrefab[k, j].GetComponent<FenFire>().isOnFenFire == false)
                    {
                        tilePrefab[k, j].GetComponent<SpriteRenderer>().sprite = groundSprite;
                    }
                    if (TileArray.Instance.tileMap[k, j] == (int)TileType.upDownWood || TileArray.Instance.tileMap[k, j] == 5 || TileArray.Instance.tileMap[k, j] == 2 || TileArray.Instance.tileMap[k, j] >= 20)
                    {
                        break;
                    }
                    continue;
                }

                //불이 비추는 방향 밝게
                TileArray.Instance.tileMap[k, j] = (int)TileType.ground;
                tilePrefab[k, j].GetComponent<SpriteRenderer>().sprite = groundSprite;
            }
            //오른쪽 방향
            for (int k = j + 1; k < TileArray.Instance.tileMap.GetLength(1); k++)
            {
                
                if (TileArray.Instance.tileMap[i, k] != 0 && TileArray.Instance.tileMap[i, k] != 1)
                {
                    if ((TileArray.Instance.tileMap[i, k] == 13 || TileArray.Instance.tileMap[i, k] == 14) && fenfirePrefab[i, k].GetComponent<FenFire>().isOnFenFire == false)
                    {
                        tilePrefab[i, k].GetComponent<SpriteRenderer>().sprite = groundSprite;
                    }
                    if (TileArray.Instance.tileMap[i, k] == (int)TileType.upDownWood || TileArray.Instance.tileMap[i, k] == 5 || TileArray.Instance.tileMap[i, k] == 2 || TileArray.Instance.tileMap[i, k] >= 20)
                    {
                        break;
                    }
                    continue;
                }

                //불이 비추는 방향 밝게
                TileArray.Instance.tileMap[i, k] = (int)TileType.ground;
                tilePrefab[i, k].GetComponent<SpriteRenderer>().sprite = groundSprite;
            }
            //왼쪽 방향
            for (int k = j - 1; k >= 0; k--)
            {
                
                if (TileArray.Instance.tileMap[i, k] != 0 && TileArray.Instance.tileMap[i, k] != 1)
                {
                    if ((TileArray.Instance.tileMap[i, k] == 13 || TileArray.Instance.tileMap[i, k] == 14) && fenfirePrefab[i, k].GetComponent<FenFire>().isOnFenFire == false)
                    {
                        tilePrefab[i, k].GetComponent<SpriteRenderer>().sprite = groundSprite;
                    }
                    if (TileArray.Instance.tileMap[i, k] == (int)TileType.upDownWood || TileArray.Instance.tileMap[i, k] == 5 || TileArray.Instance.tileMap[i, k] == 2 || TileArray.Instance.tileMap[i, k] >= 20)
                    {
                        break;
                    }
                    continue;
                }

                //불이 비추는 방향 밝게
                TileArray.Instance.tileMap[i, k] = (int)TileType.ground;
                tilePrefab[i, k].GetComponent<SpriteRenderer>().sprite = groundSprite;
            }
        }
                

            
    }
}

