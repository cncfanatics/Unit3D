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
using Unit3D;

public class TestAssert : TestCase
{
	[UnitTest]
	public void TestAssertTrue()
	{
		Assert.Throws<AssertException>(() => Assert.IsTrue(false, "#AT1"), "#AT2");
	}
	
	[UnitTest]
	public void TestAssertFalse()
	{
		Assert.Throws<AssertException>(() => Assert.IsFalse(true, "#AF1"), "#AF2");
	}
		
	[UnitTest]
	public void TestThrowsNoException()
	{
		// Ensure Throws does throw exceptions when the action doesn't throw an exception at all
		bool exceptionThrown = false;
		try
		{
			Assert.Throws<Exception>(() => new Exception("Not throwing"), "#TNE1");
		}
		catch
		{
			exceptionThrown = true;
		}
		
		if(!exceptionThrown)
		{
			throw new AssertException("#TNE2");
		}
	}
	
	[UnitTest]
	public void TestThrowsExpectedException()
	{
		// Ensure throws doesn't throw exceptions when it gets what it expects
		Assert.Throws<AssertException>(() => { throw new AssertException("#TEE1"); }, "#TEE2");
		
		// ensure no exception is thrown when throws gets a subclass of the expected exception, either
		Assert.Throws<Exception>(() => { throw new AssertException("#TEE3"); }, "#TEE4");
	}
	
	[UnitTest]
	public void TestAssertIsEqual()
	{
		Assert.Throws<AssertException>(() => Assert.IsEqual(5, 3, "#AIE1"), "#AIE2");
		Assert.IsEqual("foo", "foo", "#AIE3");
	}
	
	[UnitTest]
	public void TestAssertIsNotEqual()
	{
		Assert.Throws<AssertException>(() => Assert.IsNotEqual(3, 3, "#AINE1"), "#AINE2");
		Assert.IsNotEqual("foo", "bar", "#AIE3");
	}
	
	[UnitTest]
	public void TestAssertAlmostEqual()
	{
		Assert.Throws<AssertException>(() => Assert.IsAlmostEqual(0.3564539877, 0.3564439, "#AAE1"), "#AAE2");
		Assert.IsAlmostEqual(5.647468794654856464, 5.64746879465481984, "#AAE3");
	}
	
	[UnitTest]
	public void TestAssertNotAlmostEqual()
	{
		Assert.Throws<AssertException>(() => Assert.IsNotAlmostEqual(0.45648946546546508566516574, 0.456489465465465085684864614, "#ANAE1"), "#ANAE2");
		Assert.IsNotAlmostEqual(5.546546541657, 5.546536541657, "#ANAE3");
	}
}
