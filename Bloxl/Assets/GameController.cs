using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [Header("Hitpopup"), Space(10), SerializeField] GameObject hitpopup;
    [SerializeField] Vector3 offset;

    [HideInInspector] public Inputs inputControlls;

    private void Awake()
    {
        inputControlls = new Inputs();

        Physics2D.IgnoreLayerCollision(10, 7, true);
        Physics2D.IgnoreLayerCollision(9, 7, true);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static void FlipSprite(GameObject x)
    {
        /*SpriteRenderer sr = x.GetComponent<SpriteRenderer>();
        sr.flipX = !sr.flipX;*/

        Vector3 newScale = x.gameObject.transform.localScale;
        newScale.x *= -1;
        x.gameObject.transform.localScale = newScale;
    }

    internal void HitPopUp(float dmg, GameObject obj)
    {
        GameObject g = hitpopup;

        GameObject c = MonoBehaviour.Instantiate(g, obj.transform.position, Quaternion.identity);

        TMP_Text text = c.GetComponentInChildren<TMP_Text>();
        text.text = dmg.ToString();
        if (obj.tag == "Player")
        {
            text.color = Color.red;
        }

        StartCoroutine(HitpopupPosUpdater(text, obj));

        MonoBehaviour.Destroy(c, 1);
    }

    private IEnumerator HitpopupPosUpdater(TMP_Text textRef, GameObject targetObj)
    {
        Vector3 pos = targetObj.transform.position;

        Vector3 randomPos = Random.insideUnitSphere / 1.2f;

        for (int i = 0; i < 60; i++) 
        {
            try 
            {
                pos = targetObj.transform.position;

                textRef.transform.position = Camera.main.WorldToScreenPoint(pos + offset + randomPos);
            } 
            catch
            {
                if (textRef == null) yield break;
                textRef.transform.position = Camera.main.WorldToScreenPoint(pos + offset + randomPos);
            }
            yield return new WaitForFixedUpdate();
        }
    }
}
