using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageController : MonoBehaviour {

    public float WaterThreshold = 0.2f;
    public float PuddleThreshold = 3.0f;
    public float WaterDecay = 0.005f;
    public float BugChance = 0.005f;

    private Plant plant;
    public Text WarningText;

	// Use this for initialization
	void Start () {
        plant = GetComponentInParent<Plant>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        UpdatePlant();

        UpdateStateUI();
    }

    private void UpdatePlant()
    {
        if (plant.paused) return;

        plant.water = Mathf.Max(0, plant.water - WaterDecay);
        if (!plant.bugs)
        {
            plant.bugs = UnityEngine.Random.Range(0, 1.0f) <= BugChance;
        }
    }

    private void UpdateStateUI()
    {
        if (plant.water < WaterThreshold)
        {
            transform.Find("Dry").gameObject.SetActive(true);
            transform.Find("Normal").gameObject.SetActive(false);
            transform.parent.Find("Puddle").gameObject.SetActive(false);
        }
        else
        {
            transform.Find("Dry").gameObject.SetActive(false);
            transform.Find("Normal").gameObject.SetActive(true);
            transform.parent.Find("Puddle").gameObject.SetActive(plant.HasPuddle());
        }
        if (plant.bugs)
        {
            transform.Find("Bugs").gameObject.SetActive(true);
        }
        else
        {
            transform.Find("Bugs").gameObject.SetActive(false);
        }

    }

    public bool IsSad()
    {
        if (plant == null) return false;
        return plant.water <= WaterThreshold || plant.HasPuddle() || plant.bugs;
    }
}
