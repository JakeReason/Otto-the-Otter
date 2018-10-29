using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShatteringDoor : MonoBehaviour {
    private int speed = 20;
    public GameObject gameManager;
    private StartGame gameStartScript;

    // Use this for initialization
    void Awake()
    {
        gameStartScript = gameManager.GetComponent<StartGame>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.left * speed * Time.deltaTime, Space.World);
    }
    public void Shatter()
    {
        gameStartScript.GoodbyeDoor();
    }
}
