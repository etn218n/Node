using Node;
using UnityEngine;
using UnityEngine.UI;

namespace NodeExamples
{
    public class ParticleEmitState : State
    {
        private ParticleSystem particle;
        private Text stateUIText;
        
        public ParticleEmitState(ParticleSystem particle, Text stateUIText)
        {
            this.particle = particle;
        }

        public override void OnEnter()
        {
            stateUIText.text = "Emit Particle";
            particle.Play();
        }

        public override void OnExit()
        {
            particle.Stop();
            particle.Clear();
        }
    }
}