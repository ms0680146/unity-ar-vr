# ARTOVR
It is my project of computer animation course in NCCU! 
### Demo Website!
http://imlab3.cs.nccu.edu.tw/~105257029/
### Tools: <br>
Unity5.6 / Vuforia-unity-6-2-10 / ovr_unity_utilities_1.13.0 / Gear VR HMD <br>
### Introduction: <br>
I want to make an AR to VR experience through [Vuforia](https://developer.vuforia.com/downloads/samples)! <br>
Demo for the AR/VR Sample.<br>
https://www.youtube.com/watch?v=rLIVKO3YFos<br>
Integrating Gear VR and the AR/VR Sample in Unity 5.3 and above<br>
https://library.vuforia.com/articles/Solution/Integrating-Gear-VR-and-the-AR-VR-Sample-in-Unity-5-3-and-above.html

#### Step1-Using Vuforia with Unity 5.3's Built-in VR Functionality and Oculus Utilities:
Unity VR with Oculus Utilities for Unity 5: Enable Unity's VR and link the OVRCameraRig prefab to the ARCamera and have access to additional Oculus-specific APIs.<br>
1. Download the Oculus Utilities for Unity 5
2. Create a new Unity project and import the following:
 * arvr-x-y-z.unitypackage (In Vuforia Develop -> Samples -> Digital Eyewear -> Dowload for unity!) <br>
 * OculusUtilities.unitypackage (ovr_unity_utilities_1.13.0) 
3. Open the Vuforia-3-AR-VR.unity scene
4. Open the Android Player Settings (File > Build Settings... > Player Settings) and enable the following checkbox options:
 * Multithreaded Rendering
 * Virtual Reality Supported <br>
![image](https://vuforialibrarycontent.vuforia.com/Images/December2015/Unity53/OculusUtilitiesVRSupported.png)
5. Drag an instance of OVRCameraRig to the Hierarchy and position at [0, 2, -1].
6. Now configure the CenterEyeAnchor fields:
 * Clear Flags = Solid Color
 * Background = Black
 * Clipping Planes [Near] = 0.05 *
 * Clipping Planes [Far] = 300 *
 * TargetEye = Left
7. Duplicate the CenterEyeAnchor, name it CenterRightEyeAnchor, and set the following:
 * TargetEye = Right
We recommend Near = 0.05 and Far = 300 for AR/VR scenes, but you can adjust to suit your 3D scene.<br>
![image](https://vuforialibrarycontent.vuforia.com/Images/December2015/Unity53/OculusUtilitiesCameraSettings.png)<br>
8. Add two new Empty GameObjects (VuforiaCenterAnchor & TrackableParent) to the existing OVRCameraRig in the Vuforia-3-AR-VR scene as shown in the partial hierarchical outline below (screenshot shows full rig hierarchy):
 * TrackableParent	eGO	x=0, y=0, z=0
 * VuforiaCenterAnchor	eGO	x=0, y=0, z=0
 * eGO = Empty GameObject
 * Make all targets (i.e. Trackables) in the scene children of the TrackableParent object. In the AR-VR sample, the Trackable is the ImageTargetStones GameObject.<br>
![image](https://vuforialibrarycontent.vuforia.com/Images/December2015/Unity53/OculusUtilitiesImageTargetStones.png) <br>
9. Attach the VRIntegrationHelper.cs script to both the CenterEyeAnchor and CenterRightEyeAnchor
10. On the CenterEyeAnchor, enable the checkbox Is Left and drag the TrackableParent object onto the Trackable Parent property in the Inspector.<br>
![image](https://vuforialibrarycontent.vuforia.com/Images/December2015/Unity53/OculusUtiltiesVRIntegrationHelper.png) <br>

#### Step2-Camera Rig Binding Steps for the Two Integration Techniques:
1. Select the ARCamera in the Hierarchy
2. Vuforia 6.2 in the VuforiaConfiguration asset set the following properties:
 * Eyewear Type = Video See-Through
 * Stereo Camera Config = Gear VR (Oculus)<br>
 ![image](https://vuforialibrarycontent.vuforia.com/Images/Vuforia55/Gear/Vuforia-5-5-NativeVR-ARCamera-binding.png) <br>
3. Drag the following GameObjects in the Hierarchy to the respective fields:
 * VuforiaCenterAnchor to Central Anchor Point
 * CenterEyeAnchor (Oculus) to Left Camera
 * CenterRightEyeAnchor (Oculus) to Right Camera<br>
![image](https://vuforialibrarycontent.vuforia.com/Images/December2015/Unity53/OculusUtiltiesAnchorPoints.png) <br>
4. Click the Add Vuforia Components button that will appear under the Left Camera and also under Right Camera fields in the Inspector.

#### Step3-Additional Build Details
Place Oculus Signature Files (OSIGs) in the following Unity project location: Assets/Plugins/Android/assets/

#### Step4-VRIntegrationHelper.cs OnPreRender()
Open the VRIntegrationHelper.cs script located in Assets/Vuforia/Scripts/Utilities and replace the code in OnPreRender() with the following:
```C#
void OnPreRender()
{
    // on pre render is where projection matrix and pixel rect 
    // are set up correctly (for each camera individually)
    // so we use this to acquire this data.
     
    if (IsLeft && !mLeftCameraDataAcquired)
    {
        if (
            !float.IsNaN(mLeftCamera.projectionMatrix[0,0]) &&
            !float.IsNaN(mLeftCamera.projectionMatrix[0,1]) &&
            !float.IsNaN(mLeftCamera.projectionMatrix[0,2]) &&
            !float.IsNaN(mLeftCamera.projectionMatrix[0,3]) &&
            !float.IsNaN(mLeftCamera.projectionMatrix[1,0]) &&
            !float.IsNaN(mLeftCamera.projectionMatrix[1,1]) &&
            !float.IsNaN(mLeftCamera.projectionMatrix[1,2]) &&
            !float.IsNaN(mLeftCamera.projectionMatrix[1,3]) &&
            !float.IsNaN(mLeftCamera.projectionMatrix[2,0]) &&
            !float.IsNaN(mLeftCamera.projectionMatrix[2,1]) &&
            !float.IsNaN(mLeftCamera.projectionMatrix[2,2]) &&
            !float.IsNaN(mLeftCamera.projectionMatrix[2,3]) &&
            !float.IsNaN(mLeftCamera.projectionMatrix[3,0]) &&
            !float.IsNaN(mLeftCamera.projectionMatrix[3,1]) &&
            !float.IsNaN(mLeftCamera.projectionMatrix[3,2]) &&
            !float.IsNaN(mLeftCamera.projectionMatrix[3,3])
           )
        {
            mLeftCameraMatrixOriginal = mLeftCamera.projectionMatrix;
            mLeftCameraPixelRect = mLeftCamera.pixelRect;
            mLeftCameraDataAcquired = true;
        }
    }
    else if (!mRightCameraDataAcquired)
    {
        if (
            !float.IsNaN(mRightCamera.projectionMatrix[0,0]) &&
            !float.IsNaN(mRightCamera.projectionMatrix[0,1]) &&
            !float.IsNaN(mRightCamera.projectionMatrix[0,2]) &&
            !float.IsNaN(mRightCamera.projectionMatrix[0,3]) &&
            !float.IsNaN(mRightCamera.projectionMatrix[1,0]) &&
            !float.IsNaN(mRightCamera.projectionMatrix[1,1]) &&
            !float.IsNaN(mRightCamera.projectionMatrix[1,2]) &&
            !float.IsNaN(mRightCamera.projectionMatrix[1,3]) &&
            !float.IsNaN(mRightCamera.projectionMatrix[2,0]) &&
            !float.IsNaN(mRightCamera.projectionMatrix[2,1]) &&
            !float.IsNaN(mRightCamera.projectionMatrix[2,2]) &&
            !float.IsNaN(mRightCamera.projectionMatrix[2,3]) &&
            !float.IsNaN(mRightCamera.projectionMatrix[3,0]) &&
            !float.IsNaN(mRightCamera.projectionMatrix[3,1]) &&
            !float.IsNaN(mRightCamera.projectionMatrix[3,2]) &&
            !float.IsNaN(mRightCamera.projectionMatrix[3,3])
           )
        {
            mRightCameraMatrixOriginal = mRightCamera.projectionMatrix;
            mRightCameraPixelRect = mRightCamera.pixelRect;
            mRightCameraDataAcquired = true;
        }
    }
}
```
### Reference: <br>
Unite 2015 - Creating Mixed Reality Apps with Vuforia 5<br>
https://www.youtube.com/watch?v=RdDAjgfj_Yw
