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
using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;

namespace Unit3D
{	
	/// <summary>
	/// Test suite base class
	/// 
	/// To run tests:
	/// create a TestSuite instance
	/// Add the test cases to it
	/// and call the run method
	/// 
	/// All methods decorated with [UnitTest] will be added
	/// </summary>
	public class TestSuite : MonoBehaviour
	{
		
		/// <summary>
		/// Collection of test cases
		/// </summary>
		protected ICollection<TestCase> testCases;
		
		/// <summary>
		/// Adds all the unit tests in the passed test case to the suite
		/// </summary>
		/// <param name='testCase'>
		/// The test case to add the unit tests of
		/// </param>
		/// <exception cref='ArgumentException'>
		/// Is thrown when the test case is invalid
		/// </exception>
		public void Add(TestCase testCase)
		{
			if(testCase == null)
			{
				throw new ArgumentException("Invalid testCase");
			}
			
			if(testCases == null)
			{
				testCases = new List<TestCase>();
			}
			
			testCases.Add(testCase);
		}
		
		/// <summary>
		/// Startup all the tests in this test suite
		/// 
		/// This method starts up a coroutine to do the actual work
		/// As a result, it returns immediatly, but the tests may run for a while
		/// Do not rely on the tests being finished when this method returns
		/// </summary>
		public void Run()
		{
			StartCoroutine(RunCoroutine());
		}
		
		/// <summary>
		/// Starts the coroutine that controls code execution
		/// 
		/// This method will not return until all tests are done running
		/// </summary>
		/// <returns>
		/// IEnumerator objects, as required by generators
		/// </returns>
		protected IEnumerator RunCoroutine()
		{
			// Do some setup to keep track of things properly
			System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
			int totalFailed = 0;
			int totalSuccessful = 0;
			
			// Start stopwatch and start looping through all known test cases
			stopwatch.Start();
			foreach(TestCase testCase in testCases)
			{
				// Get the type and use reflection to loop through all methods with the UnitTest attribute
				Type testCaseType = testCase.GetType();
				foreach(MethodInfo methodInfo in testCaseType.GetMethods())
				{
					if(methodInfo.GetCustomAttributes(typeof(UnitTest), false).Length > 0)
					{
						bool failed = false;
					
						// Invoke the setup method
						try
						{
							testCase.SetUp();
						}
						catch(Exception e)
						{
							ReportFailure (methodInfo, e);
							failed = true;
						}
						
						// only continue if the setup was successful
						if(!failed)
						{
							// If we're dealing with generators, we'll need some special magic
							if(methodInfo.GetCustomAttributes(typeof(Generator), false).Length > 0)
							{
								// Get the IEnumerator that is returned by the unit test method
								IEnumerator enumerator = methodInfo.Invoke(testCase, null) as IEnumerator;
								bool moreContent = true;
								
								// Do exception handling and go through the whole generator
								do
								{
									System.Object obj;
									
									try
									{
										moreContent = enumerator.MoveNext();
										obj = enumerator.Current;
									}
									catch(Exception e)
									{
										failed = true;
										ReportFailure(methodInfo, e);
										break;
									}
									
									yield return obj;
									
								} while(moreContent);
							}
							// Normal case: just Invoke the method and be done with it :)
							else
							{
								try
								{
									methodInfo.Invoke(testCase, null);
								}
								catch(Exception e)
								{
									ReportFailure(methodInfo, e);
									failed = true;
								}
							}
						}
						
						// Teardown
						try
						{
							testCase.TearDown();
						}
						catch(Exception e)
						{
							ReportFailure(methodInfo, e);
							failed = true;
						}
						
						if(!failed)
						{
							totalSuccessful++;
						}
						else
						{
							totalFailed++;
						}
						
						// Wait for a frame for unity to react to teardown callbacks
						yield return null;
					}
				}
			}
			
			stopwatch.Stop();
			ReportTotal(totalSuccessful, totalFailed, stopwatch.ElapsedMilliseconds);
			
			// Cleanup
			Destroy(gameObject);
		}
		
		/// <summary>
		/// Report on the success of a test
		/// </summary>
		/// <param name='methodInfo'>
		/// Reflection information of the method
		/// </param>
		/// <remarks>
		/// By default, this method does nothing, and test success isn't reported
		/// </remarks>
		protected virtual void ReportSuccess(MethodInfo methodInfo)
		{
			// Do nothing, don't specificly report a success by default, they'll be added in the global count
		}
		
		/// <summary>
		/// Report on the failure of a test
		/// </summary>
		/// <param name='methodInfo'>
		/// Reflection information of the method
		/// </param>
		/// <param name='e'>
		/// The exception that caused the failure
		/// </param>
		protected virtual void ReportFailure(MethodInfo methodInfo, Exception e)
		{
			Debug.LogError(String.Format("Error while running test {0}\n{1}\n{2}", methodInfo.Name, e.Message, e.StackTrace));
		}
		
		/// <summary>
		/// Report the totals of the whole test run
		/// </summary>
		/// <param name='totalFailed'>
		/// Total failed tests
		/// </param>
		/// <param name='totalSuccessful'>
		/// Total successful tests
		/// </param>
		/// <param name='elapsedMilliseconds'>
		/// Total time elapsed in milliseconds
		/// </param>
		protected virtual void ReportTotal(int totalSuccessful, int totalFailed, long elapsedMilliseconds)
		{
			string text = string.Format("Ran {0} tests in {1}s", totalSuccessful + totalFailed, elapsedMilliseconds / 1000.0);
			if(totalFailed == 0)
			{
				text = string.Format("{0}\n{1}", text, "OK");
				Debug.Log(text);
			}
			else
			{
				text = string.Format("{0}\nSuccessful: {1}    Failed: {2}", text, totalSuccessful, totalFailed);
				Debug.LogError(text);
			}
		}
	}
					
	/// <summary>
	/// Unit test attribute
	/// 
	/// Methods will this attribute on a passed testcase will be ran 
	/// </summary>
	public class UnitTest : Attribute {}
	
	/// <summary>
	/// Generator attribute
	/// 
	/// Use this attribute if the unit test method is a generator and should yield back to the unity main thread when yielding
	/// This allows time for unity to do rendering, physics calculations, and so on,
	/// as well as wait for any threaded operations that might be running without having to resort to sleep or join
	/// (which would not let Unity run any of its own logic)
	/// </summary>
	public class Generator : Attribute {}
}