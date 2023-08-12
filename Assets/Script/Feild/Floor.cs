using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 충돌한 오브젝트의 스크립트 정보 가져오기
        PlayerBehavior playerMovement = collision.gameObject.GetComponent<PlayerBehavior>();

        if (playerMovement != null)
        {
            // isJump 상태를 false로 변경
            playerMovement.isJump = false; 
        }


        Boss1 boss = collision.gameObject.GetComponent<Boss1>();
        if (boss != null)
        {
            // isJump 상태를 false로 변경
            boss.isJump = false;
        }




        JumpMonster jumpMonster = collision.gameObject.GetComponent<JumpMonster>();
        if (jumpMonster != null)
        {
            jumpMonster.scan = false;
        }

    }
}
