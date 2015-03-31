using UnityEngine;
using System.Collections;

public class SpaceshipScript : MonoBehaviour {
    public GameObject TargetPlanet;
    public GameObject ParentPlanet;
    public int Population;

	// Use this for initialization
    void Start() {

        //поиск планеты которая может уместить поселенцев
        GameObject[] ListOfPlanets = GameObject.FindGameObjectsWithTag("Planet");
        foreach (var planet in ListOfPlanets)
            if (planet!=ParentPlanet)
                if (planet.GetComponent<PlanetScript>().Capacity > Population)
                    TargetPlanet = planet;
        //направить в сторону нацеленной планеты
        Vector3 targetDir = TargetPlanet.transform.position - transform.position;
        gameObject.transform.rotation = Quaternion.LookRotation(Vector3.forward, targetDir);
        gameObject.rigidbody2D.AddRelativeForce(new Vector2(0f,50f));
	}
	
	// Update is called once per frame
	void Update () {
	}
    void OnTriggerEnter2D(Collider2D coll)
    {
        Debug.Log("Collision");
        if (coll.gameObject == TargetPlanet) {
            Debug.Log("Target");
            coll.gameObject.GetComponent<PlanetScript>().Population += Population;
            Destroy(gameObject);
        }
    }
}
