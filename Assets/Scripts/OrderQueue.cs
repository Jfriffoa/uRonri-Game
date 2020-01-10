using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderQueue : MonoBehaviour
{
    [Header("UI")]
    public int queueMaxSize = 13;
    public GameObject prefab;

    Queue<Dir> orderQueue = new Queue<Dir>();
    Queue<GameObject> goQueue = new Queue<GameObject>();

    [Header("Gameplay")]
    public Move objectToMove;
    public float timeBetweenAction = 0.5f;

    bool isPlaying = false;

    public void AddQueue(int intDir) {
        if (intDir > (int)Dir.RIGHT || orderQueue.Count >= queueMaxSize || intDir < (int)Dir.LEFT || isPlaying)
            return;

        var direction = (Dir)intDir;
        orderQueue.Enqueue(direction);
        var go = Instantiate(prefab, transform);

        Vector3 eulerRotation = Vector3.zero;
        switch (direction){
            case Dir.DOWN:  eulerRotation.z = 90;  break;
            case Dir.RIGHT: eulerRotation.z = 180; break;
            case Dir.UP:    eulerRotation.z = 270; break;
        }

        go.transform.localRotation = Quaternion.Euler(eulerRotation);
        goQueue.Enqueue(go);
    }

    public void StartQueue() {
        StartCoroutine(PlayQueue());
    }

    IEnumerator PlayQueue() {
        isPlaying = true;

        while (orderQueue.Count > 0) {
            objectToMove.MoveInADir(orderQueue.Dequeue());
            Destroy(goQueue.Dequeue());
            yield return new WaitForSeconds(timeBetweenAction);
        }

        isPlaying = false;
    }
}
