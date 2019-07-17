using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RobotController : MonoBehaviour
{
    [SerializeField] Transform goal;

    public Camera cam;
    public NavMeshAgent agent;
    public Path_Loader waypointList;

    private Vector3 previousPosition;
    public float curSpeed;

    int gateCounter = 0;

    //Queue<Vector3> waypointQueue = new Queue<Vector3>();

    Vector3[,] fullWaypointsList = new Vector3[10,3];
    Vector3 currentWaypoint = new Vector3();

    void Start()
    {
        fullWaypointsList = waypointList.fullWaypointsList;
    }


    void Update()
    {
        print(gateCounter);
        print(currentWaypoint);
        curSpeed = GetRobotSpeed();

        //print(curSpeed);

        if (curSpeed == 0)
        {
            GetDecision();
            transform.LookAt(goal);
        }

        if(gateCounter >= 1)
        {
            MoveToWaypoint();
        }
    }

    private float GetRobotSpeed()
    {
        Vector3 curMove = transform.position - previousPosition;
        curSpeed = curMove.magnitude / Time.deltaTime;
        previousPosition = transform.position;

        return curSpeed;
    }

    private void MoveToWaypoint()
    {
        agent.SetDestination(currentWaypoint);


        float dist = Vector3.Distance(currentWaypoint, transform.position);

        if (dist < 1)
        {
            currentWaypoint = fullWaypointsList[gateCounter-1, 2];
        }
    }

    void GetDecision()
    {
        if (Input.GetKeyDown("u"))
        {
            currentWaypoint = fullWaypointsList[gateCounter, 0];

            gateCounter++;
        }
        else if (Input.GetKeyDown("c"))
        {
            currentWaypoint = fullWaypointsList[gateCounter, 1];

            gateCounter++;
        }
    }

    


    /*void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit))
            {
                agent.SetDestination(hit.point);
            }
        }
    }*/
}
