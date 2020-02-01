using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    public Transform startPoint;
    private LineRenderer lineRenderer;
    private EdgeCollider2D edgeCollider;
    private Vector2[] edgePoints;
    private List<RopeSegment> ropeSegments = new List<RopeSegment>();
    public float ropeSegLen = 0.25f;
    public int numSegments = 35;
    private float lineWidth = 0.1f;

    // Use this for initialization
    void Start()
    {
        this.lineRenderer = this.GetComponent<LineRenderer>();
        this.edgeCollider = this.GetComponent<EdgeCollider2D>();

        Vector3 ropeStartPoint = startPoint ? startPoint.position : Camera.main.ScreenToWorldPoint(Input.mousePosition);

        edgePoints = new Vector2[numSegments];
        for (int i = 0; i < numSegments; i++)
        {
            this.ropeSegments.Add(new RopeSegment(ropeStartPoint));
            ropeStartPoint.y -= ropeSegLen;
        }
    }

    // Update is called once per frame
    void Update()
    {
        this.DrawRope();
    }

    private void FixedUpdate()
    {
        this.Simulate();
        edgeCollider.points = edgePoints;
    }

    private void Simulate()
    {
        // SIMULATION
        Vector2 forceGravity = new Vector2(0f, -1.5f);

        for (int i = 1; i < this.numSegments; i++)
        {
            RopeSegment firstSegment = this.ropeSegments[i];
            Vector2 velocity = firstSegment.posNow - firstSegment.posOld;
            firstSegment.posOld = firstSegment.posNow;
            firstSegment.posNow += velocity;
            firstSegment.posNow += forceGravity * Time.fixedDeltaTime;
        }

        //CONSTRAINTS
        for (int i = 0; i < 50; i++)
        {
            this.ApplyConstraint();
        }
    }

    private void ApplyConstraint()
    {
        //Constrant to Mouse
        RopeSegment firstSegment = this.ropeSegments[0];
        firstSegment.posNow = startPoint != null ? startPoint.position : Camera.main.ScreenToWorldPoint(Input.mousePosition);
        edgePoints[0] = firstSegment.posNow;

        for (int i = 0; i < this.numSegments - 1; i++)
        {
            RopeSegment firstSeg = this.ropeSegments[i];
            RopeSegment secondSeg = this.ropeSegments[i + 1];

            float dist = (firstSeg.posNow - secondSeg.posNow).magnitude;
            float error = Mathf.Abs(dist - this.ropeSegLen);
            Vector2 changeDir = Vector2.zero;

            if (dist > ropeSegLen)
            {
                changeDir = (firstSeg.posNow - secondSeg.posNow).normalized;
            } else if (dist < ropeSegLen)
            {
                changeDir = (secondSeg.posNow - firstSeg.posNow).normalized;
            }

            Vector2 changeAmount = changeDir * error;
            if (i != 0)
            {
                firstSeg.posNow -= changeAmount * 0.5f;
                edgePoints[i] = firstSegment.posNow;
                secondSeg.posNow += changeAmount * 0.5f;
            }
            else
            {
                secondSeg.posNow += changeAmount;
            }
        }
    }

    private void DrawRope()
    {
        float lineWidth = this.lineWidth;
        lineRenderer.startWidth = lineWidth;
        lineRenderer.endWidth = lineWidth;

        Vector3[] ropePositions = new Vector3[this.numSegments];
        for (int i = 0; i < this.numSegments; i++)
        {
            ropePositions[i] = this.ropeSegments[i].posNow;
        }

        lineRenderer.positionCount = ropePositions.Length;
        lineRenderer.SetPositions(ropePositions);
    }

    public class RopeSegment
    {
        public Vector2 posNow;
        public Vector2 posOld;

        public RopeSegment(Vector2 pos)
        {
            this.posNow = pos;
            this.posOld = pos;
        }
    }
}