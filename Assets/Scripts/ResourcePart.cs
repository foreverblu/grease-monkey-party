using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResourcePart : MonoBehaviour
{

    private Outline outline;
    private GameController gameController;
    // Start is called before the first frame update
    void Start()
    {
        outline = GetComponent<Outline>();
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        outline.OutlineMode = Outline.Mode.OutlineHidden;
        outline.OutlineColor = Color.yellow;
        outline.OutlineWidth = 5f;  
        outline.enabled = false;
    }

    void OnMouseOver() {
        Transform camera = Camera.main.transform;
        float dist = Vector3.Distance(camera.position, transform.position);
        if(dist < 10) {
            outline.enabled = true;
            outline.OutlineMode = Outline.Mode.OutlineAll; 
            gameController.UpdateHighlight(gameObject.name);
        }
     
    }

    void OnMouseExit() {
        outline.enabled = false;
        outline.OutlineMode = Outline.Mode.OutlineHidden;
        gameController.UpdateHighlight("");

    }
}
