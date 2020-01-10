using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RonriGame {
    public class Player : MonoBehaviour {
        public enum Direction { UP, RIGHT, DOWN, LEFT }

        public bool overrideDistance;
        public float distance;
        public Direction defaultDirection;

        Animator _animator;
        Direction _actualDirection;
        
        void Start() {
            if (!overrideDistance)
                distance = GetComponent<SpriteRenderer>().bounds.size.x;

            _animator = GetComponent<Animator>();
            _actualDirection = defaultDirection;
            UpdateAnimation();
        }

        public void FollowOrder(Instruction order) {
            switch (order) {
                case Instruction.FORWARD:
                    var to = Vector2.right;

                    switch (_actualDirection) {
                        case Direction.UP: to = Vector2.up; break;
                        case Direction.DOWN: to = Vector2.down; break;
                        case Direction.LEFT: to = Vector2.left; break;
                    }

                    transform.Translate(to * distance, Space.World);
                    break;
                case Instruction.TURN_LEFT:
                    _actualDirection = (Direction)(((int)_actualDirection + 3) % 4);
                    UpdateAnimation();
                    break;
                case Instruction.TURN_RIGHT:
                    _actualDirection = (Direction)(((int)_actualDirection + 1) % 4);
                    UpdateAnimation();
                    break;
            }
        }

        void UpdateAnimation() {
            switch (_actualDirection) {
                case Direction.UP: _animator.SetTrigger("Up"); break;
                case Direction.LEFT: _animator.SetTrigger("Left"); break;
                case Direction.RIGHT: _animator.SetTrigger("Right"); break;
                case Direction.DOWN: _animator.SetTrigger("Down"); break;
            }
        }

        //public void MoveInADir(Dir dir) {
        //    var pos = transform.position;
        //    switch (dir) {
        //        case Dir.LEFT:
        //            pos.x -= size.x;
        //            break;
        //        case Dir.UP:
        //            pos.y += size.y;
        //            break;
        //        case Dir.DOWN:
        //            pos.y -= size.y;
        //            break;
        //        case Dir.RIGHT:
        //            pos.x += size.x;
        //            break;
        //    }
        //    transform.position = pos;
        //}
    }
}