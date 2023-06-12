using UnityEngine;

namespace Enemy
{
    public static class PhysicsDebug
    {
        public static void DebugLineSpere(Vector3 worldPosition, float radius, float seconds)
        {
            DrawRay(Vector3.forward);
            DrawRay(Vector3.back);
            DrawRay(Vector3.left);
            DrawRay(Vector3.right);
            DrawRay(Vector3.up);
            DrawRay(Vector3.down);
            
            void DrawRay(Vector3 direction) =>
                Debug.DrawRay(worldPosition, direction * radius, Color.red, seconds);
        }
    }
}