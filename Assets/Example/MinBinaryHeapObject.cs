using UnityEngine;
using UnityEngine.UI;

namespace MtC.Tools.BinaryHeap
{

    public class MinBinaryHeapObject : MonoBehaviour
    {
        [SerializeField]
        private InputField input;
        [SerializeField]
        private Text displayText;

        private FloatMinBinaryHeap heap = new FloatMinBinaryHeap();

        /// <summary>
        /// 向堆里存入新值
        /// </summary>
        public void SetInputValueToBinaryHeap()
        {
            heap.Add(float.Parse(input.text));

            DisplayBinaryHeap();
        }

        /// <summary>
        /// 从堆里移除第一个指定的值的节点
        /// </summary>
        public void RemoveFirst()
        {
            heap.RemoveFirstThroughObj(float.Parse(input.text));

            DisplayBinaryHeap();
        }

        /// <summary>
        /// 输出堆顶的值
        /// </summary>
        public void PrintBinaryHeapTopValue()
        {
            displayText.text = "堆顶的元素：" + heap.GetTopNodeObject();
        }

        /// <summary>
        /// 输出堆结构
        /// </summary>
        private void DisplayBinaryHeap()
        {
            displayText.text = heap.Print();
        }
    }
}
