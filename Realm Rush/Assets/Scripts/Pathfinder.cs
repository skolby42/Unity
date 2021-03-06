﻿using System;
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
        if (path.Count == 0)
        {
            CalculatePath();
        }

        return path;
    }

    private void CalculatePath()
    {
        LoadBlocks();
        BreadthFirstSearch();
        CreatePath();
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
            }
        }
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

    private void CreatePath()
    {
        AddPathWaypoint(endWaypoint);

        Waypoint previous = endWaypoint.ExploredFrom;
        while (previous != startWaypoint)
        {
            AddPathWaypoint(previous);
            previous = previous.ExploredFrom;
        }

        AddPathWaypoint(startWaypoint);
        path.Reverse();
    }

    private void AddPathWaypoint(Waypoint waypoint)
    {
        path.Add(waypoint);
        waypoint.IsPlaceable = false;
    }
}
