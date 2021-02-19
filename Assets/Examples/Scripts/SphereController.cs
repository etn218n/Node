using Node;
using TMPro;
using UnityEngine;

namespace NodeExamples
{
    public class SphereController : MonoBehaviour
    {
        private FSM fsm;
        private Input input;
        private Rigidbody rigidBody;
        private Character character;

        [SerializeField] private TextMeshProUGUI stateUIText;
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

            var airborneState = new SubFSM(fsm, "Sphere Airborne");

            var walkState  = new WalkState(input, rigidBody, character, stateUIText, "Walk");
            var sleepState = new ParticleEmitState(sleepParticle, stateUIText, "Sleep");
            var jumpState  = new JumpState(0.2f, rigidBody, character, stateUIText, "Jump");
            var doubleJumpState  = new JumpState(0.2f, rigidBody, character, stateUIText, "Double Jump");

            fsm.AddTransition(sleepState, walkState, () => input.HorizontalAxis != 0);
            fsm.AddTransition(sleepState, airborneState, () => UnityEngine.Input.GetKeyDown(KeyCode.Space) && character.IsGrounded);
            fsm.AddTransition(walkState, sleepState, () => input.HorizontalAxis == 0);
            fsm.AddTransition(walkState, airborneState,  () => UnityEngine.Input.GetKeyDown(KeyCode.Space) && character.IsGrounded);
            fsm.AddTransitionToPreviousNode(airborneState, () => airborneState.IsFinished);
            
            airborneState.AddTransitionToExitNode(jumpState, () => character.IsGrounded && jumpState.IsDelayPassed);
            airborneState.AddTransitionToExitNode(doubleJumpState,  () => character.IsGrounded);
            airborneState.AddTransition(jumpState, doubleJumpState, () => UnityEngine.Input.GetKeyDown(KeyCode.Space) && jumpState.IsDelayPassed);

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
