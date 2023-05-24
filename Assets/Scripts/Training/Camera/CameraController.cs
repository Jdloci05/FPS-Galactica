//SlapChickenGames
//2021
//Camera controller for x and y movement

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace scgFullBodyController
{
    public class CameraController : MonoBehaviour
    {
        public Transform parent;
        public Transform boneParent;


        void OnEnable()
        {

        }

        void LateUpdate()
        {
            transform.position = boneParent.position;
        }

    }
}