using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Wepon
{
    
    public Vector2 Direction;
    Rigidbody2D Rigid2D;
    void Start(){
        Rigid2D = GetComponent<Rigidbody2D>();
    }

    void Update(){
        Move();
    }
    protected virtual void Move(){
        Rigid2D.velocity = Direction * MoveSpeed;
    }
}
