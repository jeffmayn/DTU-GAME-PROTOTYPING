using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaToIce : MonoBehaviour
{
    [SerializeField] GameObject obj;
    [SerializeField] Texture[] textures;
    GameObject lavaField;
    private Renderer cubeRenderer;


    private void OnTriggerEnter(Collider other)
    {

        print(other.tag);
 
        if(cubeRenderer.material.mainTexture == textures[1])
        {
            cubeRenderer.material.mainTexture = textures[0];

        } else if (cubeRenderer.material.mainTexture == textures[0])
        {
            cubeRenderer.material.mainTexture = textures[1];
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (cubeRenderer.material.mainTexture == textures[1])
        {
            cubeRenderer.material.mainTexture = textures[0];

        }
        else if (cubeRenderer.material.mainTexture == textures[0])
        {
            cubeRenderer.material.mainTexture = textures[1];
        }
    }

    private void Start()
    {
        cubeRenderer = obj.GetComponent<Renderer>();

    }




}
