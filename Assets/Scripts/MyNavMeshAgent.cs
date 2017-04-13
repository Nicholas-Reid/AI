using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyNavMeshAgent : MonoBehaviour
{
    public Grid grid;
    public bool finished;

    ArrayList currentPath = new ArrayList();
    int currentNode;

    public void Update()
    {
        finished = (currentNode >= currentPath.Count);
        if (!finished)
        {
            Vector3 targetPos = (currentPath[currentNode] as Node).worldPosition;

            targetPos.y = transform.position.y;
            Vector3 toTarget = targetPos - transform.position;
            transform.forward = toTarget;
            transform.position = Vector3.MoveTowards(transform.position, targetPos, 0.1f);
            if (toTarget.magnitude < 0.8f)
            {
                currentNode++;
            }

        }
    }

    public bool SetDestination(Vector3 dest)
    {
        Node start = grid.GetNodeAt(transform.position);
        Node end = grid.GetNodeAt(dest);

        currentNode = 0;

        if (start == null || end == null)
        {
            currentPath.Clear();
            return false;
        }

        currentPath = grid.FindPath(start, end);

        return true;
    }
}