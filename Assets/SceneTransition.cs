using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour {


    public Animator transitionAnim;
    public Animator textAnim;
    public string sceneName;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if(Input.anyKeyDown)
        {
            StartCoroutine(LoadScene());
        }
	}

    IEnumerator LoadScene()
    {
        transitionAnim.SetTrigger("end");
        textAnim.SetTrigger("end");
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(sceneName);
    }
}
