using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [HideInInspector] public Inputs inputControlls;

    private void Awake()
    {
        inputControlls = new Inputs();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static void FlipSprite(GameObject obj)
    {
        Vector3 newScale = obj.transform.localScale;
        newScale.x *= -1;
        obj.transform.localScale = newScale;
    }
}
