using Node;
using UnityEngine;
using UnityEngine.UI;

namespace NodeExamples
{
    public class TrafficLightState : State
    {
        private string name;
        private float duration;
        private float elapsedTime;
        private Text uiText;
        private Image lightImage;
        private Color lightColor;

        public bool IsDone { get; private set; }
        public string Name { get => name; }

        public TrafficLightState(string name, float duration, Image lightImage, Text uiText, Color lightColor)
        {
            this.name       = name;
            this.duration   = duration;
            this.uiText     = uiText;
            this.lightImage = lightImage;
            this.lightColor = lightColor;
        }

        public override void OnEnter()
        {
            IsDone = false;
            elapsedTime = 0;
            uiText.text = name;
            lightImage.color = lightColor;
        }

        public override void OnUpdate()
        {
            if (IsDone)
                return;

            elapsedTime += Time.deltaTime;

            if (elapsedTime > duration)
                IsDone = true;
        }

        public override void OnExit()
        {
            lightImage.color = Color.white;
        }
    }
}