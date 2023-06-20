using Logic.EnemySpawners;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SpawnMarker))]
public class SpawnMarkerEditor : UnityEditor.Editor
{
    private static float SphereRadius = 0.5f;

    [DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NonSelected)]
    public static void RenderStaticGizmo(SpawnMarker spawner, GizmoType gizmoType)
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(spawner.transform.position, SphereRadius);
    }
}