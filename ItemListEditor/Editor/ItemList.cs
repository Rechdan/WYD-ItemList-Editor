using System.Runtime.InteropServices;

namespace ItemListEditor {
	[StructLayout ( LayoutKind.Sequential , CharSet = CharSet.Ansi , Pack = 1 )]
	public struct st_ItemList // 910004
	{
		[MarshalAs ( UnmanagedType.ByValArray , SizeConst = 6500 )]
		public st_ItemListItem [] Item;

		public int CheckSum;
	}

	[StructLayout ( LayoutKind.Sequential , CharSet = CharSet.Ansi , Pack = 1 )]
	public struct st_ItemListItem // 140
	{
		[MarshalAs ( UnmanagedType.ByValTStr , SizeConst = 64 )]
		public string Name;                    // 0 a 63		= 64

		public short Mesh;                     // 64 a 65		= 2
		public int Texture;                    // 66 a 69		= 4

		public short Level;                    // 70 a 71		= 2
		public short Str;                      // 72 a 73		= 2
		public short Int;                      // 74 a 75		= 2
		public short Dex;                      // 76 a 77		= 2
		public short Con;                      // 78 a 79		= 2

		[MarshalAs ( UnmanagedType.ByValArray , SizeConst = 12 )]
		public st_ItemListEffects [] Effect;   // 80 a 127		= 48

		public int Price;                      // 128 a 131	= 4
		public ushort Unique;                  // 132 a 133	= 2
		public ushort Pos;                     // 134 a 135	= 2
		public ushort Extreme;                 // 136 a 137	= 2
		public ushort Grade;                   // 138 a 139	= 2

		public st_ItemListItem New ( ) {
			st_ItemListItem rtn = new st_ItemListItem {
				Name = "" ,

				Mesh = 0 ,
				Texture = 0 ,

				Level = 0 ,
				Str = 0 ,
				Int = 0 ,
				Dex = 0 ,
				Con = 0 ,

				Effect = new st_ItemListEffects [ 12 ] ,

				Price = 0 ,
				Unique = 0 ,
				Pos = 0 ,
				Extreme = 0 ,
				Grade = 0
			};

			for ( int i = 0 ; i < rtn.Effect.Length ; i++ ) {
				rtn.Effect [ i ] = new st_ItemListEffects ( ).New ( );
			}

			return rtn;
		}
	}

	[StructLayout ( LayoutKind.Sequential , CharSet = CharSet.Ansi , Pack = 1 )]
	public struct st_ItemListEffects // 4
	{
		public short Index; // 0 a 1	= 2
		public short Value; // 2 a 3	= 2

		public st_ItemListEffects New ( ) {
			st_ItemListEffects rtn = new st_ItemListEffects {
				Index = 0 ,
				Value = 0
			};

			return rtn;
		}
	}
}