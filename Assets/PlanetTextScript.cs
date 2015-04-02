using System.Reflection;
using UnityEngine;
using System.Collections;

public class PlanetTextScript : MonoBehaviour {
    public GameObject Target;
    private PlanetScript PlanetInstance;
    private SpaceshipScript SpaceshipInstance;
    private Vector3 offset = new Vector3(0.0f, -1.5f, 0.0f);
	// Use this for initialization
	void Start () {
        if (Target.tag == "Planet") PlanetInstance = Target.GetComponent<PlanetScript>();
        if (Target.tag == "Spaceship") SpaceshipInstance = Target.GetComponent<SpaceshipScript>();
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 Pos = Target.transform.position + offset;
        transform.position = Camera.main.WorldToViewportPoint(Pos);
	}

    void OnGUI() {
        if (Target.tag == "Planet") guiText.text = "Planet " + PlanetInstance.NameOfPlanet + "\n" + PlanetInstance.Population + "/" + PlanetInstance.Capacity;
        if (Target.tag == "Spaceship") guiText.text = "Spaceship\n" + SpaceshipInstance.Population;
    }
}
