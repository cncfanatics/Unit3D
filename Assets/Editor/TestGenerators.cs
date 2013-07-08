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

using UnityEngine;
using System.Collections;
using Unit3D;

public class TestGenerators : TestCase
{
	[Generator]
	[UnitTest]
	public IEnumerable TestYieldLoop()
	{
		int i;
		for(i = 0; i < 5; i++)
		{
			yield return null;
		}
		
		Assert.IsEqual(i, 5, "#YL1");
	}
}
