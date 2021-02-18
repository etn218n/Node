using Node;
using UnityEngine;
using UnityEngine.UI;

namespace NodeExamples
{
    public class SphereController : MonoBehaviour
    {
        private FSM fsm;
        private Input input;
        private Rigidbody rigidBody;
        private Character character;

        [SerializeField] private Text stateUIText;
        [SerializeField] private ParticleSystem sleepParticle;

        private void Awake()
        {
            input     = GetComponent<Input>();
            rigidBody = GetComponent<Rigidbody>();
            character = GetComponent<Character>();
        }

        private void Start()
        {
            fsm = new FSM("Sphere");

            var airborneState = new SubFSM(fsm);

            var walkState  = new WalkState(input, rigidBody, character, stateUIText);
            var sleepState = new ParticleEmitState(sleepParticle, stateUIText);
            var jumpState  = new JumpState(0.2f, rigidBody, character, stateUIText);
            var doubleJumpState  = new JumpState(0.2f, rigidBody, character, stateUIText);

            fsm.AddTransition(sleepState, walkState, () => input.HorizontalAxis != 0);
            fsm.AddTransition(sleepState, airborneState, () => UnityEngine.Input.GetKeyDown(KeyCode.Space) && character.IsGrounded);
            fsm.AddTransition(walkState, sleepState, () => input.HorizontalAxis == 0);
            fsm.AddTransition(walkState, airborneState,  () => UnityEngine.Input.GetKeyDown(KeyCode.Space) && character.IsGrounded);
            fsm.AddTransitionToPreviousNode(airborneState, () => airborneState.IsFinished);
            
            airborneState.AddTransitionToExitNode(jumpState, () => character.IsGrounded && jumpState.IsDelayPassed);
            airborneState.AddTransitionToExitNode(doubleJumpState, () => character.IsGrounded);
            airborneState.AddTransition(jumpState, doubleJumpState, () => UnityEngine.Input.GetKeyDown(KeyCode.Space));

            fsm.SetEntry(sleepState);
            fsm.Start();
        }

        private void Update()
        {
            fsm.Update();
        }

        private void FixedUpdate()
        {
            fsm.FixedUpdate();
        }
    } 
}
