using UnityEngine;

class Node : System.IComparable<Node>
{
    public Vector3Int position;
    public int gCost;
    public int hCost;
    public Node parent;
    public int fCost => gCost + hCost * 2;

    public Node(Vector3Int pos, int g, int h, Node parent)
    {
        this.position = pos;
        this.gCost = g;
        this.hCost = h;
        this.parent = parent;
    }

    public int CompareTo(Node other)
    {
        int compare = fCost.CompareTo(other.fCost);
        if (compare == 0) compare = hCost.CompareTo(other.hCost);
        return compare;
    }
}
