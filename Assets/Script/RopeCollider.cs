using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RopeCollider : MonoBehaviour
{
 
    private void OnTriggerExit2D(Collider2D other)
    {
        PlayerBehavior playerMovement = other.gameObject.GetComponent<PlayerBehavior>();

        if (playerMovement != null)
        {
            playerMovement.isRope = false;
        }
    }
}
