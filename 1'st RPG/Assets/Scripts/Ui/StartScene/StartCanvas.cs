using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

public class StartCanvas : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void ClickStartBtn()
    {
        SceneManager.LoadScene(1);
    }
    public void ClickExitBtn()
    {
        Application.Quit();
    }
}
