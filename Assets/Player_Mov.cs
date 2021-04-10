using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Mov : MonoBehaviour
{
    [SerializeField] private float MoveSpeed;
    [SerializeField] private float WalkSpeed;
    [SerializeField] private float RunSpeed;


    private Vector3 MoveDirection;
    private Vector3 Velocity;


    [SerializeField] private bool IsGrounded;
    [SerializeField] private float GroundCheckDistance;
    [SerializeField] private LayerMask GroundMask;
    [SerializeField] private float Gravity;
    [SerializeField] private float JumpHeight;



    //References

    private Animator Anim;



    private CharacterController controller;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        Anim = GetComponentInChildren<Animator>();

    }
    private void Update()
    {


        Move();

        if(Input.GetKey(KeyCode.Mouse0))
        {
            Attack();
        }
    }

    private void Move()
    {

        IsGrounded = Physics.CheckSphere(transform.position, GroundCheckDistance, GroundMask);
        if(IsGrounded && Velocity.y <0)
        {
            Velocity.y = -2f;
        }





      
          float MoveZ = Input.GetAxis("Vertical");
         
        MoveDirection = new Vector3(  MoveZ,0,0);
        
        MoveDirection = transform.TransformDirection(MoveDirection);

        if (IsGrounded)
        {

            if (MoveDirection != Vector3.zero && !Input.GetKey(KeyCode.LeftShift))
            {

                Walk();

            }
            else if (MoveDirection != Vector3.zero && Input.GetKey(KeyCode.LeftShift))
            {
                Run();

            }

            else if (MoveDirection == Vector3.zero)
            {
                idle();
            }


            MoveDirection *= MoveSpeed;

            if(Input.GetKey(KeyCode.Space))
            {
                Jump();
                Anim.SetFloat("Speed", 0.88f, 0.1f, Time.deltaTime);
            }

           
        }
        controller.Move(MoveDirection * Time.deltaTime);


        Velocity.y += Gravity * Time.deltaTime;
        controller.Move(Velocity * Time.deltaTime);
    }


    private void Walk()
    {

        MoveSpeed = WalkSpeed;
        Anim.SetFloat("Speed", 0.2f, 0.1f, Time.deltaTime);
    }

    private void Run()
    {


        MoveSpeed = RunSpeed;
        Anim.SetFloat("Speed", 0.45f,0.1f,Time.deltaTime);
    }

    private void idle()
    {
        Anim.SetFloat("Speed", 0.05f, 0.1f, Time.deltaTime);

    }
    private void Jump()
    {

        Velocity.y = Mathf.Sqrt(JumpHeight * -2 * Gravity);
        Anim.SetFloat("Speed", 0.88f, 0.1f, Time.deltaTime);
    }

    private void Attack()
    {
        Anim.SetTrigger("Attack");
    }
   
}
