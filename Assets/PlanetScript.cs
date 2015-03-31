using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlanetScript : MonoBehaviour {
    private readonly String[] NamesOfPlanets = {
        "Izida", "Maverick", "Mars", "Saturn", "Andromeda", "Kodeus"
    };

    private readonly String[] namesOfPlanetsNum = {
        "I", "II", "III", "IV", "V"
    };

    public int Capacity; //вместимость планеты
    public int Fraction; //под чьим контролем 0 - нейтралитет, 1 - Кроганы, 2 - Азари
    public string NameOfPlanet; //имя планеты
    public int Population; //текущее население
    public GameObject SpaceShip;
    public GameObject Text;
    public Sprite azariPlanetSprite;
    public Sprite croganPlanetSprite;
    public bool isEmpty = true;
    public Sprite neutralPlanetSprite;
    private float timerColonization; //время между запусками кораблей
    private float timerPopulation; //время до увеличения численности населения


    // Use this for initialization
    private void Start() {
        //началные параметры при инициализации
        //настройка вынешнего вида планеты - фракции
        Fraction = Random.Range(0, 3);
        switch (Fraction) {
            case 0:
                gameObject.GetComponent<SpriteRenderer>().sprite = neutralPlanetSprite;
                break;
            case 1:
                gameObject.GetComponent<SpriteRenderer>().sprite = croganPlanetSprite;
                break;
            case 2:
                gameObject.GetComponent<SpriteRenderer>().sprite = azariPlanetSprite;
                break;
        }

        //настройка вместимости
        Capacity = Random.Range(500, 1000)*1000;

        //настройка популяции
        //если нейтральная - не заселена
        //если рассовая - рандомно
        if (Fraction == 0) Population = 0;
        else Population = Random.Range(100000, 500000);

        //Настройка имени планеты из предложенного массива
        NameOfPlanet = NamesOfPlanets[Random.Range(0, NamesOfPlanets.Length)] + " " +
                       namesOfPlanetsNum[Random.Range(0, namesOfPlanetsNum.Length)];

        //настройка таймеров 
        timerPopulation = 0.1f;
        timerColonization = Random.Range(5f, 10f);

        //настройка размещения
        transform.position =
            Camera.main.ScreenToWorldPoint(new Vector3(Random.Range(Screen.width/10f, Screen.width/10f*9f),
                Random.Range(Screen.height/5f, Screen.height/5f*4.5f), transform.position.z));

        //Подключение текстового описания
        var textClone = (GameObject) Instantiate(Text);
        textClone.GetComponent<PlanetTextScript>().Target = gameObject;
    }

    // Update is called once per frame
    private void Update() {
        timerPopulation -= Time.deltaTime;
        timerColonization -= Time.deltaTime;

        //Увеличение популяции
        if (Population > 0 && Population < Capacity) {
            isEmpty = false;
            if (timerPopulation < 0f) {
                timerPopulation = 0.1f;
                Population = Population + Random.Range(1, 100)*Convert.ToInt32(Math.Log(Population));
            }
        }


        //если перенаселение - лишние умирают
        if (Population > Capacity) Population--;

        //Колонизация
        if (Fraction != 0) //если не нейтралитет
            if (Population > 0)
                if (Capacity/Population < 4)
                    if (timerColonization < 0f) {
                        Debug.Log("Trying to launch a spaceship");
                        if (Random.Range(1, 3) == 2) {
                            Debug.Log("Succesfull");
                            var SpaceShipClone =
                                (GameObject) Instantiate(SpaceShip, transform.position, transform.rotation);
                            SpaceShipClone.GetComponent<SpaceshipScript>().Population = Population/10;
                            SpaceShipClone.GetComponent<SpaceshipScript>().ParentPlanet = gameObject;
                            SpaceShipClone.GetComponent<SpaceshipScript>().Fraction = Fraction;
                            Population -= Population/10;
                        }
                        timerColonization = Random.Range(5f, 10f);
                    }
    }
}