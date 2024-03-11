using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class CircleGenrator : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI circleCountText;
    [SerializeField] private TextMeshProUGUI mutCountText;
    [SerializeField] private TextMeshProUGUI birthCountText;
    [SerializeField] private TextMeshProUGUI deathCountText;
    [SerializeField] private TextMeshProUGUI dupCountText;
    [SerializeField] private List<TextMeshProUGUI> textList = new List<TextMeshProUGUI>();

    [Header("Circle Generation")]
    [SerializeField] private Vector2 start = new Vector2(-10, -10);
    [SerializeField] private int spawnAmount = 10;
    [SerializeField] private GameObject circle;
    [SerializeField] private float updateTime = 2f;
    [SerializeField] private TextAsset nameJsonFile;


    public static int circleCount;
    public static int dupCount;
    public static int mutCount;
    public static int birthsCount;
    public static int deathCount;
    public static string nameJson;

    public static GameObject[] leaderBoard;
    public static List<GameObject> circles = new List<GameObject>();


    private Vector2 end;

    private void Awake()
    {
        nameJson = nameJsonFile.text;
    }

    void Start()
    {
        end = start * -1;
        circleCount = spawnAmount;

        GenerateCircle();
    }

    float timeEleapsed = 0;
    private void Update()
    {
        timeEleapsed += Time.deltaTime;

        if(timeEleapsed >= updateTime)
        {
            GameObject[] bestCircles = BestCircles();
            leaderBoard = bestCircles;

            textList[0].text = "---";
            textList[2].text = "---";
            textList[1].text = "---";

            if (circleCount >= 1 && bestCircles[0] != null)
                textList[0].text = $"1: Circle: {bestCircles[0].name} has {bestCircles[0].GetComponent<CircleLogic>().GetEnergy()} Energy";
            if (circleCount >= 2 && bestCircles[1] != null)
                textList[1].text = $"2: Circle: {bestCircles[1].name} has {bestCircles[1].GetComponent<CircleLogic>().GetEnergy()} Energy";
            if (circleCount >= 3 && bestCircles[2] != null)
                textList[2].text = $"3: Circle: {bestCircles[2].name} has {bestCircles[2].GetComponent<CircleLogic>().GetEnergy()} Energy";

            timeEleapsed = 0;
        }

        if(circleCount <= 0)
        {
            GenerateCircle();
            circleCount = spawnAmount;
        }
        circleCountText.text = "Circle Count: " + circleCount.ToString();
        dupCountText.text = "Dup Count: " + dupCount.ToString();
        mutCountText.text = "Mut Count: " + mutCount.ToString();
        birthCountText.text = "Births Count: " + birthsCount.ToString();
        deathCountText.text = "Deaths Count: " + deathCount.ToString();
    }
    public GameObject[] BestCircles()
    {
        int bestEnergy = 0;
        int bestEnergy2 = 0;
        int bestEnergy3 = 0;
        GameObject[] bestCircles = new GameObject[3];
        for (int i = 0; i < circles.Count; i++)
        {
            if (circles[i] != null && circles[i].GetComponent<CircleLogic>().GetEnergy() > bestEnergy) {
                bestEnergy = circles[i].GetComponent<CircleLogic>().GetEnergy();
                bestCircles[2] = bestCircles[1];
                bestCircles[0] = bestCircles[1];
                bestCircles[0] = circles[i];
            } else if (circles[i] != null && circles[i].GetComponent<CircleLogic>().GetEnergy() > bestEnergy2)
            {
                bestEnergy2 = circles[i].GetComponent<CircleLogic>().GetEnergy();
                bestCircles[2] = bestCircles[1];
                bestCircles[1] = circles[i];
            } else if (circles[i] != null && circles[i].GetComponent<CircleLogic>().GetEnergy() > bestEnergy3)
            {
                bestEnergy2 = circles[i].GetComponent<CircleLogic>().GetEnergy();
                bestCircles[2] = circles[i];
            }
        }
        return bestCircles;
    }

    void GenerateCircle()
    {
        for (int i = 0; i < spawnAmount; i++)
        {
            GameObject g = Instantiate(circle, new Vector3(UnityEngine.Random.Range(start.x, end.x), UnityEngine.Random.Range(start.y, end.y), 0), Quaternion.identity);
            
            string randomName = NameController.GetRandomStringFromJson();
            g.name = randomName;
            circles.Add(g);
        }
        birthsCount++;
        mutCount = 0;
        dupCount = 0;
        deathCount = 0;
    }
}
