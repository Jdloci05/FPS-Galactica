using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animations : MonoBehaviour
{
    public Animator characterMenu;
    public Animator characterTraining;

    public void Macarena()
    {
        characterMenu.SetBool("Macarena", true);
        characterMenu.SetBool("House", false);
        characterMenu.SetBool("HipHop", false);
    }

    public void House()
    {
        characterMenu.SetBool("Macarena", false);
        characterMenu.SetBool("House", true);
        characterMenu.SetBool("HipHop", false);
    }

    public void HipHop()
    {
        characterMenu.SetBool("Macarena", false);
        characterMenu.SetBool("House", false);
        characterMenu.SetBool("HipHop", true);
    }
}
