using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{

    public int number;
    public GameObject cube;
    LapHandle lapHandle;
    MeshRenderer meshRenderer;

    void Start()
    {
        lapHandle = FindObjectOfType<LapHandle>();
        meshRenderer = GetComponent<MeshRenderer>();
    }


    void OnTriggerEnter(Collider other)
    {
        if (  other.TryGetComponent(out Sphere sphere))
        {
            lapHandle.NextCheckpoint(number);
        }  
    }
    public void Show(bool active)
    {
        cube.SetActive(active);
        meshRenderer.enabled = active;
    }
}
