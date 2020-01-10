using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Dir { LEFT, UP, DOWN, RIGHT }

public class Move : MonoBehaviour
{
    Vector2 size;

    // Start is called before the first frame update
    void Start()
    {
        size = GetComponent<SpriteRenderer>().bounds.size;
    }

    public void MoveInADir(Dir dir) {
        var pos = transform.position;
        switch (dir) {
            case Dir.LEFT:
                pos.x -= size.x;
                break;
            case Dir.UP:
                pos.y += size.y;
                break;
            case Dir.DOWN:
                pos.y -= size.y;
                break;
            case Dir.RIGHT:
                pos.x += size.x;
                break;
        }
        transform.position = pos;
    }
}
