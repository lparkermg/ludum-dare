using System;
using UnityEngine;
using UnityEditor;
using System.IO;

[InitializeOnLoad]
public static class BuildIncrementor
{
    static BuildIncrementor()
    {
        EditorApplication.update += RunOnce;
    }

    static void RunOnce()
    {
        EditorApplication.update-= RunOnce;
        ReadAndIncrementBuild();
    }

    static void ReadAndIncrementBuild()
    {
        string buildNumberFile = "buildNum.txt";

        if (!File.Exists(buildNumberFile))
            File.Create(buildNumberFile);

        var buildNumberText = File.ReadAllText(buildNumberFile);

        if (string.IsNullOrWhiteSpace(buildNumberText))
            buildNumberText = "0";

        var buildNumber = int.Parse(buildNumberText);

        buildNumber++;

        File.WriteAllText(buildNumberFile,buildNumber.ToString());
    }
}