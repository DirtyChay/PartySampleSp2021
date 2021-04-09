using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttack : MonoBehaviour
{
    public bool isAttacking;
    public int damage;
    public int cost; // how many paints it'll cost. Not checked for in this class.
    public int attackRadius; // inclusive. How much space the ranged attack covers.
    public int attackDelay;
    public int attackRange; // inclusive. How far away the player is allowed to attack.
    public float cooldownLength;
    public float cd = 0;
    public KeyCode attackButton = KeyCode.Mouse0;
    public GameObject player;

    void Update()
    {
        /* when the player presses the ranged attack button */
        if (Input.GetKeyDown(attackButton))
        {
            Vector2 markerCoordinates = getMouseCoordinates();
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


                cd = cooldownLength;
            }
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
    }

    /**
     * Checks to see if the player's target is in range.
     *
     * @source https://www.codegrepper.com/code-examples/csharp/
     * unity+how+to+check+if+a+game+object+if+with+in+a+radius
     */
    private bool targetIsInRange(Vector2 targetCoords)
    {
        GameObject parent = transform.parent.gameObject;
        Vector2 parentCoords = parent.transform.position;
        float distance = Vector2.Distance(parentCoords, targetCoords);
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