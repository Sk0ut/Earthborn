using Entitas;
using System.Diagnostics;
using UnityEngine;
using System.Collections.Generic;
using System;

class GeneratedMapSystem : MapSystem
{
	public GeneratedMapSystem (Contexts contexts) : base(contexts)
	{
		base.floors = new Floor[]{generateFloor(40,40), generateFloor(40,40), Maps.floors[2]};

	}

	private Floor generateFloor(int width, int height) {
		Process process = new Process();
		process.StartInfo.FileName = "dungeon.exe";
		process.StartInfo.CreateNoWindow = true;
//		process.StartInfo.Arguments = width.ToString() + " " + height.ToString();
		process.StartInfo.UseShellExecute = false;
		process.StartInfo.RedirectStandardOutput = true;
		process.StartInfo.RedirectStandardError = true;
		process.Start();
		string output;
		List<string> lines = new List<string>();
		while ((output = process.StandardOutput.ReadLine()) != null) 
    	{
			lines.Add (output);
		}
		process.WaitForExit();

		return generateFromString(width, height, lines.ToArray());
	}

	private Floor generateFromString(int width, int height, string[] lines) {
		UnityEngine.Debug.Log (string.Join (Environment.NewLine, lines)); 
		UnityEngine.Debug.Log (lines.Length);

		int[,] tiles = new int[height, width];
		Vector2 start = Vector2.zero;
		Vector2 end = Vector2.zero;
		for (int i = 0; i < height; ++i) {
			for (int j = 0; j < width; ++j) {
				int tile = 1;
				switch (lines [i] [j*2]) {
				case '#':
					tile = 1;
					break;
				case ' ':
				case '.':
					tile = 0;
					break;
				case '<':
					tile = 0;
					end = new Vector2 (j, height - i - 1);
					break;
				case '>':
					tile = 0;
					start = new Vector2 (j, height - i - 1);
					break;
				default:
					break;
				}
				tiles [i ,j] = tile;
			}
		}

		return new Floor(tiles, start, end);
	}
}

