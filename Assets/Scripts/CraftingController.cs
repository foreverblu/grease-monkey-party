using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CraftingController : MonoBehaviour
{
    private PlayerController playerController;
    private GameController gameController;
    public Button engineBtn;
    public Button tiresBtn;
    public Button doorBtn;
    public Button windowsBtn;
    public Button seatsBtn;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.Find("RaycastFPSController").GetComponent<PlayerController>();
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        gameObject.SetActive(false);
    }

    void OnEnable() {
        var root  = GetComponent<UIDocument>().rootVisualElement;
        engineBtn = root.Q<Button>("engine-btn");
        tiresBtn = root.Q<Button>("tires-btn");
        doorBtn = root.Q<Button>("door-btn");
        windowsBtn = root.Q<Button>("windows-btn");
        seatsBtn = root.Q<Button>("seats-btn");
        engineBtn.clicked += EngineButtonPressed;
        tiresBtn.clicked += TiresButtonPressed;
        doorBtn.clicked += DoorButtonPressed;
        windowsBtn.clicked += WindowsButtonPressed;
        seatsBtn.clicked += SeatsButtonPressed;
    }

    void CraftPart(Dictionary<string, int> resources, string part)
    {
        Dictionary<string, int> inv = playerController.inventory;
        bool canCraft = true;
        foreach (KeyValuePair<string, int> kvp in resources) {
            if (!inv.ContainsKey(kvp.Key) || inv[kvp.Key] < kvp.Value)
                canCraft = false;
        }
        if (canCraft) {
            foreach (KeyValuePair<string, int> kvp in resources)
                inv[kvp.Key] -= kvp.Value;
            if (!inv.ContainsKey(part))
                inv.Add(part, 0);
            inv[part] += 1;
        }
    }

    void EngineButtonPressed()
    {
        CraftPart(new Dictionary<string, int>() {{"metal", 1}, {"circuits", 1}}, "engine");
        gameController.UpdateInventory(playerController.inventory);
    }

    void TiresButtonPressed()
    {
        CraftPart(new Dictionary<string, int>() {{"metal", 1}, {"plastics", 2}}, "tires");
        gameController.UpdateInventory(playerController.inventory);
    }
    
    void DoorButtonPressed()
    {
        CraftPart(new Dictionary<string, int>() {{"metal", 1}, {"leather", 1}}, "door");
        gameController.UpdateInventory(playerController.inventory);
    }

    void WindowsButtonPressed()
    {
        CraftPart(new Dictionary<string, int>() {{"metal", 1}, {"glass", 1}}, "windows");
        gameController.UpdateInventory(playerController.inventory);
    }

    void SeatsButtonPressed()
    {
        CraftPart(new Dictionary<string, int>() {{"leather", 1}, {"plastic", 1}}, "seats");
        gameController.UpdateInventory(playerController.inventory);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
