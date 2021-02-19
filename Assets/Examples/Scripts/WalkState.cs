using Node;
using TMPro;
using UnityEngine;

namespace NodeExamples
{
    public class WalkState : State
    {
        private Input input;
        private string stateName;
        private Rigidbody rigidBody;
        private Character character;
        private TextMeshProUGUI stateUIText;

        public WalkState(Input input, Rigidbody rigidBody, Character character, TextMeshProUGUI stateUIText, string stateName)
        {
            this.input       = input;
            this.stateName   = stateName;
            this.rigidBody   = rigidBody;
            this.character   = character;
            this.stateUIText = stateUIText;
        }

        public override void OnEnter()
        {
            stateUIText.text = stateName;
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