using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;

public class captureSceen : MonoBehaviour
{
    private bool isCreateFolder = false;

    private void OnValidate()
    {
        string folderPath = "Assets/InfoGame/"; // the path of your project folder
        if (!System.IO.Directory.Exists(folderPath)) // if this path does not exist yet
        {
            AssetDatabase.CreateFolder("Assets", "InfoGame");
            AssetDatabase.Refresh();
        }   
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        { // capture screen shot on left mouse button down

            zxczx();
        }
    }

		[ContextMenu("cap screen")]
    void zxczx()
    {
            string folderPath = "Assets/InfoGame/"; // the path of your project folder

            if (!System.IO.Directory.Exists(folderPath)) // if this path does not exist yet
                System.IO.Directory.CreateDirectory(folderPath);  // it will get created

            var screenshotName =
                                    "Screenshot_" +
                                    System.DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss") + // puts the current time right into the screenshot name
                                    ".png"; // put youre favorite data format here
            ScreenCapture.CaptureScreenshot(System.IO.Path.Combine(folderPath, screenshotName), 1); // takes the sceenshot, the "2" is for the scaled resolution, you can put this to 600 but it will take really long to scale the image up
            AssetDatabase.Refresh();
            Debug.Log("Capture Screenshot Name " + screenshotName);
    }
}
#endif
