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

        #region Ability fields
        // animator parameters
        public string jumpParam = "Jump";
        public string slideParam = "Slide";
        public string attackParam = "Attack";
        public string deathParam = "Collision";

        // dictionary that binds the animator parameter and the ability model
        public Dictionary<string, Ability> animationStates;      
        #endregion

        private void Start()
        {
            anim = GetComponent<Animator>();
            col = GetComponent<CapsuleCollider>();
            rb = GetComponent<Rigidbody>();

            orgColHight = col.height;
            orgVectColCenter = col.center;

            // Create Abilities, by default is disabled      
            var jumpingAbility = new Ability() { AnimParamName = jumpParam, IsEnabled = false ,Key = KeyCode.Space, FuncToCall = () => JumpControls()};
            var slidinAbility = new Ability() { AnimParamName = slideParam, IsEnabled = false ,Key = KeyCode.S, FuncToCall = () => SlidingControls()};
            var attackAbility = new Ability() { AnimParamName = attackParam, IsEnabled = false ,Key = KeyCode.Mouse0, FuncToCall = () => AttackControls()};
            var abilityToDie = new Ability() { AnimParamName = deathParam, IsEnabled = false, Key = KeyCode.K, FuncToCall = () => DeathControls() };

            // bind ability to string name same as animation parameter
            // careful: the enabling of the actions are performed on Abilities manager, the idea is to enable them per milestone
            animationStates = new Dictionary<string, Ability>();
            animationStates.Add(jumpParam, jumpingAbility);
            animationStates.Add(slideParam, slidinAbility);
            animationStates.Add(attackParam, attackAbility);
            animationStates.Add(deathParam, abilityToDie);
        }

        private void Update()
        {
            SetAnimations();
            DetectInputs(); // detects posible inputs and calls function ability is enabled
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