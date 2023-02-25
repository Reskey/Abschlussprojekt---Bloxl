using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [HideInInspector] public Inputs inputControlls;

    private void Awake()
    {
        inputControlls = new Inputs();
    }

    private void Start()
    {
        if (inputControlls is null)
        {
        inputControlls = new Inputs();
        }
    }

}
