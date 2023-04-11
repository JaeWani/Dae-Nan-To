using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Player : MonoBehaviour
{
    [SerializeField]Rigidbody2D Rigid2D;


    public enum PlayerState{
        P1,
        P2        
    }
    [Header ("플레이어 번호")]
    [SerializeField]protected PlayerState CurrentPlayer;

    [Header ("조작 키")]
    [SerializeField]protected KeyCode LeftKey;
    [SerializeField]protected KeyCode RightKey;
    [SerializeField]protected KeyCode JumpKey;
    [SerializeField]protected KeyCode AttackKey;

    [Header ("플레이어 스탯")]
    public int MaxHP;
    public int HP;
    public int MoveSpeed;
    public int JumpPower;
    public int AttackDamage;
    public float AttackDelay;
    private float curDealy;
    protected Action AttackAction;
    protected Vector2 InputVector = new Vector2(0,0);
    [Header ("플레이어 상태")]
    public Vector2 Direction = Vector2.right;
    public bool CanAttack = true;
    void Start(){
       HP = MaxHP; 
       Rigid2D = GetComponent<Rigidbody2D>();
    }

    void Update(){
        _Dead();
        Move();
        Jump();
        Attack();
        _AttackDelay();
    }
    private void _Dead(){
        if(HP <= 0){
            Destroy(gameObject);
        }        
    }
    private void _AttackDelay(){
        if(curDealy <= AttackDelay && CanAttack == false){
            curDealy += Time.deltaTime;
            return;
        }
        else
            CanAttack = true;
    }
    protected virtual void Attack(){
        if(CanAttack == true){
            if(Input.GetKeyDown(AttackKey)){
                AttackAction();
                CanAttack = false;
                curDealy = 0;
            }
        }
    }
    protected virtual void Jump(){
        if(Input.GetKeyDown(JumpKey)){
            Rigid2D.AddForce(Vector3.up * JumpPower,ForceMode2D.Impulse);
        }
    }
    protected virtual void Move(){
        float veloX = HorizontalAxis("Horizontal") * MoveSpeed;
        InputVector = new Vector2(veloX,Rigid2D.velocity.y);
        Rigid2D.velocity = InputVector;
    }
    private int HorizontalAxis(string name){
        if (name != "Horizontal") return 0;
        if (Input.GetKey(LeftKey)) {
            Direction = Vector2.left;
            return -1;}
        if (Input.GetKey(RightKey)) {
            Direction = Vector2.right;
            return 1;}
        return 0;
    }

    public virtual void Hit(int Dmg){
        HP -= Dmg;
    }

    private void OnTriggerEnter2D(Collider2D other) {
         if(gameObject.CompareTag("1P")){
            if(other.gameObject.CompareTag("2P_Wepon")){
                var Dmg = other.gameObject.GetComponent<Wepon>().Damage; 
                Debug.Log(Dmg);
                Hit(Dmg);
            }
        }
        if(gameObject.CompareTag("2P")){
            if(other.gameObject.CompareTag("1P_Wepon")){
                var Dmg = other.gameObject.GetComponent<Wepon>().Damage; 
                Debug.Log(Dmg);
                Hit(Dmg);
            }
        }    
    }
}
