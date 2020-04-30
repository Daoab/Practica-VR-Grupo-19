using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float speed = 10f;
    [HideInInspector]public bool canAddScore = true;
    private Rigidbody2D rb;


    [HideInInspector] public Transform leftLimit;
    [HideInInspector] public Transform rightLimit;
    [HideInInspector] public Transform downLimit;
    [HideInInspector] public Transform upLimit;

    [Header("Height Parameters")]
    [SerializeField] [Range(0f, 1f)] float minGapPercentage = 0.4f;
    [SerializeField] [Range(0f, 1f)] float maxGapPercentage = 0.7f;
    private float heightRange, minGapHeight, maxGapHeight;

    [Header("Sub Pipes")]
    [SerializeField] Transform pipeDown;
    [SerializeField] Transform pipeUp;
    PipeCollisionDetector[] subPipes;

    public void InitializePipe(ArcadeManager manager = null)
    {
        rb = GetComponent<Rigidbody2D>();
        subPipes = GetComponentsInChildren<PipeCollisionDetector>();
        foreach (PipeCollisionDetector subPipe in subPipes) subPipe.manager = manager;

        transform.position = rightLimit.position;

        //HEIGHT
        maxGapPercentage = minGapPercentage > maxGapPercentage ? minGapPercentage : maxGapPercentage;
        heightRange = upLimit.position.y - downLimit.position.y;
        minGapHeight = minGapPercentage * heightRange;
        maxGapHeight = maxGapPercentage * heightRange;
    }

    // Update is called once per frame
    void Update()
    {
            
        if (transform.position.x < leftLimit.position.x)
        {
            Randomize();
        }

    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(-speed, 0f);
    }

    public void Randomize()
    {
        transform.position = rightLimit.position;
        canAddScore = true;
        float gapHeight = Random.Range(minGapHeight, maxGapHeight);
        float gapPosition = downLimit.position.y + Random.Range(gapHeight*0.5f, heightRange - gapHeight*0.5f);
        Debug.Log("heightRange: " + heightRange + ", gapHeight: " + gapHeight + ", gapPosition: " + gapPosition);
        pipeDown.position = new Vector3(pipeDown.position.x, gapPosition - gapHeight/2f,pipeDown.position.z);
        pipeUp.position = new Vector3(pipeUp.position.x, gapPosition + gapHeight / 2f, pipeUp.position.z);
    }

    public void Freeze()
    {
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
    }

    public void Release()
    {
        rb.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
    }
}
