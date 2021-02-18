using UnityEngine;

namespace NodeExamples
{
    public class Character : MonoBehaviour
    {
        [SerializeField] private float walkSpeed;
        [SerializeField] private float runSpeed;
        [SerializeField] private float jumpHeight;
        [SerializeField] private float bodyRadius;
        [SerializeField] private float rayLength;

        private bool isGrounded;
        
        public float WalkSpeed  => walkSpeed;
        public float RunSpeed   => runSpeed;
        public float JumpHeight => jumpHeight;
        public bool IsGrounded  => isGrounded;
        
        private void FixedUpdate()
        {
            if (Physics.Raycast(transform.position, Vector3.down, bodyRadius + rayLength))
                isGrounded = true;
            else
                isGrounded = false;
        }
    }
}