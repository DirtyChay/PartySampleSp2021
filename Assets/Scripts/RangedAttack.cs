using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttack : MonoBehaviour
{
    public bool isAttacking;
    public int attackDelay;
    public int attackRadius; // inclusive radius
    public int cooldownLength;
    public int cd = 0;
    public KeyCode attackButton = KeyCode.Mouse0;

    void Update()
    {
        /* when the player presses the ranged attack button */
        if (Input.GetKeyDown(attackButton))
        {
            if (cd.Equals(0))
            {
                Vector2 markerCoordinates = getMouseCoordinates();
                if (targetIsInRange(markerCoordinates))
                {
                }
                else
                {
                    Debug.Log("Target is out of range.");
                }
            }
            else
            {
                Debug.Log("Ranged attack is on cooldown.");
            }
        }
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
        return (distance <= attackRadius);
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