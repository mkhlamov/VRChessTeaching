using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveTile : MonoBehaviour {

    private InteractiveTest intTest;

	// Use this for initialization
	void Start () {
        intTest = FindObjectOfType<InteractiveTest>();
	}
	
	public void NextState()
    {
        intTest.NextState(gameObject);
    }
}
