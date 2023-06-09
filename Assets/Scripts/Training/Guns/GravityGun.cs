using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GravityGun : MonoBehaviour
{
    [SerializeField] public Camera cam;
    [SerializeField] public float maxGrabDistance = 10f, throwForce = 20f, lerpSpeed = 10f;
    [SerializeField] public Transform objectHolder;

    public InputActionReference Shoot;
    public InputActionReference Trow;

    Rigidbody grabbedRB;

    void Update()
    {
        if (grabbedRB)
        {
            grabbedRB.MovePosition(Vector3.Lerp(grabbedRB.position, objectHolder.transform.position, Time.deltaTime * lerpSpeed));

            if (transform.parent.name == "Gun Container 3" && Trow.action.triggered)
            {
                grabbedRB.isKinematic = false;
                grabbedRB.AddForce(cam.transform.forward * throwForce, ForceMode.VelocityChange);
                grabbedRB = null;
            }
        }

        if (transform.parent.name == "Gun Container 3" && Shoot.action.triggered)
        {
            if (grabbedRB)
            {
                grabbedRB.isKinematic = false;
                grabbedRB = null;
            }
            else
            {
                RaycastHit hit;
                Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f));
                if (Physics.Raycast(ray, out hit, maxGrabDistance))
                {
                    grabbedRB = hit.collider.gameObject.GetComponent<Rigidbody>();
                    if (grabbedRB)
                    {
                        grabbedRB.isKinematic = true;
                    }
                }
            }
        }
    }
}
