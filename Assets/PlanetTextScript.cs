using System.Reflection;
using UnityEngine;
using System.Collections;

public class PlanetTextScript : MonoBehaviour {
    public GameObject Target;
    private PlanetScript TargetInstance;
    private Vector3 offset = new Vector3(0.0f, -1.5f, 0.0f);
	// Use this for initialization
	void Start () {
        TargetInstance = Target.GetComponent<PlanetScript>();
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 Pos = Target.transform.position + offset;
        transform.position = Camera.main.WorldToViewportPoint(Pos);
	}

    void OnGUI() {
        guiText.text = "Planet " + TargetInstance.NameOfPlanet + "\n" + TargetInstance.Population + "/" + TargetInstance.Capacity;
    }
}
