using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class GameController : MonoBehaviour
{
  public GameObject timerObj;
  public GameObject inventoryObj;
  public GameObject objectivesObj;
  public GameObject itemHighlightObj;
  public GameObject tutorialObj;
  public int level = 0;

  private TextMeshProUGUI inventoryTxt;
  private TextMeshProUGUI objectiveTxt;

  private TextMeshProUGUI itemTxt;
  private bool tutOver = false;
  private Dictionary<string, bool> objectives = new Dictionary<string, bool>();
  // Start is called before the first frame update
  void Awake()
  {
    inventoryTxt = inventoryObj.GetComponent<TextMeshProUGUI>();
    objectiveTxt = objectivesObj.GetComponent<TextMeshProUGUI>();
    itemTxt = itemHighlightObj.GetComponent<TextMeshProUGUI>();
    SetObjectives(level);
    UpdateObjective("");
  }

  // Update is called once per frame
  void Update()
  {
    if (tutorialObj != null && !tutOver && Input.GetMouseButtonDown(0))
    {
      tutOver = true;
      tutorialObj.SetActive(false);
    }
  }

  void SetObjectives(int car)
  {
    Timer timer = timerObj.GetComponent<Timer>();
    if (car == 0)
    {
      timer.StartTimer(60.0f);
      objectives.Add("tire", false);
      objectives.Add("door", false);
      objectives.Add("window", false);
    }
    else if (car == 1)
    {
      timer.StartTimer(90.0f);
      objectives.Add("engine", false);
      objectives.Add("seat", false);
      objectives.Add("tire", false);
    }
    else if (car == 2)
    {
      timer.StartTimer(300.0f);
      objectives.Add("engine", false);
      objectives.Add("seat", false);
      objectives.Add("tire", false);
      objectives.Add("door", false);
      objectives.Add("window", false);
    }
    string text = "<size=20>Objective</size>\n";
    foreach (string key in objectives.Keys)
    {
      text += key + "\n";
    }
    objectiveTxt.text = text;
  }

  public bool UpdateObjective(string part)
  {
    if (objectives.ContainsKey(part) && !objectives[part])
    {
      objectives[part] = true;
      string text = "<size=20>Objective</size>\n";
      foreach (string key in objectives.Keys)
      {
        if (objectives[key])
        {
          text += "<s>" + key + "</s>\n";
        }
        else
        {
          text += key + "\n";
        }
      }
      objectiveTxt.text = text;
      return true;
    }



    return false;
  }

  public void UpdateInventory(Dictionary<string, int> inv)
  {
    string text = "<size=20>Inventory</size>\n";
    foreach (string key in inv.Keys)
    {
      text += key + ": " + inv[key] + "\n";
    }
    inventoryTxt.text = text;
  }

  public void UpdateHighlight(string n)
  {
    itemTxt.text = n;
  }

  public void GameOver(bool win)
  {
    GameObject.Find("RaycastFPSController").GetComponent<PlayerController>().GameOver();
    if(!win) {
        SceneManager.LoadScene(sceneName: "GameOver");
    }else {
        SceneManager.LoadScene(sceneName: "WinOver");
    }
  }

  public bool AllDone() {
      foreach (string key in objectives.Keys)
      {
          if(!objectives[key]) {
              return false;
          }
      }

      return true;
  }
}
