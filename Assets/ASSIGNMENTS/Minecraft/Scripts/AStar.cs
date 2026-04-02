using UnityEngine;
using System.Collections.Generic;

    //Gcost - distance from start
    //Hcost - distance from end
    //Fcost - distance sum
    
    /*
     * getPath will look like:
     * OPEN
     * CLOSED
     * add start node to open
     * while open:
     * current = node with lowest f(cost)
     * add current to closed
     * if current is target, return
     * for each neighbor in current:
     * if neighbor in closed OR !walkable
     * pass
     * if path to neighbor is shorter or neighbor is not in open
     * find f cost
     * set parent of neighbor to current
     * if neighbor not in open
     * add to open
     */

public static class AStar
{
    public static List<Vector3Int> GetPath(
        Vector3Int start,
        Vector3Int goal,
        HashSet<Vector3Int> blocked,
        Vector3Int gridMin,
        Vector3Int gridMax)
    {
        List<Vector3Int> emptyPath = new List<Vector3Int>();

        if (!IsWithinBounds(start, gridMin, gridMax) || !IsWithinBounds(goal, gridMin, gridMax))
            return emptyPath;

        if (blocked.Contains(start) || blocked.Contains(goal))
            return emptyPath;

        PriorityQueue<Node> openSet = new PriorityQueue<Node>();
        HashSet<Vector3Int> closedSet = new HashSet<Vector3Int>();
        Dictionary<Vector3Int, int> bestGCost = new Dictionary<Vector3Int, int>();

        Node startNode = new Node(start, 0, GetHCost(start, goal), null);
        openSet.Enqueue(startNode);
        bestGCost[start] = 0;

        while (openSet.Count > 0)
        {
            Node current = openSet.Dequeue();

            if (closedSet.Contains(current.position))
                continue;

            if (current.position == goal)
                return ReconstructPath(current);

            closedSet.Add(current.position);

            foreach (Vector3Int neighborPos in GetNeighbors(current.position, gridMin, gridMax))
            {
                Vector3Int normalizedNeighbor = new Vector3Int(neighborPos.x, 0, neighborPos.z);

                if (blocked.Contains(normalizedNeighbor) || closedSet.Contains(neighborPos))
                {
                    continue;
                }

                int tentativeGCost = current.gCost + GetGCost(current.position, neighborPos);

                if (bestGCost.TryGetValue(neighborPos, out int knownG) && tentativeGCost >= knownG)
                    continue;

                bestGCost[neighborPos] = tentativeGCost;

                Node neighborNode = new Node(
                    neighborPos,
                    tentativeGCost,
                    GetHCost(neighborPos, goal),
                    current
                );

                openSet.Enqueue(neighborNode);
            }
        }
        return emptyPath;
    }

    private static List<Vector3Int> ReconstructPath(Node endNode)
    {
        List<Vector3Int> path = new List<Vector3Int>();
        Node current = endNode;

        while (current != null)
        {
            path.Add(current.position);
            current = current.parent;
        }

        path.Reverse();
        return path;
    }
    //CHANGE THESE TO EUCLIDEAN
    private static int GetHCost(Vector3Int a, Vector3Int b)
    {
        return Mathf.Abs(a.x - b.x) + Mathf.Abs(a.z - b.z);
    }

    private static int GetGCost(Vector3Int a, Vector3Int b)
    {
        return 1;
    }

    private static IEnumerable<Vector3Int> GetNeighbors(Vector3Int pos, Vector3Int gridMin, Vector3Int gridMax)
    {
        Vector3Int[] directions =
        {
            new Vector3Int(1, 0, 0),
            new Vector3Int(-1, 0, 0),
            new Vector3Int(0, 0, 1),
            new Vector3Int(0, 0, -1),
            new Vector3Int(1, 0, 1),
            new Vector3Int(-1, 0, -1),
            new Vector3Int(-1, 0, 1),
            new Vector3Int(1, 0, -1),
            
            new Vector3Int(1, 1, 0),
            new Vector3Int(-1, 1, 0),
            new Vector3Int(0, 1, 1),
            new Vector3Int(0, 1, -1),
            new Vector3Int(1, 1, 1),
            new Vector3Int(-1, 1, -1),
            new Vector3Int(-1, 1, 1),
            new Vector3Int(1, 1, -1),
            
            new Vector3Int(1, -1, 0),
            new Vector3Int(-1, -1, 0),
            new Vector3Int(0, -1, 1),
            new Vector3Int(0, -1, -1),
            new Vector3Int(1, -1, 1),
            new Vector3Int(-1, -1, -1),
            new Vector3Int(-1, -1, 1),
            new Vector3Int(1, -1, -1),
        };

        foreach (Vector3Int dir in directions)
        {
            Vector3Int next = pos + dir;

            if (!IsWithinBounds(next, gridMin, gridMax))
                continue;

            yield return next;
        }
    }

    private static bool IsWithinBounds(Vector3Int pos, Vector3Int gridMin, Vector3Int gridMax)
    {
        return pos.x >= gridMin.x && pos.x <= gridMax.x &&
               pos.y >= gridMin.y && pos.y <= gridMax.y &&
               pos.z >= gridMin.z && pos.z <= gridMax.z;
    }
}