using System;
using ScriptableObjects;
using UnityEngine;
using Random = System.Random;

public class Player : BaseUnit
{
    public  GameObject   weapon;
    public  BaseSurvivor survivor;
    private float        currentExp;
    private float        expNeededForLevelup;
    private float        AxisSpeed => (float)Math.Sqrt(Math.Pow(moveSpeed, 2) / 2);

    private void Start()
    {
        currentExp          = 0;
        expNeededForLevelup = level * 10;

        var spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = survivor.sprite;
    }

    private const float weaponCooldown   = .38f;
    private       float nextWeaponAttack = weaponCooldown;
    
    private void Update()
    {
        nextWeaponAttack -= Time.deltaTime;

        if (nextWeaponAttack <= 0)
        {
            ThrowBarebell();
            nextWeaponAttack = weaponCooldown;
        }
        
        var rigidBodyComponent = GetComponent<Rigidbody2D>();

        if ((Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.W)) || (Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.UpArrow)))
            rigidBodyComponent.transform.position = GetDiagonalTransformPosition(Vector3.right, Vector3.up);
        else if ((Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.W)) || (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.UpArrow)))
            rigidBodyComponent.transform.position = GetDiagonalTransformPosition(Vector3.left, Vector3.up);
        else if ((Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.S)) || (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.DownArrow)))
            rigidBodyComponent.transform.position = GetDiagonalTransformPosition(Vector3.left, Vector3.down);
        else if ((Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D)) || (Input.GetKey(KeyCode.DownArrow) && Input.GetKey(KeyCode.RightArrow)))
            rigidBodyComponent.transform.position = GetDiagonalTransformPosition(Vector3.right, Vector3.down);
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            rigidBodyComponent.transform.position = transform.position += Vector3.right * moveSpeed * Time.deltaTime;
        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            rigidBodyComponent.transform.position = transform.position += Vector3.left * moveSpeed * Time.deltaTime;
        else if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            rigidBodyComponent.transform.position = transform.position += Vector3.up * moveSpeed * Time.deltaTime;
        else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            rigidBodyComponent.transform.position = transform.position += Vector3.down * moveSpeed * Time.deltaTime;
    }

    private void ThrowBarebell()
    {
        var barbellObject = Instantiate(weapon,this.transform.position, Quaternion.identity, GameObject.FindGameObjectWithTag("Canvas").transform);
        var script = barbellObject.GetComponent<Langhantel>();
        script.Player = this;
    }

    private void OnCollisionEnter2D(Collision2D col) => Debug.Log($"Collision with {col.gameObject.name}");

    private Vector3 GetDiagonalTransformPosition(Vector3 vector1, Vector3 vector2)
    {
        var newTransformPosition = transform.position += vector1 * AxisSpeed * Time.deltaTime;
        newTransformPosition += vector2 * AxisSpeed * Time.deltaTime;
        return newTransformPosition;
    }
}