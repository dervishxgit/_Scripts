using UnityEngine;
using System.Collections;
using System.Collections.Generic;
 
public class ReadSceneNames : MonoBehaviour
{
//    public static string[] _AllScenes_;
//    #if UNITY_EDITOR
//    private static string[] ReadNames()
//    {
//        List<string> temp = new List<string>();
//        foreach (UnityEditor.EditorBuildSettingsScene S in UnityEditor.EditorBuildSettings.scenes)
//        {
//            if (S.enabled)
//            {
//                string name = S.path.Substring(S.path.LastIndexOf('/')+1);
//                name = name.Substring(0,name.Length-6);
//                temp.Add(name);
//            }
//        }
//        return temp.ToArray();
//    }
//    [UnityEditor.MenuItem("CONTEXT/ReadSceneNames/Update Scene Names")]
//    private static void UpdateNames(UnityEditor.MenuCommand command)
//    {
//        ReadSceneNames context = (ReadSceneNames)command.context;
//        //context._AllScenes_ = ReadNames();
//		_AllScenes_ = ReadNames();
//    }
// 
//    private void Reset()
//    {
//        _AllScenes_ = ReadNames();
//    }
//    #endif
	
	public static List<string> _AllScenes_;
    #if UNITY_EDITOR
    private static List<string> ReadNames()
    {
        List<string> temp = new List<string>();
        foreach (UnityEditor.EditorBuildSettingsScene S in UnityEditor.EditorBuildSettings.scenes)
        {
            if (S.enabled)
            {
                string name = S.path.Substring(S.path.LastIndexOf('/')+1);
                name = name.Substring(0,name.Length-6);
                temp.Add(name);
            }
        }
        return temp;
    }
    [UnityEditor.MenuItem("CONTEXT/ReadSceneNames/Update Scene Names")]
    private static void UpdateNames(UnityEditor.MenuCommand command)
    {
        ReadSceneNames context = (ReadSceneNames)command.context;
        //context._AllScenes_ = ReadNames();
		_AllScenes_ = ReadNames();
    }
 
    private void Reset()
    {
        _AllScenes_ = ReadNames();
    }
    #endif
}
