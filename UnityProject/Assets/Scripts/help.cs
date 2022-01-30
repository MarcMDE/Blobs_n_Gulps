using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class help : MonoBehaviour
{
    [SerializeField] GameObject panel;
    [SerializeField] GameObject win;

    [SerializeField] Text blob, gulp;
    void Start()
    {
        panel.SetActive(false);
    }

    void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.H))
        {
            panel.SetActive(!panel.activeInHierarchy);
        }
        */

        if (Input.GetKeyDown(KeyCode.R))
            SceneManager.LoadScene(0);

        if (int.Parse(blob.text) > int.Parse(gulp.text)*3 || int.Parse(gulp.text) > int.Parse(blob.text) * 3)
        {
            panel.SetActive(true);
        }
    }
}
