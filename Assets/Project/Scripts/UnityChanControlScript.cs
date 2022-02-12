using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

        public bool isGameRunning = false;
        private Rigidbody rb;
        private Animator anim;
        private Vector3 velocity;
        private float horizontalInput;
        private float verticalInput = 1;
        
        private Button startButton;

        [SerializeField] private float leftRightSpeed = 7.0f;
        [SerializeField] private float jumpPower = 5.0f;
        [SerializeField] private float animSpeed = 1.5f;
        #endregion

        #region Ability fields
        // animator parameters
        [HideInInspector] public string JUMP_PARAM = "Jump";
        [HideInInspector] public string SLIDE_PARAM = "Slide";
        [HideInInspector] public string ATTACK_PARAM = "Attack";
        [HideInInspector] public string DEATH_PARAM = "Collision";
        [HideInInspector] public string SPEED_PARAM = "Speed";
        [HideInInspector] public string DIRECTION_PARAM = "Direction";
        [HideInInspector] public string RUNNING = "Running";
        

        // dictionary that binds the animator parameter and the ability model
        public Dictionary<string, Ability> animationStates;      
        #endregion

        public event Action OnDeath;

        private void Start()
        {
            anim = GetComponent<Animator>();
            rb = GetComponent<Rigidbody>();

            // Create Abilities, by default is disabled      
            var jumpingAbility = new Ability() { AnimParamName = JUMP_PARAM, IsEnabled = false ,Key = KeyCode.Space, FuncToCall = JumpControls};
            var slidingAbility = new Ability() { AnimParamName = SLIDE_PARAM, IsEnabled = false ,Key = KeyCode.S, FuncToCall = SlidingControls};
            var attackAbility = new Ability() { AnimParamName = ATTACK_PARAM, IsEnabled = false ,Key = KeyCode.Mouse0, FuncToCall = AttackControls};
            var abilityToDie = new Ability() { AnimParamName = DEATH_PARAM, IsEnabled = true, Key = KeyCode.K, FuncToCall = DeathControls };

            // bind ability to string name same as animation parameter
            // careful: the enabling of the actions are performed on Abilities manager, the idea is to enable them per milestone
            animationStates = new Dictionary<string, Ability>
            {
                {JUMP_PARAM, jumpingAbility},
                {SLIDE_PARAM, slidingAbility},
                {ATTACK_PARAM, attackAbility},
                {DEATH_PARAM, abilityToDie}
            };

            startButton = GameObject.Find("Start Button").GetComponent<Button>();

            if (startButton)
            {
                startButton.onClick.AddListener(delegate { isGameRunning = true; });
                startButton.onClick.AddListener(SetRunningStateTrue);
            }
        }

        private void Update()
        {
            if (!isGameRunning) return;
            SetAnimations();
            DetectInputs(); // detects possible inputs and calls function ability is enabled
        }

        private void FixedUpdate()
        {
            SidewaysMovement();
        }

        public void SetRunningStateTrue()
        {
            anim.SetBool(RUNNING, true);
        }
        
        public void SetRunningStateFalse()
        {
            anim.SetBool(RUNNING, false);
        }

        #region Functions for calling dictionary action depending on player input

        // detects all possible inputs we set and perform action if enabled
        private void DetectInputs()
        {
            if (Input.GetKeyDown(TryGetKeyCode(JUMP_PARAM)))
            {
                PerformActionIfEnabled(JUMP_PARAM);
            }
            else if (Input.GetKeyDown(TryGetKeyCode(SLIDE_PARAM)))
            {
                PerformActionIfEnabled(SLIDE_PARAM);
            }
            else if (Input.GetKeyDown(TryGetKeyCode(ATTACK_PARAM)))
            {
                PerformActionIfEnabled(ATTACK_PARAM);
            }
            // tmp to test death animation
            else if (Input.GetKeyDown(TryGetKeyCode(DEATH_PARAM)))
            {
                PerformActionIfEnabled(DEATH_PARAM);
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
        }

        // ====== Set of functions that can be called if enabled =========
        private void JumpControls()
        {
            if (!anim.IsInTransition(0))
            {
                rb.useGravity = true;

                rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
                anim.SetTrigger(JUMP_PARAM);
            }            
        }           

        private void SlidingControls()
        {            
            anim.SetTrigger(SLIDE_PARAM);
        }

        private void AttackControls()
        {            
            anim.SetTrigger(ATTACK_PARAM);            
        }

        private void DeathControls()
        {
            anim.SetTrigger(DEATH_PARAM);
            anim.SetBool(RUNNING, false);
            isGameRunning = false;
            OnDeath?.Invoke();
        }
        #endregion

        #region Other
        private void SetAnimations()
        {
            horizontalInput = Input.GetAxis("Horizontal");
            anim.SetFloat(SPEED_PARAM, verticalInput);
            anim.SetFloat(DIRECTION_PARAM, horizontalInput);
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