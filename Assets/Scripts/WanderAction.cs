using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WanderAction : Action
{
    public KeyCode key;

    public GameObject ground;

    public Vector3 goal;

    public override float Evaluate(Agent a)
    {
        return 0.5f;
    }
    public override void UpdateAction(Agent agent)
    {
        // if we've hit the final node, call MoveNavAgent again
        if (agent.navAgent.finished)
            MoveNavAgent(agent);
    }

    public override void Enter(Agent agent)
    {
        MoveNavAgent(agent);
    }
    public override void Exit(Agent agent)
    {
    }

    public void MoveNavAgent(Agent agent)
    {
        //goal = new Vector3(Random.Range(1, 30), 0, Random.Range(30, 1));

        //Vector3 mousePosition = Input.mousePosition;
        if (Input.GetMouseButtonDown(0))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {
                // whatever tag you are looking for on your game object
                if (hit.collider.tag == "Ground")
                {
                    goal = new Vector3((int)hit.point.x, 0, (int)hit.point.z);
                }
            }
            
        }
        else if (Input.GetKeyDown(key))
        {
            goal = new Vector3(Random.Range(1, 30), 0, Random.Range(30, 1));
        }


        agent.navAgent.SetDestination(goal);
    }
}

