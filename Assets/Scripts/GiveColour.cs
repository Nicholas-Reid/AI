using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveColour : MonoBehaviour
{
    public Material[] materialSelection = new Material[5];
    
    int index;
    int index2;

     // Use this for initialization
    void Start()
    {
        index = Random.Range(0, 5);

        gameObject.GetComponent<MeshRenderer>().material = materialSelection[index];

	}
	
	// Update is called once per frame
	void Update ()
    {
        index2 = Random.Range(0, 5);

        if (Input.GetKeyDown(KeyCode.C))
        {
            gameObject.GetComponent<MeshRenderer>().material = materialSelection[index2];

        }
    }
}
