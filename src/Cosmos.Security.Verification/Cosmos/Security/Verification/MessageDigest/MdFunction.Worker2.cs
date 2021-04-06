﻿using System;
using Cosmos.Collections;

namespace Cosmos.Security.Verification.MessageDigest
{
    public partial class MdFunction
    {
        /// <summary>
        /// MD2 Worker
        /// </summary>
        private class Md2Worker : IMessageDigestWorker
        {
            public byte[] Hash(ReadOnlySpan<byte> buff)
            {
                buff = HashStep1(buff);
                buff = HashStep2(buff);
                var result = HashStep3(buff);
                var finish = new byte[16];
#if NETFRAMEWORK || NETSTANDARD2_0
                result.Slice(0, 16).CopyTo(finish);
#else
                result[..16].CopyTo(finish);
#endif

                return finish;
            }

            private static ReadOnlySpan<byte> HashStep1(ReadOnlySpan<byte> array)
            {
                var __c = (byte) (16 - array.Length % 16);

                if (array.Length % 16 == 0)
                {
                    __c = 16;
                    var useless = new byte[array.Length + 1];
                    array.CopyTo(useless);
#if NETFRAMEWORK || NETSTANDARD2_0
                    useless[useless.Length - 1] = __c;
#else
                    useless[^1] = __c;
#endif
                    array = useless;
                }

                while (array.Length % 16 != 0)
                {
                    var useless = new byte[array.Length + 1];
                    array.CopyTo(useless);
#if NETFRAMEWORK || NETSTANDARD2_0
                    useless[useless.Length - 1] = __c;
#else
                    useless[^1] = __c;
#endif
                    array = useless;
                }

                return array;
            }

            private static ReadOnlySpan<byte> HashStep2(ReadOnlySpan<byte> array)
            {
                var __o = new byte[array.Length + 16];
                array.CopyTo(__o);

                var __c = new byte[16];
                byte __l = 0;

                for (var i = 0; i < array.Length / 16; i++)
                {
                    for (var j = 0; j < 16; j++)
                    {
                        var c = array[i * 16 + j];
                        __c[j] = (byte) (__c[j] ^ _table[c ^ __l]);

                        __l = __c[j];
                    }
                }

                __c.Copy(0, __o, array.Length, 16);

                return __o;
            }

            private static ReadOnlySpan<byte> HashStep3(ReadOnlySpan<byte> array)
            {
                var __x = new byte[48];

                for (var i = 0; i < array.Length / 16; i++)
                {
                    for (var j = 0; j < 16; j++)
                    {
                        __x[16 + j] = array[i * 16 + j];
                        __x[32 + j] = (byte) (__x[16 + j] ^ __x[j]);
                    }


                    byte t = 0;

                    for (var f = 0; f < 18; f++)
                    {
                        for (var k = 0; k < 48; k++)
                        {
                            __x[k] = (byte) (__x[k] ^ _table[t]);
                            t = __x[k];
                        }

                        t = (byte) ((t + f) % 256);
                    }
                }

                return __x;
            }

            private static byte[] _table =
            {
                41, 46, 67, 201, 162, 216, 124, 1, 61, 54, 84, 161, 236, 240, 6, 19,
                98, 167, 5, 243, 192, 199, 115, 140, 152, 147, 43, 217, 188, 76, 130, 202,
                30, 155, 87, 60, 253, 212, 224, 22, 103, 66, 111, 24, 138, 23, 229, 18,
                190, 78, 196, 214, 218, 158, 222, 73, 160, 251, 245, 142, 187, 47, 238, 122,
                169, 104, 121, 145, 21, 178, 7, 63, 148, 194, 16, 137, 11, 34, 95, 33,
                128, 127, 93, 154, 90, 144, 50, 39, 53, 62, 204, 231, 191, 247, 151, 3,
                255, 25, 48, 179, 72, 165, 181, 209, 215, 94, 146, 42, 172, 86, 170, 198,
                79, 184, 56, 210, 150, 164, 125, 182, 118, 252, 107, 226, 156, 116, 4, 241,
                69, 157, 112, 89, 100, 113, 135, 32, 134, 91, 207, 101, 230, 45, 168, 2,
                27, 96, 37, 173, 174, 176, 185, 246, 28, 70, 97, 105, 52, 64, 126, 15,
                85, 71, 163, 35, 221, 81, 175, 58, 195, 92, 249, 206, 186, 197, 234, 38,
                44, 83, 13, 110, 133, 40, 132, 9, 211, 223, 205, 244, 65, 129, 77, 82,
                106, 220, 55, 200, 108, 193, 171, 250, 36, 225, 123, 8, 12, 189, 177, 74,
                120, 136, 149, 139, 227, 99, 232, 109, 233, 203, 213, 254, 59, 0, 29, 57,
                242, 239, 183, 14, 102, 88, 208, 228, 166, 119, 114, 248, 235, 117, 75, 10,
                49, 68, 80, 180, 143, 237, 31, 26, 219, 153, 141, 51, 159, 17, 131, 20
            };
        }
    }
}