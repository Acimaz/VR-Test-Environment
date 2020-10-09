using System.Collections;
using System.Collections.Generic;
using OculusSampleFramework;
using UnityEngine;

public class TestSceneSetup : MonoBehaviour
{
    [SerializeField] GameObject leftHandPrefab, rightHandPrefab;
    
    HandsManager handsManager;
    Transform leftHandAnchor, rightHandAnchor;
    Transform originalLeftHand, originalRightHand;
    GameObject leftHand, rightHand;
    
#if UNITY_EDITOR

    void FindReferences()
    {
        if (handsManager == null)
        {
            handsManager = GameObject.FindObjectOfType<HandsManager>();
        }

        if (leftHandAnchor != null && rightHandAnchor != null && leftHand != null && rightHand != null)
        {
            return;
        }
        GameObject[] all = Resources.FindObjectsOfTypeAll<GameObject>();
        foreach (GameObject o in all)
        {
            if (!o.scene.isLoaded)
            {
                continue;
            }

            if (o.name == "LeftHandAnchor")
            {
                leftHandAnchor = o.transform;
            }
            if (o.name == "RightHandAnchor")
            {
                rightHandAnchor = o.transform;
            }
            if (o.name == "OVRCustomHandPrefab_L")
            {
                originalLeftHand = o.transform;
            }
            if (o.name == "OVRCustomHandPrefab_R")
            {
                originalRightHand = o.transform;
            }
            if (o.name == "Left Hand Prefab")
            {
                leftHand = o;
            }
            if (o.name == "Right Hand Prefab")
            {
                rightHand = o;
            }
        }
    }
    
    public void SetupHands()
    {
        FindReferences();
        if (leftHand != null)
        {
            Debug.Log("Left hand is already setup and existing. GameObject name in scene: " + leftHand.name);
            Debug.Log("If you want to setup a new hand, delete the current hand in scene or push the 'Reset hands' button");
        }
        else if (leftHandPrefab != null)
        {
            leftHand = Instantiate(leftHandPrefab, leftHandAnchor);
            leftHand.name = "Left Hand Prefab";
            leftHand.transform.localPosition = Vector3.zero;
            leftHand.AddComponent<OVRHand>().TestSetup(OVRHand.Hand.HandLeft);
            OVRCustomSkeleton skeleton = leftHand.AddComponent<OVRCustomSkeleton>();
            skeleton.TestSetup(OVRSkeleton.SkeletonType.HandLeft, true);
            skeleton.TryAutoMapBonesByName();
        }
        else
        {
            Debug.Log("Left prefab reference is not set yet. Skipping left hand setup");
        }

        if (rightHand != null)
        {
            Debug.Log("Right hand is already setup and existing. GameObject name in scene: " + rightHand.name);
            Debug.Log("If you want to setup a new hand, delete the current hand in scene or push the 'Reset hands' button");
        }
        else if (rightHandPrefab != null)
        {
            rightHand = Instantiate(rightHandPrefab, rightHandAnchor);
            rightHand.name = "Right Hand Prefab";
            rightHand.transform.localPosition = Vector3.zero;
            rightHand.AddComponent<OVRHand>().TestSetup(OVRHand.Hand.HandRight);
            OVRCustomSkeleton skeleton = rightHand.AddComponent<OVRCustomSkeleton>();
            skeleton.TestSetup(OVRSkeleton.SkeletonType.HandRight, true);
            skeleton.TryAutoMapBonesByName();
        }
        else
        {
            Debug.Log("Right prefab reference is not set yet. Skipping right hand setup");
        }
        SetupHandsManager();
    }

    void SetupHandsManager()
    {
        FindReferences();
        SkinnedMeshRenderer leftHandMeshRenderer = null;
        if (leftHand != null)
        {
            leftHandMeshRenderer = leftHand.GetComponentInChildren<SkinnedMeshRenderer>();
        }

        SkinnedMeshRenderer rightHandMeshRenderer = null;
        if (rightHand != null)
        {
            rightHandMeshRenderer = rightHand.GetComponentInChildren<SkinnedMeshRenderer>();
        }
        handsManager.TestSetup(leftHand, rightHand, leftHandMeshRenderer, rightHandMeshRenderer);
    }

    void ResetHandsManager()
    {
        FindReferences();
        SkinnedMeshRenderer leftHandMeshRenderer = originalLeftHand?.GetComponentInChildren<SkinnedMeshRenderer>();
        SkinnedMeshRenderer rightHandMeshRenderer = originalRightHand?.GetComponentInChildren<SkinnedMeshRenderer>();
        handsManager.TestSetup(originalLeftHand.gameObject, originalRightHand.gameObject, leftHandMeshRenderer, rightHandMeshRenderer);
    }

    public void ResetHands()
    {
        FindReferences();
        DestroyImmediate(leftHand);
        DestroyImmediate(rightHand);
        leftHand = null;
        rightHand = null;
        ResetHandsManager();
        Debug.Log("Destroyed hands in scene");
    }

    public void ToggleOriginalHands()
    {
        FindReferences();
        originalLeftHand.gameObject.SetActive(!originalLeftHand.gameObject.activeInHierarchy);
        originalRightHand.gameObject.SetActive(!originalRightHand.gameObject.activeInHierarchy);
    }
#endif
}
