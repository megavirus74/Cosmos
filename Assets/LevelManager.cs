using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {
    public GameObject planetGameObject;
	// Use this for initialization
	void Start () {
	    for (int i = 0; i < 5; i++) Instantiate(planetGameObject);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
