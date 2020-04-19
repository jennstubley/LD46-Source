using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {

    private Plant plant;
    private GameObject cover;

	// Use this for initialization
	void Start () {
        plant = GetComponentInParent<Plant>();
        cover = transform.Find("Cover").gameObject;
	}
	
	// Update is called once per frame
	void Update () {
        float posX = CalcPosX();
        float width = CalcWidth();
        cover.transform.localPosition = new Vector3(posX, cover.transform.localPosition.y, cover.transform.localPosition.z);
        cover.GetComponent<SpriteRenderer>().size = new Vector2(width, cover.GetComponent<SpriteRenderer>().size.y);
    }

    private float CalcPosX()
    {
        //const float ratio = -2.5f / 80.0f;
        // return ratio * Mathf.Floor((1.0f - plant.Health) * 80.0f);
        return (1.0f - plant.Health) * -2.5f;
    }

    private float CalcWidth()
    {
        //const float ratio = 5.0f / 80.0f;
        // return ratio * Mathf.Ceil(plant.Health * 80.0f);
        return plant.Health * 5.0f;
    }
}
