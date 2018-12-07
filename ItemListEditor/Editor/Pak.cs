using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ItemListEditor
{
	public static class Pak
	{
		public static T ToStruct<T>(Byte[] Data)
		{
			unsafe
			{
				fixed (Byte* pBuffer = Data)
				{
					return (T)Marshal.PtrToStructure(new IntPtr((void*)&pBuffer[0]), typeof(T));
				}
			}
		}
		public static T ToStruct<T>(Byte[] Data, Int32 Start)
		{
			unsafe
			{
				fixed (Byte* pBuffer = Data)
				{
					return (T)Marshal.PtrToStructure(new IntPtr((void*)&pBuffer[Start]), typeof(T));
				}
			}
		}

		public static Byte[] ToByteArray<T>(T Struct)
		{
			Byte[] Data = new Byte[Marshal.SizeOf(Struct)];

			unsafe
			{
				fixed (Byte* Buffer = Data)
				{
					Marshal.StructureToPtr(Struct, new IntPtr((void*)Buffer), true);
				}
			}

			return Data;
		}
	}
}