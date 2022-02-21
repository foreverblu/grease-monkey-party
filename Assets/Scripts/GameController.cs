using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject timerObj;
    // Start is called before the first frame update
    void Start()
    {
        Timer timer = timerObj.GetComponent<Timer>();
        timer.StartTimer(100.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
