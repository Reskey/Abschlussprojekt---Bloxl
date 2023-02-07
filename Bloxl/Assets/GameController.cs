using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [HideInInspector] public Inputs inputControlls;

    private void Awake()
    {
        inputControlls = new Inputs();
    }
}
