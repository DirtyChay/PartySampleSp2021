﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttack : MonoBehaviour
{
    public GameObject rangedAttackerPrefab; // unit that spawns and attacks
    public bool isAttacking;
    public int damage;
    public int paintCost; // not checked for
    public int attackRadius; // Inclusive radius of attack.
    public int attackDelay;
    public int attackRange; // Inclusive max distance for a valid attack.
    public float cooldownLength;
    public float cd = 0;
    public KeyCode attackButton = KeyCode.Mouse0;
    private Vector2 markerCoordinates; // holds the coordinates to strike
    private Vector2 screenBounds; // so that we can spawn this attacker to a side of the screen

    private void Start()
    {
        screenBounds =
            Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
    }

    void Update()
    {
        /* when the player presses the ranged attack button */
        if (Input.GetKeyDown(attackButton))
        {
            isAttacking = false;
            markerCoordinates = getMouseCoordinates();
            if (!targetIsInRange(markerCoordinates))
            {
                Debug.Log("Target is out of range.");
            }
            else if (cd > 0)
            {
                Debug.Log("Ranged attack is on cooldown.");
            }
            else
            {
                Debug.Log("Attack start.");
                isAttacking = true;
                cd = cooldownLength;
            }
        }

        if (isAttacking)
        {
            spawnAttacker();
            StartCoroutine(delayedAttackRoutine());
            isAttacking = false;
        }

        if (cd > 0)
        {
            cd -= Time.deltaTime;
        }
    }

    /**
     * After the delay, spawn a small regional attack on the location.
     */
    IEnumerator delayedAttackRoutine()
    {
        yield return new WaitForSeconds(attackDelay);
        // spawn attack on the location
    }

    /**
     * Spawns a unit to the right of the screen.
     * Possible? Try to modify the prefab's lifespan to whatever we have set here.
     * 
     * @source https://youtu.be/E7gmylDS1C4?t=434
     */
    private void spawnAttacker()
    {
        GameObject a = Instantiate(rangedAttackerPrefab) as GameObject;
        a.transform.position = new Vector2(screenBounds.x - 1, screenBounds.y - 1);
    }

    /**
     * Checks to see if the player's target is in range.
     *
     * @source https://www.codegrepper.com/code-examples/csharp/
     * unity+how+to+check+if+a+game+object+if+with+in+a+radius
     */
    private bool targetIsInRange(Vector2 targetCoords)
    {
        Vector2 playerCoords = transform.position;
        float distance = Vector2.Distance(playerCoords, targetCoords);
        return (distance <= attackRange);
    }

    /**
     * Returns the position in the world where the player's mouse is hovering over.
     * 
     * @source https://stackoverflow.com/questions/46998241/getting-mouse-position-in-unity
     */
    private Vector2 getMouseCoordinates()
    {
        Vector3 mousePos = Input.mousePosition; // mouse position in pixels
        Vector2 screenPos = new Vector2(mousePos.x, mousePos.y);
        Vector2 worldPos = Camera.main.ScreenToWorldPoint(screenPos);
        return worldPos;
    }
}