using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Controller2D target;
    [SerializeField] private Vector2 focusAreaSize;

    FocusArea focusArea;

    private float verticalOffset;
    [SerializeField] private float verticalSmoothTime;
    private float smoothVelocityY;

    struct FocusArea
    {
        private Vector2 centre;
        private Vector2 velocity;
        private float left, right;
        public float top, bottom;


        public Vector2 Centre { get { return centre; } }

        public FocusArea(Bounds targetBounds, Vector2 size)
        {
            left = targetBounds.center.x - size.x / 2;
            right = targetBounds.center.x + size.x / 2;
            bottom = targetBounds.min.y;
            top = targetBounds.min.y + size.y;

            centre = new Vector2((left + right) / 2, (top + bottom) / 2);
            velocity = Vector2.zero;
        }

        public void Update(Bounds targetBounds)
        {
            float shiftX = 0;
            float shiftY = 0;
             
            if (targetBounds.min.x < left)
            {
                shiftX = targetBounds.min.x - left;
            }
            else if (targetBounds.max.x > right)
            {
                shiftX = targetBounds.max.x - right;
            }

            left += shiftX;
            right += shiftX;

            if (targetBounds.min.y < bottom)
            {
                shiftY = targetBounds.min.y - bottom;
            }
            else if (targetBounds.max.y > top)
            {
                shiftY = targetBounds.max.y - top;
            }

            top += shiftY;
            bottom += shiftY;
            centre = new Vector2((left + right) / 2, (top + bottom) / 2);
            velocity = new Vector2(shiftX, shiftY);
        }      
    }
	
    void Start()
    {
        focusArea = new FocusArea(target.GetComponent<BoxCollider2D>().bounds, focusAreaSize);
    }

    void LateUpdate()
    {
        focusArea.Update(target.GetComponent<BoxCollider2D>().bounds);
        
        Vector2 focusPosition = focusArea.Centre + Vector2.up * verticalOffset;

        focusPosition.y = Mathf.SmoothDamp(transform.position.y, focusPosition.y, ref smoothVelocityY, verticalSmoothTime);
        transform.position = new Vector3(focusPosition.x, focusPosition.y, -10);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawCube(focusArea.Centre, focusAreaSize);
    }

}
