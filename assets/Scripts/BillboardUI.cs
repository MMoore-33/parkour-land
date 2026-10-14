using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class BillboardUI : MonoBehaviour
{
    void Update()
    {
        Camera cam = null;

        if (Application.isPlaying)
        {
            cam = Camera.main;
        }
#if UNITY_EDITOR
        else
        {
            cam = SceneView.lastActiveSceneView.camera;
        }
#endif

        if (cam != null)
        {
            transform.LookAt(cam.transform);
            transform.Rotate(0, 180f, 0);
        }
    }
}