using UnityEngine;
using System.IO;

public class RenderToImage : MonoBehaviour
{
    public Camera renderCamera; // Assign your camera here
    public RenderTexture renderTexture; // Assign your RenderTexture here
    public string savePath = "Assets/RenderedImages/"; // Path to save the images

    public void CaptureImage(string fileName)
    {
        // Set the camera's target texture
        RenderTexture currentRT = RenderTexture.active;
        RenderTexture.active = renderTexture;

        // Render the camera's view
        renderCamera.Render();

        // Create a Texture2D to store the RenderTexture data
        Texture2D image = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.RGBA32, false);
        image.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        image.Apply();

        // Save the image as a PNG
        byte[] bytes = image.EncodeToPNG();
        if (!Directory.Exists(savePath))
        {
            Directory.CreateDirectory(savePath);
        }
        File.WriteAllBytes(savePath + fileName + ".png", bytes);

        // Clean up
        RenderTexture.active = currentRT;
        Destroy(image);

        Debug.Log("Image saved to: " + savePath + fileName + ".png");
    }

    void Start()
    {
        CaptureImage("PoliceCap");
    }
}