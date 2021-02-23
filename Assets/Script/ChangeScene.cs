using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    void Update()
    {
        if(Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
        
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                if(hit.collider.CompareTag("Art"))
                {
                    SceneManager.LoadScene("ArtClubList", LoadSceneMode.Single);
                }
                if(hit.collider.CompareTag("Sport"))
                {
                    SceneManager.LoadScene("SportClubList");
                }
                if(hit.collider.CompareTag("Social"))
                {
                    SceneManager.LoadScene("SocialClubList");
                }
                if(hit.collider.CompareTag("Music"))
                {
                    SceneManager.LoadScene("MusicClubList");
                }
                if(hit.collider.CompareTag("Volunteer"))
                {
                    SceneManager.LoadScene("VolunteerClubList");
                }
                if(hit.collider.CompareTag("Religion"))
                {
                    SceneManager.LoadScene("ReligionClubList");
                }
                if(hit.collider.CompareTag("Science"))
                {
                    SceneManager.LoadScene("ScienceClubList");
                }
                if(hit.collider.CompareTag("Finish"))
                {
                    Application.Quit();
                }

            }
        }
    }
}
