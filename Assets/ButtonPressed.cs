using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonPressed : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private PlayerController playerControllerScript;

    public bool buttonPressed;

    public void OnPointerDown(PointerEventData eventData) {
        buttonPressed= true;
    }

    public void OnPointerUp(PointerEventData eventData) {
        buttonPressed= false;
    }

    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(buttonPressed) {
            playerControllerScript.GetComponent<Animator>().speed = 3;
        }
        else {
            playerControllerScript.GetComponent<Animator>().speed = 1;
        }
    }
}
