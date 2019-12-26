using System;
using System.IO;
using System.Runtime.InteropServices;

namespace Mimax.Performance.Interop
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var fs = new FileStream("samples.dat", FileMode.OpenOrCreate))
            {
                var buffer = new byte[4096];
                var bytesRead = fs.Read(buffer, 0, buffer.Length); //buffer.Length;
                var gch = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                unsafe
                {
                    var sum = 0.0;
                    var pDblBuff = (double*) (void*) gch.AddrOfPinnedObject();
                    for (var i = 0; i < bytesRead / sizeof(double); i++)
                        sum += pDblBuff[i];
                        //pDblBuff[i] = 231.3211233;
                    fs.Write(buffer, 0, buffer.Length);
                    gch.Free();
                }
            }

        }

        //We are taking the parameter by ref and not by out because we need to take its address,
        //and __makeref requires an initialized value.
        public static unsafe void ReadPointerTypedRef<T>(byte[] data, int offset, ref T value)
        {
            //We aren't actually modifying 'value' -- just need an lvalue to start with
            TypedReference tr = __makeref(value);
            fixed (byte* ptr = &data[offset])
            {
                //The first pointer-sized field of TypedReference is the object address, so we
                //CHAPTER 10 ■ Performance Patterns
                //303
                    //overwrite it with a pointer into the right location in the data array:
                    * (IntPtr*)&tr = (IntPtr)ptr;
                //__refvalue copies the pointee from the TypedReference to 'value'
                value = __refvalue(tr, T);
            }
        }
    }
}