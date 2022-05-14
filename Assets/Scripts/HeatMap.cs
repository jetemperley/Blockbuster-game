using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using System.IO;

public class HeatMap : MonoBehaviour
{

    public int pixelsPerWorldUnit = 10;
    public Vector2 cameraSize;
    public float farplane;
    private Camera cam;
    private Texture2D tex;
    private Vector3 drawSize;
    // Start is called before the first frame update
    void Start()
    {
        buildHeatmap();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void buildHeatmap(){
        Vector2Int res = new Vector2Int((int)(cameraSize.x*pixelsPerWorldUnit), (int)(cameraSize.y*pixelsPerWorldUnit));
        tex = new Texture2D(res.x, res.y, TextureFormat.ARGB32, false);

        // set up camera
        cam = (new GameObject()).AddComponent<Camera>();
        cam.gameObject.name = "heatmap cam";
        cam.transform.position = transform.position;
        cam.transform.rotation = Quaternion.Euler(90, 0, 0);
        cam.orthographic = true;
        cam.aspect = cameraSize.x/cameraSize.y;
        cam.orthographicSize = cameraSize.y;
        cam.nearClipPlane = 0;
        cam.farClipPlane = farplane;
        cam.gameObject.SetActive(false);
        Debug.Log(cam.targetTexture);

        // set a render target
        RenderTexture currentTex = RenderTexture.active;
        RenderTexture newTex = new RenderTexture(res.x, res.y, 32, RenderTextureFormat.ARGB32);
        Debug.Log(newTex.format);
        Debug.Log(newTex.isReadable);
        RenderTexture.active = newTex;
        cam.targetTexture = newTex;

        // do render
        cam.Render();
        tex.ReadPixels(new Rect(0, 0, res.x, res.y), 0, 0);

        // revert active render texture
        RenderTexture.active = currentTex;

        // save file and cleanup
        byte[] bytes = tex.EncodeToPNG();
        File.WriteAllBytes("map.png", bytes);
        DestroyImmediate(cam.gameObject);
        newTex.Release();
    }

    private void OnDrawGizmosSelected() {
        if (drawSize == null)
            drawSize = new Vector3();
        drawSize.Set(cameraSize.x, farplane, cameraSize.y);
        Gizmos.DrawWireCube(transform.position + Vector3.down*farplane/2, drawSize);
    }
}
