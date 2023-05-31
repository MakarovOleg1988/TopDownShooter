using UnityEngine;
using UnityEditor;

namespace Assets.Scripts.Editor
{
    public class MeshInfo : EditorWindow
    {
        private int vertexCount;
        private int submeshCount;
        private int triangleCount;

        [MenuItem("Tools/Mesh Info")]
        static void Init()
        {
            MeshInfo window = (MeshInfo)EditorWindow.GetWindow(typeof(MeshInfo));
            window.titleContent.text = "Mesh Info";
        }

        void OnSelectionChange()
        {
            Repaint();
        }

        void OnGUI()
        {
            vertexCount = 0;
            triangleCount = 0;
            submeshCount = 0;

            foreach (GameObject g in Selection.gameObjects)
            {
                if (g.GetComponent<MeshFilter>())
                {
                    vertexCount += g.GetComponent<MeshFilter>().sharedMesh.vertexCount;
                    triangleCount += g.GetComponent<MeshFilter>().sharedMesh.triangles.Length / 3;
                    submeshCount += g.GetComponent<MeshFilter>().sharedMesh.subMeshCount;
                }
            }

            EditorGUILayout.LabelField("Vertices: ", vertexCount.ToString());
            EditorGUILayout.LabelField("Triangles: ", triangleCount.ToString());
            EditorGUILayout.LabelField("SubMeshes: ", submeshCount.ToString());
        }
    }
}
