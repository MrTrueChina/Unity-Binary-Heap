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
            protected override float CalculateSort(int obj)
            {
                return obj;
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

            protected override float CalculateSort(IntObject obj)
            {
                return obj.Value;
            }
        }

        // —————— 存入 ——————
        [Test]
        [TestCase(new int[] { 1 }, new int[] { 1 })]
        [TestCase(new int[] { 1, 2, 3 }, new int[] { 1, 2, 3 })]
        [TestCase(new int[] { 1, 2, 0 }, new int[] { 0, 2, 1 })]
        [TestCase(new int[] { 1, 1, 1 }, new int[] { 1, 1, 1 })]
        public void Add(int[] elements, int[] expectedList)
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


        // —————— 删除 ——————

        /// <summary>
        /// 删除第一个指定的内容
        /// </summary>
        /// <param name="elements"></param>
        /// <param name="removeNum"></param>
        /// <param name="expectedList"></param>
        [Test]
        [TestCase(new int[] { 1, 3, 5, 0, 8 }, 0, new int[] { 1, 3, 5, 8 })]
        [TestCase(new int[] { 1 }, 1, new int[] { })]
        [TestCase(new int[] { 1, 3, 2 }, 1, new int[] { 2, 3 })]
        public void Remove(int[] elements, int removeNum, int[] expectedList)
        {
            IntMinHeap heap = new IntMinHeap();

            foreach (int element in elements)
            {
                heap.Add(element);
            }

            bool removeResult = heap.Remove(removeNum);

            List<int> numList = heap.GetList();

            for (int i = 0; i < numList.Count; i++)
            {
                Assert.AreEqual(expectedList[i], numList[i]);
            }
            Assert.AreEqual(true, removeResult);
        }

        /// <summary>
        /// 删除第一个指定的内容，没有指定内容的情况
        /// </summary>
        [Test]
        public void RemoveNone()
        {
            IntMinHeap heap = new IntMinHeap();

            heap.Add(1);

            bool removeResult = heap.Remove(0);

            Assert.AreEqual(false, removeResult);
        }

        /// <summary>
        /// 移除所有符合条件的元素
        /// </summary>
        /// <param name="elements"></param>
        /// <param name="removeNum"></param>
        /// <param name="expectedList"></param>
        [Test]
        [TestCase(new int[] { 1, 3, 5, 0, 8 }, -1, new int[] { })]
        [TestCase(new int[] { 1 }, 10, new int[] { 1 })]
        [TestCase(new int[] { 1, 3, 2 }, 2, new int[] { 1, 2 })]
        public void RemoveAllGreatThan(int[] elements, int removeNum, int[] expectedList)
        {
            IntMinHeap heap = new IntMinHeap();

            foreach (int element in elements)
            {
                heap.Add(element);
            }

            heap.RemoveAll(obj => obj > removeNum);

            List<int> numList = heap.GetList();

            for (int i = 0; i < numList.Count; i++)
            {
                Assert.AreEqual(expectedList[i], numList[i]);
            }
        }


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

        /// <summary>
        /// 检测堆里是否有至少一个对象符合标准
        /// </summary>
        /// <param name="elements"></param>
        /// <param name="removeNum"></param>
        /// <param name="expectedResult"></param>
        [Test]
        [TestCase(new int[] { 1, 3, 5, 0, 8 }, 5, true)]
        [TestCase(new int[] { }, 10, false)]
        [TestCase(new int[] { 1, 3, 2 }, 100, false)]
        public void AnyGreatThan(int[] elements, int removeNum, bool expectedResult)
        {
            IntMinHeap heap = new IntMinHeap();

            foreach (int element in elements)
            {
                heap.Add(element);
            }

            bool checkResult = heap.Any(obj => obj > removeNum);

            Assert.AreEqual(expectedResult, checkResult);
        }

        /// <summary>
        /// 检测堆里是否所有对象都符合标准，如果列表为空会按照 <see cref="System.Collections.Generic.List{T}"/> 的方式返回
        /// </summary>
        /// <param name="elements"></param>
        /// <param name="removeNum"></param>
        /// <param name="expectedResult"></param>
        [Test]
        [TestCase(new int[] { 1, 3, 5, 0, 8 }, 5, false)]
        [TestCase(new int[] { }, 10, true)]
        [TestCase(new int[] { 1, 3, 2 }, -1, true)]
        public void AllGreatThan(int[] elements, int removeNum, bool expectedResult)
        {
            IntMinHeap heap = new IntMinHeap();

            foreach (int element in elements)
            {
                heap.Add(element);
            }

            bool checkResult = heap.All(obj => obj > removeNum);

            Assert.AreEqual(expectedResult, checkResult);
        }

        /// <summary>
        /// 获取整个对象列表
        /// </summary>
        /// <param name="elements"></param>
        /// <param name="expectedList"></param>
        [Test]
        [TestCase(new int[] { 1, 2, 3 }, new int[] { 1, 2, 3 })]
        [TestCase(new int[] { 1, 2, 3 }, new int[] { 1, 2, 3 })]
        [TestCase(new int[] { }, new int[] { })]
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

        // TODO：有节点向上移动

        // TODO：有节点向下移动

        // TODO：有节点向上移动同时有节点向下移动

        // TODO：所有节点不动
    }
}
