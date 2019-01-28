using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaths {

    public bool IsInRange(float n, float a, float b) {
        if (n >= a && n <= b) return true;
        return false;
    }

    public double CalculateAngle(float x, float y) {
        double angle = Mathf.Atan2(y, x);
        angle = angle * (180.0 / Mathf.PI);
        return angle;
    }

    public float[] CollisionAngles(Collision2D collision, Vector3 lastFrameVelocity) {
        float[] angles = new float[2];
        float surfaceAngle;
        float collisionAngle;

        Vector3 normal = collision.contacts[0].normal;
        collisionAngle = Vector3.Angle(lastFrameVelocity, normal);
        surfaceAngle = Vector3.Angle(new Vector3(1, 0, 0), normal);
        collisionAngle = collisionAngle % 90;
        surfaceAngle = Mathf.Abs((float)surfaceAngle % 360);

        //Primer elemento: ángulo de la superfície
        //Segundo elemento: ángulo de colisión
        angles[0] = surfaceAngle;
        angles[1] = collisionAngle;
        return angles;
    }

    public bool IsWall(float n) {
        return IsInRange(n, 350, 0) || IsInRange(n, 0, 10) || IsInRange(n, 170, 190);
    }

    public bool IsFloor(float n) {
        return IsInRange(n, 45, 135) || IsInRange(n, 225, 315);
    }
}
