using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class Grid : MonoBehaviour
{
    const int cols = 32;
    const int rows = 32;

    public GameObject trumpWall;
    public GameObject refillStation;
    public GameObject oilStation;

    Node[,] grid;

    ArrayList openNodes = new ArrayList();
    ArrayList closedNodes = new ArrayList();

    public ArrayList FindPath(Node start, Node end)
    {
        ArrayList path = new ArrayList();

        openNodes.Clear();
        openNodes.Add(start);
        closedNodes.Clear();

        foreach (Node n in grid)
        {
            if (n != null)
            {
                n.parent = null;
                n.totalCost = 0;
            }

        }

        // wipe all parent and totalCost data from the nodes

        while (openNodes.Count > 0)
        {
            //openNodes.Sort();

            Node currentNode = openNodes[0] as Node;

            if (currentNode == end)
            {
                Node node = currentNode;
                // output the path here
                while (node != start)
                {
                    path.Insert(0, node);
                    node = node.parent;
                }
                break;
            }

            openNodes.Remove(currentNode);
            closedNodes.Add(currentNode);

            foreach (Node.Edge e in currentNode.edges)
            {
                if (!closedNodes.Contains(e.node) && !openNodes.Contains(e.node))
                {
                    openNodes.Add(e.node);

                    float newCost = currentNode.totalCost + e.cost;
                    if (newCost > e.node.totalCost)
                    {
                        e.node.totalCost = newCost;
                        e.node.parent = currentNode;
                    }
                }
            }
        }
        return path;
    }

    /*  void MakeTestGrid()
      {
          grid = new Node[9];
          grid[0] = new Node(new Vector3(0, 0, 0));
          grid[1] = new Node(new Vector3(1, 0, 0));
          grid[2] = new Node(new Vector3(2, 0, 0));
          grid[3] = new Node(new Vector3(0, 1, 0));
          grid[4] = new Node(new Vector3(1, 1, 0));
          grid[5] = new Node(new Vector3(2, 1, 0));
          grid[6] = new Node(new Vector3(0, 2, 0));
          grid[7] = new Node(new Vector3(1, 2, 0));
          grid[8] = new Node(new Vector3(2, 2, 0));


          grid[0].AddEdge(grid[3]);
          grid[1].AddEdge(grid[2]);
          grid[1].AddEdge(grid[4]);
          grid[2].AddEdge(grid[5]);
          grid[2].AddEdge(grid[1]);
          grid[3].AddEdge(grid[0]);
          grid[3].AddEdge(grid[4]);
          grid[3].AddEdge(grid[6]);
          grid[4].AddEdge(grid[1]);
          grid[4].AddEdge(grid[3]);
          grid[4].AddEdge(grid[7]);
          grid[5].AddEdge(grid[2]);
          grid[5].AddEdge(grid[8]);
          grid[6].AddEdge(grid[3]);
          grid[7].AddEdge(grid[4]);
          grid[7].AddEdge(grid[8]);
          grid[8].AddEdge(grid[5]);
          grid[8].AddEdge(grid[7]);
      }
                                      */

    void PathfindingGrid()
    {
        int[,] gridLayout = new int[rows, cols]
        {
            {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
            {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
            {1, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1},
            {1, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1},
            {1, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
            {1, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
            {1, 0, 0, 0, 0, 0, 0, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 1},
            {1, 0, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
            {1, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
            {1, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 1, 1, 1, 1, 1, 0, 0, 1, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1},
            {1, 0, 1, 1, 1, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
            {1, 0, 1, 3, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
            {1, 0, 1, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 1},
            {1, 0, 1, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 1},
            {1, 0, 1, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 1},
            {1, 0, 1, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 1, 0, 0, 1, 0, 1},
            {1, 0, 1, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 1, 0, 0, 1, 0, 1},
            {1, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 1, 0, 1},
            {1, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 1, 0, 1},
            {1, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 1, 0, 1, 0, 0, 1, 0, 1},
            {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 3, 0, 1, 0, 1},
            {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 1, 1, 1, 0, 1},
            {1, 1, 1, 1, 1, 1, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 1},
            {1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 0, 1, 0, 0, 0, 0, 0, 0, 1},
            {1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 2, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1},
            {1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1},
            {1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1},
            {1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1},
            {1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1},
            {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1},
            {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1},
            {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},

        };

        grid = new Node[rows, cols];

        for (int x = 0; x < rows; x++)
        {
            for (int y = 0; y < cols; y++)
            {
                if (gridLayout[x, y] == 0)
                {
                    grid[x, y] = new Node(new Vector3(x, 0, y));
                }
                if (gridLayout[x, y] == 1)
                {
                    GameObject obj = Instantiate(trumpWall, new Vector3(x, 0, y), new Quaternion());
                }
                if (gridLayout[x, y] == 2)
                {
                    GameObject obj = Instantiate(refillStation, new Vector3(x, 0, y), new Quaternion());
                }
                if (gridLayout[x, y] == 3)
                {
                    GameObject obj = Instantiate(oilStation, new Vector3(x, 0, y), new Quaternion());
                }

            }

        }

        for (int x = 0; x < cols; x++)
        {
            for (int y = 0; y < rows; y++)
            {
                if (gridLayout[x, y] == 0)
                {
                    if (x > 0 && grid[x - 1, y] != null)
                    {
                        grid[x, y].AddEdge(grid[x - 1, y]);
                    }
                    if (x < cols - 1 && grid[x + 1, y] != null)
                    {
                        grid[x, y].AddEdge(grid[x + 1, y]);
                    }
                    if (y > 0 && grid[x, y - 1] != null)
                    {
                        grid[x, y].AddEdge(grid[x, y - 1]);
                    }
                    if (y < rows - 1 && grid[x, y + 1] != null)
                    {
                        grid[x, y].AddEdge(grid[x, y + 1]);
                    }
                }
            }
        }
        return;
    }


    void Start()
    {
        PathfindingGrid();
        ArrayList path = FindPath(grid[0, 2], grid[2, 2]);
    }

    public Node GetNodeAt(Vector3 pos)
    {
        int i = Mathf.Clamp((int)(pos.x + 0.5f), 0, rows - 1);
        int j = Mathf.Clamp((int)(pos.z + 0.5f), 0, cols - 1);

        return grid[i, j];
    }
}
