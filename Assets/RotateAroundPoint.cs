using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAroundPoint : MonoBehaviour
{
    public GameObject target;
    public float orbitDistance = 10.0f;
    public float orbitDegreesPerSec = 180.0f;
    public bool isRotating;

    private void LateUpdate()
    {
        if (target == null)
        {
            isRotating = false;
        }

        target = GameObject.Find("Bullet 2(Clone)");

        if (isRotating == true)
        {
            transform.position = target.transform.position + (transform.position - target.transform.position).normalized * orbitDistance;
            transform.RotateAround(target.transform.position, Vector3.up, orbitDegreesPerSec * Time.deltaTime);

        }
        
    }
    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Zone"))
        {
            isRotating = true;
        }
    }

}
