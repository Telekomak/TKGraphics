using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Mathematics;

namespace TKGraphics.GLComponents
{
    public static class Extensions
    {
        #region Matrix to array

        #region Matrix2
        public static float[] ToArray(this Matrix2 matrix, bool rowMajor = false)
        {
            //https://austinmorlan.com/posts/opengl_matrices/
            //[row, column]
            float[] retData = new float[4];
            int index = 0;

            if (rowMajor)
            {
                for (int i = 0; i < 2; i++)
                {
                    for (int j = 0; j < 2; j++)
                    {
                        retData[index] = matrix[i, j];
                        index++;
                    }
                }

                return retData;
            }

            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    retData[index] = matrix[j, i];
                    index++;
                }
            }

            return retData;
        }
        public static float[] ToArray(this Matrix2x3 matrix, bool rowMajor = false)
        {
            float[] retData = new float[6];
            int index = 0;

            if (rowMajor)
            {
                for (int i = 0; i < 2; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        retData[index] = matrix[i, j];
                        index++;
                    }
                }

                return retData;
            }

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    retData[index] = matrix[j, i];
                    index++;
                }
            }

            return retData;
        }
        public static float[] ToArray(this Matrix2x4 matrix, bool rowMajor = false)
        {
            float[] retData = new float[8];
            int index = 0;

            if (rowMajor)
            {
                for (int i = 0; i < 2; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        retData[index] = matrix[i, j];
                        index++;
                    }
                }

                return retData;
            }

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    retData[index] = matrix[j, i];
                    index++;
                }
            }

            return retData;
        }
        #endregion

        #region Matrix3
        public static float[] ToArray(this Matrix3x2 matrix, bool rowMajor = false)
        {
            float[] retData = new float[6];
            int index = 0;

            if (rowMajor)
            {
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 2; j++)
                    {
                        retData[index] = matrix[i, j];
                        index++;
                    }
                }

                return retData;
            }

            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    retData[index] = matrix[j, i];
                    index++;
                }
            }

            return retData;
        }
        public static float[] ToArray(this Matrix3 matrix, bool rowMajor = false)
        {
            float[] retData = new float[9];
            int index = 0;

            if (rowMajor)
            {
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        retData[index] = matrix[i, j];
                        index++;
                    }
                }

                return retData;
            }

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    retData[index] = matrix[j, i];
                    index++;
                }
            }

            return retData;
        }
        public static float[] ToArray(this Matrix3x4 matrix, bool rowMajor = false)
        {
            float[] retData = new float[12];
            int index = 0;

            if (rowMajor)
            {
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        retData[index] = matrix[i, j];
                        index++;
                    }
                }

                return retData;
            }

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    retData[index] = matrix[j, i];
                    index++;
                }
            }

            return retData;
        }
        #endregion

        #region Matrix4
        public static float[] ToArray(this Matrix4x2 matrix, bool rowMajor = false)
        {
            float[] retData = new float[8];
            int index = 0;

            if (rowMajor)
            {
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 2; j++)
                    {
                        retData[index] = matrix[i, j];
                        index++;
                    }
                }

                return retData;
            }

            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    retData[index] = matrix[j, i];
                    index++;
                }
            }

            return retData;
        }
        public static float[] ToArray(this Matrix4x3 matrix, bool rowMajor = false)
        {
            float[] retData = new float[12];
            int index = 0;

            if (rowMajor)
            {
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        retData[index] = matrix[i, j];
                        index++;
                    }
                }

                return retData;
            }

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    retData[index] = matrix[j, i];
                    index++;
                }
            }

            return retData;
        }
        public static float[] ToArray(this Matrix4 matrix, bool rowMajor = false)
        {
            float[] retData = new float[16];
            int index = 0;

            if (rowMajor)
            {
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        retData[index] = matrix[i, j];
                        index++;
                    }
                }

                return retData;
            }

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    retData[index] = matrix[j, i];
                    index++;
                }
            }

            return retData;
        }
        #endregion

        #endregion
    }
}
