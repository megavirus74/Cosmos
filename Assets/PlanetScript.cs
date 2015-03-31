using System;
using System.Threading;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlanetScript : MonoBehaviour {
    public int Capacity; //вместимость планеты
    public string NameOfPlanet; //имя планеты
    public int Fraction; //под чьим контролем 0 - нейтралитет
    public bool isEmpty = true;
    public int Population; //текущее население
    public GameObject Text;
    public GameObject SpaceShip;
    private float timerPopulation;
    private float timerColonization;


    // Use this for initialization
    private void Start() {
        //началные параметры при инициализации
        Fraction = 0;
        Capacity = Random.Range(200000, 400000);
        Population = Random.Range(100000,300000);
        NameOfPlanet = "Test";
        timerPopulation = 0.1f;
        timerColonization = Random.Range(5f, 10f);
        transform.position =
            Camera.main.ScreenToWorldPoint(new Vector3(Random.Range(Screen.width/10f, Screen.width/10f*9f),
                Random.Range(Screen.height/5f, Screen.height/5f*4.5f),transform.position.z));
        GameObject textClone = (GameObject) Instantiate(Text);
        textClone.GetComponent<PlanetTextScript>().Target = gameObject;
        if (Population > 0)
            isEmpty = false;
    }

    // Update is called once per frame
    private void Update() {
        timerPopulation -= Time.deltaTime;
        timerColonization -= Time.deltaTime;

        //Увеличение популяции
        if (Population > 0 && Population < Capacity)
        {
            if (timerPopulation < 0f)
            {
                timerPopulation = 0.1f;
                Population += Random.Range(1,100)*Convert.ToInt32(Math.Log(Population));
            }
        }

        if (Population > Capacity) Population--;

        //Колонизация
        if (Capacity/Population < 4)
            if (timerColonization < 0f) {
                Debug.Log("Trying to launch a spaceship");
                if (Random.Range(1, 3) == 2)
                {
                    Debug.Log("Succesfull");
                    GameObject SpaceShipClone = (GameObject) Instantiate(SpaceShip,transform.position,transform.rotation);
                    SpaceShipClone.GetComponent<SpaceshipScript>().Population = Population/10;
                    SpaceShipClone.GetComponent<SpaceshipScript>().ParentPlanet = gameObject;
                    Population -= Population/10;
                }
                timerColonization = Random.Range(5f,10f);
            }
    }
}