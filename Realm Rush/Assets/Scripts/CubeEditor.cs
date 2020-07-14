using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[SelectionBase]
[RequireComponent(typeof(Waypoint))]
public class CubeEditor : MonoBehaviour
{
    Waypoint waypoint;

    private void Awake()
    {
        waypoint = GetComponent<Waypoint>();
    }

    void Update()
    {
        SnapToGrid();
        UpdateLabel();
    }

    private void SnapToGrid()
    {
        int gridSize = waypoint.GetGridSize();
        Vector2Int gridPos = waypoint.GetGridPos();
        transform.position = new Vector3(
            gridPos.x * gridSize, 
            0f, 
            gridPos.y * gridSize
        );
    }

    private void UpdateLabel()
    {
        Vector2Int gridPos = waypoint.GetGridPos();
        TextMesh textMesh = GetComponentInChildren<TextMesh>();
        textMesh.text = $"{gridPos.x},{gridPos.y}";
        gameObject.name = $"Cube ({gridPos.x},{gridPos.y})";
    }
}
