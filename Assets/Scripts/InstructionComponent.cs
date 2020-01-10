using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.EventSystems;

namespace RonriGame {
    public enum Instruction { FORWARD, TURN_LEFT, TURN_RIGHT }

    public class InstructionComponent : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
        public Image icon;

        int _index = -1;
        OrderManager _order;

        Color _originalColor;
        Color _glowColor = Color.white;

        public Instruction Instruction { get; private set; }

        public void Glow(float seconds) {
            StartCoroutine(GlowCoroutine(seconds));
        }

        IEnumerator GlowCoroutine(float seconds) {
            icon.color = _glowColor;
            yield return new WaitForSeconds(seconds);
            icon.color = _originalColor;
        }

        internal void Setup(Instruction instruction, int idx, Sprite sprite, OrderManager order) {
            Instruction = instruction;
            _index = idx;
            _order = order;
            icon.sprite = sprite;
            _originalColor = icon.color;
        }

        internal void UpdateIndex(int newIndex) {
            _index = newIndex;
        }

        public void Discard() {
            if (_index != -1) {
                _order.Discard(_index);
            }

            Destroy(gameObject);
        }

        void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData) {
            if (_order.IsPlaying)
                return;

            icon.color = _glowColor;
        }

        void IPointerExitHandler.OnPointerExit(PointerEventData eventData) {
            if (_order.IsPlaying)
                return;

            icon.color = _originalColor;
        }
    }
}