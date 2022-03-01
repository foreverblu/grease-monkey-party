using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Camera camera;
    public float rayDistance;
    public Dictionary<string, int> inventory {get; private set;}

    private GameController gameController;
    // Start is called before the first frame update
    void Start()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        inventory = new Dictionary<string, int>();
        // Dummie Data
        inventory.Add("metal", 1);
        inventory.Add("rubber", 2);
        inventory.Add("glass", 3);
        gameController.UpdateInventory(inventory);
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
                inventory[hit.collider.gameObject.name.ToLower()] += 1;
                Destroy(hit.collider.gameObject);
                gameController.UpdateInventory(inventory);
            }else if(hit.collider.tag == "Part") {
                Debug.Log("You picked up a part!");
                if(!inventory.ContainsKey(hit.collider.gameObject.name)) {
                    inventory.Add(hit.collider.gameObject.name.ToLower(), 0);
                }
                inventory[hit.collider.gameObject.name.ToLower()] += 1;
                Destroy(hit.collider.gameObject);
                gameController.UpdateInventory(inventory);

            }else if(hit.collider.gameObject.name == "Broken Car") {
                foreach(string item in inventory.Keys) {
                    if(gameController.UpdateObjective(item)) {
                        inventory[item]--;
                        gameController.UpdateInventory(inventory);

                        break;
                    }
                }
            }

        }
    }
}
