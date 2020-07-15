using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    [SerializeField] Waypoint startWaypoint = null, endWaypoint = null;

    private Dictionary<Vector2Int, Waypoint> grid = new Dictionary<Vector2Int, Waypoint>();
    private Queue<Waypoint> queue = new Queue<Waypoint>();
    private Vector2Int[] directions = { Vector2Int.up, Vector2Int.right, Vector2Int.down, Vector2Int.left };

    // State
    private Waypoint searchCenter = null;
    private bool isRunning = true;
    private List<Waypoint> path = new List<Waypoint>();

    public List<Waypoint> GetPath()
    {
        LoadBlocks();
        ColorStartAndEnd();
        BreadthFirstSearch();
        GeneratePath();

        return path;
    }

    private void LoadBlocks()
    {
        var waypoints = FindObjectsOfType<Waypoint>();

        foreach (Waypoint waypoint in waypoints)
        {
            Vector2Int gridPos = waypoint.GetGridPos();
            if (!grid.ContainsKey(gridPos))
            {
                grid.Add(gridPos, waypoint);
            }
            else
            {
                Debug.LogWarning($"Skipping overlapping waypoint: {waypoint}");
                waypoint.SetTopColor(Color.red);
            }
        }
    }

    private void ColorStartAndEnd()
    {
        startWaypoint.SetTopColor(Color.green);
        endWaypoint.SetTopColor(Color.red);
    }

    private void BreadthFirstSearch()
    {
        queue.Enqueue(startWaypoint);

        while (queue.Count > 0 && isRunning)
        {
            searchCenter = queue.Dequeue();
            searchCenter.IsExplored = true;

            StopIfEndFound();
            ExploreNeighbors();
        }
    }

    private void StopIfEndFound()
    {
        if (searchCenter == endWaypoint)
        {
            isRunning = false;
        }
    }

    private void ExploreNeighbors()
    {
        if (!isRunning) return;

        foreach (Vector2Int direction in directions)
        {
            Vector2Int neighborCoords = searchCenter.GetGridPos() + direction;

            if (grid.ContainsKey(neighborCoords))
            {
                QueueNewNeighbor(neighborCoords);
            }
        }
    }

    private void QueueNewNeighbor(Vector2Int neighborCoords)
    {
        Waypoint neighbor = grid[neighborCoords];
        if (neighbor.IsExplored || queue.Contains(neighbor)) return;

        queue.Enqueue(neighbor);
        neighbor.ExploredFrom = searchCenter;
    }

    private void GeneratePath()
    {
        path.Add(endWaypoint);
        Waypoint previous = endWaypoint.ExploredFrom;
        while (previous != startWaypoint)
        {
            path.Add(previous);
            previous = previous.ExploredFrom;
        }
        path.Add(startWaypoint);
        path.Reverse();
    }
}
