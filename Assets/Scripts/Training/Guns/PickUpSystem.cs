using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PickUpSystem : MonoBehaviour
{

    // public ProjectileGun gunScript;
    public InputActionReference Recoger;
    public InputActionReference Tirar;
    public Rigidbody rb;
    public BoxCollider coll;
    public Transform player, gunContainer, fpsCam;

    public float pickUpRange, pickUpTime;
    public float dropForwardForce, dropUpwardForce;

    public bool equipped;
    public static bool slotFull;

    public Animator anim;

    private void Start()
    {

        if (!equipped)
        {
          //  gunScript.enabled = false;
            rb.isKinematic = false;
            coll.isTrigger = false;
            rb.useGravity = true;
        }
        if (equipped)
        {
            slotFull = true;
            rb.isKinematic = true;
            rb.useGravity = false;
            coll.isTrigger = true;
        }
    }
    private void Update()
    {
        //Check if player is in range and "E" is pressed
        Vector3 distanceToPlayer = player.position - transform.position;
        if (Recoger.action.triggered && !equipped && distanceToPlayer.magnitude <= pickUpRange && !slotFull) PickUp();

        //Drop if equipped and "Q" is pressed
        if (equipped && Tirar.action.triggered) Drop();
    }

    private void PickUp()
    {
        anim.SetBool("Aiming", true);
        equipped = true;
        slotFull = true;

        //Make weapon a child of the camera and move it to the equippedPosition
        transform.SetParent(gunContainer);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(Vector3.zero);
        transform.localScale = Vector3.one;

        //Make Rigidbody Kinematic and BoxCollider a trigger
        rb.isKinematic = true;
        rb.useGravity = false;
        coll.isTrigger = true;

        //Enable script
     //   gunScript.enabled = true;
    }

    private void Drop()
    {
        anim.SetBool("Aiming", false);
        equipped = false;
        slotFull = false;

        //Set parent to null
        transform.SetParent(null);

        //Make Rigidbody and BoxCollider normal
        rb.isKinematic = false;
        rb.useGravity = true;
        coll.isTrigger = false;

        rb.velocity = player.GetComponent<Rigidbody>().velocity;

        //Add force
        rb.AddForce(fpsCam.forward * dropForwardForce, ForceMode.Impulse);
        rb.AddForce(fpsCam.up * dropUpwardForce, ForceMode.Impulse);
        //Add random rotation
        float random = Random.Range(-1f, 1f);
        rb.AddTorque(new Vector3(random,random,random) * 10);

        //Disable script
      //  gunScript.enabled = false;
    }

}
