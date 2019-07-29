﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    [SerializeField] RobotController robot;
    [SerializeField] Gate_Loader loadedGates;
    [SerializeField] UIController ui;

    public float score;
    int currentGate;
    int random;
    int robotDecision;
    int gateCount;
    int robotProfile;
    float gateScore;
    float[] valuePicker = new float[1000];

    //666 = null value
    float[,] scoresA =
    {
        {2000, 1920, 0},
        {-2400, -2304, 0},
        {3200, 0, 666},
        {-2560, 0, 666},
        {5000, 0, 666},
        {-5500, 0, 666},
        {1200, 0, 666},
        {-1100, 0, 666},
        {2000, 0, 666},
        {-2200, 0, 666},

    };

    float[,] probA =
    {
        {0.66f, 0.33f, 0.01f},
        {0.66f, 0.33f, 0.01f},
        {0.8f, 0.2f, 666},
        {0.8f, 0.2f, 666},
        {0.001f, 0.999f, 666},
        {0.001f, 0.999f, 666},
        {0.5f, 0.5f, 666},
        {0.5f, 0.5f, 666},
        {0.8f, 0.2f, 666},
        {0.8f, 0.2f, 666},
    };


    float[,] scoresB =
    {
        {1920, 666},
        {-2304, 666},
        {2400, 666},
        {-1920, 666},
        {2500, 0},
        {-2750, 0},
        {540, 666},
        {-495, 666},
        {1500, 666},
        {-1650, 666},
    };

    float[,] probB =
    {
        {1, 666},
        {1, 666},
        {1, 666},
        {1, 666},
        {0.002f, 0.998f},
        {0.002f, 0.998f},
        {1, 666},
        {1, 666},
        {1, 666},
        {1, 666},
    };


    private void Update()
    {
        gateCount = ui.gateCounter;
        currentGate = loadedGates.GateOrder[gateCount];

        robotProfile = robot.RobotProfile;
        robotDecision = robot.robotGateDecisions[currentGate, robotProfile];
    }

    void ReceivedMessageAccept()
    {
        AddToScore();
    }

    void ReceivedMessageAlter()
    {
        robotDecision = AlterDecision(robotDecision);
        AddToScore();
    }

    void AddToScore()
    {
        gateScore = CalculateGateScore();
        score = score + gateScore;
    }

    private int AlterDecision(int decision)
    {
        if (decision == 1)
        {
            decision = 0;
        }
        else if (decision == 0)
        {
            decision = 1;
        }

        return decision;
    }

    private float CalculateGateScore()
    {
        float firstOutcomeA = scoresA[currentGate,0];
        float firstProbA = 1000 * probA[currentGate, 0];

        float secondOutcomeA = scoresA[currentGate, 1];
        float secondProbA = 1000 * probA[currentGate, 1];

        float thirdOutcomeA = scoresA[currentGate, 2];
        float thirdProbA = 1000 * probA[currentGate, 2];

        float firstOutcomeB = scoresB[currentGate, 0];
        float firstProbB = 1000 * probB[currentGate, 0];

        float secondOutcomeB = scoresB[currentGate, 1];
        float secondProbB = 1000 * probB[currentGate, 1];

        int i = 0;
        int p = 0;
        int j = 0;

        if (robotDecision == 0)
        {
            random = Random.Range(0, 1000);
            if(thirdOutcomeA != 666)
            {
                for (i = 0; i < firstProbA; i++)
                {
                    valuePicker[i] = firstOutcomeA;
                    p++;
                    j++;
                }
                for (i = p; i < secondProbA + firstProbA; i++)
                {
                    valuePicker[i] = secondOutcomeA;
                    j++;
                }
                for (i = j; i < thirdProbA + secondProbA + firstProbA; i++)
                {
                    valuePicker[i] = thirdOutcomeA;
                }
            }
            else
            {
                for (i = 0; i < firstProbA; i++)
                {
                    valuePicker[i] = firstOutcomeA;
                    p++;
                }
                for (i = p; i < secondProbA + firstProbA; i++)
                {
                    valuePicker[i] = secondOutcomeA;
                }
            }
        }
        else if (robotDecision == 1)
        {
            random = Random.Range(0, 1000);

            if (secondOutcomeB != 666)
            {
                for (i = 0; i < firstProbB; i++)
                {
                    valuePicker[i] = firstOutcomeB;
                    p++;
                }
                for (i = p; i < secondProbB + firstProbA; i++)
                {
                    valuePicker[i] = secondOutcomeB;
                }
            }
            else
            {
                for (i = 0; i < firstProbB; i++)
                {
                    valuePicker[i] = firstOutcomeB;
                }
            }
        }
        else
        {
            print("error in scorekeeper end");
            return (88888888);
        }

       /* print("Random number = " + random);
        print("Value chosen = " + valuePicker[random]);
        print("Value at 0 = " + valuePicker[0] + " Value at 500 = " + valuePicker[499] + " Value at end = " + valuePicker[999]);*/

        return valuePicker[random];
    }






    /*if (robotDecision == 0)
    {
        random = Random.Range(0, 1000);
        print("0 - " + random);
        if (thirdProbA == 666000)
        {
            if (random >= 0 && random < thirdProbA)
            {
                return (thirdOutcomeA);
            }
            else if (random >= thirdProbA && random < secondProbA + thirdProbA)
            {
                return (secondOutcomeA);
            }
            else if (random > secondProbA + thirdProbA && random <= firstProbA + secondProbA + thirdProbA)
            {
                return (firstOutcomeA);
            }
            else
            {
                print("error in scorekeeper decision 0.0");
                return (88888888);
            }
        }
        else
        {
            if (random >= 0 && random < secondProbA)
            {
                return (firstOutcomeA);
            }
            else if (random >= secondProbA && random < firstProbA + secondProbA)
            {
                return (secondOutcomeA);
            }
            else
            {
                print("error in scorekeeper decision 0.1");
                return (88888888);
            }
        }

    }
    else if (robotDecision == 1)
    {
        random = Random.Range(0, 1000);
        print("1 - " + random);
        if (secondProbB == 666000)
        {
            if (random >= 0 && random < firstProbB)
            {
                return (firstOutcomeB);  
            }
            else if (random >= secondProbA && random < firstProbB + secondProbB)
            {
                return (secondOutcomeB);
            }
            else
            {
                print("error in scorekeeper decision 1");
                return (88888888);
            }
        }
        else
        {
            return (firstOutcomeB);
        }
    }
    else
    {
        print("error in scorekeeper end");
        return (88888888);
    }
}*/
}
