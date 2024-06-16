using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointManager : MonoBehaviour
{
    private MoveCheck moveCheckScript;
    private SelectWaypoint selectWaypoint;
    private Dictionary<string, GameObject> waypointDict = new Dictionary<string, GameObject>();
    private Transform selectedObject;
    private bool isMoving;


    private void Start()
    {
        
        moveCheckScript = GetComponent<MoveCheck>();
        selectWaypoint = GetComponent<SelectWaypoint>();
        GameObject[] waypoints = GameObject.FindGameObjectsWithTag("Waypoint");

        foreach (var waypoint in waypoints)
        {
            waypointDict[waypoint.name] = waypoint;
        }
    }


   
    private void LateUpdate()
    {
        WaypointsController();
    }

    private void WaypointsController()
    {
        selectedObject = SelectChess.GetSelection();
       
        if (selectedObject != null)
        {
            List<Vector2Int> moveablePoints = moveCheckScript.GetMoveablePoints();
            ActivateMoveablePoints(moveablePoints);
        }
        if(selectedObject == null) 
        {
             DeactivateAllWaypoints();
        }
    }

    private void ActivateMoveablePoints(List<Vector2Int> moveablePoints)
    {
        DeactivateAllWaypoints();

        if (moveablePoints != null)
        {
            foreach (Vector2Int point in moveablePoints)
            {
                string waypointName = $"Point_{point.x}_{point.y}";
                if (waypointDict.TryGetValue(waypointName, out GameObject waypoint))
                {
                    waypoint.SetActive(true);
                }
            }
        }
    }

    private void DeactivateAllWaypoints()
    {
        foreach (var waypoint in waypointDict.Values)
        {
            waypoint.SetActive(false);
        }
    }
}
