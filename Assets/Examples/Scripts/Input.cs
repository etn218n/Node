using UnityEngine;

namespace NodeExamples
{
    public class Input : MonoBehaviour
    {
        public float VerticalAxis   { get; private set; }
        public float HorizontalAxis { get; private set; }

        private void Update()
        {
            VerticalAxis   = UnityEngine.Input.GetAxisRaw("Vertical");
            HorizontalAxis = UnityEngine.Input.GetAxisRaw("Horizontal");
        }
    }
}