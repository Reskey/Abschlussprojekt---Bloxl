using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Skripts.Management
{
    public class GameController : MonoBehaviour
    {
        [HideInInspector] public Inputs inputControlls;

        private void Awake()
        {
            inputControlls = new Inputs();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void FlipSprite(GameObject x)
        {
            Vector3 newScale = x.gameObject.transform.localScale;
            newScale.x *= -1;
            x.gameObject.transform.localScale = newScale;
        }
    }
}