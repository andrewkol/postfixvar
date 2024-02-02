using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp18
{
    class Myqueue<T>
    {
        private T[] array;
        private int count, first, last;

        public Myqueue(int n)
        {
            array = new T[n];
        }

        public void Push(T val)
        {
            if (count >= array.Length)
                throw new Exception("Queue is full");
            array[last] = val;
            count++;
            last = (last + 1) % array.Length;
        }

        public T Pop()
        {
            if (count == 0)
                throw new Exception("Queue is empty");
            count--;
            T ret = array[first];
            first = (first + 1) % array.Length;
            return ret;
        }
        public T Top()
        {
            if (count == 0)
                throw new Exception("Queue is empty");
            return array[first];
        }

        public bool isEmpty()
        {
            if (count == 0)
                return true;
            return false;
        }
        public void Clear()
        {
            count = 0;
        }
    }
}
