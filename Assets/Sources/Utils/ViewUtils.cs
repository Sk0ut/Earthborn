using UnityEngine;

public class ViewUtils
{
    public static bool IsInViewport(Camera camera, Transform transform)
    {
        var screenPoint = camera.WorldToViewportPoint(transform.position);
        var onScreen = screenPoint.x >= 0 &&
                       screenPoint.x <= 1 &&
                       screenPoint.y >= 0 &&
                       screenPoint.y <= 1;
        return onScreen;
    }
}