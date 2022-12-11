using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Langhantel : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var audios     = this.GetComponents<AudioSource>();
        var soundIndex = Random.Range(0, audios.Length);
        audios[soundIndex].Play();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update() 
    {
        var body    = GetComponent<Rigidbody2D>();
        
        var gymGirl = GameObject.Find("SexyGymGirl");
        var enemy   = gymGirl?.GetComponent<Rigidbody2D>() ;

        if (enemy == null)
            return;
        
        //Set this value in the inspector
        Vector3 targetPosition = enemy.transform.position;

        float moveSpeed = 4.78f;

        //This code is called once every frame
        //Lets start by finding the direction between our object and the target position
        //You can find the direction from point A to point B via Vector subtraction
        //Calling transform by itself grabs the transform that is attached to the game object that this script is attached to
        Vector3 directionToMove = targetPosition - transform.position;

        //Now we have the direction, but we need to calculate the distance to move. 
        //We will scale our direction vector to the wanted magnitude        
        directionToMove = directionToMove.normalized * Time.deltaTime * moveSpeed;
        //A normalized vector is a vector with length 1
        //Time.deltaTime is the time since Update() was last called. This is used so that we get a constant speed, regardless of frame rate
        //Finish by scaling by our desired movement speed

        //Now we add our direction to our current position. We are going to clamp the vector here to make sure we don't go past our target destination
        float maxDistance                            = Vector3.Distance(transform.position, targetPosition);
        body.transform.position = transform.position = transform.position + Vector3.ClampMagnitude(directionToMove, maxDistance);
    }
}
