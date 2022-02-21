using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Dictionary<string, int> inventory;
    // Start is called before the first frame update
    void Start()
    {
        // Dummie Data
        inventory.Add("metal", 1);
        inventory.Add("rubber", 2);
        inventory.Add("glass", 3);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
