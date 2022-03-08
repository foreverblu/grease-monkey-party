using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GameController : MonoBehaviour
{
    public GameObject timerObj;
    public GameObject inventoryObj;
    public GameObject objectivesObj;
    public GameObject itemHighlightObj;

    private TextMeshProUGUI inventoryTxt;
    private TextMeshProUGUI objectiveTxt;
    
    private TextMeshProUGUI itemTxt;
    private Dictionary<string, bool> objectives = new Dictionary<string, bool>();
    // Start is called before the first frame update
    void Awake()
    {
        Timer timer = timerObj.GetComponent<Timer>();
        inventoryTxt = inventoryObj.GetComponent<TextMeshProUGUI>();
        objectiveTxt = objectivesObj.GetComponent<TextMeshProUGUI>();
        itemTxt = itemHighlightObj.GetComponent<TextMeshProUGUI>();
        timer.StartTimer(60.0f);
        SetObjectives(-1);
        UpdateObjective("");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetObjectives(int car) {
        objectives.Add("tire", false);
        objectives.Add("door", false);
        objectives.Add("window", false);
        string text = "<size=20>Objective</size>\n";
        foreach(string key in objectives.Keys) {
            text += key + "\n";
        }
        objectiveTxt.text = text;
    }

    public bool UpdateObjective(string part) {
        if(objectives.ContainsKey(part) && !objectives[part]) {
            objectives[part] = true;
            string text = "<size=20>Objective</size>\n";
            foreach(string key in objectives.Keys) {
                if(objectives[key]) {
                    text += "<s>" + key + "</s>\n";
                }else {
                    text += key + "\n";
                }
            }
            objectiveTxt.text = text;
            return true;
        }

        

        return false;
    }

    public void UpdateInventory(Dictionary<string, int> inv) {
        string text = "<size=20>Inventory</size>\n";
        foreach(string key in inv.Keys) {
            text += key + ": " + inv[key] + "\n";
        }
        inventoryTxt.text = text;
    }

    public void UpdateHighlight(string n) {
        itemTxt.text = n;
    }
}
