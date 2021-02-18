using Node;
using UnityEngine;
using UnityEngine.UI;

namespace NodeExamples
{
    public class TrafficLight : MonoBehaviour
    {
        [SerializeField] private Image redLightImage;
        [SerializeField] private Image yellowLightImage;
        [SerializeField] private Image greenLightImage;

        [SerializeField] private Text stateUIText;
        
        private FSM fsm;
        
        private void Start()
        {
            fsm = new FSM("Traffic Light");

            var go = new TrafficLightState("Go", 1f, greenLightImage, stateUIText, Color.green);
            var stop = new TrafficLightState("Stop", 1f, redLightImage, stateUIText, Color.red);
            var ready = new TrafficLightState("Ready", 1f, yellowLightImage, stateUIText, Color.yellow);
            var slowdown = new TrafficLightState("Slowdown", 1f, yellowLightImage, stateUIText, Color.yellow);
            
            fsm.AddTransition(stop, ready,    () => stop.IsDone);
            fsm.AddTransition(ready, go,      () => ready.IsDone);
            fsm.AddTransition(go, slowdown,   () => go.IsDone);
            fsm.AddTransition(slowdown, stop, () => slowdown.IsDone);
            
            fsm.SetEntry(go);
            fsm.Start();
        }

        private void Update()
        {
            fsm.Update();
        }
    }
}
