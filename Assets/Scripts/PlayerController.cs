using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class PlayerController : MonoBehaviour
{
  public Camera camera;
  public float rayDistance;
  public Dictionary<string, int> inventory { get; private set; }
  public GameObject craftingUIObj;

  private GameController gameController;
  private CraftingController craftingController;
  private FirstPersonController fpController;
  private CarController carController;
  private string[] resources = new string[] { "metal", "plastic", "glass", "circuits", "leather" };

  private bool showCraftingUI;
  // Start is called before the first frame update
  private bool inCar;
  [SerializeField] private GameObject carSeat;

  void Start()
  {
    gameController = GameObject.Find("GameController").GetComponent<GameController>();
    fpController = GameObject.Find("RaycastFPSController").GetComponent<FirstPersonController>();
    carController = GameObject.Find("Car").GetComponent<CarController>();
    craftingController = craftingUIObj.GetComponent<CraftingController>();
    showCraftingUI = false;
    carController.SetBroken();
    inCar = false;

    inventory = new Dictionary<string, int>();
    foreach (string r in resources)
      inventory.Add(r, 0);

    gameController.UpdateInventory(inventory);
  }

  // Update is called once per frame
  void Update()
  {
    if (inCar)
    {
      if (Input.GetKeyDown(KeyCode.Q))
      {
        inCar = false;
        carController.hasDriver = false;
        fpController.enabled = true;
      }
      else
      {
        // Sync transform
        transform.position = carSeat.transform.position;
        transform.rotation = carSeat.transform.rotation;
      }
    }
    else
    {
      if (Input.GetKeyDown(KeyCode.E))
      {
        Pickup();
      }
    }
  }

  public void GameOver() {
    Cursor.lockState = CursorLockMode.None;
    Cursor.visible = true;
    fpController.enabled = false;
  }
  void Pickup()
  {
    RaycastHit hit;
    Ray ray = camera.ScreenPointToRay(Input.mousePosition);

    if (Physics.Raycast(ray, out hit, rayDistance))
    {
      if (hit.collider.tag == "Resource")
      {
        Debug.Log("You hit a pickObject!");
        inventory[hit.collider.gameObject.name.ToLower()] += 1;
        Destroy(hit.collider.gameObject);
        gameController.UpdateInventory(inventory);
        gameController.UpdateHighlight("");
      }
      else if (hit.collider.tag == "Part")
      {
        Debug.Log("You picked up a part!");
        if (!inventory.ContainsKey(hit.collider.gameObject.name))
        {
          inventory.Add(hit.collider.gameObject.name.ToLower(), 0);
        }
        inventory[hit.collider.gameObject.name.ToLower()] += 1;
        Destroy(hit.collider.gameObject);
        gameController.UpdateInventory(inventory);
      }
      else if (hit.collider.tag == "Workbench")
      {
        Debug.Log("Opened workbench!");
        showCraftingUI = !showCraftingUI;
        craftingController.gameObject.SetActive(showCraftingUI);
        if (showCraftingUI)
        {
          Cursor.lockState = CursorLockMode.None;
          Cursor.visible = true;
          fpController.enabled = false;
        }
        else
        {
          Cursor.lockState = CursorLockMode.Locked;
          Cursor.visible = false;
          fpController.enabled = true;
        }
      }
      else if (hit.collider.gameObject.name == "Car")
      {
        if (carController.isBroken)
        {
          foreach (string item in inventory.Keys)
          {
            if (gameController.UpdateObjective(item))
            {
              inventory[item]--;
              gameController.UpdateInventory(inventory);
              carController.UpdatePart(item);
              break;
            }
          }
        }
        else
        {
          Debug.Log("You got on the car!");
          carController.hasDriver = true;
          inCar = true;
          fpController.enabled = false;
        }
      }
    }
  }
}
