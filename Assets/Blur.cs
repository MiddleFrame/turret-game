using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blur 
{
    public static StaticBluredScreen blur;
    public static void EnableOrDisableBlur(bool active)
    {
       if(blur!=null)
        if(active)
        blur.Capture();
        else
        blur.Release();
    }
}
