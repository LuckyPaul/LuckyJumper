/***
 * 
 *    Title: LPFamework
 *           主题： 自定义脚本创建工具      
 *    Description: 
 *           功能： 
 *                  
 *    Date:2018/09/11
 *    Version: 0.1版本
 *    Modify Recoder: 
 *    Author: WSX
 *   
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.ProjectWindowCallback;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace LuckyPual.Tools
{
    public class PersonalScriptsCreater : UnityEditor.AssetModificationProcessor
    {


        public static void OnWillCreateAsset(string path)
        {
            path = path.Replace(".meta", "");
            int index = path.LastIndexOf(".");
            string file = path.Substring(index);
            if (file != ".cs" && file != ".js" && file != ".boo") return;
            //string fileExtension = file;

            index = Application.dataPath.LastIndexOf("Assets");
            path = Application.dataPath.Substring(0, index) + path;
            file = System.IO.File.ReadAllText(path);

            file = file.Replace("#CreateTime#", System.DateTime.Now.ToString("d"));
            file = file.Replace("#ProjectName#", "LPFamework");
            file = file.Replace("#NAMESPACE#", "LuckyPual");
            file = file.Replace("#Author#", "WSX");


            System.IO.File.WriteAllText(path, file);
            AssetDatabase.Refresh();
        }




        [MenuItem("Assets/Create/LPFamework C# Script", false, 80)]
        public static void CreateCSharp()
        {
            ProjectWindowUtil.StartNameEditingIfProjectWindowExists(0,
          ScriptableObject.CreateInstance<CreatePersonalCSScriptAsset>(),
          GetSelectPathOrFallback() + "/LPFameworkScript.cs", null, "Assets/Scripts/LPFarmework/ConfigFile/Template/MyCharpTemplate.txt");

        }
        [MenuItem("Assets/Create/LPFamework Lua", false, 80)]
        public static void CreateLua()
        {
            ProjectWindowUtil.StartNameEditingIfProjectWindowExists(0,
          ScriptableObject.CreateInstance<CreatePersonalCSScriptAsset>(),
          GetSelectPathOrFallback() + "/LPFameworkLua.lua", null, "Assets/Scripts/LPFarmework/ConfigFile/Template/MyLuaTemplate.txt");

        }

        public static string GetSelectPathOrFallback()
        {
            string path = "Assets";
            foreach (UnityEngine.Object obj in Selection.GetFiltered(typeof(UnityEngine.Object), SelectionMode.Assets))
            {
                path = AssetDatabase.GetAssetPath(obj);
                if (!string.IsNullOrEmpty(path) && File.Exists(path))
                {
                    path = Path.GetDirectoryName(path);
                    break;
                }
            }
            return path;
        }
    }

    class CreatePersonalCSScriptAsset : EndNameEditAction
    {
        public override void Action(int instanceId, string pathName, string resourceFile)
        {
            UnityEngine.Object o = CreateScriptAssetFromTemplate(pathName, resourceFile);
            ProjectWindowUtil.ShowCreatedAsset(o);
        }

        internal static UnityEngine.Object CreateScriptAssetFromTemplate(string pathName, string resourceFile)
        {
            string fullPath = Path.GetFullPath(pathName);
            StreamReader streamReader = new StreamReader(resourceFile);
            string text = streamReader.ReadToEnd();
            streamReader.Close();
            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(pathName);
            text = Regex.Replace(text, "#SCRIPTNAME#", fileNameWithoutExtension);

            bool encoderShouldEmitUTF8Identifier = true;
            bool throwOnInvalidBytes = false;
            UTF8Encoding encoding = new UTF8Encoding(encoderShouldEmitUTF8Identifier, throwOnInvalidBytes);
            bool append = false;
            StreamWriter streamWriter = new StreamWriter(fullPath, append, encoding);
            streamWriter.Write(text);
            streamWriter.Close();
            AssetDatabase.ImportAsset(pathName);
            return AssetDatabase.LoadAssetAtPath(pathName, typeof(UnityEngine.Object));
        }
    }
}
