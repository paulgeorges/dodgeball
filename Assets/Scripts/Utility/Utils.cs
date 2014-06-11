using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// General utilities functionality.
/// </summary>
public static class Utils
{
	public static string[] GetFileNamesInResources(string folderName, string filter)
	{
		string path = string.Format("{0}/Resources/{1}", Application.dataPath, folderName);
		return GetFileNamesInDirectory(path, filter);
	}

	public static string[] GetFileNamesInPersistentStorage(string folderName, string filter)
	{
		string path = string.Format("{0}/{1}", Application.persistentDataPath, folderName);
		return GetFileNamesInDirectory(path, filter);
	}

	public static string[] GetFileNamesInDirectory(string path, string filter)
	{
		List<string> fileEntries = new List<string>();
		
		string[] fullFilePaths = System.IO.Directory.GetFiles(path, filter);
		foreach (string filePath in fullFilePaths) {
			fileEntries.Add(Path.GetFileNameWithoutExtension(filePath));
		}
		
		return fileEntries.ToArray();
	}
}
