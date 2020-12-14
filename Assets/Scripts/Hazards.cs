using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazards : MonoBehaviour
{
    GameObject lavaField;
    private Renderer cubeRenderer;

    private void Start()
    {
        lavaField = GameObject.FindWithTag("Lava");
    }

    private void OnTriggerEnter(Collider other)
    {
        cubeRenderer = lavaField.GetComponent<Renderer>();
        PlayerController player = other.GetComponent<PlayerController>();
        if(cubeRenderer.material.mainTexture.name == "lava_texture") 
        {
            player.Die();
        }

        

    }
}
