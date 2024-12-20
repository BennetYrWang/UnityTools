using System.Collections;

namespace BennetYiranWang.DataStructure
{
    
    public class UnrolledLinkedList<T> : ICollection
    {
        private Node<T> head, tail;

        private int capacity, nodeArraySize;
        //Object head = 16 byte, 1.5 cache line is 96, ideal size of an array is 80
        public UnrolledLinkedList(int initialSize)
        {
            unsafe
            {
                nodeArraySize = 80 / sizeof(T);
            }
        }
        
        private class Node<T>
        {
            public int count = 0;
            public Node<T> prev, next;
            public T[] data;


            public void Add()
            {
                UnrolledLinkedList<int> list = new UnrolledLinkedList<int>(1) {1,2,3 };
            }
            
            public void Remove()
            {
                
            }
            
            public void Trim()
            {
                
            }
        }
    }
}