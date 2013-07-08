/*
 *  This file is part of Unit3D.
 *
 *  Unit3D is free software: you can redistribute it and/or modify
 *  it under the terms of the GNU Lesser General Public License as published by
 *  the Free Software Foundation, either version 3 of the License, or
 *  (at your option) any later version.
 *
 *  Unit3D is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *  GNU General Public License for more details.
 *
 *  You should have received a copy of the GNU Lesser General Public License
 *  along with Unit3D.  If not, see <http://www.gnu.org/licenses/>.
 *
 */

using System;
using UnityEditor;
using UnityEngine;
using System.Collections;
using Unit3D;

public class TestRunner
{
	[MenuItem("Tools/Unit tests")]
	public static void startTests()
	{
		// Start play mode and create a new scene
		if(!EditorApplication.isPlaying)
		{
			throw new System.InvalidOperationException("Play mode should be turned on before starting tests");
		}	
		
		// Startup game object and schedule unity èto run our tests
		GameObject testRunner = GameObject.Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Editor/TestRunner.prefab", typeof(GameObject)) as GameObject, Vector3.zero, Quaternion.identity) as GameObject;
		TestSuite testSuite = testRunner.GetComponent<TestSuite>();
		testSuite.Add(new TestAssert());
		testSuite.Add(new TestGenerators());
		testSuite.Run();
	}
}
