using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node 
{
    public Vector3 worldPosition;

    public Node parent;
    public float totalCost;

    public class Edge
    {
        public Node node;
        public float cost = 1.0f;
    }

    public ArrayList edges = new ArrayList();

    public void AddEdge(Node n2)
    {
        Edge edge = new Edge();
        edge.node = n2;
        edges.Add(edge);
    }

    public Node(Vector3 m_worldPosition)
    {
        worldPosition = m_worldPosition;
    }

}

