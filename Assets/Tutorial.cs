using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour {

    public Plant CurrentPlant;

    private Text text;
    private float stepTimer;
    private Step currentStep;
    private enum Step
    {
        Intro,
        NoBugDelay,
        Bugs,
        BugText,
        NoDryDelay,
        Dry,
        DryText,
        KeepGoing,
        Done
    }

	// Use this for initialization
	void Start () {
        Debug.Log("Startign tutorial");
        text = GetComponentInChildren<Text>();
        text.text = "This is your plant. Try to keep it alive.";
        stepTimer = 3;
	}
	
	// Update is called once per frame
	void Update () {
        if (currentStep == Step.Done) return;

        stepTimer -= Time.deltaTime;
        if (stepTimer <= 0)
        {
            NextStep();
        }

        if (currentStep != Step.BugText && currentStep != Step.Bugs && currentStep != Step.Done)
        {
            CurrentPlant.bugs = false;
        }

        if (currentStep != Step.DryText && currentStep != Step.Dry && currentStep != Step.Done)
        {
            CurrentPlant.water = 1.0f;
        }

        if (currentStep == Step.BugText && !CurrentPlant.bugs)
        {
            NextStep();
        }

        if (currentStep == Step.DryText && CurrentPlant.water > 0.8f)
        {
            NextStep();
        }
    }

    private void NextStep()
    {
        currentStep++;
        switch (currentStep)
        {
            case Step.NoBugDelay:
                stepTimer = 2;
                text.text = "So far so good...";
                return;
            case Step.Bugs:
                CurrentPlant.bugs = true;
                stepTimer = 0.5f;
                return;
            case Step.BugText:
                text.text = "Ewww bugs! Click on them to remove them.";
                stepTimer = 50000;
                return;
            case Step.NoDryDelay:
                stepTimer = 2;
                text.text = "Much better!";
                return;
            case Step.Dry:
                CurrentPlant.water = 0.0f;
                stepTimer = 0.5f;
                return;
            case Step.DryText:
                text.text = "Oh no, now it's wilting. Click the cloud to water it.";
                stepTimer = 5000;
                return;
            case Step.KeepGoing:
                text.text = "All fixed! If you keep your plant happy it will grow.";
                stepTimer = 2;
                return;
            case Step.Done:
                text.gameObject.SetActive(false);
                stepTimer = 5;
                return;
        }
    }
}
