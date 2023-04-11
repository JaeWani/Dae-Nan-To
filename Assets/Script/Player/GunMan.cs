using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GunMan : Player
{
    [SerializeField] GameObject bullet;

    SpriteRenderer spriteRenderer;
    private void Start() {
        AttackAction = _Attack;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private string Tag(){
        string tag = null;
        switch(CurrentPlayer){
            case PlayerState.P1:
            tag = "1P_Wepon";
            break;

            case PlayerState.P2:
            tag = "2P_Wepon";
            break;
        }
        return tag;
    }
    private void _Attack(){
        var b =  Instantiate(bullet,transform.position,Quaternion.identity).GetComponent<Bullet>();
        b.gameObject.tag = Tag();
        b.Damage = AttackDamage;
        b.Direction = Direction;
        var spr = b.GetComponent<SpriteRenderer>();
        spr.color = spriteRenderer.color;
    }
}
