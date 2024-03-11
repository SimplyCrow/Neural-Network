using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class CircleLogic : MonoBehaviour
{
    [Header("UI NAME TAG")]
    [SerializeField] private TextMeshProUGUI nameTagText;
    [SerializeField] private Canvas nameTagCanvas;

    [Header("SETTINGS")]
    [SerializeField] private int[] layersShape;
    [SerializeField] private Vector2 startRoom;
    [SerializeField] private Gradient gradient;
    [SerializeField] private float boostSpeed;

    private Brain brain;
    private int energy = 10;
    private float[] inputs;
    private float[] outputs;
    private Vector2 endRoom;
    private Rigidbody2D rb;
    private SpriteRenderer sr;


    private void Awake()
    {
        brain = new Brain(layersShape);
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        nameTagText.text = name;
        nameTagCanvas.worldCamera = Camera.main;

        endRoom = new Vector2(startRoom.x * -1, startRoom.y * -1);

        if(brain is null)
            brain = new Brain(layersShape);

        inputs = new float[layersShape[0]];
    }

    public GameObject FindClosestFood()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Food");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }

    float timeElapsed = 0;

    void Update()
    {
        if (rb.velocity.y < 2.5 && rb.velocity.x < 2.5)
        {
            transform.position += transform.up * boostSpeed * Time.deltaTime;
        }

        if (timeElapsed >= 1)
        {
            energy -= 1;
            timeElapsed = 0;
        }

        if(energy >= 30)
        {
            Duplicate();
        }
        else if (energy <= 0)
        {
            CircleGenrator.circleCount -= 1;
            CircleGenrator.deathCount += 1;
            Destroy(gameObject);
        }

        sr.color = gradient.Evaluate((float)energy / 30);

        timeElapsed += Time.deltaTime;

        Cycle();
    }

    private void Cycle()
    {
        inputs = new float[inputs.Length];

        GameObject closedFood = FindClosestFood();
        if (closedFood is not null)
        {
            inputs[0] = (transform.position - closedFood.transform.position).x;
            inputs[1] = (transform.position - closedFood.transform.position).y;
        }        
        inputs[2] = Math.Abs(transform.eulerAngles.z) / 360f;
        //inputs[3] = rb.velocity.x;
        //inputs[4] = rb.velocity.y;

        outputs = brain.Calculate(inputs);
        transform.eulerAngles -= new Vector3(0, 0, Mathf.Clamp(outputs[0], -2, 2));
        nameTagCanvas.transform.rotation = Quaternion.identity;
    }

    private void Duplicate()
    {
        CircleGenrator.circleCount += 1;
        CircleGenrator.dupCount += 1;

        GameObject c0 = Instantiate(gameObject, new Vector3(UnityEngine.Random.Range(startRoom.x, endRoom.x), UnityEngine.Random.Range(startRoom.y, endRoom.y), 0), Quaternion.identity);
        GameObject c1 = Instantiate(gameObject, new Vector3(UnityEngine.Random.Range(startRoom.x, endRoom.x), UnityEngine.Random.Range(startRoom.y, endRoom.y), 0), Quaternion.identity);
 
        c0.name = NameController.GetRandomStringFromJson();
        c1.name = NameController.GetRandomStringFromJson(); ;

        c0.GetComponent<CircleLogic>().energy = energy / 2;
        c1.GetComponent<CircleLogic>().energy = energy / 2;

        CircleGenrator.circles.Add(c0);
        CircleGenrator.circles.Add(c1);

        c0.GetComponent<CircleLogic>().SetLayers(brain.GetLayers());
        c0.GetComponent<CircleLogic>().Mutate();
        c1.GetComponent<CircleLogic>().SetLayers(brain.GetLayers());
        c1.GetComponent<CircleLogic>().Mutate();

        Destroy(gameObject);
    }

    public void Mutate()
    {
        brain.Mutate();
    }
    public void SetLayers(Layer[] layers)
    {
        brain.SetLayers(layers);
    }

    public float[] GetInputs()
    {
        return inputs;
    }

    public float[] GetOutputs()
    {
        return outputs;
    }

    public int GetEnergy()
    {
        return energy;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Food")
        {
            Destroy(collision.gameObject);
            energy += 10;
        }
    }

}

