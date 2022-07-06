using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField]
    private Sprite[] sprites;
    private SpriteRenderer sr;
    private Rigidbody2D rb;

    [SerializeField]
    private float size = 1f;
    [SerializeField]
    private float minSize = 0.5f;
    [SerializeField]
    private float maxSize = 1.5f;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        sr.sprite = sprites[Random.Range(0, sprites.Length)];

        transform.eulerAngles = new Vector3(0f, 0f, Random.value * 360);
        transform.localScale = Vector3.one * size;

        rb.mass = size;
    }
}
