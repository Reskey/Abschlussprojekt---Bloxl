using Cinemachine.Utility;
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

    internal void HitPopUp(float dmg, GameObject obj, Vector2 direction)
    {
        GameObject hitpopupPrefab = hitpopup;

        GameObject c = MonoBehaviour.Instantiate(hitpopupPrefab, obj.transform.position, Quaternion.identity);

        TMP_Text text = c.GetComponentInChildren<TMP_Text>();
        text.text = "-" + dmg.ToString();

        if (obj.tag == "Player")
        {
            if (dmg < 0)
            {
                text.color = Color.green;
                text.text = "+" + Mathf.Abs(dmg).ToString();
            }
            else
            {
                text.color = Color.red;
                text.text = "-" + dmg.ToString();
            }
        }

        StartCoroutine(HitpopupPosUpdater(text, obj, direction));

        MonoBehaviour.Destroy(c, 1);
    }

    private IEnumerator HitpopupPosUpdater(TMP_Text textRef, GameObject targetObj, Vector2 direction)
    {
        Vector3 randomPos = Random.insideUnitSphere / 1.2f;

        Vector3 directionalOffset = new Vector3(offset.x * direction.x, offset.y, offset.z);

        while (textRef && targetObj) 
        {
            Vector3 pos = targetObj.transform.position;

            textRef.transform.position = Camera.main.WorldToScreenPoint(pos + directionalOffset + randomPos);

            yield return new WaitForFixedUpdate();
        }
        Destroy(textRef);
    }

    public void BreakObjectIntoPieces(GameObject obj, int numPieces, float force, float torque, float fadeTime, Vector2 direction)
    {
        // Get the size of the object
        Vector3 size = obj.transform.transform.localScale.Abs();

        // Get the sprite of the object
        SpriteRenderer spriteRenderer = obj.GetComponent<SpriteRenderer>();

        Sprite sprite = spriteRenderer.sprite;
        Texture2D texture = sprite.texture;
        Rect textureRect = sprite.textureRect;
        Color[] pixels = texture.GetPixels((int)textureRect.x, (int)textureRect.y, (int)textureRect.width, (int)textureRect.height);

        // Loop through the number of pieces and create a sprite for each piece
        for (int i = 0; i < numPieces; i++)
        {
            // Create a new texture for the piece
            Texture2D pieceTexture = new Texture2D((int)textureRect.width, (int)textureRect.height);
            pieceTexture.SetPixels(pixels);
            pieceTexture.Apply();

            // Crop the texture to the size of the piece
            Rect pieceRect = new Rect(Random.Range(0, textureRect.width - size.x), Random.Range(0, textureRect.height - size.y), size.x, size.y);
            Color[] piecePixels = pieceTexture.GetPixels((int)pieceRect.x, (int)pieceRect.y, (int)pieceRect.width, (int)pieceRect.height);
            pieceTexture = new Texture2D((int)pieceRect.width, (int)pieceRect.height);
            pieceTexture.SetPixels(piecePixels);
            pieceTexture.Apply();

            // Create a sprite from the texture
            Sprite pieceSprite = Sprite.Create(pieceTexture, new Rect(0, 0, pieceTexture.width / 4f, pieceTexture.height / 4f), new Vector2(0.5f, 0.5f));

            // Create a new GameObject for the piece
            GameObject pieceObj = new GameObject(obj.name + "_Piece_" + i.ToString(), typeof(SpriteRenderer), typeof(Rigidbody2D));
            pieceObj.transform.position = obj.transform.position + Random.insideUnitSphere;
            pieceObj.transform.localScale = obj.transform.localScale;

            pieceObj.tag = "BrokenPiece";
            pieceObj.layer = 7; //Piece Layer

            // Set the sprite of the piece
            SpriteRenderer sr = pieceObj.GetComponent<SpriteRenderer>();
            sr.sprite = pieceSprite;
            sr.color = obj.GetComponent<SpriteRenderer>().color;
            sr.sortingOrder = -2;
            sr.sprite.texture.filterMode = FilterMode.Point;

            // Add a collider to the piece
            BoxCollider2D collider = pieceObj.AddComponent<BoxCollider2D>();
            collider.size = new Vector2(0.014f, 0.014f);

            //Force direction


            // Add a rigidbody to the piece
            Rigidbody2D rb = pieceObj.GetComponent<Rigidbody2D>();
            rb.gravityScale = 1f;
            rb.AddForce(direction * force);
            rb.AddTorque(Random.Range(-torque, torque));
            rb.velocity = Random.insideUnitCircle + direction * (force / 1.6f);

            // Add a script to fade out the piece
            DestroyAfterTime destroyScript = pieceObj.AddComponent<DestroyAfterTime>();
            destroyScript.fadeTime = fadeTime * Random.RandomRange(1, 1.6f);

        }

        Destroy(obj);
    }
}
