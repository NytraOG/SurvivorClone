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
        // var body = this.GetComponent<Rigidbody2D>();
        // var player                      = GameObject.Find(nameof(Player))?.GetComponent<Player>();
        
        //Move to sexy nyt, aber nicht heute
        // transform.position      = new Vector3(transform.position.x,player.transform.position.y,transform.position.z);
        // body.transform.position = transform.position += Vector3.right * moveSpeed * Time.deltaTime;
    }
}
