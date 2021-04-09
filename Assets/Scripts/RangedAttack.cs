using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttack : MonoBehaviour
{
    public bool isAttacking;
    public int attackDelay;
    public int attackRadius;
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
                Vector3 markerCoordinates = getMouseCoordinates();
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
                Debug.Log("Ranged attack is on cooldown!");
            }
        }
    }

    /**
     * Checks to see if the player's target is in range.
     *
     * @source https://www.codegrepper.com/code-examples/csharp/
     * unity+how+to+check+if+a+game+object+if+with+in+a+radius
     */
    private bool targetIsInRange(Vector3 targetCoords)
    {
        GameObject parentUnit = transform.parent.gameObject;
        Vector3 parentCoords = parentUnit.transform.position;
        float distance = Vector3.Distance(parentCoords, targetCoords);
        return distance <= attackRadius;
    }

    /**
     * Returns the position in the world where the player's mouse is hovering over.
     * 
     * @source https://gamedevbeginner.com/
     * how-to-convert-the-mouse-position-to-world-space-in-unity-2d-3d/#screen_to_world_2d
     */
    private Vector3 getMouseCoordinates()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane;
        return Camera.main.ScreenToWorldPoint(mousePos);
    }
}