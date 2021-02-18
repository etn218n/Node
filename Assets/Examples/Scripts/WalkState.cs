using Node;
using UnityEngine;
using UnityEngine.UI;

namespace NodeExamples
{
    public class WalkState : State
    {
        private Input input;
        private Rigidbody rigidBody;
        private Character character;
        private Text stateUIText;

        public WalkState(Input input, Rigidbody rigidBody, Character character, Text stateUIText)
        {
            this.input       = input;
            this.rigidBody   = rigidBody;
            this.character   = character;
            this.stateUIText = stateUIText;
        }

        public override void OnEnter()
        {
            stateUIText.text = "Walk";
        }

        public override void OnFixedUpdate()
        {
            float moveVelocity = input.HorizontalAxis * character.WalkSpeed;
            rigidBody.velocity = new Vector3(moveVelocity, 0, 0);
        }

        public override void OnExit()
        {
            rigidBody.velocity = Vector3.zero;
        }
    }
}