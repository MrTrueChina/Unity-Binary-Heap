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
        /// ����������ֵ
        /// </summary>
        public void SetInputValueToBinaryHeap()
        {
            heap.Add(float.Parse(input.text));

            DisplayBinaryHeap();
        }

        /// <summary>
        /// �Ӷ����Ƴ���һ��ָ����ֵ�Ľڵ�
        /// </summary>
        public void RemoveFirst()
        {
            heap.RemoveFirstThroughObj(float.Parse(input.text));

            DisplayBinaryHeap();
        }

        /// <summary>
        /// ����Ѷ���ֵ
        /// </summary>
        public void PrintBinaryHeapTopValue()
        {
            displayText.text = "�Ѷ���Ԫ�أ�" + heap.GetTopNodeObject();
        }

        /// <summary>
        /// ����ѽṹ
        /// </summary>
        private void DisplayBinaryHeap()
        {
            displayText.text = heap.Print();
        }
    }
}
