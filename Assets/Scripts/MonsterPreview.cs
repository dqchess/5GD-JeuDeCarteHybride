using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterPreview : MonoBehaviour
{
    public float rotationSpeed;
    public GameObject model;

    void Update()
    {
        if (GameManager.Instance.state == GameManager.State.STATS)
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }

    public void StartFight()
    {
        transform.LookAt(Camera.main.transform.position + Vector3.up * 90f);
    }
}
