using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataLogger : MonoBehaviour
{
    [SerializeField] string partipantID;
    [SerializeField] Gate_Loader loadedGates;
    [SerializeField] RobotController robot;
    [SerializeField] ScoreKeeper score;
    [SerializeField] UIController UI;
    [SerializeField] bool isTutorial = false;

    float time = 0;
    int currentGate;
    int nextGate;
    string fileNamePath;
    string fileHeader;

    void Start()
    {
        fileNamePath = @"C:\Users\tj-bridgwater\Documents\Risky Corridor Experiment\DataLogs\Particpant" + partipantID + ".txt";
        fileHeader = "DATA FOR PARTICIPANT: " + partipantID + " - START OF ROUND";

        currentGate = loadedGates.GateOrder[UI.gateCounter];

        using (System.IO.StreamWriter file =
                    new System.IO.StreamWriter(fileNamePath, true))
        {
            file.WriteLine(fileHeader);
            file.WriteLine("PROFILE: " + robot.RobotProfile.ToString());
            file.WriteLine("  Current Gate: " + currentGate.ToString());
        }
    }

    void Update()
    {
        time += Time.deltaTime;

        currentGate = loadedGates.GateOrder[UI.gateCounter];
        nextGate = loadedGates.GateOrder[(UI.gateCounter+1)];

    }

    void WriteGateNumber()
    {
        //TODO:: start timer when UI appears... or start timer here in this script as not needed elsewhere

        using (System.IO.StreamWriter file =
                    new System.IO.StreamWriter(fileNamePath, true))
        {
            file.WriteLine("  Current Gate: " + nextGate.ToString());
        }
    }

    void WriteQuestioned()
    {
        //TODO:: Add time from start of gate to data logger
        using (System.IO.StreamWriter file =
                    new System.IO.StreamWriter(fileNamePath, true))
        {
            file.WriteLine("    QUESTIONED at: " + time + " seconds");
        }
    }

    void WriteQuestionedFurther()
    {
        //TODO:: Add time from start of gate to data logger

        using (System.IO.StreamWriter file =
                    new System.IO.StreamWriter(fileNamePath, true))
        {
            file.WriteLine("    QUESTIONED FURTHER at: " + time + " seconds");
        }
    }

    void WriteDecisionAccepted()
    {
        //TODO:: Add time from start of gate to data logger

        using (System.IO.StreamWriter file =
                    new System.IO.StreamWriter(fileNamePath, true))
        {
            file.WriteLine("    ACCEPTED at: " + time + " seconds");
        }
    }

    void WriteDecisionAltered()
    {
        //TODO:: Add time from start of gate to data logger

        using (System.IO.StreamWriter file =
                    new System.IO.StreamWriter(fileNamePath, true))
        {
            file.WriteLine("    ALTERED at at: " + time + " seconds");
        }
    }

    void WriteScore()
    {
        using (System.IO.StreamWriter file =
                    new System.IO.StreamWriter(fileNamePath, true))
        {
            file.WriteLine("ROUND SCORE: " + score.score);
            file.WriteLine(" ");
            file.WriteLine(" ");
        }
    }

    void ResetTimer()
    {
        time = 0;
    }
}
