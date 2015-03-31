using UnityEngine;

public class SpaceshipScript : MonoBehaviour {
    public Sprite AsariShipSprite;
    public Sprite CroganShipSprite;
    private bool FoundNeutral;
    public int Fraction;
    public GameObject ParentPlanet;
    public int Population;
    public GameObject TargetPlanet;

    // Use this for initialization
    private void Start() {
        //настройка фракции корабля
        if (Fraction == 1) gameObject.GetComponent<SpriteRenderer>().sprite = CroganShipSprite;
        if (Fraction == 2) gameObject.GetComponent<SpriteRenderer>().sprite = AsariShipSprite;

        //поиск планеты которая может уместить поселенцев
        //если нейтральная - заселять в первую очередь
        //затем если только может вместить в себя корабль

        GameObject[] ListOfPlanets = GameObject.FindGameObjectsWithTag("Planet");
        for (int i = 0; i < ListOfPlanets.Length; i++) {
            if (ListOfPlanets[i] != ParentPlanet) {
                if (ListOfPlanets[i].GetComponent<PlanetScript>().Fraction == 0) {
                    TargetPlanet = ListOfPlanets[i];
                    FoundNeutral = true;
                }
                if (!FoundNeutral)
                    if (ListOfPlanets[i].GetComponent<PlanetScript>().Capacity > Population) {
                        TargetPlanet = ListOfPlanets[i];
                    }
            }
        }
        //направить в сторону нацеленной планеты
        Vector3 targetDir = TargetPlanet.transform.position - transform.position;
        gameObject.transform.rotation = Quaternion.LookRotation(Vector3.forward, targetDir);
        gameObject.rigidbody2D.AddRelativeForce(new Vector2(0f, 50f));
    }

    // Update is called once per frame
    private void Update() {
    }

    private void OnTriggerEnter2D(Collider2D coll) {
        Debug.Log("Collision");
        var clonePlanetScript = coll.gameObject.GetComponent<PlanetScript>();
        //когда корабль сталкивается с планетой
        if (coll.gameObject == TargetPlanet) {
            switch (Fraction) {
                case 1:
                    if (clonePlanetScript.Fraction == 2 || clonePlanetScript.Fraction == 0) {
                        // если на корабле больше людей - планета захвачена
                        if (Population > clonePlanetScript.Population) {
                            clonePlanetScript.Population = Population - clonePlanetScript.Population;
                            coll.gameObject.GetComponent<SpriteRenderer>().sprite = clonePlanetScript.croganPlanetSprite;
                            clonePlanetScript.Fraction = 1;
                        }
                            //иначе на планете умирает столько человек сколько было на корабле
                        if (Population < clonePlanetScript.Population)
                            clonePlanetScript.Population = clonePlanetScript.Population-Population;
                    }
                    if (clonePlanetScript.Fraction == 1) clonePlanetScript.Population += Population;

                    break;
                case 2:
                    if (clonePlanetScript.Fraction == 1 || clonePlanetScript.Fraction == 0)
                    {
                        // если на корабле больше людей - планета захвачена
                        if (Population > clonePlanetScript.Population)
                        {
                            clonePlanetScript.Population = Population - clonePlanetScript.Population;
                            coll.gameObject.GetComponent<SpriteRenderer>().sprite = clonePlanetScript.azariPlanetSprite;
                            clonePlanetScript.Fraction = 2;
                        }
                        //иначе на планете умирает столько человек сколько было на корабле
                        if (Population < clonePlanetScript.Population)
                            clonePlanetScript.Population =clonePlanetScript.Population- Population;
                    }
                    if (clonePlanetScript.Fraction == 2) clonePlanetScript.Population += Population;
                    break;
            }
            coll.gameObject.GetComponent<PlanetScript>().Population += Population;
            Destroy(gameObject);
        }
    }
}