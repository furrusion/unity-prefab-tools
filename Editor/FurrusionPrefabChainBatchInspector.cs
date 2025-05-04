// MIT License
//
// Copyright (c) 2025 Furrusion
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

using UnityEditor;
using UnityEngine;
using System.Text;
using System.IO;

public class FurrusionPrefabChainBatchInspector : EditorWindow
{
    [MenuItem("Furrusion/Tools/Prefab Variant Chain/Batch Inspection")]
    public static void ShowWindow()
    {
        GetWindow<FurrusionPrefabChainBatchInspector>("Furrusion Prefab Chain Batch Inspector");
    }

    private Vector2 scroll;
    private string chainResult = "";

    private void OnGUI()
    {
        GUILayout.Label("Inspect All Prefab Chains", EditorStyles.boldLabel);

        if (GUILayout.Button("Inspect All Prefabs In Project"))
        {
            InspectAllPrefabs();
        }

        if (!string.IsNullOrEmpty(chainResult) && GUILayout.Button("Save All Chains to File"))
        {
            SaveChainToFile();
        }

        GUILayout.Space(10);
        scroll = EditorGUILayout.BeginScrollView(scroll);
        EditorGUILayout.TextArea(chainResult, GUILayout.ExpandHeight(true));
        EditorGUILayout.EndScrollView();
    }

    void InspectAllPrefabs()
    {
        chainResult = "All Prefab Chains:\n";
        StringBuilder sb = new StringBuilder();

        string[] prefabGuids = AssetDatabase.FindAssets("t:Prefab");
        foreach (string guid in prefabGuids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);
            if (prefab != null)
            {
                sb.AppendLine($"{prefab.name}.prefab:");
                GameObject current = prefab;
                while (current != null)
                {
                    var basePrefab = PrefabUtility.GetCorrespondingObjectFromSource(current);
                    if (basePrefab != null)
                    {
                        sb.AppendLine($"    {current.name} -> {basePrefab.name}");
                        current = basePrefab;
                    }
                    else
                    {
                        sb.AppendLine($"    {current.name} -> <Root Prefab>");
                        current = null;
                    }
                }
                sb.AppendLine();
            }
        }

        chainResult = sb.ToString();
    }

    void SaveChainToFile()
    {
        string folderPath = Path.Combine(Application.dataPath, "PrefabChains");
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        string filePath = Path.Combine(folderPath, "AllPrefabs_chain.txt");
        File.WriteAllText(filePath, chainResult);

        Debug.Log($"All prefab chains saved to: {filePath}");
        EditorUtility.RevealInFinder(filePath);
    }
}
