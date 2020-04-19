using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action : MonoBehaviour {

    public float Length = 1;
    public float Decay = 0.02f;
    public string Name;
    public GameObject Obj;

    private float activeTimer;
    private bool applyAction;
    private bool mouseDown;
    private Plant plant;

    // Use this for initialization
    void Start() {
        plant = GetComponentInParent<Plant>();
    }

    // Update is called once per frame
    void Update() {
        if (mouseDown || applyAction)
        {
            applyAction = false;
           plant.ApplyAction(Name, Time.deltaTime);
        }
        if (Obj != null)
        {
            Obj.SetActive(mouseDown);
        }
    }

    void OnMouseDown()
    {
        if (Length == 0)
        {
            applyAction = true;
            return;
        }
        mouseDown = true;
    }

    void OnMouseUp()
    {
        mouseDown = false;
    }

    void OnMouseExit()
    {
        mouseDown = false;
    }
}
