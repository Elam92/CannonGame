using UnityEngine;

namespace CannonGame
{
    /*  ArcRenderer Class
     * 
     *  Used to draw/display a trajectory to a given position from an origin point.
     */ 
    [RequireComponent(typeof(LineRenderer))]
    public class ArcRenderer : MonoBehaviour
    {
        // How detailed we want the arc to be.
        public int resolution = 30;

        // The component to draw the arc.
        private LineRenderer lr;

        // Use this for initialization
        void Start()
        {
            lr = GetComponent<LineRenderer>();
            // Account for last part of the mesh when drawing.
            lr.positionCount = resolution + 1;
        }

        // Draws an arc from an origin point to the target position. Shape of arc is determined by velocity,
        // time to get to the target, and gravity.
        public void DrawArc(Vector3 origin, Vector3 targetPosition, Vector3 initialVelocity, float timeToTarget, float gravity)
        {
            //Vector3 prevDrawPoint = origin;

            // Set the points along the path to target based on resolution detail.
            Vector3[] linePoints = new Vector3[resolution + 1];
            for (int i = 0; i <= resolution; i++)
            {
                // Calculate the points along the trajectory.
                float simulationTime = i / (float)resolution * timeToTarget;

                // Calculate d = (v0 * t) + (1/2at^2)
                // Initial Velocity displacement at point in time.
                Vector3 v0 = (initialVelocity * simulationTime);
                // Acceleration displacement at point in time squared.
                Vector3 acceleration = (Vector3.up * gravity * simulationTime * simulationTime * 0.5f);

                Vector3 displacement =  v0 + acceleration;
                Vector3 drawPoint = origin + displacement;

                linePoints[i] = drawPoint;

                //Debug.DrawLine(prevDrawPoint, drawPoint, Color.green);
                //prevDrawPoint = drawPoint;
            }

            // Draw the arc based on calculated points.
            lr.SetPositions(linePoints);
        }
    }
}
