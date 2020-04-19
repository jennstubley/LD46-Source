using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Plant : MonoBehaviour {

    public StageController ActiveStage;

    public float StageLength = 5;
    public float StartingHealth = 3;
    public GameObject GameOverObj;
    public float SadnessSpeed = 0.25f;
    public float HappinessSpeed = 0.25f;
    
    public AudioSource GrowSound;

    public float water;
    public bool bugs = false;
    public bool paused = false;

    private int currentStage = 0;

    public float Health
    {
        get
        {
            return health / StageLength;
        }
    }
    private float health;

    // Use this for initialization
    void Start ()
    {
        water = 1.0f;
        health = StartingHealth;
        SetActiveStage();
    }

    private void SetActiveStage()
    {
        foreach (Transform child in transform)
        {
            if (child.gameObject.name == "Actions" || child.gameObject.name == "Health" || child.gameObject.name == "Dirt" || child.gameObject.name == "Puddle") continue;
            child.gameObject.SetActive(false);
        }
        ActiveStage = transform.Find(string.Format("Stage_{0}", currentStage)).GetComponent<StageController>();
        ActiveStage.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update ()
    {
        if (!paused)
        {
            UpdateHealth();
            CheckGrowth();
        }
        CheckLoss();
        paused = IsDone();
    }

    public void ApplyAction(string name, float deltaTime)
    {
        if (name == "Water")
        {
            water += deltaTime;
        }
        else if (name == "Bugs")
        {
            bugs = false;
        }
    }

    private void UpdateHealth()
    {
        if (ActiveStage.IsSad())
        {
            health = Mathf.Max(0, health - Time.deltaTime * SadnessSpeed);
        }
        else
        {
            health = Mathf.Min(StageLength, health + Time.deltaTime * HappinessSpeed);
        }
    }

    public bool IsDone()
    {
        return currentStage == 4;
    }

    public bool HasPuddle()
    {
        if (ActiveStage == null) return false;
        return water >= ActiveStage.PuddleThreshold;
    }
    private void CheckGrowth()
    {
        if (health >= StageLength)
        {
            GrowSound.Play();
            currentStage++;
            SetActiveStage();
            if (!IsDone())
            {
                health = StartingHealth;
            }
        }
    }

    private void CheckLoss()
    {
        if (health <= 0)
        {
            GameOverObj.SetActive(true);
            paused = true;
        }
    }
}
