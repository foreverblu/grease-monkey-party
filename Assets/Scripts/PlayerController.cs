using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class PlayerController : MonoBehaviour
{
    public Camera camera;
    public float rayDistance;
    public Dictionary<string, int> inventory {get; private set;}
    public GameObject craftingUIObj;

    private GameController gameController;
    private CraftingController craftingController;
    private FirstPersonController fpController;

    private bool showCraftingUI;
    // Start is called before the first frame update
    void Start()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        fpController = GameObject.Find("RaycastFPSController").GetComponent<FirstPersonController>();
        craftingController = craftingUIObj.GetComponent<CraftingController>();
        showCraftingUI = false;

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
        } else if(Input.GetKeyDown (KeyCode.C)) {
            showCraftingUI = !showCraftingUI;
            craftingController.gameObject.SetActive(showCraftingUI);
            fpController.m_MouseLook.lockCursor = showCraftingUI;
            fpController.m_MouseLook.UpdateCursorLock();
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
