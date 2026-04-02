using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public Transform target;

    public Vector3Int gridMin = new Vector3Int(0, 0, 0);
    public Vector3Int gridMax = new Vector3Int(10, 10, 0);

    public float moveSpeed = 1f;
    public float reachDistance = 0.05f;
    
    public bool recalculateWhenTargetMoves = true;

    private int awarenessRange = 10;
    private int roamRadius = 20;

    public List<Vector3Int> currentPath = new List<Vector3Int>();
    private int pathIndex = 0;
    private Vector3Int lastTargetGridPos;

    private enum NpcState
    {
        Roaming,
        Chasing
    }

    private NpcState currentState = NpcState.Roaming;

    private Vector3Int roamGoal;
    private bool hasRoamGoal = false;

    void Start()
    {
        if (target == null)
        {
            Debug.LogWarning("NPC target is not assigned.");
            return;
        }

        lastTargetGridPos = Vector3Int.RoundToInt(target.position);
        
        UpdateState();
        if (currentState == NpcState.Chasing)
            BuildPath();
        else
            PickNewRoamGoal();
    }

    void Update()
    {
        if (!target)
            return;

        UpdateState();

        switch (currentState)
        {
            case NpcState.Chasing:
                Chase();
                break;

            case NpcState.Roaming:
                Roam();
                break;
        }

        FollowPath();
    }

    // Specifically when player is target
    public void BuildPath()
    {
        if (target == null)
            return;

        Vector3Int goalInt = Vector3Int.RoundToInt(target.position);
        BuildPathTo(goalInt);
    }
    
    private void BuildPathTo(Vector3Int goalInt)
    {
        Vector3Int startInt = Vector3Int.RoundToInt(transform.position);

        HashSet<Vector3Int> blocked = GetBlockedTiles();
        blocked.Remove(startInt);
        blocked.Remove(goalInt);

        List<Vector3Int> newPath = AStar.GetPath(startInt, goalInt, blocked, gridMin, gridMax);

        if (newPath == null || newPath.Count == 0)
        {
            currentPath = new List<Vector3Int>();
            pathIndex = 0;
            Debug.Log($"No path found from {startInt} to {goalInt}.");
            return;
        }

        currentPath = newPath;
        pathIndex = 0;

        if (currentPath.Count > 0 && currentPath[0] == startInt)
            pathIndex = 1;
    }

    private void FollowPath()
    {
        if (currentPath == null || currentPath.Count == 0)
            return;

        if (pathIndex >= currentPath.Count)
            return;

        Vector3 nextPos = currentPath[pathIndex];

        transform.position = Vector3.MoveTowards(
            transform.position,
            nextPos,
            moveSpeed * Time.deltaTime
        );

        if (Vector3.Distance(transform.position, nextPos) <= reachDistance)
        {
            transform.position = nextPos;
            pathIndex++;
        }
    }

    private HashSet<Vector3Int> GetBlockedTiles()
    {
        HashSet<Vector3Int> blocked = new HashSet<Vector3Int>();
        HashSet<Vector3Int> allTiles = new HashSet<Vector3Int>();

        GameObject[] tiles = GameObject.FindGameObjectsWithTag("Cube");

        foreach (GameObject tile in tiles)
        {
            allTiles.Add(Vector3Int.RoundToInt(tile.transform.position));
        }

        foreach (Vector3Int tilePos in allTiles)
        {
            if (allTiles.Contains(tilePos + Vector3Int.up * 2))
            {
                blocked.Add(tilePos);
            }
        }

        return blocked;
    }

    private bool HasActivePath()
    {
        return currentPath != null && currentPath.Count > 0 && pathIndex < currentPath.Count;
    }

    private bool ReachedCurrentPathGoal()
    {
        if (currentPath == null || currentPath.Count == 0)
            return true;

        Vector3Int myGridPos = Vector3Int.RoundToInt(transform.position);
        return myGridPos == currentPath[currentPath.Count - 1];
    }

    private void Roam()
    {
        if (!HasActivePath() || ReachedCurrentPathGoal())
        {
            PickNewRoamGoal();
        }
    }

    private void Chase()
    {
        Vector3Int currTargetGridPos = Vector3Int.RoundToInt(target.position);

        if (!HasActivePath())
        {
            lastTargetGridPos = currTargetGridPos;
            BuildPath();
            return;
        }

        if (recalculateWhenTargetMoves && currTargetGridPos != lastTargetGridPos)
        {
            lastTargetGridPos = currTargetGridPos;
            BuildPath();
        }
    }

    private void UpdateState()
    {
        Vector3Int myGridPos = Vector3Int.RoundToInt(transform.position);
        Vector3Int targetGridPos = Vector3Int.RoundToInt(target.position);

        int tileDistance = Mathf.Abs(myGridPos.x - targetGridPos.x) +
                           Mathf.Abs(myGridPos.z - targetGridPos.z);

        NpcState newState = tileDistance <= awarenessRange
            ? NpcState.Chasing
            : NpcState.Roaming;

        if (newState != currentState)
        {
            currentState = newState;

            currentPath.Clear();
            pathIndex = 0;

            if (currentState == NpcState.Chasing)
            {
                hasRoamGoal = false;
                lastTargetGridPos = targetGridPos;
                BuildPath();
            }
            else
            {
                hasRoamGoal = false;
                PickNewRoamGoal();
            }
        }
    }

    private void PickNewRoamGoal()
    {
        Vector3Int myGridPos = Vector3Int.RoundToInt(transform.position);
        HashSet<Vector3Int> blocked = GetBlockedTiles();

        for (int attempt = 0; attempt < 20; attempt++)
        {
            int offsetX = Random.Range(-roamRadius, roamRadius + 1);
            int offsetZ = Random.Range(-roamRadius, roamRadius + 1);

            Vector3Int candidate = new Vector3Int(
                myGridPos.x + offsetX,
                myGridPos.y,
                myGridPos.z + offsetZ
            );

            Vector3Int normalizedCandidate = new Vector3Int(candidate.x, 0, candidate.z);

            if (candidate.x < gridMin.x || candidate.x > gridMax.x ||
                candidate.z < gridMin.z || candidate.z > gridMax.z)
            {
                continue;
            }

            if (candidate == myGridPos)
                continue;

            if (blocked.Contains(normalizedCandidate))
                continue;

            List<Vector3Int> testPath = AStar.GetPath(myGridPos, candidate, blocked, gridMin, gridMax);

            if (testPath != null && testPath.Count > 1)
            {
                roamGoal = candidate;
                hasRoamGoal = true;
                BuildPathTo(candidate);
                return;
            }
        }

        hasRoamGoal = false;
        currentPath.Clear();
        pathIndex = 0;
    }

    void OnDrawGizmos()
    {
        if (currentPath == null || currentPath.Count == 0)
            return;

        for (int i = 0; i < currentPath.Count; i++)
        {
            Vector3 pos = currentPath[i];
            Gizmos.color = (i == pathIndex) ? Color.blue : Color.green;
            Gizmos.DrawCube(pos, new Vector3(0.9f, 0.2f, 0.9f));

            if (i > 0)
            {
                Vector3 prevPos = currentPath[i - 1];
                Gizmos.DrawLine(prevPos, pos);
            }
        }

        if (hasRoamGoal)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(roamGoal, 0.25f);
        }
    }
}