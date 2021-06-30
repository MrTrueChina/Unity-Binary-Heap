using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;
using System;

namespace MtC.Tools.BinaryHeap
{
    /// <summary>
    /// 二叉堆的测试类
    /// </summary>
    [TestFixture]
    public class BinaryHeapTest
    {
        /// <summary>
        /// 整数最小堆
        /// </summary>
        private class IntMinHeap : BinaryHeap<int>
        {
            protected override int Comparison(int a, int b)
            {
                return a - b;
            }
        }

        /// <summary>
        /// 整数包装后的最小堆
        /// </summary>
        private class IntObjectMinHeap : BinaryHeap<IntObjectMinHeap.IntObject>
        {
            public class IntObject
            {
                public int Value { get; private set; }

                public IntObject(int value)
                {
                    Value = value;
                }
            }

            protected override int Comparison(IntObject a, IntObject b)
            {
                return a.Value - b.Value;
            }
        }

        // —————— 存入 ——————

        // 特殊情况：只存入一个

        // 标准情况，每次存入不同的内容

        // 存入相同的内容

        // 存入虽然实际不同但比较相同的内容


        // —————— 删除 ——————

        // 删除第一个指定的内容

        // 删除所有符合要求的内容
        // 极端情况：全删除

        // 删除不存在的内容


        // —————— 查询 ——————

        /// <summary>
        /// 普通的查询堆顶情况
        /// </summary>
        /// <param name="elements"></param>
        /// <param name="expectedTop"></param>
        [Test]
        [TestCase(new int[] { 1, 3, 5, 0, 8 }, 0)]
        [TestCase(new int[] { 1 }, 1)]
        [TestCase(new int[] { 1, 3, 2 }, 1)]
        public void GetTopNormal(int[] elements, int expectedTop)
        {
            IntMinHeap heap = new IntMinHeap();

            foreach (int element in elements)
            {
                heap.Add(element);
            }

            Assert.AreEqual(expectedTop, heap.GetTop());
        }

        /// <summary>
        /// 查询堆顶的特殊情况，Int型，堆中没有元素
        /// </summary>
        [Test]
        public void GetTopNoneInt()
        {
            IntMinHeap intMinHeap = new IntMinHeap();
            Assert.AreEqual(default(int), intMinHeap.GetTop());
        }

        /// <summary>
        /// 查询堆顶的特殊情况，对象型，堆中没有元素
        /// </summary>
        [Test]
        public void GetTopNoneIntObject()
        {
            IntObjectMinHeap intMinHeap = new IntObjectMinHeap();
            Assert.AreEqual(default(IntObjectMinHeap.IntObject), intMinHeap.GetTop());
        }

        /// <summary>
        /// 查询是否含有某个元素的通常情况
        /// </summary>
        /// <param name="elements"></param>
        /// <param name="expectedContains"></param>
        /// <param name="expectedNotContains"></param>
        [Test]
        [TestCase(new int[] { 1, 3, 5, 0, 8 }, 0, 2)]
        [TestCase(new int[] { 1 }, 1, 0)]
        [TestCase(new int[] { 1, 3, 2 }, 1, 5)]
        public void ContainsNormal(int[] elements, int expectedContains, int expectedNotContains)
        {
            IntMinHeap heap = new IntMinHeap();

            foreach (int element in elements)
            {
                heap.Add(element);
            }

            Assert.AreEqual(true, heap.Contains(expectedContains));
            Assert.AreEqual(false, heap.Contains(expectedNotContains));
        }

        /// <summary>
        /// 查询是否含有某个元素，堆是空的的情况
        /// </summary>
        public void ContainsNone()
        {
            IntMinHeap heap = new IntMinHeap();

            Assert.AreEqual(false, heap.Contains(0));
        }

        // Any，注意参数和 List 的 Any 一致

        // All，注意参数和 List 的 All 一致

        /// <summary>
        /// 获取整个对象列表
        /// </summary>
        /// <param name="elements"></param>
        /// <param name="expectedList"></param>
        [Test]
        [TestCase(new int[] { 1, 2, 3 }, new int[] { 1, 2, 3 })]
        [TestCase(new int[] { 1, 2, 0 }, new int[] { 0, 2, 1 })]
        public void GetList(int[] elements, int[] expectedList)
        {
            IntMinHeap heap = new IntMinHeap();

            foreach (int element in elements)
            {
                heap.Add(element);
            }

            List<int> numList = heap.GetList();

            for (int i = 0; i < numList.Count; i++)
            {
                Assert.AreEqual(expectedList[i], numList[i]);
            }
        }

        /// <summary>
        /// 查询总元素数
        /// </summary>
        /// <param name="elements"></param>
        /// <param name="expectedCount"></param>
        [Test]
        [TestCase(new int[] { }, 0)]
        [TestCase(new int[] { 1, 2, 3, 4 }, 4)]
        [TestCase(new int[] { 2, 5, 8 }, 3)]
        public void Count(int[] elements, int expectedCount)
        {
            IntMinHeap heap = new IntMinHeap();

            foreach (int element in elements)
            {
                heap.Add(element);
            }

            Assert.AreEqual(expectedCount, heap.Count);
        }


        // —————— 更新 ——————

        // 有节点向上移动

        // 有节点向下移动

        // 有节点向上移动同时有节点向下移动

        // 所有节点不动
    }
}
