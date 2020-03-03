using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;

#if UNITY_EDITOR
using input = GoogleARCore.InstantPreviewInput;
#endif

public class hw_ARController : MonoBehaviour
{
    // we will fill this list with the planes that arcore detected in the current frame
    private List<DetectedPlane> m_NewDetectedPlanes = new List<DetectedPlane>();

    public GameObject gridPrefab;
    public GameObject portal;
    public GameObject ARCamera;

    void Update()
    {
        // check arcore session status
        if (Session.Status != SessionStatus.Tracking) 
            return;
        
        // the following function will fill m_NewDetectedPlanes with the planes that ARCore detected in the current frame
        Session.GetTrackables<DetectedPlane>(m_NewDetectedPlanes, TrackableQueryFilter.New);

        // instantiate a Grid for each DetectedPlane in m_NewDetectedPlanes
        for (int i = 0; i < m_NewDetectedPlanes.Count; ++i) {
            GameObject grid = Instantiate(gridPrefab, Vector3.zero, Quaternion.identity, transform);

            // This function will set the position of grid and modify the verticles of the attached mesh
            grid.GetComponent<hw_GridVisualiser>().Initialize(m_NewDetectedPlanes[i]);
        }

        // check if the user touches the screen
        Touch touch;
        if (Input.touchCount < 1 || (touch = Input.GetTouch(0)).phase != TouchPhase.Began) 
            return;

        // let's now check if the user touched any of the tracked planes
        TrackableHit hit;

        if (Frame.Raycast(touch.position.x, touch.position.y, TrackableHitFlags.PlaneWithinPolygon, out hit)) {
            // let's now place the portal the portal on top of the tracked plane that we touched

            // enable the portal
            portal.SetActive(true);

            // create a new anchor
            Anchor anchor = hit.Trackable.CreateAnchor(hit.Pose);

            // set the position of the portal to be the same as the hit position
            portal.transform.position = hit.Pose.position;
            portal.transform.rotation = hit.Pose.rotation;

            // we want the portal to face the camera
            Vector3 cameraPosition = ARCamera.transform.position;

            // the portal should only rotate around the Y axis
            cameraPosition.y = hit.Pose.position.y;

            // rotate the portal to face the camera
            portal.transform.LookAt(cameraPosition, portal.transform.up);

            // arcore will keep understanding the world and update the anchors accordingly hence we need to attach our portal to the anchor
            portal.transform.parent = anchor.transform;
        }
    }
}
