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

public class FurrusionPrefabChainSingleInspector : EditorWindow
{
    [MenuItem("Furrusion/Tools/Prefab Variant Chain/Prefab Inspector")]
    public static void ShowWindow()
    {
        GetWindow<FurrusionPrefabChainSingleInspector>("Furrusion Prefab Chain Single Inspector");
    }

    private GameObject selected;
    private Vector2 scroll;
    private string chainResult = "";

    private void OnGUI()
    {
        GUILayout.Label("Select a Prefab Variant", EditorStyles.boldLabel);

        selected = (GameObject)EditorGUILayout.ObjectField("Prefab Object", selected, typeof(GameObject), false);

        if (selected != null && GUILayout.Button("Inspect Chain"))
        {
            ShowChain(selected);
        }

        if (!string.IsNullOrEmpty(chainResult) && GUILayout.Button("Save Chain to File"))
        {
            SaveChainToFile();
        }

        GUILayout.Space(10);
        scroll = EditorGUILayout.BeginScrollView(scroll);
        EditorGUILayout.TextArea(chainResult, GUILayout.ExpandHeight(true));
        EditorGUILayout.EndScrollView();
    }

    void ShowChain(GameObject obj)
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("Prefab Chain:");

        GameObject current = obj;
        while (current != null)
        {
            var basePrefab = PrefabUtility.GetCorrespondingObjectFromSource(current);
            if (basePrefab != null)
            {
                sb.AppendLine($"{current.name} -> {basePrefab.name}");
                current = basePrefab;
            }
            else
            {
                sb.AppendLine($"{current.name} -> <Root Prefab>");
                current = null;
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

        string filePath = Path.Combine(folderPath, selected != null ? $"{selected.name}_chain.txt" : "SinglePrefab_chain.txt");
        File.WriteAllText(filePath, chainResult);

        Debug.Log($"Prefab chain saved to: {filePath}");
        EditorUtility.RevealInFinder(filePath);
    }
}
