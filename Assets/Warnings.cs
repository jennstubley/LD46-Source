using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Warnings : MonoBehaviour {

    private List<Plant> plants;

	// Use this for initialization
	void Start () {
        plants = Resources.FindObjectsOfTypeAll<Plant>().ToList();
	}
	
	// Update is called once per frame
	void Update () {
        bool puddleWarn = false;
		foreach (Plant plant in plants)
        {
            if (plant.gameObject.activeSelf && plant.HasPuddle())
            {
                puddleWarn = true;
                break;
            }
        }
        GetComponentInChildren<Text>().text = puddleWarn ? "Yikes, plants don't like puddles. Try not to water them too much." : "";
    }
}
