using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyBoardController : MonoBehaviour
{
    

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < 3; i++)
        {
            if(Input.GetKeyDown(KeyCode.Alpha1+i) && CircleGenrator.leaderBoard[i] != null)
            {
                transform.position = new Vector3(CircleGenrator.leaderBoard[i].transform.position.x, CircleGenrator.leaderBoard[i].transform.position.y, -10);
                Camera.main.orthographicSize = 5;
                CircleGenrator.leaderBoard[i].GetComponent<MouseController>().Mark();
                Camera.main.GetComponent<UiController>().SetFollow(true, gameObject);
            }
        }
    }
}
