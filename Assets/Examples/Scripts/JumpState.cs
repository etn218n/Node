﻿using Node;
using UnityEngine;
using UnityEngine.UI;

namespace NodeExamples
{
    public class JumpState : State
    {
        private float delay;
        private Rigidbody rigidBody;
        private Character character;
        private Text stateUIText;

        private float timeElapsed;
        
        public bool IsDelayPassed { get; private set; }

        public JumpState( float delay, Rigidbody rigidBody, Character character, Text stateUIText)
        {
            this.delay       = delay;
            this.rigidBody   = rigidBody;
            this.character   = character;
            this.stateUIText = stateUIText;
        }

        public override void OnEnter()
        {
            timeElapsed   = 0;
            IsDelayPassed = false;
            
            stateUIText.text = "Jump";
            
            Launch();
        }

        public override void OnUpdate()
        {
            timeElapsed += Time.deltaTime;

            if (timeElapsed >= delay)
                IsDelayPassed = true;
        }

        private void Launch()
        {
            var velocity = rigidBody.velocity;
            velocity.y += Mathf.Sqrt(-2f * Physics.gravity.y * character.JumpHeight);

            rigidBody.velocity = velocity;
        }
    }
}