using System;
using System.Collections.Generic;
using UnityEngine;

namespace UnityChan
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(CapsuleCollider))]
    [RequireComponent(typeof(Rigidbody))]
    public class UnityChanControlScript : MonoBehaviour
    {
        #region Fields
        public bool IsDead { get; set; }

        //        
        public float lookSmoother = 3.0f;
        public bool useCurves = true;
        public float useCurvesHeight = 0.5f;

        //
        private CapsuleCollider col;
        private Rigidbody rb;
        private Vector3 velocity;
        private float orgColHight;
        private Vector3 orgVectColCenter;
        private Animator anim;
        private AnimatorStateInfo currentBaseState;

        //
        private float horizontalInput;
        private float verticalInput = 1;
        public float leftRightSpeed = 7.0f;
        public float rotateSpeed = 2.0f;
        public float jumpPower = 5.0f;
        public float animSpeed = 1.5f;

        //
        private int idleState = Animator.StringToHash("Base Layer.Idle");
        private int locoState = Animator.StringToHash("Base Layer.Locomotion");
        private int jumpState = Animator.StringToHash("Base Layer.Jump");
        private int restState = Animator.StringToHash("Base Layer.Rest");
        private int slideState = Animator.StringToHash("Base Layer.Slide");
        private int attackState = Animator.StringToHash("Base Layer.Attack");

        Dictionary<int, Action> animationStates;

        #endregion

        private void Start()
        {
            anim = GetComponent<Animator>();
            col = GetComponent<CapsuleCollider>();
            rb = GetComponent<Rigidbody>();

            orgColHight = col.height;
            orgVectColCenter = col.center;

            // populate dictionary
            animationStates = new Dictionary<int, Action>();
            animationStates.Add(idleState, () => WhenIdle());
            animationStates.Add(locoState, () => WhenRunning());
            animationStates.Add(jumpState, () => WhenJumping());
            animationStates.Add(restState, () => WhenResting());
            animationStates.Add(slideState, () => WhenSliding());
            animationStates.Add(attackState, () => WhenAttacking());
        }

        private void Update()
        {
            SetAnimations();
            JumpControls();
            SlidingControls();
            AttackControls();
        }

        private void FixedUpdate()
        {
            SidewaysMovement();

            // perform action depending on current state
            currentBaseState = anim.GetCurrentAnimatorStateInfo(0);
            ;
            ActionsDependingOnAnimationState(currentBaseState.nameHash);
        }

        #region Functions for Update()

        private void SetAnimations()
        {
            horizontalInput = Input.GetAxis("Horizontal");
            anim.SetFloat("Speed", verticalInput);
            anim.SetFloat("Direction", horizontalInput);
            anim.speed = animSpeed;
        }

        private void JumpControls()
        {
            rb.useGravity = true;

            if (Input.GetButtonDown("Jump"))
            {
                rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
                anim.SetBool("Jump", true);
            }
        }        
        private void SlidingControls()
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                anim.SetBool("Slide", true);
            }
        }
        private void AttackControls()
        {
            if (Input.GetMouseButtonDown((int)MouseButtonDown.MBD_LEFT))
            {
                anim.SetBool("Attack", true);
            }
        }
        #endregion

        #region Functions for FixedUpdate()

        // enables player to only move move sideways
        private void SidewaysMovement()
        {
            velocity = new Vector3(horizontalInput, 0, 0);
            velocity = transform.TransformDirection(velocity);
            velocity *= leftRightSpeed;
            transform.localPosition += velocity * Time.fixedDeltaTime;
        }

        // performs action depending on current state of character
        private void ActionsDependingOnAnimationState(int currentState)
        {            
            // check that dictionary has that state
            if (animationStates.ContainsKey(currentState))
            {
                animationStates[currentState](); // invoke action in dictionary that corresponds to currentState
            }

            return;
        }

        void ResetCollider()
        {
            col.height = orgColHight;
            col.center = orgVectColCenter;
        }

        #region Actions depending on current state

        void WhenRunning()
        {
            if (useCurves)
            {
                ResetCollider();
            }
        }

        void WhenIdle()
        {
            if (useCurves)
            {
                ResetCollider();
            }

            if (Input.GetButtonDown("Jump"))
            {
                anim.SetBool("Rest", true);
            }
        }

        void WhenJumping()
        {
            if (!anim.IsInTransition(0))
            {
                if (useCurves)
                {
                    float jumpHeight = anim.GetFloat("JumpHeight");
                    float gravityControl = anim.GetFloat("GravityControl");
                    if (gravityControl > 0)
                        rb.useGravity = false;

                    Ray ray = new Ray(transform.position + Vector3.up, -Vector3.up);
                    RaycastHit hitInfo = new RaycastHit();

                    if (Physics.Raycast(ray, out hitInfo))
                    {
                        if (hitInfo.distance > useCurvesHeight)
                        {
                            col.height = orgColHight - jumpHeight;
                            float adjCenterY = orgVectColCenter.y + jumpHeight;
                            col.center = new Vector3(0, adjCenterY, 0);
                        }
                        else
                        {
                            ResetCollider();
                        }
                    }
                }

                anim.SetBool("Jump", false);
            }
        }

        void WhenResting()
        {
            if (!anim.IsInTransition(0))
            {
                anim.SetBool("Rest", false);
            }
        }

        private void WhenSliding()
        {
            if (!anim.IsInTransition(0))
            {
                anim.SetBool("Slide", false);
            }
        }
        private void WhenAttacking()
        {
            if (!anim.IsInTransition(0))
            {
                anim.SetBool("Attack", false);
            }
        }
        #endregion

        #endregion
    }
}