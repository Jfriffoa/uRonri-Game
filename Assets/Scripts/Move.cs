using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    Vector2 size;

    // Start is called before the first frame update
    void Start()
    {
        size = GetComponent<SpriteRenderer>().bounds.size;
    }

    public void MoveInADir(int dir) {
        var pos = transform.position;
        switch (dir) {
            case 0:
                pos.x -= size.x;
                break;
            case 1:
                pos.y += size.y;
                break;
            case 2:
                pos.y -= size.y;
                break;
            case 3:
                pos.x += size.x;
                break;
        }
        transform.position = pos;
    }
}
