using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterPreview : MonoBehaviour
{
    public float rotationSpeed;
    [HideInInspector] public GameObject model;


    void Start()
    {
        
    }

    void Update()
    {
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
}
