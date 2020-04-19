using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable]
public class PlantList
{
    public Plant[] Plants;
}

[Serializable]
public class Levels : MonoBehaviour {

    public PlantList[] LevelPlants;
    public int CurrentLevel;

    public GameObject NextLevelObj;
    public GameObject YouWinObj;

    // Use this for initialization
    void Start () {
        NextLevel();

    }
	
	// Update is called once per frame
	void Update () {
        CheckWin();
	}

    private void CheckWin()
    {
        foreach (Plant plant in LevelPlants[CurrentLevel].Plants)
        {
            if (!plant.IsDone())
            {
                return;
            }
        }

        // All plants are done.
        if (CurrentLevel + 1 == LevelPlants.Length)
        {
            YouWinObj.SetActive(true);
        }
        else
        {
            NextLevelObj.SetActive(true);
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void NextLevel()
    {
        NextLevelObj.SetActive(false);
        if (CurrentLevel >= 0)
        {
            foreach (Plant plant in LevelPlants[CurrentLevel].Plants)
            {
                plant.gameObject.SetActive(false);
            }
        }
        CurrentLevel++;
        foreach (Plant plant in LevelPlants[CurrentLevel].Plants)
        {
            plant.gameObject.SetActive(true);
        }
    }
}
