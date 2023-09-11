using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyScreenShot : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Camera _camera;
    List<Vector3> diractions = new List<Vector3>()
    {
        Vector3.forward, -Vector3.forward, Vector3.right, -Vector3.right, Vector3.up, -Vector3.up
    };
    private void Start()
    {
        StartCoroutine(TakeScreenShot());
    }

    IEnumerator TakeScreenShot()
    {
        for (int i = 0; i < diractions.Count; i++)
        {
            transform.LookAt(transform.position+diractions[i]);
            yield return new WaitForEndOfFrame();
            RenderTexture screenTexture = new RenderTexture(Screen.width, Screen.height, 16);
            _camera.targetTexture = screenTexture;
            RenderTexture.active = screenTexture;
            _camera.Render();
            Texture2D renderedTexture = new Texture2D(Screen.width, Screen.height);
            renderedTexture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
            RenderTexture.active = null;
            byte[] byteArray = renderedTexture.EncodeToPNG();
            System.IO.File.WriteAllBytes(Application.dataPath + $"/cameracapture{i}.png", byteArray);
            print("Done");
        }
    }

}
