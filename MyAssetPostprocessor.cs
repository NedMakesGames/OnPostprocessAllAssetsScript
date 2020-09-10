//    Copyright (C) 2020 Ned Makes Games

//    This program is free software: you can redistribute it and/or modify
//    it under the terms of the GNU General Public License as published by
//    the Free Software Foundation, either version 3 of the License, or
//    (at your option) any later version.

//    This program is distributed in the hope that it will be useful,
//    but WITHOUT ANY WARRANTY; without even the implied warranty of
//    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
//    GNU General Public License for more details.

//    You should have received a copy of the GNU General Public License
//    along with this program. If not, see <https://www.gnu.org/licenses/>.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

class MyAssetPostprocessor : AssetPostprocessor {

    public static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets,
            string[] movedAssets, string[] movedFromAssetPaths) {

        if(Scan("Assets/Data", importedAssets, deletedAssets, movedAssets, movedFromAssetPaths)) {
            PrintChanges(importedAssets, deletedAssets, movedAssets, movedFromAssetPaths);
            // Do work
        }
    }

    private static bool Scan(string target, string[] importedAssets, string[] deletedAssets,
            string[] movedAssets, string[] movedFromAssetPaths) {
        return Scan(target, importedAssets) || Scan(target, deletedAssets) || Scan(target, movedAssets) || 
            Scan(target, movedFromAssetPaths);
    }

    private static bool Scan(string target, string[] arr) {
        foreach(var str in arr) {
            if(str.Contains(target)) {
                return true;
            }
        }
        return false;
    }

    private static void PrintChanges(string[] importedAssets, string[] deletedAssets,
            string[] movedAssets, string[] movedFromAssetPaths) {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("Changes to data folder detected!");

        sb.Append("New assets: ");
        if(importedAssets.Length > 0) {
            sb.Append(String.Join(", ", importedAssets));
            sb.AppendLine(".");
        } else {
            sb.AppendLine("none.");
        }
        
        sb.Append("Deleted assets: ");
        if(deletedAssets.Length > 0) {
            sb.Append(String.Join(", ", deletedAssets));
            sb.AppendLine(".");
        } else {
            sb.AppendLine("none.");
        }

        sb.Append("Moved assets: ");
        if(movedAssets.Length > 0) {
            for(int i = 0; i < movedAssets.Length; i++) {
                sb.Append($"{movedFromAssetPaths[i]} moved to {movedAssets[i]}");
            }
            sb.Append(".");
        } else {
            sb.Append("none.");
        }

        Debug.Log(sb.ToString());
    }
}

