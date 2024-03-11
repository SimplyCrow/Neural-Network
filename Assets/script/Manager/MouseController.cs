using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    public bool alwaysOn = false;
    
    public void Mark()
    {
        alwaysOn = true;
        Select();
    }

    private void Select()
    {
        UiController.isOnCircle = true;
        UiController.nameOfCircle = name;
        UiController.energyOfCircle = GetComponent<CircleLogic>().GetEnergy();
        UiController.inputs = GetComponent<CircleLogic>().GetInputs();
        UiController.outputs = GetComponent<CircleLogic>().GetOutputs();
        UiController.circleOn = gameObject;
    }

    public void UnSelect()
    {
        UiController.isOnCircle = false;
        UiController.circleOn = null;
        UiController.followCircle = false;
        UiController.nameOfCircle = "NONE";
        UiController.energyOfCircle = -1;
        UiController.inputs = new float[300];
        UiController.outputs = new float[300];
    }

    private void OnMouseOver()
    {
        Select();
    }
    private void OnMouseExit()
    {
        if (!alwaysOn)
            UnSelect();
    }

    private void OnDestroy()
    {
        alwaysOn = false;
        UiController.isOnCircle = false;
        Camera.main.GetComponent<UiController>().SetFollow(false, gameObject);
        UiController.circleOn = null;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
            if (hit.collider != null && hit.collider.gameObject.tag == "Circle")
            {
                if (hit.collider.gameObject == gameObject)
                {
                    alwaysOn = true;
                    Select();
                }
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            alwaysOn = false;
            UnSelect();
        }
    }

}
