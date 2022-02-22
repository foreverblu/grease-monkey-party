using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Camera camera;
    public float rayDistance;
    public float distnace;
    public Dictionary<string, int> inventory {get; private set;}
    // Start is called before the first frame update
    void Start()
    {
        inventory = new Dictionary<string, int>();
        // Dummie Data
        inventory.Add("metal", 1);
        inventory.Add("rubber", 2);
        inventory.Add("glass", 3);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown (KeyCode.E)) {
            Pickup();
        }
    }

    void Pickup() {
        RaycastHit hit;
        Ray ray = camera.ScreenPointToRay (Input.mousePosition);

        if (Physics.Raycast (ray, out hit, rayDistance)) {
                if (hit.collider.tag == "Resource") {
                    Debug.Log ("You hit a pickObject!");
            }
        }
    }
}
