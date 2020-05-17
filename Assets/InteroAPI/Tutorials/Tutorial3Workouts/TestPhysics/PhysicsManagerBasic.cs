using Intero.Common;
using Intero.Physics;
using System.Diagnostics;

public class PhysicsManagerBasic: PhysicsManager
    {
        // InteroBody1D previousLocation;
        // public float kP = 1.0f, kD = 1.0f;
        // this functions returns the new location
        // returns the new velocity of the object 
        public override void ResetLocation()
        {
           // previousLocation = null;
        }
        public override InteroBody1D UpdateLocation(float realDistance, float realSpeed, ErgData e)
        {
            // UnityEngine.Debug.Log("Jojo");
            InteroBody1D targetLocation = new InteroBody1D(e);
            return targetLocation;
            /*
            // InteroBody1D newLocation = new InteroBody1D(e);
            // first time?
            if (previousLocation == null)
            {
                // teleport to new location
                previousLocation = targetLocation;
                return new InteroBody1D(e.distance, 0);
            }
            else
            {
                float dT = targetLocation.time - previousLocation.time;

                if (dT < 0.0001f)
                {
                    previousLocation.copy(targetLocation);
                    // mantain the old speed
                    return new InteroBody1D(realDistance, realSpeed);
                }

                float deltaDistance = targetLocation.distance - realDistance;
                float speed = deltaDistance / dT;
                // Debug.Log($"delta\t{e.distance}\t{deltaDistance}\t{dT}");
                if (System.Math.Abs(deltaDistance) > 15)
                {
                    // teleport to new location
                    previousLocation.copy(targetLocation);
                    return new InteroBody1D(e.distance, realSpeed);
                }
                // PID controller
                float deltaForce = kD * speed + kP * (targetLocation.distance - realDistance);
                previousLocation.copy(targetLocation);
                return new InteroBody1D(realDistance, deltaForce);
            }
            */
        }
    }
