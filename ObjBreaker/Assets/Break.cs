using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Break : MonoBehaviour
{

    public int numPieces; // Number of pieces to break the object into
    public float force; // Force with which the pieces will be scattered
    public float torque; // Amount of torque to apply to each piece
    public float fadeTime; // Time it takes for the pieces to fade out

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collision object has a "Breakable" tag
        if (collision.gameObject.CompareTag("Breakable"))
        {
            // Get the collision point and normal
            ContactPoint2D contact = collision.contacts[0];

            // Get the size of the object
            Vector3 size = transform.localScale;

            // Get the sprite of the object
            Sprite sprite = GetComponent<SpriteRenderer>().sprite;
            Texture2D texture = sprite.texture;
            Rect textureRect = sprite.textureRect;
            Color[] pixels = texture.GetPixels((int)textureRect.x, (int)textureRect.y, (int)textureRect.width, (int)textureRect.height);

            // Loop through the number of pieces and create a sprite for each piece
            for (int i = 0; i < numPieces; i++)
            {
                Vector3 position = new Vector3(Random.Range(-size.x / 2f, size.x / 2f), Random.Range(-size.y / 2f, size.y / 2f), 0f);
                GameObject pieceObj = new GameObject("Piece " + i.ToString(), typeof(SpriteRenderer), typeof(Rigidbody2D), typeof(PieceFader));
                pieceObj.transform.position = transform.position + position;
                pieceObj.transform.localScale = transform.localScale / Mathf.Sqrt(numPieces);

                // Set the sprite of the piece to a random sprite from the original object
                SpriteRenderer pieceRenderer = pieceObj.GetComponent<SpriteRenderer>();
                pieceRenderer.sprite = sprite;
                pieceRenderer.color = spriteRenderer.color;

                // Add a force to the piece in the direction of the collision normal
                Rigidbody2D rb = pieceObj.GetComponent<Rigidbody2D>();
 
                rb.AddForce(contact.normal * force);

                // Set the fade time of the piece fader
                 pieceFader = pieceObj.GetComponent<PieceFader>();
                pieceFader.fadeTime = fadeTime;
            }

            Destroy(gameObject);

        }
    }
}
