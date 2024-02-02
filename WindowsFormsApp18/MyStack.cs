using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp18
{
    class MyStack<T>
    {
        private T[] array;
        private int count;
        public MyStack(int n)
        {
            array = new T[n];
        }
        public void Push(T a)
        {
            if (count >= array.Length)
            {
                throw new Exception("Stack overflow");
            }
            array[count++] = a;
        }

        public T Pop()
        {
            if (count == 0)
            {
                throw new Exception("Stack is empty");
            }
            return array[--count];
        }

        public T Top()
        {
            if (count <= 0)
                throw new Exception("Stack is empty");
            return array[count - 1];
        }

        public bool isEmpty()
        {
            if (count <= 0)
                return true;
            else
                return false;
        }
        public void Clear()
        {
            count = 0;
        }
    }
}
