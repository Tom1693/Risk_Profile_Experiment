﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class BonusRoundRobotController : MonoBehaviour
{
    //defines the robot profile:
    // 0 = risk averse
    // 1 = risk seeking
    // 2 = expected value
    // 3 = human approach
    public int RobotProfile = 0;

    [SerializeField] Transform goal;
    [SerializeField] AudioSource engineSound;
    [SerializeField] BonusRoundScoreKeeper bonusScoreKeeper;
    [SerializeField] GameObject endScreen;

    Vector3[,] fullWaypointsList = new Vector3[10, 3];
    public Vector3[] waypointsToTravel = new Vector3[20];
    public Vector3 currentWaypoint = new Vector3();

    public Camera cam;
    public NavMeshAgent agent;
    public Path_Loader waypointList;

    public Renderer trackLeft;
    public Renderer trackRight;

    private Vector3 previousPosition;
    public float curSpeed;
    public float prevSpeed;

    public int waypointCounter = 0;
    public int gateCounter = 0;

    //1 = certain route
    //0 = uncertain route

    public int[,] robotGateDecisions = new int[10, 4] {
       { 1, 0, 0, 1 },
       { 1, 0, 1, 0 },
       { 1, 0, 0, 1 },
       { 1, 0, 1, 0 },
       { 1, 0, 1, 0 },
       { 1, 0, 0, 1 },
       { 1, 0, 0, 1 },
       { 1, 0, 1, 0 },
       { 1, 0, 0, 1 },
       { 1, 0, 1, 0 },
       };


    // Start is called before the first frame update
    void Start()
    {
        fullWaypointsList = waypointList.fullWaypointsList;
        GetRoute();
    }

    // Update is called once per frame
    void Update()
    {
        curSpeed = GetRobotSpeed();

        if (curSpeed == 0)
        {
            RotateForwards();
        }

        DisplayEndScreen();
        PlayEngineNoise(curSpeed);
        AnimateTracks();
        GetCurrentWaypoint();
        MoveToWaypoint();
        
    }


    void MoveToWaypoint()
    {
        agent.SetDestination(currentWaypoint);
    }

    void GetCurrentWaypoint()
    {
        currentWaypoint = waypointsToTravel[waypointCounter];

        float dist = Vector3.Distance(currentWaypoint, transform.position);

        if (dist < 1)
        {
            waypointCounter++;
        }

        if(waypointCounter%2 == 0)
        {
            gateCounter = waypointCounter / 2;
        }
        else
        {
            gateCounter = (waypointCounter - 1)/2;
        }
    }

    void GetRoute()
    {
        for(int i = 0; i < 20; i=i+2)
        {
            waypointsToTravel[i] = fullWaypointsList[(i / 2), robotGateDecisions[i/2,RobotProfile]];
            waypointsToTravel[i+1] = fullWaypointsList[(i / 2), 2];
        }
    }


    private void AnimateTracks()
    {
        float offset = Time.time * curSpeed;

        trackLeft.material.SetTextureOffset("_MainTex", new Vector2(offset, 0));
        trackRight.material.SetTextureOffset("_MainTex", new Vector2(offset, 0));
    }

    private float GetRobotSpeed()
    {
        Vector3 curMove = transform.position - previousPosition;
        curSpeed = curMove.magnitude / Time.deltaTime;
        previousPosition = transform.position;

        return curSpeed;
    }

    void PlayEngineNoise(float curSpeed)
    {
        engineSound.volume = 0.1f;
        engineSound.pitch = 1f * curSpeed / 3;

        if (curSpeed == 0)
        {
            engineSound.volume = 0.05f;
            engineSound.pitch = 1f;
        }
    }


    void DisplayEndScreen()
    {
        if (curSpeed == 0 && waypointCounter > 7)
        {
            endScreen.SetActive(true);
        }
        else
        {
            endScreen.SetActive(false);
        }
    }

    void RotateForwards()
    {
        float rotSpeed = 75f;

        // The step size is equal to speed times frame time.
        var step = rotSpeed * Time.deltaTime;

        // Rotate our transform a step closer to the target's.
        transform.rotation = Quaternion.RotateTowards(transform.rotation, goal.rotation, step);

        float angleDiff = transform.rotation.y - goal.rotation.y;
        float angleDiffDeg = angleDiff * 180 / Mathf.PI;

        if (angleDiffDeg < 90 && angleDiffDeg != 0)
        {
            float offset = Time.time * rotSpeed / 25;

            trackLeft.material.SetTextureOffset("_MainTex", new Vector2(offset, 0));
            trackRight.material.SetTextureOffset("_MainTex", new Vector2(-offset, 0));

            engineSound.volume = 0.1f;
            engineSound.pitch = 1.5f;
        }
        else if (angleDiffDeg > 90 && angleDiffDeg != 0)
        {
            float offset = Time.time * rotSpeed / 25;

            trackLeft.material.SetTextureOffset("_MainTex", new Vector2(-offset, 0));
            trackRight.material.SetTextureOffset("_MainTex", new Vector2(offset, 0));

            engineSound.volume = 0.1f;
            engineSound.pitch = 1.5f;
        }

    }
}
