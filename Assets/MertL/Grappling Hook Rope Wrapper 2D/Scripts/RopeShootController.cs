using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace RopeWrapper
{
    /// <summary>
    /// Shoots/Releases & Anchors the rope;
    /// Rope is shot by left mouse click towards the mouse position,
    /// Rope is relesead by right mouse click,
    /// Anchoring is done according to triggering of collider.
    /// 
    /// !!! WARNING: Rope anchor point is stationary. It will not move if the anchored object moves !!!
    /// </summary>

    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(RopeWrapController))]

    public class RopeShootController : MonoBehaviour
    {
        public PlayerBehavior target;

        public Transform player;

        public float ropeShootVelocity;
        private bool isRopeShot;

        private float pushOutIncrement;

        private void Start()
        {
            target = player.GetComponent<PlayerBehavior>();

            GetComponent<Collider2D>().enabled = false;
            pushOutIncrement = GetComponent<LineRenderer>().startWidth / 100f;
            GetComponent<Rigidbody2D>().gravityScale = 0f;
        }

        //Shoot or Release the rope according to mouse input
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space) && !isRopeShot && target.positionUpDown == false && !(target.isJump) && !(target.isDamage))
                ShootTheRope();
            else if (target.positionUpDown && isRopeShot)
                ReleaseTheRope();
        }

        //When left mouse button is clicked, shoot the rope from player's current position towards position of mouse pointer with a preassigned magnitude of velocity
        //Linerenderer is assigned two vertex points so that it can be drawn on screen.
        private void ShootTheRope()
        {
            target.isJump = true;

            isRopeShot = true;
            GetComponent<Collider2D>().enabled = true;
            transform.position = player.position;

            Vector2 shootVector = Vector2.up; // 플레이어의 윗 방향 벡터로 설정
            GetComponent<Rigidbody2D>().velocity = ropeShootVelocity * shootVector;

            GetComponent<RopeWrapController>().RopeIsJustShot();
        }

        //When right mouse button is clicked, cancel the rope by zeroing velocity and disabling collider to avoid any problems while rope is not shot
        //Linerenderer vertex count should be cleared so that rope is not drawn on screen anymore
        private void ReleaseTheRope()
        {
            isRopeShot = false;
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            GetComponent<Collider2D>().enabled = false;
            GetComponent<RopeWrapController>().RopeIsReleased();
        }

        //Anchor the rope if it touches an obstacle
        // !!! REMINDER: Rope anchor point is stationary. It will not move if the anchored object moves !!!
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.name != player.name) //Rope cannot anchor on player itself
            {
                target.isRope = true;
               
                GetComponent<Rigidbody2D>().velocity = Vector2.zero;                //Anchor the rope by stopping the rope movement. 
                GetComponent<Collider2D>().enabled = false;             //Disable the collider to avoid any problems
                AnchorTheRope(collision);
                GetComponent<RopeWrapController>().RopeIsAnchored(transform.position);
            }
        }

        private void AnchorTheRope(Collider2D anchoredCollider)
        {
            
            if (anchoredCollider.GetType() != typeof(BoxCollider2D) || anchoredCollider.GetComponent<BoxCollider2D>() == null)
                Debug.LogError("Objects the rope anchors and wraps should have PolygonCollider2D" + "\nObjects to be wrapped should also have Rigidbody2D attached as component!");
            else
                PushAnchorOutOfColliderBoundaries((BoxCollider2D)anchoredCollider);
        }

        //To avoid complications with raycasts, the anchor should be set outside of the collider boundaries.
        //So we push the anchor out of the anchored colliders boundaries and towards the player.
        private void PushAnchorOutOfColliderBoundaries(BoxCollider2D collider)
        {
            //Check if anchor is in collider boundaries
            if (collider.OverlapPoint(transform.position))
            {
                Vector2 pushVector = (Vector2)player.transform.position - (Vector2)transform.position;
                Vector3 pushVectorClamped = Vector2.ClampMagnitude(pushVector, pushOutIncrement);   //Push towards the player with pushStepAmount
                transform.position += pushVectorClamped;

                PushAnchorOutOfColliderBoundaries(collider);    //Make recursive call until the anchor is out of collider bounds
            }
        }
    }
}