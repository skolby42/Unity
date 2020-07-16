using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Waypoint : MonoBehaviour
{
    public bool IsExplored { get; set; }
    public Waypoint ExploredFrom { get; set; }
    public bool IsPlaceable { get; set; } = true;

    const int gridSize = 10;


    public int GetGridSize()
    {
        return gridSize;
    }

    public Vector2Int GetGridPos()
    {
        return new Vector2Int(
            Mathf.RoundToInt(transform.position.x / gridSize),
            Mathf.RoundToInt(transform.position.z / gridSize)
        );
    }

    //private void OnMouseOver()
    //{
    //    if (Input.GetMouseButtonDown((int)MouseButton.LeftMouse))
    //    {
    //        PlaceTower();
    //    }
    //}

    private void OnMouseDown()
    {
        PlaceTower();
    }

    private void PlaceTower()
    {
        if (IsPlaceable)
        {
            FindObjectOfType<TowerFactory>().AddTower(this);
        }
    }
}
