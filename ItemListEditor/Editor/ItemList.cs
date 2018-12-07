using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ItemListEditor
{
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
	public struct st_ItemList // 910004
	{
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 6500)]
		public st_ItemListItem[] Item;

		public Int32 CheckSum;
	}

	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
	public struct st_ItemListItem // 140
	{
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
		public String Name;                     // 0 a 63		= 64

		public Int16 Mesh;                      // 64 a 65		= 2
		public Int32 Texture;                   // 66 a 69		= 4

		public Int16 Level;                     // 70 a 71		= 2
		public Int16 Str;                       // 72 a 73		= 2
		public Int16 Int;                       // 74 a 75		= 2
		public Int16 Dex;                       // 76 a 77		= 2
		public Int16 Con;                       // 78 a 79		= 2

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
		public st_ItemListEffects[] Effect;     // 80 a 127		= 48

		public Int32 Price;                     // 128 a 131	= 4
		public UInt16 Unique;                   // 132 a 133	= 2
		public UInt16 Pos;                      // 134 a 135	= 2
		public UInt16 Extreme;                  // 136 a 137	= 2
		public UInt16 Grade;                    // 138 a 139	= 2

		public st_ItemListItem New()
		{
			st_ItemListItem rtn = new st_ItemListItem();

			rtn.Name = "";

			rtn.Mesh = 0;
			rtn.Texture = 0;

			rtn.Level = 0;
			rtn.Str = 0;
			rtn.Int = 0;
			rtn.Dex = 0;
			rtn.Con = 0;

			rtn.Effect = new st_ItemListEffects[12];

			rtn.Price = 0;
			rtn.Unique = 0;
			rtn.Pos = 0;
			rtn.Extreme = 0;
			rtn.Grade = 0;

			for (Int32 i = 0; i < rtn.Effect.Length; i++)
				rtn.Effect[i] = new st_ItemListEffects().New();

			return rtn;
		}
	}

	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
	public struct st_ItemListEffects // 4
	{
		public Int16 Index; // 0 a 1	= 2
		public Int16 Value; // 2 a 3	= 2

		public st_ItemListEffects New()
		{
			st_ItemListEffects rtn = new st_ItemListEffects();

			rtn.Index = 0;
			rtn.Value = 0;

			return rtn;
		}
	}
}