using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveRotate : MonoBehaviour
{
    public GameObject Zone;
    public BoxCollider box;

    private void Update()
    {
       // transform.Rotate(new Vector3(0, rotationSpeed, 0) * Time.deltaTime);
    }
    private void OnCollisionEnter(Collision col)
    {

        if (col.gameObject.CompareTag("Ground"))
        {
            Zone.SetActive(true);
        }
        

        if (col.gameObject.CompareTag("Targets"))
        {
            box.isTrigger = true;
        }
    }

    private void OnCollisionExit(Collision col)
    {
        if (col.gameObject.CompareTag("Targets"))
        {
            box.isTrigger = false;
        }
    }

}
