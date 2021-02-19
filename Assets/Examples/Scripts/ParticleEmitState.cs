using Node;
using TMPro;
using UnityEngine;

namespace NodeExamples
{
    public class ParticleEmitState : State
    {
        private string stateName;
        private ParticleSystem particle;
        private TextMeshProUGUI stateUIText;

        public ParticleEmitState(ParticleSystem particle, TextMeshProUGUI stateUIText, string stateName)
        {
            this.stateName   = stateName;
            this.particle    = particle;
            this.stateUIText = stateUIText;
        }

        public override void OnEnter()
        {
            stateUIText.text = stateName;
            particle.Play();
        }

        public override void OnExit()
        {
            particle.Stop();
            particle.Clear();
        }
    }
}