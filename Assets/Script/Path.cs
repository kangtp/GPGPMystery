using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{

    [SerializeField]
    public int[,] pathMap = new int[,]
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
    public int[,] m_pathMap = new int[,]
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


    private GameObject Hunter;
    private GameObject Monster;

    public int startPosition_x;
    public int startPosition_y;
    public int GoalPosition_x;
    public int GoalPosition_y;

    public int m_startPosition_x;
    public int m_startPosition_y;
    public int m_GoalPosition_x;
    public int m_GoalPosition_y;

    private const int pathable = 1; // 통행가능
    private const int pathable_monster = 0; // 어둑시니 통행가능
    private const int blocked = 22; // 막혀있음
    private const int visited = 33;

    private int row;
    private int col;

    private List<(int, int)> pathList = new List<(int, int)>();
    private List<(int, int)> m_pathList = new List<(int, int)>();


    public float size;

    private void Start()
    {
        size = 1.5f;
        row = pathMap.GetLength(0);
        col = pathMap.GetLength(1);
        Get_tilemap();
        PopulatePathmap();
    }

    public void Go_Button()
    {
        Get_tilemap();
        /*
                if (monster_FindPath(m_startPosition_x, m_startPosition_y) && hunter_FindPath(startPosition_x, startPosition_y))
                {
                    StartCoroutine(move_hunter());
                    StartCoroutine(move_monster());
                    Debug.Log("클리어");
                }
                else
                {
                    Debug.Log("게임오버");
                }
                PrintArray(m_pathMap);
                */

        BFS();
       // Debug.Log(path.Count.ToString());
        PrintPathList(path);
        StartCoroutine(move_hunter());
    }


    public void Get_tilemap()
    {
        if (TileArray.Instance != null)
        {
            for (int i = 0; i < TileArray.Instance.tileMap.GetLength(0); i++)
            {
                for (int j = 0; j < TileArray.Instance.tileMap.GetLength(1); j++)
                {
                    pathMap[i, j] = TileArray.Instance.tileMap[i, j];
                    m_pathMap[i, j] = TileArray.Instance.tileMap[i, j];
                    if (pathMap[i, j] == 7)
                    {
                        startPosition_x = i;
                        startPosition_y = j;
                        pathMap[i, j] = 1;
                    }
                    else if (pathMap[i, j] == 8)
                    {
                        GoalPosition_x = i;
                        GoalPosition_y = j;
                        pathMap[i, j] = 1;
                    }

                    else if (m_pathMap[i, j] == 9)
                    {
                        m_startPosition_x = i;
                        m_startPosition_y = j;
                        m_pathMap[i, j] = 0;
                    }
                    else if (m_pathMap[i, j] == 10)
                    {
                        m_GoalPosition_x = i;
                        m_GoalPosition_y = j;
                        m_pathMap[i, j] = 0;
                    }

                }
            }
        }
    }

    public void PopulatePathmap()
    {
        for (int i = 0; i < TileArray.Instance.tileMap.GetLength(0); i++)
        {
            for (int j = 0; j < TileArray.Instance.tileMap.GetLength(1); j++)
            {

                if (TileArray.Instance.tileMap[i, j] == 7)
                {
                    GameObject prefab = Resources.Load("Hunter") as GameObject;
                    Hunter = Instantiate(prefab, Vector3.zero, Quaternion.identity) as GameObject;
                    Hunter.transform.position = TileArray.Instance.tilePrefab[i, j].transform.position;
                }

                if (TileArray.Instance.tileMap[i, j] == 9)
                {
                    GameObject prefab = Resources.Load("Monster") as GameObject;
                    Monster = Instantiate(prefab, Vector3.zero, Quaternion.identity) as GameObject;
                    Monster.transform.position = TileArray.Instance.tilePrefab[i, j].transform.position;
                }


            }
        }
    }

    IEnumerator move_hunter()
    {
        if (Hunter != null)
        {
            int direction_x = 0;
            int direction_y = 0;

            for (int i = 0; i < path.Count - 1; i++)
            {
                direction_x = path[i + 1].X - path[i].X;
                direction_y = path[i + 1].Y - path[i].Y;
                yield return new WaitForSeconds(0.5f);
                Hunter.transform.position = new Vector3(Hunter.transform.position.x + direction_x, Hunter.transform.position.y - direction_y, 0);
            }
            yield return new WaitForSeconds(0.5f);
            Hunter.transform.position = new Vector3(Hunter.transform.position.x + direction_x, Hunter.transform.position.y - direction_y, 0);

        }
    }

    IEnumerator move_monster()
    {
        if (Monster != null)
        {
            int direction_x = 0;
            int direction_y = 0;
            m_pathList.Reverse();

            for (int i = 0; i < m_pathList.Count - 1; i++)
            {
                direction_x = m_pathList[i + 1].Item1 - m_pathList[i].Item1;
                direction_y = m_pathList[i + 1].Item2 - m_pathList[i].Item2;
                yield return new WaitForSeconds(0.5f);
                Debug.Log("x : " + direction_x + ", y : " + direction_y);
                Monster.transform.position = new Vector3(Monster.transform.position.x + direction_y, Monster.transform.position.y - direction_x, 0);
            }
            yield return new WaitForSeconds(0.5f);
            Monster.transform.position = new Vector3(Monster.transform.position.x + direction_y, Monster.transform.position.y - direction_x, 0);
        }
    }

    ////////////////////////////////////////////////////////////////////////////
    /*
    public bool hunter_FindPath(int x, int y)
    {
        int rowN = pathMap.GetLength(0);
        int colN = pathMap.GetLength(1);

        if (x < 0 || y < 0 || x >= rowN || y >= colN)
        {
            return false;
        }
        else if (pathMap[x, y] != pathable)
        {
            return false;
        }
        else if (x == GoalPosition_x && y == GoalPosition_y)
        {
            pathMap[x, y] = visited;
            pathList.Add((x, y));
            return true;
        }
        else
        {
            pathMap[x, y] = visited;

            if (hunter_FindPath(x - 1, y) || hunter_FindPath(x, y + 1) || hunter_FindPath(x + 1, y) || hunter_FindPath(x, y - 1))
            {
                pathList.Add((x, y));
                return true;
            }
            pathMap[x, y] = blocked;
            return false;
        }
    }

    public bool monster_FindPath(int x, int y)
    {
        int rowN = m_pathMap.GetLength(0);
        int colN = m_pathMap.GetLength(1);

        if (x < 0 || y < 0 || x >= rowN || y >= colN)
        {
            return false;
        }
        else if (m_pathMap[x, y] != pathable_monster)
        {
            return false;
        }
        else if (x == m_GoalPosition_x && y == m_GoalPosition_y)
        {
            m_pathMap[x, y] = visited;
            m_pathList.Add((x, y));
            return true;
        }
        else
        {
            m_pathMap[x, y] = visited;

            if (monster_FindPath(x - 1, y) || monster_FindPath(x, y + 1) || monster_FindPath(x + 1, y) || monster_FindPath(x, y - 1))
            {
                m_pathList.Add((x, y));
                return true;
            }
            m_pathMap[x, y] = blocked;
            return false;
        }
    }

    private void PrintArray(int[,] array)
    {
        string output = "Array Contents:\n";
        int rows = array.GetLength(0);
        int cols = array.GetLength(1);

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                output += array[i, j] + " ";
            }
            output += "\n";
        }

        Debug.Log(output);
    }
*/
    ///////////////////////////////////////////////////////////////////////////
    /*
    public bool[,] visitMap;
    int[,] DIRECTION_OFFSETS =
    {
    {1, 0},		
	{-1, 0},			
	{0, 1},			
	{0, -1}			
};

    int NUM_DIRECTIONS = 4;

    struct MapPosition
    {
        public int x;
        public int y;
        public int direction;
    };

    public enum PosStatus { NOT_VISIT = 0, WALL = 1, VISIT = 2 };


    bool is_movable(int[,] maze, MapPosition pos)
    {
        // 현재 위치가 미로의 범위를 벗어나는 지 확인
        if (pos.x < 0 || pos.y < 0 || pos.x >= row || pos.y >= col)
            return false;
        // 현재 위치가 벽이거나 이미 방문한 곳인지 확인
        if (maze[pos.y, pos.x] != pathable)
            return false;
        return true;
    }

    // BFS 
    bool findPath(int[,] maze, int[,] visited, MapPosition pos, int goal)
    {

        Queue<MapPosition> queue = new Queue<MapPosition>();
        MapPosition currPos;
        MapPosition nextPos;
        int nextX;
        int nextY;

        queue.Enqueue(pos); // 처음 시작 위치를 큐에 넣기
        while (queue.Count > 0)
        {
            currPos = queue.Dequeue(); // 큐에서 꺼내기
            if (maze[currPos.x, currPos.y] == goal)          // 도착 지점에 도달한 경우
                return true;
            if (visited[currPos.x, currPos.y] == 33)     // 현재 위치를 방문했는지 확인
                continue;
            visited[currPos.x, currPos.y] = 33; // 현재 위치를 VISIT 으로 변경
            while (currPos.direction < NUM_DIRECTIONS)
            {
                // 다음 좌표 설정
                nextX = currPos.x + DIRECTION_OFFSETS[currPos.direction, 0];
                nextY = currPos.y + DIRECTION_OFFSETS[currPos.direction, 1];
                nextPos.x = nextX;
                nextPos.y = nextY;
                nextPos.direction = 0;

                if (is_movable(maze, nextPos))
                {
                    // 다음 이동 지점에 이전 이동 거리 + 1 저장
                    maze[nextY, nextX] = maze[currPos.x, currPos.y] + 1;
                    queue.Enqueue(nextPos); // 다음 이동 지점 큐에 저장
                }
                pos.direction += 1;
                //pathList.Add((pos.x,pos.y));
            }
        }
        return true;
    }
*/
    ///////////////////////////////////////////////////////////////////////////

    class Pos
    {
        public Pos(int x, int y) { X = x; Y = y; }
        public int X;
        public int Y;

    }


    List<Pos> path = new List<Pos>();

    // !!!!!!!!! 밑에 getlength한번 테스트해보고 안되면 위치 바꿔보기
    private void BFS()
    {
        int[] dirX = new int[] { -1, 0, 1, 0 };
        int[] dirY = new int[] { 0, -1, 0, 1 };

        bool[,] found = new bool[pathMap.GetLength(0), pathMap.GetLength(1)];
        Pos[,] parent = new Pos[pathMap.GetLength(0), pathMap.GetLength(1)];

        for (int i = 0; i < pathMap.GetLength(0); i++)
        {
            for (int j = 0; j < pathMap.GetLength(1); j++)
            {
                parent[i, j] = new Pos(0, 0);
            }
        }

        Queue<Pos> q = new Queue<Pos>();
        q.Enqueue(new Pos(startPosition_x, startPosition_y));
        found[startPosition_x, startPosition_y] = true;

        parent[startPosition_x, startPosition_y] = new Pos(startPosition_x, startPosition_y);



        while (q.Count > 0)
        {
            Pos pos = q.Dequeue();

            int nowX = pos.X;
            int nowY = pos.Y;


            for (int i = 0; i < 4; i++)
            {
                int nextX = nowX + dirX[i];
                int nextY = nowY + dirY[i];

                if (nextX < 0 || nextX >= pathMap.GetLength(0) || nextY < 0 || nextY >= pathMap.GetLength(1))
                    continue;
                if (pathMap[nextX, nextY] != 1) //////////여기서 0으로 바꿔주면댐
                    continue;
                if (found[nextX, nextY])
                    continue;

                q.Enqueue(new Pos(nextX, nextY));
                found[nextX, nextY] = true;
                parent[nextX, nextY] = new Pos(nowX, nowY);
            }
        }

        int x = GoalPosition_x;
        int y = GoalPosition_y;

        while (parent[x, y].X != x || parent[x, y].Y != y)
        {
            path.Add(new Pos(y, x));

            Pos pos = parent[x, y];
            x = pos.X;
            y = pos.Y;

        }
        path.Add(new Pos(y, x));
        path.Reverse();
        //PrintFoundArray(found);
    }


    private void PrintFoundArray(bool[,] found)
    {
        int rows = found.GetLength(0);
        int cols = found.GetLength(1);

        string output = "Found Array:\n";

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                output += found[i, j] ? "1 " : "0 ";
            }
            output += "\n";
        }

        Debug.Log(output);
    }

    private void PrintPathList(List<Pos> path)
    {
        string output = "Path List:\n";

        foreach (Pos pos in path)
        {
            output += $"(Y: {pos.Y}, X: {pos.X})\n";
        }

        Debug.Log(output);
    }
}


