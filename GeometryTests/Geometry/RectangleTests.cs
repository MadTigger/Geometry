﻿/*
The MIT License (MIT)

Copyright (c) 2007 Roger Hill

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files 
(the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, 
publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do 
so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF 
MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE 
FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN 
CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/

using Geometry;
using NUnit.Framework;

namespace GeometryTests
{
    [TestFixture]
    public class RectangleTests
    {
        [Test]
        [Category("Rectangle")]
        public void TestBasicProperties()
        {
            var r = new Rectangle(0, 0, 2, 3);

            Assert.That(r.Top == 0f, Is.True, "Failed top check");
            Assert.That(r.Left == 0f, Is.True, "Failed left check");

            Assert.That(r.Width == 2f, Is.True, "Failed width check");
            Assert.That(r.Height == 3f, Is.True, "Failed height check");

            Assert.That(r.Perimeter == 10f, Is.True, "Failed perimeter check");
            Assert.That(r.Area == 6f, Is.True, "Failed area check");

            Assert.That(r.Right == 2f, Is.True, "Failed right side check");
            Assert.That(r.Bottom == 3f, Is.True, "Failed bottom side check");
            Assert.That(r.Top == 0f, Is.True, "Failed top side check");
            Assert.That(r.Left == 0f, Is.True, "Failed left side check");

            Assert.That(r.Center.X == 1f, Is.True, "Failed center X check");
            Assert.That(r.Center.Y == 1.5f, Is.True, "Failed center X check");
        }

        [Test]
        [Category("Rectangle")]
        public void TestContainsGeometry()
        {
            var r = new Rectangle(0, 0, 10, 10);

            // point inside
            Assert.That(r.Contains(new Point2(3, 3)), Is.True, "Failed contained check 1");

            // point outside
            Assert.That(r.Contains(new Point2(12, 12)), Is.False, "Failed contained check 2");

            // points on border
            Assert.That(r.Contains(new Point2(0, 0)), Is.True, "Failed contained check 3");
            Assert.That(r.Contains(new Point2(10, 10)), Is.True, "Failed contained check 4");

            // rectangle inside
            Assert.That(r.Contains(new Rectangle(2, 2, 2, 2)), Is.True, "Failed contained check 5");

            // rectangle outside
            Assert.That(r.Contains(new Rectangle(-5, -5, 2, 2)), Is.False, "Failed contained check 6");

            // rectangle outside & surrounds
            Assert.That(r.Contains(new Rectangle(-50, -50, 200, 200)), Is.False, "Failed contained check 7");

            // rectangle inside & tangent
            Assert.That(r.Contains(new Rectangle(0, 0, 2, 2)), Is.True, "Failed contained check 8");

            // rectangle outside & tangent
            Assert.That(r.Contains(new Rectangle(-2, -2, 2, 2)), Is.False, "Failed contained check 9");

            // rectangle intersects
            Assert.That(r.Contains(new Rectangle(-2, -2, 10, 10)), Is.False, "Failed contained check 10");
        }

        [Test]
        [Category("Rectangle")]
        [Category("Geometry")]
        [TestCase(-2, -2, 10, 10)] // overlapping & tangent
        [TestCase(2, 2, 2, 2)] // containing
        [TestCase(-50, -50, 200, 200)] // contains
        [TestCase(0, 0, 2, 2)] // tangent
        [TestCase(-2, -2, 2, 2)] // overlapping
        public void TestRectangleIntersectsGeometry(float top, float left, float width, float height)
        {
            var r1 = new Rectangle(0, 0, 10, 10);
            var r2 = new Rectangle(left, top, width, height);

            Assert.That(r1.Intersects(r2), Is.True, "Failed intersect check");
        }

        [Test]
        [Category("Rectangle")]
        [Category("Geometry")]
        [TestCase(0f, 0f, 6f)] // overlapping
        [TestCase(0f, 0f, 2f)] // offset
        [TestCase(0f, 0f, 1f)] // tangent
        [TestCase(5f, 5f, 1f)] // contained
        [TestCase(5f, 5f, 25f)] // containing
        public void Rectangle_RectangleCircleIntersection_Pass(float x, float y, float radius)
        {
            var r = new Rectangle(0, 0, 10, 10);
            var c = new Circle(x, y, radius);

            Assert.That(r.Intersects(c), Is.True, "Failed intersect check");
        }

        [Test]
        [Category("Rectangle")]
        [Category("Math")]
        [TestCase(1f, 1f)]
        [TestCase(0.5f, 0.5f)]
        [TestCase(-0.5f, -0.5f)]
        public void Rectangle_Scale_Pass(float x, float y)
        {
            var r = new Rectangle(0f, 0f, 2f, 2f);
            r = r.Scale(x, y);

            Assert.That(r.Width == (2f * x), Is.True, "Failed width check");
            Assert.That(r.Height == (2f * y), Is.True, "Failed height check");
        }

        [Test]
        [Category("Rectangle")]
        public void TestUnion()
        {
            var r1 = new Rectangle(0, 0, 2, 2);
            var r2 = new Rectangle(1, 1, 2, 2);
            var r3 = Rectangle.Union(r1, r2);

            Assert.That(r3.X == 0f, Is.True, "Failed X check");
            Assert.That(r3.Y == 0f, Is.True, "Failed Y check");
            Assert.That(r3.Width == 3f, Is.True, "Failed width check");
            Assert.That(r3.Height == 3f, Is.True, "Failed height check");
        }

        [Test]
        [Category("Rectangle")]
        public void TestOperatorOverloads()
        {
            var r1 = new Rectangle(0, 0, 2, 3);
            var v1 = new Vector2(1f, 1f);

            var r2 = r1 + v1;
            Assert.That(r2.Top == 1f && r2.Top == 1f && r2.Height == 3f && r2.Width == 2f, Is.True, "Failed add check");

            r2 = r1 - v1;
            Assert.That(r2.Top == -1f && r2.Top == -1f && r2.Height == 3f && r2.Width == 2f, Is.True, "Failed subtract check");

            r2 = r1 * 3f;
            Assert.That(r2.Top == 0f && r2.Top == 0f && r2.Height == 9f && r2.Width == 6f, Is.True, "Failed multiply check");

            r2 = r1 / 2f;
            Assert.That(r2.Top == 0f && r2.Top == 0f && r2.Height == 1.5f && r2.Width == 1f, Is.True, "Failed divide check");

            r2 = new Rectangle(0, 0, 2, 3);
            Assert.That(r1.Equals(r2), Is.True, "Failed equals check");

            r2 = new Rectangle(1, 2, 4, 5);
            Assert.That(r1.Equals(r2), Is.False, "Failed not equals check");
        }

        [Test]
        [Category("Rectangle")]
        public void TestIntersectsRectangle1()
        {
            // overlap
            var r1 = new Rectangle(0, 0, 1, 1);
            var r2 = new Rectangle(0, 0, 1, 1);

            Assert.That(r1.Intersects(r2), Is.True);
        }

        [Test]
        [Category("Rectangle")]
        public void TestIntersectsRectangle2()
        {
            // doesn't intersect
            var rectangle1 = new Rectangle(0, 0, 1, 1) + Vector2.One;
            var rectangle2 = new Rectangle(0, 0, 1, 1) - Vector2.One;

            Assert.That(rectangle1.Intersects(rectangle2), Is.False);
        }
    }
}
