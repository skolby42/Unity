using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerFactory : MonoBehaviour
{
    [SerializeField] int towerLimit = 5;
    [SerializeField] Tower towerPrefab = null;
    [SerializeField] Transform towerParentTransform = null;
    
    // State
    private Queue<Tower> towerQueue = new Queue<Tower>();

    public void AddTower(Waypoint waypoint)
    {
        if (towerPrefab == null) return;

        if (towerQueue.Count < towerLimit)
        {
            CreateTower(waypoint);
        }
        else
        {
            MoveTower(waypoint);
        }
    }

    private void CreateTower(Waypoint waypoint)
    {
        Tower tower = Instantiate(towerPrefab, waypoint.transform.position, Quaternion.identity, towerParentTransform);

        waypoint.IsPlaceable = false;
        tower.AttachedWaypoint = waypoint;

        towerQueue.Enqueue(tower);
    }

    private void MoveTower(Waypoint waypoint)
    {
        Tower tower = towerQueue.Dequeue();

        waypoint.IsPlaceable = false;
        tower.transform.position = waypoint.transform.position;
        tower.AttachedWaypoint.IsPlaceable = true;
        tower.AttachedWaypoint = waypoint;

        towerQueue.Enqueue(tower);
    }
}
