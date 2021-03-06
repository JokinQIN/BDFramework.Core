﻿using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace Code.BDFramework.Core.Tools
{
    static public class BApplication
    {

        #region 路径相关

        /// <summary>
        /// 项目根目录
        /// </summary>
        static public string ProjectRoot { get; private set; } = Application.dataPath.Replace("/Assets", "");
        /// <summary>
        /// Library
        /// </summary>
        static public string Library { get; private set; } = ProjectRoot + "/Library";
        /// <summary>
        /// 资源的根目录
        /// </summary>
        static public string RuntimeResourceLoadPath { get; private set; } =  "Assets/Resource/Runtime";
        
        /// <summary>
        /// Editor的资源路径
        /// </summary>
        public static string EditorResourcePath { get; private set; } = "Assets/Resource_SVN";
        /// <summary>
        /// Editor的资源路径
        /// </summary>
        public static string EditorResourceRuntimePath { get; private set; } = EditorResourcePath + "/Runtime";



        /// <summary>
        /// 获取所有runtime的目录
        /// </summary>
        /// <returns></returns>
        public static  List<string> GetAllRuntimeDirects()
        {
            //搜索所有资源
            var root = Application.dataPath;

            //获取根路径所有runtime
            var directories = Directory.GetDirectories(root, "*", SearchOption.TopDirectoryOnly).ToList();
            for (int i = directories.Count - 1; i >= 0; i--)
            {
                var dir = directories[i].Replace(BApplication.ProjectRoot + "/", "").Replace("\\", "/") + "/Runtime";
                if (!Directory.Exists(dir))
                {
                    directories.RemoveAt(i);
                }
                else
                {
                    directories[i] = dir;
                }
            }

            return directories;
        }


        /// <summary>
        /// 获取所有资源
        /// </summary>
        /// <returns></returns>
        public static List<string> GetAllAssetsPath()
        {
            List<string> allAssetsList =new List<string>();
            var directories = GetAllRuntimeDirects();
            //所有资源列表
            foreach (var dir in directories)
            {
                var rets = Directory.GetFiles(dir, "*.*", SearchOption.AllDirectories)
                    .Where((s) => !s.EndsWith(".meta"));
                allAssetsList.AddRange(rets);
            }

            
            for (int i = 0; i < allAssetsList.Count; i++)
            {
                var res = allAssetsList[i];
                allAssetsList[i] = res.Replace("\\", "/");
            }
            
            return allAssetsList;
        }
        #endregion
        
    }
}