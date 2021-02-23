using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class ClickClub : MonoBehaviour, ITrackableEventHandler
{
    private GameObject target;
    public GameObject videoplayer, club1, club2, club_intro, club_logo, background, back;
    public VideoClip clip1, clip2;

    public Material intro_club1, intro_club2, logo_club1, logo_club2;

    protected TrackableBehaviour mTrackableBehaviour;
	public void OnTrackableStateChanged(TrackableBehaviour.Status previousStatus,TrackableBehaviour.Status newStatus)
	{
		if (newStatus == TrackableBehaviour.Status.DETECTED ||
        newStatus == TrackableBehaviour.Status.TRACKED ||
        newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
	   	{

	        Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " found");

	        if (mTrackableBehaviour.TrackableName == "realkickkick")
	        {         
	        	videoplayer.GetComponent<VideoPlayer>().Play();
	        	videoplayer.GetComponent<VideoPlayer>().Pause();

	        }
	       OnTrackingFound();
	    }
	   else if (previousStatus == TrackableBehaviour.Status.TRACKED && newStatus == TrackableBehaviour.Status.NO_POSE)
	    {
	        Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " lost");
            videoplayer.SetActive(false);
            background.SetActive(false);
            club_intro.SetActive(false);
            club_logo.SetActive(false);
	        OnTrackingLost();
	    }
	    else
	    {
	        // For combo of previousStatus=UNKNOWN + newStatus=UNKNOWN|NOT_FOUND
	        // Vuforia is starting, but tracking has not been lost or found yet
	        // Call OnTrackingLost() to hide the augmentations
	        OnTrackingLost();
	    }

	}

    protected virtual void OnTrackingFound()
	{
	    var rendererComponents = GetComponentsInChildren<Renderer>(true);
	    var colliderComponents = GetComponentsInChildren<Collider>(true);
	    var canvasComponents = GetComponentsInChildren<Canvas>(true);

	    // Enable rendering:
	    foreach (var component in rendererComponents)
	        component.enabled = true;

	    // Enable colliders:
	    foreach (var component in colliderComponents)
	        component.enabled = true;

	    // Enable canvas':
	    foreach (var component in canvasComponents)
	        component.enabled = true;
	}


	protected virtual void OnTrackingLost()
	{
	    var rendererComponents = GetComponentsInChildren<Renderer>(true);
	    var colliderComponents = GetComponentsInChildren<Collider>(true);
	    var canvasComponents = GetComponentsInChildren<Canvas>(true);

	    // Disable rendering:
	    foreach (var component in rendererComponents)
	        component.enabled = false;

	    // Disable colliders:
	    foreach (var component in colliderComponents)
	        component.enabled = false;

	    // Disable canvas':
	    foreach (var component in canvasComponents)
	        component.enabled = false;
	}
    // Start is called before the first frame update
    void Start()
    {
	     mTrackableBehaviour = GetComponent<TrackableBehaviour>();
	    if (mTrackableBehaviour)
	    {
	        mTrackableBehaviour.RegisterTrackableEventHandler(this);
	    }   

        background.SetActive(false);
        videoplayer.SetActive(false);
        club_intro.SetActive(false);
        club_logo.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Escape))
        {
            if(background.activeSelf == true)
            {
                background.SetActive(false);
                videoplayer.SetActive(false);
                club_intro.SetActive(false);
                club_logo.SetActive(false);
            }else
            {
                SceneManager.LoadScene("ClubCategory", LoadSceneMode.Single);
            }
        }

        if(Input.GetMouseButtonDown(0))
        {
            target = GetClickedObject();

            if(target.Equals(club1))
            {
                videoplayer.GetComponent<VideoPlayer>().clip = clip1;
                club_intro.GetComponent<MeshRenderer>().material = intro_club1;
                club_logo.GetComponent<MeshRenderer>().material = logo_club1;
            }

            if(target.Equals(club2))
            {
                videoplayer.GetComponent<VideoPlayer>().clip = clip2;
                club_intro.GetComponent<MeshRenderer>().material = intro_club2;
                club_logo.GetComponent<MeshRenderer>().material = logo_club2;
            }

            if(target.Equals(back))
            {
                if(background.activeSelf == true)
                {
                    background.SetActive(false);
                    videoplayer.SetActive(false);
                    club_intro.SetActive(false);
                    club_logo.SetActive(false);
                }else
                {
                    SceneManager.LoadScene("ClubCategory", LoadSceneMode.Single);
                }
            }
        }
    }

    private GameObject GetClickedObject()
    {
        GameObject target = null;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if((Physics.Raycast(ray.origin, ray.direction*10, out hit)))
        {
            target = hit.collider.gameObject;

            if(!target.Equals(back))
            {
                background.SetActive(true);
                videoplayer.SetActive(true);
                club_intro.SetActive(true);
                club_logo.SetActive(true);
            }
        }

        return target;
    }
}
