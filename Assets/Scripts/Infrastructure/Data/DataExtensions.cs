using Infrastructure.Services.PersistantProgress;
using UnityEditor;
using UnityEngine;

namespace Infrastructure.Data
{
    public static class DataExtensions
    {
        public static Vector3Data AsVectorData(this Vector3 vector) => 
            new Vector3Data(vector.x, vector.y, vector.z);

        public static Vector3 AsUnityVector(this Vector3Data vectorData) => 
            new Vector3(vectorData.X, vectorData.Y, vectorData.Z);

        public static T ToDeserialized<T>(this string json) => 
            JsonUtility.FromJson<T>(json);

        public static string ToJson(this object progress)
        {
            return JsonUtility.ToJson(progress);
        }

        public static Vector3 AddVector(this Vector3 vector, Vector3 vectorToadd)
        {
            vector.x += vectorToadd.x;
            vector.y += vectorToadd.y;
            vector.z += vectorToadd.z;

            return vector;
        }
    }
}