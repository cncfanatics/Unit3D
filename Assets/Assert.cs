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
using System;
using System.Collections;

namespace Unit3D
{
	public class AssertException : Exception
	{
		public AssertException(string message) : base(message) {}
	}
	
	public class Assert
	{
		/// <summary>
		/// Throws an exception if the specified condition is not true
		/// </summary>
		/// <param name='condition'>
		/// The condition to check
		/// </param>
		/// <param name='msg'>
		/// The message to place in the exception
		/// </param>
		public static void IsTrue(bool condition, string msg)
		{
			if(!condition)
			{
				throw new AssertException(msg);
			}
		}
		
		/// <summary>
		/// Throws an exception if the specified condition is true
		/// </summary>
		/// <param name='condition'>
		/// The condition to check
		/// </param>
		/// <param name='msg'>
		/// The message to place in the exception
		/// </param>
		public static void IsFalse(bool condition, string msg)
		{
			Assert.IsTrue(!condition, msg);
		}
		
		/// <summary>
		/// Verify that the passed task throws the expected exception
		/// </summary>
		/// <param name='task'>
		/// Task to run
		/// </param>
		/// <param name='msg'>
		/// The error message
		/// </param>
		/// <typeparam name='T'>
		/// The type of exception you expect to be thrown
		/// </typeparam>
		public static void Throws<T>(Action task, string msg) where T : Exception
	 	{
			try
			{
			    task();
			}
			catch (Exception ex)
			{
			    AssertType<T>(ex, msg);
			    return;
			}
			
			throw new AssertException(msg);
		}
		
		/// <summary>
		/// Asserts the type of the passed object
		/// </summary>
		/// <param name='o'>
		/// The object to typecheck
		/// </param>
		/// <param name='msg'>
		/// The message to throw
		/// </param>
		/// <typeparam name='T'>
		/// The type the object must be
		/// </typeparam>
		/// <exception cref='AssertException'>
		/// Is thrown when the passed object is not of the expected type
		/// </exception>
		public static void AssertType<T>(System.Object o, string msg)
		{
			if(typeof(T).IsSubclassOf(o.GetType()))
			{
				throw new AssertException(msg);
			}
		}
		
		/// <summary>
		/// Assert equality of the two passed objects
		/// </summary>
		/// <param name='o1'>
		/// Object 1
		/// </param>
		/// <param name='o2'>
		/// Object 2
		/// </param>
		/// <param name='msg'>
		/// Error message in case of failure
		/// </param>
		/// <exception cref='AssertException'>
		/// Is thrown when the two objects are not equal
		/// </exception>
		public static void IsEqual(System.Object o1, System.Object o2, string msg)
		{
			if(!o1.Equals(o2))
			{
				throw new AssertException(msg);
			}
		}
		
		/// <summary>
		/// Assert inequality of the two passed objects
		/// </summary>
		/// <param name='o1'>
		/// Object 1
		/// </param>
		/// <param name='o2'>
		/// Object 2
		/// </param>
		/// <param name='msg'>
		/// Error message in case of failure
		/// </param>
		/// <exception cref='AssertException'>
		/// Is thrown when the two objects are equal
		/// </exception>
		public static void IsNotEqual(System.Object o1, System.Object o2, string msg)
		{
			if(o1.Equals(o2))
			{
				throw new AssertException(msg);
			}
		}
		
		/// <summary>
		/// Assert equality of the two passed objects within 7 decimal paces
		/// </summary>
		/// <param name='o1'>
		/// Object 1
		/// </param>
		/// <param name='o2'>
		/// Object 2
		/// </param>
		/// <param name='msg'>
		/// Error message in case of failure
		/// </param>
		/// <exception cref='AssertException'>
		/// Is thrown when the two objects are inequal
		/// </exception>
		public static void IsAlmostEqual(double o1, double o2, string msg)
		{
			if(Math.Round (o1, 7) != Math.Round (o2, 7))
			{
				throw new AssertException(msg);
			}
		}
		
		/// <summary>
		/// Assert inequality of the two passed objects within 7 decimal paces
		/// </summary>
		/// <param name='o1'>
		/// Object 1
		/// </param>
		/// <param name='o2'>
		/// Object 2
		/// </param>
		/// <param name='msg'>
		/// Error message in case of failure
		/// </param>
		/// <exception cref='AssertException'>
		/// Is thrown when the two objects are equal
		/// </exception>
		public static void IsNotAlmostEqual(double o1, double o2, string msg)
		{
			if(Math.Round (o1, 7) == Math.Round (o2, 7))
			{
				throw new AssertException(msg);
			}
		}
	}
}