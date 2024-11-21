using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tester : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var sth = Modules.SaveLoadSystem.Settings.GetOrCreateSettings();
        print(sth.GetSaveFilePath());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
