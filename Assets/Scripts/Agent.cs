using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Agent : MonoBehaviour
{

    // an array of all available actions
    Action[] actions;
    // the actionn we're currently carrying out
    public Action currentAction;

    public MyNavMeshAgent navAgent;

    // Use this for initialization
    void Start()
    {
        navAgent = GetComponent<MyNavMeshAgent>();
       
        // get all the action-derived classes that are siblings of us
        actions = GetComponents<Action>();
    }
    // Update is called once per frame
    void Update()
    {
        // find the best action each frame (TODO - not every frame?)
        Action best = GetBestAction();
        // if it’s different from what we were doing, switch the FSM
        if (best != currentAction)
        {
            if (currentAction)
                currentAction.Exit(this);
            currentAction = best;
            if (currentAction)
                currentAction.Enter(this);
        }
        // update the current action
        if (currentAction)
            currentAction.UpdateAction(this);
    }
    // checks all our available actions and evaluates each one, getting the best
    Action GetBestAction()
    {
        Action action = null;
        float bestValue = 0;
        foreach (Action a in actions)
        {
            float value = a.Evaluate(this);
            if (action == null || value > bestValue)
            {
                action = a;
                bestValue = value;
            }
        }
        return action;
    }
}
