/*
 *  This file is part of Unit3D.
 *
 *  Foobar is free software: you can redistribute it and/or modify
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

namespace Unit3D
{
	public class TestCase
	{	
		/// <summary>
		/// Setup method
		/// 
		/// This method is ran before every method with the UnitTest decorator
		/// Use it to instantiate required data for each test
		/// </summary>
		public virtual void SetUp()
		{
		}
		
		/// <summary>
		/// This method is ran after every method with the UnitTest decorator
		/// Use it to cleanup all data used by test case
		/// 
		/// REMEMBER: Don't leave global state in the scene unless you're in a throw-away scene you don't care about poluting
		/// </summary>
		public virtual void TearDown()
		{
		}
	}
}