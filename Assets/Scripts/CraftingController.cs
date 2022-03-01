using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CraftingController : MonoBehaviour
{
    private PlayerController playerController;
    private GameController gameController;
    public Button engineBtn;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.Find("RaycastFPSController").GetComponent<PlayerController>();
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        var root  = GetComponent<UIDocument>().rootVisualElement;
        engineBtn = root.Q<Button>("engine-btn");

        engineBtn.clicked += EngineButtonPressed;

        gameObject.SetActive(false);
    }

    void EngineButtonPressed()
    {
        playerController.inventory.Add("Eggs", 3);
        gameController.UpdateInventory(playerController.inventory);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
