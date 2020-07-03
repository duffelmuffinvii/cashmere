using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class Reader : MonoBehaviour
{

    protected FileInfo sourceFile = null;
    protected StreamReader fileReader = null;
    public string lineReader = " "; 

    public string NextLine
    {
        get
        {
            return this.lineReader;

        }
        set
        {

            NextLine = value;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        sourceFile = new FileInfo("Assets/source.txt");
        fileReader = sourceFile.OpenText();
        if (lineReader != null)
        {
            lineReader = fileReader.ReadLine();
        }                                                                                                               
    }

    // Update is called once per frame
    void Update()
    {
        if (lineReader != null)
        {
            lineReader = fileReader.ReadLine();
        }
        


    }
}
