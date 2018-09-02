using System.Collections.Generic;
using NUnit.Framework;

/*
 *  测试代码，需要 using NUnit.Fra,ework
 *  测试类加 [TestFixture]，测试方法加 [Test]
 *  可以在Unity的 TestRunner 面板进行测试
 */
[TestFixture]
public class MinBinaryHeapWithGenericTest
{
    [Test]
    public void SetNode_Node_Normal_1()
    {
        MinBinaryHeapWithGeneric<string> binary = new MinBinaryHeapWithGeneric<string>();

        /*
         *  0 1 2 3 4 5 6
         *  
         *     0
         *   1   2
         *  3 4 5 6
         *  
         *  0 1 2 3 4 5 6
         */

        binary.SetNode(new Node<string> { obj = "0", value = 0 });
        binary.SetNode(new Node<string> { obj = "1", value = 1 });
        binary.SetNode(new Node<string> { obj = "2", value = 2 });
        binary.SetNode(new Node<string> { obj = "3", value = 3 });
        binary.SetNode(new Node<string> { obj = "4", value = 4 });
        binary.SetNode(new Node<string> { obj = "5", value = 5 });
        binary.SetNode(new Node<string> { obj = "6", value = 6 });

        List<Node<string>> nodes = binary.GetNodes();

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
        MinBinaryHeapWithGeneric<string> binary = new MinBinaryHeapWithGeneric<string>();

        /*
         *  6 5 4 3 2 1 0
         *  
         *     0
         *   3   1
         *  6 4 5 2
         *  
         *  0 3 1 6 4 5 2
         */

        binary.SetNode(new Node<string> { obj = "six", value = 6 });
        binary.SetNode(new Node<string> { obj = "five", value = 5 });
        binary.SetNode(new Node<string> { obj = "four", value = 4 });
        binary.SetNode(new Node<string> { obj = "three", value = 3 });
        binary.SetNode(new Node<string> { obj = "two", value = 2 });
        binary.SetNode(new Node<string> { obj = "one", value = 1 });
        binary.SetNode(new Node<string> { obj = "zero", value = 0 });

        List<Node<string>> nodes = binary.GetNodes();

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
        MinBinaryHeapWithGeneric<string> binary = new MinBinaryHeapWithGeneric<string>();

        /*
         *  1.1  2.5  721  6.8  194  0.3  9.9 
         *  
         *           0.3
         *     2.5         1.1
         *  6.8   194   721   9.9
         *  
         *  0.3  2.5  1.1  6.8  194  721  9.9
         */

        binary.SetNode(new Node<string> { obj = "1", value = 1.1f });
        binary.SetNode(new Node<string> { obj = "2", value = 2.5f });
        binary.SetNode(new Node<string> { obj = "7", value = 721f });
        binary.SetNode(new Node<string> { obj = "6", value = 6.8f });
        binary.SetNode(new Node<string> { obj = "1", value = 194f });
        binary.SetNode(new Node<string> { obj = "0", value = 0.3f });
        binary.SetNode(new Node<string> { obj = "9", value = 9.9f });

        List<Node<string>> nodes = binary.GetNodes();

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
        MinBinaryHeapWithGeneric<string> binary = new MinBinaryHeapWithGeneric<string>();

        /*
         *  6 5 4 5 2 1 0
         *  
         *     0
         *   4   1
         *  6 5 5 2
         *  
         *  0 4 1 6 5 5 2
         */

        binary.SetNode(new Node<string> { obj = "six", value = 6 });
        binary.SetNode(new Node<string> { obj = "five", value = 5 });
        binary.SetNode(new Node<string> { obj = "four", value = 4 });
        binary.SetNode(new Node<string> { obj = "five", value = 5 });
        binary.SetNode(new Node<string> { obj = "two", value = 2 });
        binary.SetNode(new Node<string> { obj = "one", value = 1 });
        binary.SetNode(new Node<string> { obj = "zero", value = 0 });

        List<Node<string>> nodes = binary.GetNodes();

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
        MinBinaryHeapWithGeneric<string> binary = new MinBinaryHeapWithGeneric<string>();

        /*
         *  0 1 2 3 4 5 6
         *  
         *     0
         *   1   2
         *  3 4 5 6
         *  
         *  0 1 2 3 4 5 6
         */

        binary.SetNode("0", 0);
        binary.SetNode("1", 1);
        binary.SetNode("2", 2);
        binary.SetNode("3", 3);
        binary.SetNode("4", 4);
        binary.SetNode("5", 5);
        binary.SetNode("6", 6);

        List<Node<string>> nodes = binary.GetNodes();

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
        MinBinaryHeapWithGeneric<string> binary = new MinBinaryHeapWithGeneric<string>();

        /*
         *  0 1 2 3 4 5 6
         *  
         *     0
         *   1   2
         *  3 4 5 6
         *  
         *  0 1 2 3 4 5 6
         */

        binary.SetNode(new Node<string> { obj = "0", value = 0 });
        binary.SetNode(new Node<string> { obj = "1", value = 1 });
        binary.SetNode(new Node<string> { obj = "2", value = 2 });
        binary.SetNode(new Node<string> { obj = "3", value = 3 });
        binary.SetNode(new Node<string> { obj = "4", value = 4 });
        binary.SetNode(new Node<string> { obj = "5", value = 5 });
        binary.SetNode(new Node<string> { obj = "6", value = 6 });

        Assert.AreEqual("0", binary.GetTopNodeObject());
    }
    [Test]
    public void GetTopNodeObject_Null()
    {
        MinBinaryHeapWithGeneric<string> binary = new MinBinaryHeapWithGeneric<string>();
        
        Assert.AreEqual(null, binary.GetTopNodeObject());
    }


    [Test]
    public void RemoveTop_Normal()
    {
        MinBinaryHeapWithGeneric<string> binary = new MinBinaryHeapWithGeneric<string>();

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

        binary.SetNode(new Node<string> { obj = "0", value = 0 });
        binary.SetNode(new Node<string> { obj = "1", value = 1 });
        binary.SetNode(new Node<string> { obj = "2", value = 2 });
        binary.SetNode(new Node<string> { obj = "3", value = 3 });
        binary.SetNode(new Node<string> { obj = "4", value = 4 });
        binary.SetNode(new Node<string> { obj = "5", value = 5 });
        binary.SetNode(new Node<string> { obj = "6", value = 6 });

        binary.RemoveTop();

        List<Node<string>> nodes = binary.GetNodes();

        float[] valueExpected = new float[6] { 1, 3, 2, 6, 4, 5 };

        for (int i = 0; i < 6; i++)
        {
            Assert.AreEqual(valueExpected[i], nodes[i].value);
        }
    }
    [Test]
    public void RemoveTop_Null()
    {
        MinBinaryHeapWithGeneric<string> binary = new MinBinaryHeapWithGeneric<string>();

        binary.RemoveTop();

        Assert.AreEqual(0, binary.GetNodes().Count);
    }


    [Test]
    public void RemoveNode_Normal_Null()
    {
        MinBinaryHeapWithGeneric<string> binary = new MinBinaryHeapWithGeneric<string>();

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

        binary.SetNode(null, 0);
        binary.SetNode(null, 5);
        binary.SetNode(null, 2);
        binary.SetNode(null, 6);
        binary.SetNode(null, 7);
        binary.SetNode(null, 5);
        binary.SetNode(null, 3);
        binary.SetNode(null, 8);
        binary.SetNode(null, 9);
        binary.SetNode(null, 9);
        binary.SetNode(null, 8);
        binary.SetNode(null, 6);
        binary.SetNode(null, 7);
        binary.SetNode(null, 4);
        binary.SetNode(null, 4);

        List<Node<string>> nodes = binary.GetNodes();

        
        binary.RemoveFirstThroughValue(6);

        float[] valueExpected = new float[14] { 0, 4, 2, 5, 7, 5, 3, 8, 9, 9, 8, 6, 7, 4 };

        for (int i = 0; i < 14; i++)
            Assert.AreEqual(valueExpected[i], nodes[i].value);


        binary.RemoveFirstThroughValue(5);

        valueExpected = new float[13] { 0, 4, 2, 4, 7, 5, 3, 8, 9, 9, 8, 6, 7 };

        for (int i = 0; i < 13; i++)
            Assert.AreEqual(valueExpected[i], nodes[i].value);


        binary.RemoveFirstThroughValue(199);

        valueExpected = new float[13] { 0, 4, 2, 4, 7, 5, 3, 8, 9, 9, 8, 6, 7 };

        for (int i = 0; i < 13; i++)
            Assert.AreEqual(valueExpected[i], nodes[i].value);
    }


    [Test]
    public void ContainsValue_CanFind()
    {
        MinBinaryHeapWithGeneric<string> binary = new MinBinaryHeapWithGeneric<string>();

        /*
         *  0 1 2 3 4 5 6
         */

        binary.SetNode(new Node<string> { obj = "0", value = 0 });
        binary.SetNode(new Node<string> { obj = "1", value = 1 });
        binary.SetNode(new Node<string> { obj = "2", value = 2 });
        binary.SetNode(new Node<string> { obj = "3", value = 3 });
        binary.SetNode(new Node<string> { obj = "4", value = 4 });
        binary.SetNode(new Node<string> { obj = "5", value = 5 });
        binary.SetNode(new Node<string> { obj = "6", value = 6 });

        Assert.IsTrue(binary.ContainsValue(0));
        Assert.IsTrue(binary.ContainsValue(1));
        Assert.IsTrue(binary.ContainsValue(2));
        Assert.IsTrue(binary.ContainsValue(3));
        Assert.IsTrue(binary.ContainsValue(4));
        Assert.IsTrue(binary.ContainsValue(5));
        Assert.IsTrue(binary.ContainsValue(6));
    }
    [Test]
    public void ContainsValue_CantFind()
    {
        MinBinaryHeapWithGeneric<string> binary = new MinBinaryHeapWithGeneric<string>();

        /*
         *  0 1 2 3 4 5 6
         */

        binary.SetNode(new Node<string> { obj = "0", value = 0 });
        binary.SetNode(new Node<string> { obj = "1", value = 1 });
        binary.SetNode(new Node<string> { obj = "2", value = 2 });
        binary.SetNode(new Node<string> { obj = "3", value = 3 });
        binary.SetNode(new Node<string> { obj = "4", value = 4 });
        binary.SetNode(new Node<string> { obj = "5", value = 5 });
        binary.SetNode(new Node<string> { obj = "6", value = 6 });

        Assert.IsFalse(binary.ContainsValue(10));
        Assert.IsFalse(binary.ContainsValue(11));
        Assert.IsFalse(binary.ContainsValue(12));
        Assert.IsFalse(binary.ContainsValue(13));
        Assert.IsFalse(binary.ContainsValue(14));
        Assert.IsFalse(binary.ContainsValue(15));
        Assert.IsFalse(binary.ContainsValue(16));
    }


    [Test]
    public void FindFirstThroughValue_CanFind()
    {
        MinBinaryHeapWithGeneric<string> binary = new MinBinaryHeapWithGeneric<string>();

        /*
         *  6 5 4 3 2 1 0
         *  
         *     0
         *   3   1
         *  6 4 5 2
         *  
         *  0 3 1 6 4 5 2
         */

        binary.SetNode(new Node<string> { obj = "six", value = 6 });
        binary.SetNode(new Node<string> { obj = "five", value = 5 });
        binary.SetNode(new Node<string> { obj = "four", value = 4 });
        binary.SetNode(new Node<string> { obj = "three", value = 3 });
        binary.SetNode(new Node<string> { obj = "two", value = 2 });
        binary.SetNode(new Node<string> { obj = "one", value = 1 });
        binary.SetNode(new Node<string> { obj = "zero", value = 0 });


        Assert.AreEqual("zero", binary.FindFirstThroughValue(0));
    }
    [Test]
    public void FindFirstThroughValue_CantFind()
    {
        MinBinaryHeapWithGeneric<string> binary = new MinBinaryHeapWithGeneric<string>();

        /*
         *  6 5 4 3 2 1 0
         *  
         *     0
         *   3   1
         *  6 4 5 2
         *  
         *  0 3 1 6 4 5 2
         */

        binary.SetNode(new Node<string> { obj = "six", value = 6 });
        binary.SetNode(new Node<string> { obj = "five", value = 5 });
        binary.SetNode(new Node<string> { obj = "four", value = 4 });
        binary.SetNode(new Node<string> { obj = "three", value = 3 });
        binary.SetNode(new Node<string> { obj = "two", value = 2 });
        binary.SetNode(new Node<string> { obj = "one", value = 1 });
        binary.SetNode(new Node<string> { obj = "zero", value = 0 });


        Assert.AreEqual(null, binary.FindFirstThroughValue(7));
    }
}
