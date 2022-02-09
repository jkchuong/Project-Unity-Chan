using System;
using System.Collections.Generic;
using UnityEngine;

namespace UnityChan
{

    public class Ability
    {
        public string AnimParamName { get; set; }
        public KeyCode Key { get; set; }
        public Action FuncToCall { get; set; }
        public bool IsEnabled { get; set; }
    }

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

        //
        private float horizontalInput;
        private float verticalInput = 1;
        public float leftRightSpeed = 7.0f;
        public float rotateSpeed = 2.0f;
        public float jumpPower = 5.0f;
        public float animSpeed = 1.5f;

        #endregion

        #region Enabling movement mechanics
        // animator parameters
        private string jumpParam = "Jump";
        private string slideParam = "Slide";
        private string attackParam = "Attack";
        private string deathParam = "Collision";

        // dictionary that binds the animator parameter, the key that is pressed and the action to call
        public Dictionary<string, Node> animationStates;

        // custom struct to that holds keycode and function to call information
        public struct Node
        {
            public KeyCode Key;
            public Action FuncToCall;
            public bool IsEnabled { get; set; }
        }        
        #endregion

        private void Start()
        {
            anim = GetComponent<Animator>();
            col = GetComponent<CapsuleCollider>();
            rb = GetComponent<Rigidbody>();

            orgColHight = col.height;
            orgVectColCenter = col.center;

            // Dictionary binds animator parameter and keycode and function to call
            // careful: the enabling of the actions are performed on Abilities manager, the idea is to enable them per milestone
            animationStates = new Dictionary<string, Node>();
            animationStates.Add(jumpParam, new Node(){ IsEnabled = true, Key = KeyCode.Space, FuncToCall = () => JumpControls()});
            animationStates.Add(slideParam, new Node() { IsEnabled = true, Key = KeyCode.S, FuncToCall = () => SlidingControls() });
            animationStates.Add(attackParam, new Node() { IsEnabled = false, Key = KeyCode.Mouse0, FuncToCall = () => AttackControls()});

            // tmp state just to test when she dead
            animationStates.Add(deathParam, new Node() { IsEnabled = false, Key = KeyCode.K, FuncToCall = () => DeathControls() });
        }

        private void Update()
        {
            SetAnimations();
            DetectInputs(); // detects posible inputs and calls function if they exist i.e registered in the dictionary
        }

        private void FixedUpdate()
        {
            SidewaysMovement();
        }

        #region Functions for calling dictionary action depending on player input

        // detects all posible inputs we set and perform action if enabled
        private void DetectInputs()
        {
            if (Input.GetKeyDown(TryGetKeyCode(jumpParam)))
            {
                PerformActionIfEnabled(jumpParam);
            }
            else if (Input.GetKeyDown(TryGetKeyCode(slideParam)))
            {
                PerformActionIfEnabled(slideParam);
            }
            else if (Input.GetKeyDown(TryGetKeyCode(attackParam)))
            {
                PerformActionIfEnabled(attackParam);
            }
            // tmp to test death animation
            else if (Input.GetKeyDown(TryGetKeyCode(deathParam)))
            {
                PerformActionIfEnabled(deathParam);
            }
        }

        // return keycode if mapped in our dictionary
        private KeyCode TryGetKeyCode(string animatorParam)
        {
            return animationStates.ContainsKey(animatorParam) ? animationStates[animatorParam].Key : default;
        }

        // if param exists and is enabled then call the function that it points to
        private void PerformActionIfEnabled(string requestedAction)
        {
            if (animationStates.ContainsKey(requestedAction) && animationStates[requestedAction].IsEnabled)
            {               
                animationStates[requestedAction].FuncToCall(); // invoke action in dictionary that corresponds to it
            }
            return;
        }

        // ====== Set of functions that can be called if enabled =========
        protected void JumpControls()
        {
            if (!anim.IsInTransition(0))
            {
                rb.useGravity = true;

                rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
                anim.SetTrigger(jumpParam);
            }            
        }           
        protected void SlidingControls()
        {            
            anim.SetTrigger(slideParam);
        }
        protected void AttackControls()
        {            
            anim.SetTrigger(attackParam);            
        }
        protected void DeathControls()
        {
            anim.SetTrigger(deathParam);
        }
        #endregion

        #region Other
        private void SetAnimations()
        {
            horizontalInput = Input.GetAxis("Horizontal");
            anim.SetFloat("Speed", verticalInput);
            anim.SetFloat("Direction", horizontalInput);
            anim.speed = animSpeed;
        }

        // enables player to only move move sideways
        private void SidewaysMovement()
        {
            velocity = new Vector3(horizontalInput, 0, 0);
            velocity = transform.TransformDirection(velocity);
            velocity *= leftRightSpeed;
            transform.localPosition += velocity * Time.fixedDeltaTime;
        }        

        #endregion
    }
}