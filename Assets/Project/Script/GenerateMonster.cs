using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;


public class GenerateMonster : MonoBehaviour
{

    public ARPlaneManager planeManager;
    public GameObject monster;

    private bool isMonsterPlaced;
    private void Start()
    {
         isMonsterPlaced = false;
    }

    // Update is called once per frame
    void Update()
    {
        foreach (ARPlane plane in planeManager.trackables)
        {
            if(!isMonsterPlaced && plane.alignment == PlaneAlignment.HorizontalUp)
            {
                isMonsterPlaced=true;
                Instantiate(monster, plane.transform.position, Quaternion.identity);
                HidePlaneDetection();
            }
        }
            
    }

    void HidePlaneDetection()
    {
        planeManager.enabled = false;
        foreach(ARPlane plane in planeManager.trackables)
            {
            plane.gameObject.SetActive(false);
        }
    }
}
