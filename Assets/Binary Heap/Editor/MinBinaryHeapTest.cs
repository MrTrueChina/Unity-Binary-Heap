using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

/*
 *  测试代码，需要 using NUnit.Fra,ework
 *  测试类加 [TestFixture]，测试方法加 [Test]
 *  可以在Unity的 TestRunner 面板进行测试
 */
[TestFixture]
public class MinBinaryHeapTest
{
    [Test]
    public void SetNode_Node_Normal_1()
    {
        MinBinaryHeap<string> heap = new MinBinaryHeap<string>();

        /*
         *  0 1 2 3 4 5 6
         *  
         *     0
         *   1   2
         *  3 4 5 6
         *  
         *  0 1 2 3 4 5 6
         */

        heap.SetNode(new MinBinaryHeapNode<string> { obj = "0", value = 0 });
        heap.SetNode(new MinBinaryHeapNode<string> { obj = "1", value = 1 });
        heap.SetNode(new MinBinaryHeapNode<string> { obj = "2", value = 2 });
        heap.SetNode(new MinBinaryHeapNode<string> { obj = "3", value = 3 });
        heap.SetNode(new MinBinaryHeapNode<string> { obj = "4", value = 4 });
        heap.SetNode(new MinBinaryHeapNode<string> { obj = "5", value = 5 });
        heap.SetNode(new MinBinaryHeapNode<string> { obj = "6", value = 6 });

        List<MinBinaryHeapNode<string>> nodes = heap.GetNodes();

        string[] objExpected = new string[7] { "0", "1", "2", "3", "4", "5", "6" };
        float[] valueExpected = new float[7] { 0, 1, 2, 3, 4, 5, 6 };

        for (int i = 0; i < 7; i++)
        {
            Assert.AreEqual(objExpected[i], nodes[i].obj);
            Assert.AreEqual(valueExpected[i], nodes[i].value);
        }
    }
    [Test]
    public void SetNode_Node_Normal_2()
    {
        MinBinaryHeap<string> heap = new MinBinaryHeap<string>();

        /*
         *  6 5 4 3 2 1 0
         *  
         *     0
         *   3   1
         *  6 4 5 2
         *  
         *  0 3 1 6 4 5 2
         */

        heap.SetNode(new MinBinaryHeapNode<string> { obj = "six", value = 6 });
        heap.SetNode(new MinBinaryHeapNode<string> { obj = "five", value = 5 });
        heap.SetNode(new MinBinaryHeapNode<string> { obj = "four", value = 4 });
        heap.SetNode(new MinBinaryHeapNode<string> { obj = "three", value = 3 });
        heap.SetNode(new MinBinaryHeapNode<string> { obj = "two", value = 2 });
        heap.SetNode(new MinBinaryHeapNode<string> { obj = "one", value = 1 });
        heap.SetNode(new MinBinaryHeapNode<string> { obj = "zero", value = 0 });

        List<MinBinaryHeapNode<string>> nodes = heap.GetNodes();

        string[] objExpected = new string[7] { "zero", "three", "one", "six", "four", "five", "two" };
        float[] valueExpected = new float[7] { 0, 3, 1, 6, 4, 5, 2 };

        for (int i = 0; i < 7; i++)
        {
            Assert.AreEqual(objExpected[i], nodes[i].obj);
            Assert.AreEqual(valueExpected[i], nodes[i].value);
        }
    }
    [Test]
    public void SetNode_Node_Normal_3()
    {
        MinBinaryHeap<string> heap = new MinBinaryHeap<string>();

        /*
         *  1.1  2.5  721  6.8  194  0.3  9.9 
         *  
         *           0.3
         *     2.5         1.1
         *  6.8   194   721   9.9
         *  
         *  0.3  2.5  1.1  6.8  194  721  9.9
         */

        heap.SetNode(new MinBinaryHeapNode<string> { obj = "1", value = 1.1f });
        heap.SetNode(new MinBinaryHeapNode<string> { obj = "2", value = 2.5f });
        heap.SetNode(new MinBinaryHeapNode<string> { obj = "7", value = 721f });
        heap.SetNode(new MinBinaryHeapNode<string> { obj = "6", value = 6.8f });
        heap.SetNode(new MinBinaryHeapNode<string> { obj = "1", value = 194f });
        heap.SetNode(new MinBinaryHeapNode<string> { obj = "0", value = 0.3f });
        heap.SetNode(new MinBinaryHeapNode<string> { obj = "9", value = 9.9f });

        List<MinBinaryHeapNode<string>> nodes = heap.GetNodes();

        string[] objExpected = new string[7] { "0", "2", "1", "6", "1", "7", "9" };
        float[] valueExpected = new float[7] { 0.3f, 2.5f, 1.1f, 6.8f, 194, 721, 9.9f };

        for (int i = 0; i < 7; i++)
        {
            Assert.AreEqual(objExpected[i], nodes[i].obj);
            Assert.AreEqual(valueExpected[i], nodes[i].value);
        }
    }

    [Test]
    public void SetNode_Node_Equal()
    {
        MinBinaryHeap<string> heap = new MinBinaryHeap<string>();

        /*
         *  6 5 4 5 2 1 0
         *  
         *     0
         *   4   1
         *  6 5 5 2
         *  
         *  0 4 1 6 5 5 2
         */

        heap.SetNode(new MinBinaryHeapNode<string> { obj = "six", value = 6 });
        heap.SetNode(new MinBinaryHeapNode<string> { obj = "five", value = 5 });
        heap.SetNode(new MinBinaryHeapNode<string> { obj = "four", value = 4 });
        heap.SetNode(new MinBinaryHeapNode<string> { obj = "five", value = 5 });
        heap.SetNode(new MinBinaryHeapNode<string> { obj = "two", value = 2 });
        heap.SetNode(new MinBinaryHeapNode<string> { obj = "one", value = 1 });
        heap.SetNode(new MinBinaryHeapNode<string> { obj = "zero", value = 0 });

        List<MinBinaryHeapNode<string>> nodes = heap.GetNodes();

        string[] objExpected = new string[7] { "zero", "four", "one", "six", "five", "five", "two" };
        float[] valueExpected = new float[7] { 0, 4, 1, 6, 5, 5, 2 };

        for (int i = 0; i < 7; i++)
        {
            Assert.AreEqual(objExpected[i], nodes[i].obj);
            Assert.AreEqual(valueExpected[i], nodes[i].value);
        }
    }

    [Test]
    public void SetNode_T_Float_Normal_1()
    {
        MinBinaryHeap<string> heap = new MinBinaryHeap<string>();

        /*
         *  0 1 2 3 4 5 6
         *  
         *     0
         *   1   2
         *  3 4 5 6
         *  
         *  0 1 2 3 4 5 6
         */

        heap.SetNode("0", 0);
        heap.SetNode("1", 1);
        heap.SetNode("2", 2);
        heap.SetNode("3", 3);
        heap.SetNode("4", 4);
        heap.SetNode("5", 5);
        heap.SetNode("6", 6);

        List<MinBinaryHeapNode<string>> nodes = heap.GetNodes();

        string[] objExpected = new string[7] { "0", "1", "2", "3", "4", "5", "6" };
        float[] valueExpected = new float[7] { 0, 1, 2, 3, 4, 5, 6 };

        for (int i = 0; i < 7; i++)
        {
            Assert.AreEqual(objExpected[i], nodes[i].obj);
            Assert.AreEqual(valueExpected[i], nodes[i].value);
        }
    }


    [Test]
    public void GetTopNodeObject_Normal()
    {
        MinBinaryHeap<string> heap = new MinBinaryHeap<string>();

        /*
         *  0 1 2 3 4 5 6
         *  
         *     0
         *   1   2
         *  3 4 5 6
         *  
         *  0 1 2 3 4 5 6
         */

        heap.SetNode(new MinBinaryHeapNode<string> { obj = "0", value = 0 });
        heap.SetNode(new MinBinaryHeapNode<string> { obj = "1", value = 1 });
        heap.SetNode(new MinBinaryHeapNode<string> { obj = "2", value = 2 });
        heap.SetNode(new MinBinaryHeapNode<string> { obj = "3", value = 3 });
        heap.SetNode(new MinBinaryHeapNode<string> { obj = "4", value = 4 });
        heap.SetNode(new MinBinaryHeapNode<string> { obj = "5", value = 5 });
        heap.SetNode(new MinBinaryHeapNode<string> { obj = "6", value = 6 });

        Assert.AreEqual("0", heap.GetTopNodeObject());
    }
    [Test]
    public void GetTopNodeObject_Null()
    {
        MinBinaryHeap<string> heap = new MinBinaryHeap<string>();
        
        Assert.AreEqual(null, heap.GetTopNodeObject());
    }


    [Test]
    public void RemoveTop_Normal()
    {
        MinBinaryHeap<string> heap = new MinBinaryHeap<string>();

        /*
         *  0 1 2 3 4 5 6
         *  
         *     0
         *   1   2
         *  3 4 5 6
         *  
         *     1
         *   3   2
         *  6 4 5
         *  
         *  1 3 2 6 4 5
         */

        heap.SetNode(new MinBinaryHeapNode<string> { obj = "0", value = 0 });
        heap.SetNode(new MinBinaryHeapNode<string> { obj = "1", value = 1 });
        heap.SetNode(new MinBinaryHeapNode<string> { obj = "2", value = 2 });
        heap.SetNode(new MinBinaryHeapNode<string> { obj = "3", value = 3 });
        heap.SetNode(new MinBinaryHeapNode<string> { obj = "4", value = 4 });
        heap.SetNode(new MinBinaryHeapNode<string> { obj = "5", value = 5 });
        heap.SetNode(new MinBinaryHeapNode<string> { obj = "6", value = 6 });

        heap.RemoveTop();

        List<MinBinaryHeapNode<string>> nodes = heap.GetNodes();

        float[] valueExpected = new float[6] { 1, 3, 2, 6, 4, 5 };

        for (int i = 0; i < 6; i++)
        {
            Assert.AreEqual(valueExpected[i], nodes[i].value);
        }
    }
    [Test]
    public void RemoveTop_Null()
    {
        MinBinaryHeap<string> heap = new MinBinaryHeap<string>();

        heap.RemoveTop();

        Assert.AreEqual(0, heap.GetNodes().Count);
    }

    [Test]
    public void RemoveNode_Normal()
    {
        MinBinaryHeap<string> heap = new MinBinaryHeap<string>();

        /*
         *  0 5 2 6 7 5 3 8 9 9 8 6 7 4 4
         *  
         *         0
         *     5       2
         *   6   7   5   3
         *  8 9 9 8 6 7 4 4
         *  
         *  
         *  remove 6
         *  
         *         0
         *     4       2
         *   5   7   5   3
         *  8 9 9 8 6 7 4
         *  
         *  0 4 2 5 7 5 3 8 9 9 8 6 7 4
         *  
         *  
         *  remove 5
         *  
         *         0
         *     4       2
         *   4   7   5   3
         *  8 9 9 8 6 7
         *  
         *  0 4 2 4 7 5 3 8 9 9 8 6 7
         */

        heap.SetNode(null, 0);
        heap.SetNode(null, 5);
        heap.SetNode(null, 2);
        heap.SetNode(null, 6);
        heap.SetNode(null, 7);
        heap.SetNode(null, 5);
        heap.SetNode(null, 3);
        heap.SetNode(null, 8);
        heap.SetNode(null, 9);
        heap.SetNode(null, 9);
        heap.SetNode(null, 8);
        heap.SetNode(null, 6);
        heap.SetNode(null, 7);
        heap.SetNode(null, 4);
        heap.SetNode(null, 4);

        List<MinBinaryHeapNode<string>> nodes = heap.GetNodes();

        
        heap.RemoveNode(nodes[3]);    //nodes[3]是 6

        float[] valueExpected = new float[14] { 0, 4, 2, 5, 7, 5, 3, 8, 9, 9, 8, 6, 7, 4 };

        for (int i = 0; i < 14; i++)
            Assert.AreEqual(valueExpected[i], nodes[i].value);


        heap.RemoveNode(nodes[3]);    //nodes[3]是 5

        valueExpected = new float[13] { 0, 4, 2, 4, 7, 5, 3, 8, 9, 9, 8, 6, 7 };

        for (int i = 0; i < 13; i++)
            Assert.AreEqual(valueExpected[i], nodes[i].value);
    }
    [Test]
    public void RemoveNode_OneNode()
    {
        MinBinaryHeap<string> heap = new MinBinaryHeap<string>();

        /*
         * 0
         *    0
         * remove 0
         * 
         * Count == 0
         */

        heap.SetNode(null, 0);

        List<MinBinaryHeapNode<string>> nodes = heap.GetNodes();


        heap.RemoveNode(nodes[0]);

        Assert.AreEqual(0, heap.Count);
    }
    [Test]
    public void RemoveNode_BottomNotLast()
    {
        MinBinaryHeap<string> heap = new MinBinaryHeap<string>();

        /*
         *  0 5 2 6 7 5 3 8 9 9 8 6 7 4 11
         *  
         *         0
         *     5       2
         *   6   7   5   3
         *  8 9 9 8 6 7 4 11
         *  
         *  remove 8
         *  
         *  8没有子节点，则子节点替代下调应该没有影响，之后由队尾的11到8的位置
         *  
         *         0
         *     5       2
         *   6   7   5   3
         * 11 9 9 8 6 7 4
         * 
         * 0 5 2 6 7 5 3 11 9 9 8 6 7 4
         * 
         * remove 11
         * 
         *         0
         *     4       2
         *   5   7   5   3
         *  6 9 9 8 6 7
         *  
         *  0,4,2,5,7,5,3,6,9,9,8,6,7
         */

        heap.SetNode(null, 0);
        heap.SetNode(null, 5);
        heap.SetNode(null, 2);
        heap.SetNode(null, 6);
        heap.SetNode(null, 7);
        heap.SetNode(null, 5);
        heap.SetNode(null, 3);
        heap.SetNode(null, 8);
        heap.SetNode(null, 9);
        heap.SetNode(null, 9);
        heap.SetNode(null, 8);
        heap.SetNode(null, 6);
        heap.SetNode(null, 7);
        heap.SetNode(null, 4);
        heap.SetNode(null, 11);

        List<MinBinaryHeapNode<string>> nodes = heap.GetNodes();


        heap.RemoveNode(nodes[7]);    //nodes[7]是最底下的第一个 8

        float[] valueExpected = new float[14] { 0, 5, 2, 6, 7, 5, 3, 11, 9, 9, 8, 6, 7, 4 };

        for (int i = 0; i < 14; i++)
            Assert.AreEqual(valueExpected[i], nodes[i].value);


        heap.RemoveNode(nodes[7]);    //nodes[7]是最底下的 11

        valueExpected = new float[13] { 0, 4, 2, 5, 7, 5, 3, 6, 9, 9, 8, 6, 7 };

        for (int i = 0; i < 13; i++)
            Assert.AreEqual(valueExpected[i], nodes[i].value);
    }
    [Test]
    public void RemoveNode_Last()
    {
        MinBinaryHeap<string> heap = new MinBinaryHeap<string>();

        /*
         *  0 5 2 6 7 5 3 8 9 9 8 6 7 4 11
         *  
         *         0
         *     5       2
         *   6   7   5   3
         *  8 9 9 8 6 7 4 11
         *  
         *  remove 11
         *  
         *  11没有左子节点，则最小左子节点顶替被删除节点后向下调整无影响，最小左子节点下标等于自身下标
         *  
         *  11是最后一个节点，则最后一个节点顶替原最小左子节点向上调整也无影响
         *  
         *  最后删除最后一个节点，也就是 11
         *  
         *         0
         *     5       2
         *   6   7   5   3
         *  8 9 9 8 6 7 4
         *  
         *  0,5,2,6,7,5,3,8,9,9,8,6,7,4
         */

        heap.SetNode(null, 0);
        heap.SetNode(null, 5);
        heap.SetNode(null, 2);
        heap.SetNode(null, 6);
        heap.SetNode(null, 7);
        heap.SetNode(null, 5);
        heap.SetNode(null, 3);
        heap.SetNode(null, 8);
        heap.SetNode(null, 9);
        heap.SetNode(null, 9);
        heap.SetNode(null, 8);
        heap.SetNode(null, 6);
        heap.SetNode(null, 7);
        heap.SetNode(null, 4);
        heap.SetNode(null, 11);

        List<MinBinaryHeapNode<string>> nodes = heap.GetNodes();


        heap.RemoveNode(nodes[14]);    //nodes[14]是最后一个节点：11

        float[] valueExpected = new float[14] { 0, 5, 2, 6, 7, 5, 3, 8, 9, 9, 8, 6, 7, 4 };

        for (int i = 0; i < 14; i++)
            Assert.AreEqual(valueExpected[i], nodes[i].value);


        heap.RemoveNode(nodes[13]);    //nodes[13]是最后一个节点：4

        valueExpected = new float[13] { 0, 5, 2, 6, 7, 5, 3, 8, 9, 9, 8, 6, 7 };

        for (int i = 0; i < 13; i++)
            Assert.AreEqual(valueExpected[i], nodes[i].value);
    }


    [Test]
    public void Swap_Normal()
    {
        List<int> intList = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };

        intList.Swap(0, 9);

        int[] expected = new int[10] { 9, 1, 2, 3, 4, 5, 6, 7, 8, 0 };

        for (int i = 0; i < 10; i++)
            Assert.AreEqual(expected[i], intList[i]);
    }
    [Test]
    public void Swap_OutOfRange()
    {
        List<int> intList = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };

        intList.Swap(-1, 9);
        intList.Swap(0, 10);
        intList.Swap(-1, 10);

        int[] expected = new int[10] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };

        for (int i = 0; i < 10; i++)
            Assert.AreEqual(expected[i], intList[i]);
    }

    [Test]
    public void ListComparisonDirectionTest()
    {
        List<int> intList = new List<int>() { 7, 5, 9, 8, 4, 6, 2, 3, 1 };

        intList.Sort((a, b) => a - b);

        Debug.Log(intList[0]);
    }
}
