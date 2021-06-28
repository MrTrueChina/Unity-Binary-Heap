using UnityEngine;
using UnityEngine.UI;

namespace MtC.Tools.BinaryHeap
{

    public class MinBinaryHeapObject : MonoBehaviour
    {
        [SerializeField]        //让private的变量在inspector面板显示
        InputField _input;
        [SerializeField]
        Text _displayText;

        static MinBinaryHeapDemo _binaryHeap;


        private void Awake()
        {
            _binaryHeap = new MinBinaryHeapDemo();
        }


        public void SetInputValueToBinaryHeap()
        {
            _binaryHeap.SetValue(float.Parse(_input.text));

            DisplayBinaryHeap();
        }

        public void RemoveBinaryHeapTopNode()
        {
            _binaryHeap.RemoveTop();

            DisplayBinaryHeap();
        }

        public void PrintBinaryHeapTopValue()
        {
            Debug.Log(_binaryHeap.GetTop());
        }

        void DisplayBinaryHeap()
        {
            _displayText.text = _binaryHeap.Print();
        }
    }
}
