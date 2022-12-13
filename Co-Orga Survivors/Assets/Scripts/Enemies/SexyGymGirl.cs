using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.Video;

public class SexyGymGirl : BaseUnit
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Contains("Sexy"))
            return;
        // var enemy =  collision.gameObject. as BaseUnit;
        var a = this.GetComponent<AudioSource>();
        a.Play();
    } 
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var body = this.GetComponent<Rigidbody2D>();
        
        var player          = GameObject.Find(nameof(Player))?.GetComponent<Player>();
        var targetPosition  = player.transform.position;
        var directionToMove = targetPosition - transform.position;
        directionToMove = directionToMove.normalized * Time.deltaTime * moveSpeed;

        //Move to sexy nyt
        float maxDistance                            = Vector3.Distance(transform.position, targetPosition);
        body.transform.position = transform.position = transform.position + Vector3.ClampMagnitude(directionToMove, maxDistance);
        
        // transform.position      = new Vector3(transform.position.x,player.transform.position.y,transform.position.z);
        // body.transform.position = transform.position += Vector3.right * moveSpeed * Time.deltaTime;
    }
}
