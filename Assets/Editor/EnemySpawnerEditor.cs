using Logic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(EnemySpawner))]
public class EnemySpawnerEditor : UnityEditor.Editor
{
    private static float SphereRadius = 0.5f;

    [DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NonSelected)]
    public static void RenderStaticGizmo(EnemySpawner spawner, GizmoType gizmoType)
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(spawner.transform.position, SphereRadius);
    }
}
