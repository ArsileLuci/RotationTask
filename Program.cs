using System;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace RotatorApp
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //int[] input = new int[] { 1, 4, 16, 8, 8, 16, 32, 16, 8, 4, 4, 32,64, 128, 128, 128, 64, 32, 16, 8, 8, 8};
            int[] input = new int[]
            {
                1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
                1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
                1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
                1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
                1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
                1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
                1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
                1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
                1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
                1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
                1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
                1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
                1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
                1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
                1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
                1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
                1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
                1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
                1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
                1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
                1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
                1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
                1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1
            };
            Console.WriteLine($"Vector<int> Size:{Vector<int>.Count}");
            int arrSize = 1;
            int[] arr = null;
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            sw.Start();
            List<byte> path = null;
            for (int i = 0; i < 100000; i++)
            {
                path = new List<byte>();
                (arr, path) = tryL(input, input.Length, path, out arrSize);
            }
            sw.Stop();
            Console.WriteLine($"{sw.ElapsedMilliseconds}");
            int[] x = new int[arrSize];
            Array.Copy(arr, 0, x, 0, arrSize);
            foreach (int item in x)
            {
                Console.Write($"{item} ");
            }
            Console.WriteLine();
            foreach (byte b in path)
            {
                Console.Write(b == 0 ? "R" : "L");
                Console.Write(" ");
            }
        }

        private static (int[], List<byte>) tryL(int[] input, int curSize, List<byte> path, out int arrSize)
        {
            int size = Vector<int>.Count;

            arrSize = _gel(input.Length, size);
            int prevArrSize = int.MaxValue;

            bool flag = false;
            int iteratableLength = input.Length;

            int[] bestArr = null;
            int bestSize = int.MaxValue;
            List<byte> bestPath = null;

            int[] arr = new int[arrSize];
            int[] prevValues = new int[arrSize];
            int[] actualValue = new int[arrSize];
            input.CopyTo(actualValue, 0);
            arrSize = curSize;
            while (prevArrSize > arrSize)
            {
                //actualValue.CopyTo(prevValues, 0);
                //actualValue.CopyTo(arr, 0);
                arr[arrSize - 1] = actualValue[arrSize - 1];
                prevValues[arrSize - 1] = actualValue[arrSize - 1];
                prevArrSize = arrSize;
                for (int i = 0; i < iteratableLength - 1; i += size)
                {
                    Vector<int> vf = new Vector<int>(actualValue, i);
                    Vector<int> vs = new Vector<int>(actualValue, i + 1);

                    vf.CopyTo(prevValues, i);
                    ((vf ^ vs) & vf).CopyTo(arr, i);
                }
                int offset = 0;

                for (int i = 0; i < arrSize; i++)
                {
                    if (arr[i] == 0)
                    {
                        int zerosCount = 1;
                        while (arr[i + zerosCount] == 0)
                        {
                            zerosCount++;
                        }
                        for (int j = 0; j < ((zerosCount >> 1) + 1); j++)
                        {
                            actualValue[i + j - offset] = arr[i + zerosCount] << 1;
                        }
                        if ((zerosCount & 0x1) == 0)
                        {
                            flag = true;
                            actualValue[i + (zerosCount >> 1) - offset] = arr[i + zerosCount];
                        }

                        offset += (zerosCount >> 1) + (zerosCount & 0x1);
                        i += zerosCount;
                    }
                    else
                    {
                        actualValue[i - offset] = arr[i];
                    }
                }

                arrSize -= offset;
                for (int i = arrSize; i < arrSize + offset; i++)
                {
                    arr[i] = 0;
                    actualValue[i] = 0;
                }

                iteratableLength = _gel(arrSize, size);


                if (flag)
                {
                    flag = false;
                    List<byte> tmpPath = new List<byte>(path);
                    int[] tmpArr;
                    (tmpArr, tmpPath) = tryR(prevValues, prevArrSize, tmpPath, out int sizeL);
                    if (sizeL < bestSize)
                    {
                        bestSize = sizeL;
                        bestArr = tmpArr;
                        bestPath = tmpPath;
                    }
                }
                path.Add(1);
            }

            if (bestSize < arrSize)
            {
                arrSize = bestSize;
                return (bestArr, bestPath);
            }
            return (actualValue, path);
        }

        private static (int[], List<byte>) tryR(int[] input, int curSize, List<byte> path, out int arrSize)
        {
            int size = Vector<int>.Count;

            arrSize = _gel(input.Length, size);
            int prevArrSize = int.MaxValue;

            bool flag = false;
            int iteratableLength = input.Length;

            int[] bestArr = null;
            int bestSize = int.MaxValue;
            List<byte> bestPath = null;

            int[] arr = new int[arrSize];
            int[] prevValues = new int[arrSize];
            int[] actualValue = new int[arrSize];
            input.CopyTo(actualValue, 0);
            arrSize = curSize;
            bool cyclePassed = false;
            while (prevArrSize > arrSize)
            {

                //actualValue.CopyTo(prevValues, 0);
                //actualValue.CopyTo(arr, 0);
                arr[arrSize - 1] = actualValue[arrSize - 1];
                prevValues[arrSize - 1] = actualValue[arrSize - 1];

                prevArrSize = arrSize;
                for (int i = 0; i < iteratableLength - 1; i += size)
                {
                    Vector<int> vf = new Vector<int>(actualValue, i);
                    Vector<int> vs = new Vector<int>(actualValue, i + 1);

                    vf.CopyTo(prevValues, i);
                    ((vf ^ vs) & vf).CopyTo(arr, i);
                }
                int offset = 0;

                for (int i = 0; i < arrSize; i++)
                {
                    if (arr[i] == 0)
                    {
                        int zerosCount = 1;
                        while (arr[i + zerosCount] == 0)
                        {
                            zerosCount++;
                        }
                        for (int j = 0; j < ((zerosCount >> 1) + 1); j++)
                        {
                            actualValue[i + j - offset] = arr[i + zerosCount] << 1;
                        }
                        if ((zerosCount & 0x1) == 0)
                        {
                            flag = true;
                            actualValue[i - offset] = arr[i + zerosCount];
                        }

                        offset += (zerosCount >> 1) + (zerosCount & 0x1);
                        i += zerosCount;
                    }
                    else
                    {
                        actualValue[i - offset] = arr[i];
                    }
                }

                arrSize -= offset;
                for (int i = arrSize; i < arrSize + offset; i++)
                {
                    arr[i] = 0;
                    actualValue[i] = 0;
                }

                iteratableLength = _gel(arrSize, size);

                if (flag && cyclePassed)
                {
                    flag = false;
                    List<byte> tmpPath = new List<byte>(path);
                    int[] tmpArr;
                    (tmpArr, tmpPath) = tryL(prevValues, prevArrSize, tmpPath, out int sizeR);
                    if (sizeR < bestSize)
                    {
                        bestPath = tmpPath;
                        bestSize = sizeR;
                        bestArr = tmpArr;
                    }
                }
                path.Add(0);
                cyclePassed = true;
            }

            if (bestSize < arrSize)
            {
                arrSize = bestSize;
                return (bestArr, bestPath);
            }
            return (actualValue, path);
        }
        private static int _gel(int actual, int size)
        {
            if (actual % size == 1)
            {
                return actual;
            }
            if (actual % size == 0)
            {
                return actual + 1;
            }
            return ((actual / size) + 1) * size + 1;
        }
    }
}
