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

    /*
        public class Node
        {

            public int x;
            public int y; // 행렬안 칸의 x,y좌표

            public Node prev; // 연결리스트로 나타내기 위한 이전 노드 참조 변수

            public Node(int x, int y, Node prev)
            {
                this.x = x;
                this.y = y;
                this.prev = prev;
            }

        }

        public List<Node> bfs(int[,] maze)
        {
            //check visited matrix
            int numRows = maze.GetLength(0);
            int numClos = maze.GetLength(1);

            bool[,] visited = new bool[numRows, numClos];

            List<Node> paths = new List<Node>();
            Queue<Node> queue = new Queue<Node>();

            visited[startPosition_y, startPosition_x] = true;
            queue.Enqueue(new Node(startPosition_x, startPosition_y, null));
            int[] dist_x = { 1, 0, -1, 0 };
            int[] dist_y = { 0, 1, 0, -1 };
            while (queue.Count > 0)
            {
                Node node = queue.Dequeue();
                if (node.x == GoalPosition_x && node.y == GoalPosition_y)
                {
                    Node s = node;
                    while (s != null)
                    {
                        paths.Insert(0, s);
                        s = s.prev;
                    }
                    break;
                }

                for (int i = 0; i < dist_x.Length; i++)
                {
                    int vx = node.x + dist_x[i];
                    int vy = node.x + dist_x[i];
                    if (is_path(vx, vy, maze, visited, numRows, numClos))
                    {
                        visited[vx, vy] = true;
                        queue.Enqueue(new Node(vx, vy, node));
                    }
                }
            }



            return paths;
        }

        public bool is_path(int x, int y, int[,] board, bool[,] visited, int row, int col)
        {
            if (x < 0 || y < 0 || x > col - 1|| y > row - 1)
            {
                return false;
            }
            if (visited[y, x] || board[y, x] == 1)
            {
                return false;
            }
            return true;
        }
    */

    private const int pathable = 0; // 통행가능
    private const int pathable_monster = 1; // 어둑시니 통행가능
    private const int blocked = 22; // 막혀있음
    private const int visited = 33;

    private List<(int,int)> pathList = new List<(int,int)>();
    private List<(int,int)> m_pathList = new List<(int,int)>();

    public bool hunter_FindPath(int x, int y)
    {
        int rowN = pathMap.GetLength(0);
        int colN = pathMap.GetLength(1);

        if (x < 0 || y < 0 || x > rowN || y > colN)
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
            pathList.Add((x,y));
            return true;
        }
        else
        {
            pathMap[x, y] = visited;
            
            if (hunter_FindPath(x - 1, y) || hunter_FindPath(x, y + 1) || hunter_FindPath(x + 1, y) || hunter_FindPath(x, y - 1))
            {
                pathList.Add((x,y));
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

        if (x < 0 || y < 0 || x > rowN || y > colN)
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
            m_pathList.Add((x,y));
            return true;
        }
        else
        {
            m_pathMap[x, y] = visited;
            
            if (hunter_FindPath(x - 1, y) || hunter_FindPath(x, y + 1) || hunter_FindPath(x + 1, y) || hunter_FindPath(x, y - 1))
            {
                m_pathList.Add((x,y));
                return true;
            }
            m_pathMap[x, y] = blocked;
            return false;
        }
    }

    private void PrintPath()
    {
        m_pathList.Reverse();
        foreach ((int x, int y) in pathList)
        {
            Debug.Log($"Path: ({x}, {y})");
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

    public float size;

    private void Start()
    {
        size = 1.5f;

        Get_tilemap();
        PopulatePathmap();
    }

    public void Go_Button()
    {
        Get_tilemap();
        if(monster_FindPath(m_startPosition_x,m_startPosition_y))
        {
            Debug.Log("클리어");
        }
        else
        {
            Debug.Log("게임오버");
        }
        PrintPath();
        
        PrintArray(m_pathMap);
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
                        pathMap[i, j] = 0;
                    }
                    if (pathMap[i, j] == 8)
                    {
                        GoalPosition_x = i;
                        GoalPosition_y = j;
                        pathMap[i, j] = 0;
                    }
                    if (pathMap[i, j] == 9)
                    {
                        m_startPosition_x = i;
                        m_startPosition_y = j;
                        m_pathMap[i, j] = 1;
                    }
                    if (pathMap[i, j] == 10)
                    {
                        m_GoalPosition_x = i;
                        m_GoalPosition_y = j;
                        m_pathMap[i, j] = 1;
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
                    GameObject hunter = Instantiate(prefab, Vector3.zero, Quaternion.identity) as GameObject;
                    hunter.transform.position = TileArray.Instance.tilePrefab[i, j].transform.position;
                }

                if (TileArray.Instance.tileMap[i, j] == 9)
                {
                    GameObject prefab = Resources.Load("Monster") as GameObject;
                    GameObject monster = Instantiate(prefab, Vector3.zero, Quaternion.identity) as GameObject;
                    monster.transform.position = TileArray.Instance.tilePrefab[i, j].transform.position;
                }


            }
        }
    }
}