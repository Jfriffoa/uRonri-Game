using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RonriGame {
    public class OrderManager : MonoBehaviour {
        [Header("UI")]
        public int queueMaxSize = 13;
        public GameObject prefab;
        public Sprite[] icons;

        List<InstructionComponent> orderQueue = new List<InstructionComponent>();

        [Header("Gameplay")]
        public Player objectToMove;
        public float timeBetweenAction = 0.5f;

        public bool IsPlaying { get; private set; }

        public void AddQueue(int intOrder) {
            if (intOrder > (int)Instruction.TURN_RIGHT || orderQueue.Count >= queueMaxSize || intOrder < 0 || IsPlaying)
                return;

            var order = (Instruction)intOrder;
            var go = Instantiate(prefab, transform);
            var script = go.GetComponent<InstructionComponent>();
            script.Setup(order, orderQueue.Count, icons[intOrder], this);
            orderQueue.Add(script);
        }

        internal void Discard(int index) {
            for (int i = index; i < orderQueue.Count; i++) {
                orderQueue[i].UpdateIndex(i - 1);
            }

            orderQueue.RemoveAt(index);
        }

        public void StartQueue() {
            StartCoroutine(PlayQueue());
        }

        IEnumerator PlayQueue() {
            IsPlaying = true;

            for (int i = 0; i < orderQueue.Count; i++) {
                orderQueue[i].Glow(timeBetweenAction);
                objectToMove.FollowOrder(orderQueue[i].Instruction);
                yield return new WaitForSeconds(timeBetweenAction);
            }

            IsPlaying = false;
        }
    }
}