using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakObj : MonoBehaviour
{
    public static void BreakObjectIntoPieces(GameObject obj, int numPieces, float force, float torque, float fadeTime)
    {
        // Get the size of the object

        Vector3 size = obj.transform.localScale;
        Vector2 direction = Vector2.left;

        if (obj.transform.localScale.x < 0)
        {
            size = new Vector3(-obj.transform.localScale.x, obj.transform.localScale.y, obj.transform.localScale.z);
            direction = Vector2.right;
        }

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
            sr.sortingOrder = -1;
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
