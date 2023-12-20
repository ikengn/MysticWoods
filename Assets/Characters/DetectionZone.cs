using System;
using System.Collections.Generic;
using UnityEngine;

public class DetectionZone : MonoBehaviour
{
    public String target = "Player";
    public Collider2D col;
    public List<Collider2D> detecteds = new List<Collider2D>();

    //detect when objects enter range
    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.gameObject.tag == target){
            detecteds.Add(collider);
        }
    }

    void OnTriggerExit2D(Collider2D collider) {
        if (collider.gameObject.tag == target){
            detecteds.Remove(collider);
        }
    }
}
