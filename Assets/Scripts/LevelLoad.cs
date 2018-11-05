using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoad : MonoBehaviour {
    public GameObject game_Manager;
    private LevelSelect gameScript;

    private void Awake()
    {
        gameScript = game_Manager.GetComponent<LevelSelect>();
    }

    void LoadNow()
    {
        gameScript.loadLevel();
    }
}
