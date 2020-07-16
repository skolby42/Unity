using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Waypoint : MonoBehaviour
{
    public bool IsExplored { get; set; }
    public Waypoint ExploredFrom { get; set; }
    public bool IsPlaceable { get; set; } = true;

    [SerializeField] Tower towerPrefab = null;

    const int gridSize = 10;

    // State
    private Tower attachedTower = null;


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
    //        print(gameObject.name);
    //    }
    //}

    private void OnMouseDown()
    {
        SetTowerAttached();
    }

    private void SetTowerAttached()
    {
        if (IsPlaceable)
        {
            AttachTower();
        }
        else
        {
            RemoveTower();
        }
    }

    private void AttachTower()
    {
        if (towerPrefab == null) return;

        attachedTower = Instantiate(towerPrefab, transform.position, Quaternion.identity, transform);
        IsPlaceable = false;
    }

    private void RemoveTower()
    {
        if (attachedTower == null) return;

        Destroy(attachedTower.gameObject);
        IsPlaceable = true;
    }
}
