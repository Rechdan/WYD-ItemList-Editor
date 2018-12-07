using System;
using System.Runtime.InteropServices;

namespace ItemListEditor {
	public static class Pak {
		public static T ToStruct<T> ( byte [] Data ) {
			unsafe {
				fixed ( byte* pBuffer = Data ) {
					return ( T ) Marshal.PtrToStructure ( new IntPtr ( ( void* ) &pBuffer [ 0 ] ) , typeof ( T ) );
				}
			}
		}
		public static T ToStruct<T> ( byte [] Data , int Start ) {
			unsafe {
				fixed ( byte* pBuffer = Data ) {
					return ( T ) Marshal.PtrToStructure ( new IntPtr ( ( void* ) &pBuffer [ Start ] ) , typeof ( T ) );
				}
			}
		}

		public static byte [] ToByteArray<T> ( T Struct ) {
			byte [] Data = new byte [ Marshal.SizeOf ( Struct ) ];

			unsafe {
				fixed ( byte* Buffer = Data ) {
					Marshal.StructureToPtr ( Struct , new IntPtr ( ( void* ) Buffer ) , true );
				}
			}

			return Data;
		}
	}
}