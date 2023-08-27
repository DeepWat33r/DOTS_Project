using UnityEngine;
using UnityEditor;
using System.IO;
using UnityEditor.ProjectWindowCallback;

public class CreateECSComponent
{
    [MenuItem("Assets/Create/ECS Component", false, 80)]
    public static void CreateComponent()
    {
        string path = AssetDatabase.GetAssetPath(Selection.activeObject);
        if (string.IsNullOrEmpty(path))
        {
            path = "Assets";
        }

        string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath(path + "/NewECSComponent.cs");

        ProjectWindowUtil.StartNameEditingIfProjectWindowExists(
            0,
            ScriptableObject.CreateInstance<DoCreateECSComponent>(),
            assetPathAndName,
            null,
            null);
    }

    private class DoCreateECSComponent : EndNameEditAction
    {
        public override void Action(int instanceId, string pathName, string resourceFile)
        {
            string name = Path.GetFileNameWithoutExtension(pathName);
            string content = $@"using System;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
            
public struct {name} : IComponentData
{{
            
}}
";
            File.WriteAllText(pathName, content);
            AssetDatabase.ImportAsset(pathName);
        }
    }
}