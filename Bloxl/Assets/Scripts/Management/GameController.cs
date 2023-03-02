using Assets.Skripts.Player;
using Cinemachine.Utility;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using TMPro;
using UnityEngine;

namespace Assets.Skripts.Management
{
    public class GameController : MonoBehaviour
    {
        [Header("Hitpopup"), Space(10), SerializeField] GameObject hitpopup;
        [SerializeField] Vector3 offset;

        public static GameObject HealItem;
        [SerializeField] private GameObject healItem;

        [HideInInspector] public Inputs inputControlls;

        private static GameController instance;

        public static Vector2 PlayerDirection => FindObjectOfType<PlayerController>().transform.localScale.x switch
        {
            > 0 => Vector2.right,
            < 0 => Vector2.left,
            _ => Vector2.zero,
        };

        private void Awake()
        {
            inputControlls = new Inputs();

            instance = GetComponent<GameController>();

            HealItem = instance.healItem;

            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Piece"));
            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Enemies"), LayerMask.NameToLayer("Piece"));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void FlipSprite(GameObject x)
        {
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

        public static void SplitSprite(GameObject obj, int numPieces, Vector2 direction)
        {
            float force = 5.8f;
            float torque = 0f;

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
                GameObject pieceObj = new GameObject(obj.name + "Piece" + i.ToString(), typeof(SpriteRenderer), typeof(Rigidbody2D));
                pieceObj.transform.position = obj.transform.position + Random.insideUnitSphere;
                pieceObj.transform.localScale = obj.transform.localScale;

                pieceObj.layer = LayerMask.NameToLayer("Piece");

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
                //StartCoroutine(FadeBehaviour(pieceObj, Random.RandomRange(1, 1.6f) * 6f));
                instance.StartCoroutine(FadeBehaviour(pieceObj, Random.Range(1f, 1.6f) * 6f));
            }

        }
        private static IEnumerator FadeBehaviour(GameObject x, float timeOut)
        {
            float startTime = Time.time;

            SpriteRenderer sp = x.GetComponent<SpriteRenderer>();

            while (Time.time - startTime < timeOut)
            {
                float alpha = Mathf.Lerp(1f, 0f, (Time.time - startTime) / timeOut);

                sp.color = new Color(sp.color.r, sp.color.g, sp.color.b, alpha);
                yield return null;
            }

            Destroy(x);
        }   
    }
}
