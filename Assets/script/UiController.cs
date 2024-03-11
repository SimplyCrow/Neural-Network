using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiController : MonoBehaviour
{
    [Header("Circle Stats UI")]
    [SerializeField] private TextMeshProUGUI energyLevelText;
    [SerializeField] private TextMeshProUGUI nameOfCircleText;
    [SerializeField] private GameObject circleStatsCanvas;
    [SerializeField] private TextMeshProUGUI[] inputTexts;
    [SerializeField] private TextMeshProUGUI[] outputTexts;
    [SerializeField] private Toggle followToggle;

    public static bool isOnCircle;
    public static int energyOfCircle;
    public static string nameOfCircle;
    public static float[] inputs;
    public static float[] outputs;
    public static GameObject circleOn;
    public static bool followCircle;

    // Update is called once per frame
    void Update()
    {
        followCircle = followToggle.isOn;
        if (isOnCircle)
        {
            if(followCircle && circleOn  != null)
            {
                Camera.main.gameObject.transform.position = new Vector3(circleOn.transform.position.x, circleOn.transform.position.y, -10);
            }

            circleStatsCanvas.SetActive(true);
            nameOfCircleText.text = "Name: " + nameOfCircle;
            energyLevelText.text = "Energy: " + energyOfCircle.ToString();
            if (inputTexts.Length == inputs.Length)
            {
                for (int i = 0; i < inputTexts.Length; i++)
                {
                    inputTexts[i].text = "Input " + (i + 1).ToString() + ": " +inputs[i].ToString();
                }
            }
            if(outputs.Length == outputTexts.Length) {
                for (int i = 0; i < outputs.Length; i++)
                {
                    outputTexts[i].text = "Output " + (i + 1).ToString() + ": " + outputs[i].ToString();
                }
            }
        }
        else
            circleStatsCanvas.SetActive(false);

        if(circleOn == null)
        {
            isOnCircle = false;
            followCircle = false;
        }

    }

    public void SetFollow(bool follow, GameObject sender)
    {
        followToggle.isOn = follow;
    }
}
