using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class DamageableCharacter : MonoBehaviour, Damageable
{
    public bool disableSimulation = false;

    //immune time between hit
    public bool Invincible { 
        get {
            return isInvincible;
        }
        set {
            isInvincible = value;
            if (isInvincible) {
                invincibleTimeElapsed = 0f;
            }
        }
    }
    public bool allowInvincible = false;
    bool isInvincible = false;
    public float invincibleDuration = 0.25f;
    float invincibleTimeElapsed = 0f;
    
    public Vector3 textOffset = new Vector3(-60, 0, 0);
    Animator animator;
    Rigidbody2D rb;
    Collider2D physicsCollider;
    bool isAlive = true;

    //health texts
    public GameObject healthText;
    public float Health {
        set {
            if (value < health) {
                animator.SetTrigger("hit");
                RectTransform text = Instantiate(healthText).GetComponent<RectTransform>();
                text.transform.position = Camera.main.WorldToScreenPoint(gameObject.transform.position) + textOffset;
                Canvas canvas = GameObject.FindObjectOfType<Canvas>();
                text.SetParent(canvas.transform);
            }
            health = value;
            if (health <= 0) {
                animator.SetBool("isAlive", false);
                Targetable = false;
            }
        }
        get {
            return health;
        }
    }
    public float health = 3f;

    // check if targetable after dead
    public bool Targetable {
        set {
            targetable = value;
            if (disableSimulation) {
                rb.simulated = false;
            }
            physicsCollider.enabled = value;
        }
        get {
            return targetable;
        }
    }
    public bool targetable = true;

    public void Start() {
        animator = GetComponent<Animator>();

        // Making sure it's alive
        animator.SetBool("isAlive", isAlive);

        rb = GetComponent<Rigidbody2D>();
        physicsCollider = GetComponent<Collider2D>();
    }

    // Take damage on hit
    public void OnHit(float damage) {
        if (!Invincible) {
            Health -= damage;

            if (allowInvincible) {

                //activate invincibility and timer
                Invincible = true;
            }
        }
        
    }

    public void OnHit(float damage, Vector2 knockback) {
        if (!Invincible) {
            Health -= damage;

            // Apply force
            rb.AddForce(knockback);

            if (allowInvincible) {

                //activate invincibility and timer
                Invincible = true;
            }
        }
    }

    public void OnObjectDestroy() {
        Destroy(gameObject);
    }

    public void FixedUpdate() {
        if (Invincible) {
            invincibleTimeElapsed += Time.deltaTime;
            if (invincibleTimeElapsed > invincibleDuration) {
                Invincible = false;
            }
        }
    }

}
