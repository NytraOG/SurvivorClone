using System.Collections;
using System.Collections.Generic;
using ScriptableObjects;
using UnityEngine;

public class Player : MonoBehaviour
{
    public BaseSurvivor survivor;

    // Start is called before the first frame update
    void Start()
    {
        var spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = survivor.sprite;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
