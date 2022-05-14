using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

using System.IO;
using System;

public class HeatMap : MonoBehaviour
{

    public int pixelsPerWorldUnit = 10;
    public Vector2 cameraSize;
    public float farplane;
    private Camera cam;
    private Texture2D tex;
    private Vector3 drawSize;
    public Color heatColor;
    public float heatAlpha = 0.5f;
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
        cam.orthographicSize = cameraSize.y/2;
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

        // get the file with position data and parse it

        List<Vector3> vecs = new List<Vector3>();
        if (File.Exists(PlayerStats.getInst().getPosFilename())){
            string[] lines = File.ReadAllLines(PlayerStats.getInst().getPosFilename());
            foreach (string line in lines){
                
                string[] pos = line.Split(",");
                foreach (string posn in pos){
                    string[] nums = posn.Split(" ");
                    if (nums.Length != 3)
                        continue;
                    try {
                    vecs.Add(new Vector3(
                        float.Parse(nums[0]),
                        float.Parse(nums[1]),
                        float.Parse(nums[2])
                        ));
                        Debug.Log("parsed " + vecs[vecs.Count-1]);
                    } catch (Exception e){
                        Debug.Log("failed to parse: " + posn);
                    }
                    
                }
            }
        }

        float zMax = transform.position.z + cameraSize.y/2;
        float zMin = transform.position.z - cameraSize.y/2;
        float xMax = transform.position.x + cameraSize.x/2;
        float xMin = transform.position.x - cameraSize.x/2;



        foreach (Vector3 v in vecs){
            // translate the position to be relative the map
            Vector3 relative = v - new Vector3(xMin, 0, zMin);
            Vector3Int relInt = new Vector3Int((int)relative.x, (int)relative.y, (int)relative.z);
            relInt = relInt*pixelsPerWorldUnit;

            // get the world unit relating to the death
            try {
                Color[] pixels = tex.GetPixels(relInt.x, relInt.z, pixelsPerWorldUnit, pixelsPerWorldUnit);

                for (int i = 0; i < pixels.Length; i++){
                    pixels[i] = heatAlpha*heatColor + (1-heatAlpha)*pixels[i];
                    pixels[i].a = 1;
                }

                tex.SetPixels(relInt.x, relInt.z, pixelsPerWorldUnit, pixelsPerWorldUnit, pixels);
            } catch (Exception e){
                Debug.Log("some position was omitted, probably out of camera bounds");
            }


        }

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
