using System.Collections.Generic;

namespace OnlineCafe
{
    public partial class CustomList<Type>
    {
        // To keep the capacity of the List Total Space for Elements
        private int _capacity = 0;
        //Private Count Length of the List
        private int _count = 0;
        //Total Length of List Public
        public int Count { get { return _count; } }
        // Public To keep the capacity of the List Total Space for Elements
        public int Capacity { get { return _capacity; } }
        // The array of Type can be int, char, string
        private Type[] _array;
        //To get the index of each element
        public Type this[int index]
        {
            get { return _array[index]; }
            set { _array[index] = value; }
        }
        //Intailise without Specifcing the Size
        public CustomList()
        {
            _count = 0;
            _capacity = 4;
            _array = new Type[_capacity];
        }
        //Intailise when size is provided
        public CustomList(int size)
        {
            _count = 0;
            _capacity = size;
            _array = new Type[_capacity];
        }
        //To Add an Element
        public void Add(Type element)
        {
            //To check if the max capacity is reached
            //If Yes increase the capacity
            if (_count == _capacity)
            {
                GrowSize();
            }
            _array[_count] = element;
            _count++;

        }
        //To extend the size of the List
        //By Creating a new List with size double of old List
        //And adding the old elements
        void GrowSize()
        {
            _capacity = _capacity * 2;
            Type[] temp = new Type[_capacity];
            for (int i = 0; i < _count; i++)
            {
                temp[i] = _array[i];
            }
            _array = temp;
        }
        //To add a List of Array to another Array
        public void AddRange(CustomList<Type> elements)
        {
            _capacity = _capacity + elements.Count + 4;
            //Total Capacity first array + second array + 4 (extra)
            Type[] temp = new Type[_capacity];
            //temp array
            for (int i = 0; i < _count; i++)
            {
                temp[i] = _array[i];
                //Iterate first array to temp list
            }
            int k = 0;
            //To Iterate second List
            for (int i = _count; i < _count + elements.Count; i++)
            {
                temp[i] = elements[k];
                //Iterate second array to temp list
                k++;
            }
            _array = temp;
            _count = _count + elements.Count;
        }
        //To check if an element is present in an array
        public bool Contains(Type element)
        {
            bool isContains = false;
            foreach (Type data in _array)
            {
                if (data.Equals(element))
                {
                    //If found isContains is true
                    isContains = true;
                    break;
                }
            }
            return isContains;
        }

        //To find the index of an element
        public int IndexOf(Type element)
        {
            int index = -1;
            for (int i = 0; i < _capacity; i++)
            {
                if (_array[i].Equals(element))
                {
                    index = i;
                    break;
                }
            }
            return index;
        }

        //To Insert At a Index
        //Copying to a tem array
        public void Insert(int index, Type element)
        {
            _capacity = _capacity + 1 + 4;
            Type[] temp = new Type[_capacity];
            for (int i = 0; i < _count + 1; i++)
            {
                if (i < index)
                {
                    //Before insert index
                    temp[i] = _array[i];
                }
                else if (i == index)
                {
                    //At insert index
                    temp[i] = element;
                }
                else
                {
                    //After Inserting i value - 1
                    temp[i] = _array[i - 1];
                }
            }
            _array = temp;
            //Increase Count as as i value inserted
            _count++;
        }


        //1,2,3,4,5
        //1,2,4,5
        public void RemoveAt(int index)
        {
            //Cannot Remove have Overwrite
            for (int i = 0; i < _count - 1; i++)
            {
                if (i >= index)
                {
                    _array[i] = _array[i + 1];
                }
            }
            _count--;
        }

        //Remove element
        public bool Remove(Type element)
        {
            int index=IndexOf(element);
            if(index>=0)
            {
                RemoveAt(index);
                return true;
            }
            return false;
        }

        //To Reverse an Array
        public void Reverse()
        {
            Type[] temp=new Type[_capacity];
            int j=0;
            for(int i=_count-1;i>=0;i--)
            {
                temp[j]=_array[i];
                j++;
            }
            _array=temp;
        }

        //To Insert List of elements at a Range
        public void InsertRange(int index,CustomList<Type> elements)
        {
            _capacity=_capacity+elements.Count+4;
            Type[] temp=new Type[_capacity];
            for(int i=0;i<index;i++)
            {
                //Upto Index Position
                temp[i]=_array[i];
            }
            int j=0;
            for(int i=index;i<index+elements.Count;i++)
            {
                //After index position to end of second List
                temp[i]=elements[j];
                j++;
            }
            int k=0;
            for(int i=index+elements.Count;i<_count+elements.Count;i++)
            {
                //Remaining elements in first list
                temp[i]=_array[index+k];
                k++;
            }
            _array=temp;
            _count=_count+elements.Count;
        }

        //Sorting List Bubble Sort
        public void Sort()
        {
            for(int i=0;i<_count-1;i++)
            {
                for(int j=0;j<_count-1;j++)
                {
                    if(isGreater(_array[j],_array[j+1]))
                    {
                        //Swap
                        Type temp=_array[j];
                        _array[j]=_array[j+1];
                        _array[j+1]=temp;
                    }
                }
            }
        }

        public bool isGreater(Type value1,Type value2)
        {
            //Compare
            int compare=Comparer<Type>.Default.Compare(value1,value2);
            if(compare>0)
            {
                return true;
            }
            return false;
        }
    }
}



//Remove Logic
// bool isEqual = false;
// for (int i = 0; i < _count - 1; i++)
// {
//     if (isEqual || _array[i].Equals(element))
//     {
//         isEqual = true;
//         _array[i] = _array[i + 1];
//     }
// }
// _count--;