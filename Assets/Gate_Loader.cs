using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Gate_Loader : MonoBehaviour
{
    [SerializeField] GameObject[] Gates;
    [SerializeField] GameObject[] PositiveGatePrefabs;
    [SerializeField] GameObject[] NegativeGatePrefabs;

    public NavMeshSurface surface;

    bool isGatesInstantiated = false;

    int[] Rotations = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    int[] GateOrder = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
    //add UI for questions

    void Start()
    {
        ShuffleGates(GateOrder);

        GenerateRandomRotations();

        InstantiateGatePrefabs(isGatesInstantiated);

        //updates navmesh
        surface.BuildNavMesh();

    }

    private void GenerateRandomRotations()
    {
        for (int i = 0; i < GateOrder.Length; i++)
        {
            Rotations[i] = UnityEngine.Random.Range(0, 2);
        }
    }

    private void InstantiateGatePrefabs(bool isGatesInstantiated)
    {
        if (!isGatesInstantiated)
        {
            int posGates = 0;
            int negGates = 0;

            for (int i = 0; i < GateOrder.Length; i++)
            {
                if (GateOrder[i] % 2 == 0) //if even
                {
                    if (Rotations[i] == 0)
                    {
                        Instantiate(PositiveGatePrefabs[posGates], Gates[i].transform.position, Quaternion.identity);
                    }
                    else
                    {
                        Instantiate(PositiveGatePrefabs[posGates], Gates[i].transform.position, transform.rotation * Quaternion.Euler(0f, 180f, 0f));
                    }

                    posGates++;
                }
                else //if odd
                {
                    if (Rotations[i] == 0)
                    {
                        Instantiate(NegativeGatePrefabs[negGates], Gates[i].transform.position, Quaternion.identity);
                    }
                    else
                    {
                        Instantiate(NegativeGatePrefabs[negGates], Gates[i].transform.position, transform.rotation * Quaternion.Euler(0f, 180f, 0f));
                    }

                    negGates++;
                }
            }
        }
        isGatesInstantiated = true;
    }


    private void ShuffleGates(int[] GateOrder)
    {
        for (int i = GateOrder.Length - 1; i > 0; i--)
        {
            
            // Randomize a number between 0 and i (so that the range decreases each time)
            int rnd = UnityEngine.Random.Range(0, i);

            // Save the value of the current i, otherwise it'll overright when we swap the values
            int temp = GateOrder[i];

            // Swap the new and old values
            GateOrder[i] = GateOrder[rnd];
            GateOrder[rnd] = temp;
        }
    }
}
