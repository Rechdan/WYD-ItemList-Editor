using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ItemListEditor {
	public partial class FormItemList : Form {
		public string FilePath = "";
		public st_ItemList List;
		public int Editing;

		public FormItemList ( ) {
			try {
				this.InitializeComponent ( );

				this.FixNumericValues ( );

				this.Shown += this.FormShow;

				this.listBox.MouseDoubleClick += this.ChangeItemSelect;

				this.slot0.CheckStateChanged += this.SlotChange;
				this.slot1.CheckStateChanged += this.SlotChange;
				this.slot2.CheckStateChanged += this.SlotChange;
				this.slot3.CheckStateChanged += this.SlotChange;
				this.slot4.CheckStateChanged += this.SlotChange;
				this.slot5.CheckStateChanged += this.SlotChange;
				this.slot6.CheckStateChanged += this.SlotChange;
				this.slot7.CheckStateChanged += this.SlotChange;
				this.slot8.CheckStateChanged += this.SlotChange;
				this.slot9.CheckStateChanged += this.SlotChange;
				this.slot10.CheckStateChanged += this.SlotChange;
				this.slot11.CheckStateChanged += this.SlotChange;
				this.slot12.CheckStateChanged += this.SlotChange;
				this.slot13.CheckStateChanged += this.SlotChange;
				this.slot14.CheckStateChanged += this.SlotChange;
				this.slot15.CheckStateChanged += this.SlotChange;

				this.save.Click += this.SaveItem;
				this.cancel.Click += this.CancelItem;
				this.clean.Click += this.CleanItem;

				this.export.Click += this.SaveItemListCSV;
				this.saveall.Click += this.SaveItemListBIN;
			}
			catch ( Exception ex ) {
				MessageBox.Show ( ex.Message );
			}
		}

		private void FormShow ( object sender , EventArgs e ) {
			try {
				this.LoadItemList ( );
			}
			catch ( Exception ex ) {
				MessageBox.Show ( ex.Message );
			}
		}

		private void ChangeItemSelect ( object sender , MouseEventArgs e ) {
			try {
				this.Editing = this.listBox.SelectedIndex;

				this.LoadItem ( );
			}
			catch ( Exception ex ) {
				MessageBox.Show ( ex.Message );
			}
		}

		private void SlotChange ( object sender , EventArgs e ) {
			try {
				this.posicao.Text = this.UpdatePosition ( ).ToString ( );
			}
			catch ( Exception ex ) {
				MessageBox.Show ( ex.Message );
			}
		}

		private void SaveItem ( object sender , EventArgs e ) {
			try {
				this.SaveItem ( );
			}
			catch ( Exception ex ) {
				MessageBox.Show ( ex.Message );
			}
		}
		private void CancelItem ( object sender , EventArgs e ) {
			try {
				if ( MessageBox.Show ( "Ao confirmar será carregado o ultimo save deste item." , "Deseja cancelar a edição desse item?" , MessageBoxButtons.YesNo , MessageBoxIcon.Question ) == DialogResult.Yes ) {
					this.LoadItem ( );
				}
			}
			catch ( Exception ex ) {
				MessageBox.Show ( ex.Message );
			}
		}
		private void CleanItem ( object sender , EventArgs e ) {
			try {
				if ( MessageBox.Show ( "Ao confirmar você terá de salvar para que a mudança tome efeito." , "Deseja limpar este item?" , MessageBoxButtons.YesNo , MessageBoxIcon.Question ) == DialogResult.Yes ) {
					this.name.Text = "";
					this.mesh.Value = 0;
					this.textura.Value = 0;
					this.level.Value = 0;
					this.forca.Value = 0;
					this.inteligencia.Value = 0;
					this.destreza.Value = 0;
					this.constituicao.Value = 0;
					this.preco.Value = 0;
					this.unique.Value = 0;
					this.posicao.Text = "";
					this.anct.Value = 0;
					this.grau.Value = 0;

					this.EF1.SelectedIndex = 0;
					this.EF2.SelectedIndex = 0;
					this.EF3.SelectedIndex = 0;
					this.EF4.SelectedIndex = 0;
					this.EF5.SelectedIndex = 0;
					this.EF6.SelectedIndex = 0;
					this.EF7.SelectedIndex = 0;
					this.EF8.SelectedIndex = 0;
					this.EF9.SelectedIndex = 0;
					this.EF10.SelectedIndex = 0;
					this.EF11.SelectedIndex = 0;
					this.EF12.SelectedIndex = 0;

					this.EFV1.Value = 0;
					this.EFV2.Value = 0;
					this.EFV3.Value = 0;
					this.EFV4.Value = 0;
					this.EFV5.Value = 0;
					this.EFV6.Value = 0;
					this.EFV7.Value = 0;
					this.EFV8.Value = 0;
					this.EFV9.Value = 0;
					this.EFV10.Value = 0;
					this.EFV11.Value = 0;
					this.EFV12.Value = 0;

					this.CheckPosition ( 0 );
				}
			}
			catch ( Exception ex ) {
				MessageBox.Show ( ex.Message );
			}
		}

		private void SaveItemListCSV ( object sender , EventArgs e ) {
			try {
				using ( SaveFileDialog save = new SaveFileDialog ( ) ) {
					save.Filter = "*.csv|*.csv";
					save.Title = "Selecione onde deseja salvar sua ItemList.csv";
					save.ShowDialog ( );

					if ( save.FileName != "" ) {
						File.Create ( save.FileName ).Close ( );

						List<string> Itens = new List<string> ( );
						ComboBox Combo = new ComboBox ( );
						string Temp = "";

						Defines.ItemEffects ( Combo );

						for ( int i = 0 ; i < 6500 ; i++ ) {
							st_ItemListItem Item = this.List.Item [ i ];

							if ( Item.Name != "" ) {
								Temp = $"{i},{Item.Name},{Item.Mesh}.{Item.Texture},{Item.Level}.{Item.Str}.{Item.Int}.{Item.Dex}.{Item.Con},{Item.Unique},{Item.Price},{Item.Pos},{Item.Extreme},{Item.Grade}";

								for ( int j = 0 ; j < Item.Effect.Length ; j++ ) {
									if ( Item.Effect [ j ].Index != 0 && Item.Effect [ j ].Index > 0 && Item.Effect [ j ].Index < Combo.Items.Count ) {
										Temp += $",{Combo.Items [ Item.Effect [ j ].Index ]},{Item.Effect [ j ].Value}";
									}
								}

								Itens.Add ( Temp );
							}
						}

						File.WriteAllLines ( save.FileName , Itens );

						MessageBox.Show ( $"Arquivo {save.FileName} salvo com sucesso!" );
					}
				}
			}
			catch ( Exception ex ) {
				MessageBox.Show ( ex.Message );
			}
		}
		private void SaveItemListBIN ( object sender , EventArgs e ) {
			try {
				using ( SaveFileDialog save = new SaveFileDialog ( ) ) {
					save.Filter = "*.bin|*.bin";
					save.Title = "Selecione onde deseja salvar sua ItemList.bin";
					save.ShowDialog ( );

					if ( save.FileName != "" ) {
						byte [] toSave = Pak.ToByteArray ( this.List );

						for ( int i = 0 ; i < toSave.Length ; i++ ) {
							toSave [ i ] ^= 0x5A;
						}

						File.Create ( save.FileName ).Close ( );
						File.WriteAllBytes ( save.FileName , toSave );

						MessageBox.Show ( $"Arquivo {save.FileName} salvo com sucesso!" );
					}
				}
			}
			catch ( Exception ex ) {
				MessageBox.Show ( ex.Message );
			}
		}

		private void FixNumericValues ( ) {
			try {

				this.mesh.Minimum = short.MinValue;
				this.mesh.Maximum = short.MaxValue;

				this.textura.Minimum = int.MinValue;
				this.textura.Maximum = int.MaxValue;

				this.level.Minimum = short.MinValue;
				this.level.Maximum = short.MaxValue;

				this.forca.Minimum = short.MinValue;
				this.forca.Maximum = short.MaxValue;

				this.inteligencia.Minimum = short.MinValue;
				this.inteligencia.Maximum = short.MaxValue;

				this.destreza.Minimum = short.MinValue;
				this.destreza.Maximum = short.MaxValue;

				this.constituicao.Minimum = short.MinValue;
				this.constituicao.Maximum = short.MaxValue;

				this.preco.Minimum = int.MinValue;
				this.preco.Maximum = int.MaxValue;

				this.unique.Minimum = ushort.MinValue;
				this.unique.Maximum = ushort.MaxValue;

				this.anct.Minimum = ushort.MinValue;
				this.anct.Maximum = ushort.MaxValue;

				this.grau.Minimum = ushort.MinValue;
				this.grau.Maximum = ushort.MaxValue;

				Defines.ItemEffects ( this.EF1 );
				Defines.ItemEffects ( this.EF2 );
				Defines.ItemEffects ( this.EF3 );
				Defines.ItemEffects ( this.EF4 );
				Defines.ItemEffects ( this.EF5 );
				Defines.ItemEffects ( this.EF6 );
				Defines.ItemEffects ( this.EF7 );
				Defines.ItemEffects ( this.EF8 );
				Defines.ItemEffects ( this.EF9 );
				Defines.ItemEffects ( this.EF10 );
				Defines.ItemEffects ( this.EF11 );
				Defines.ItemEffects ( this.EF12 );

				this.EF1.SelectedIndex = 0;
				this.EF2.SelectedIndex = 0;
				this.EF3.SelectedIndex = 0;
				this.EF4.SelectedIndex = 0;
				this.EF5.SelectedIndex = 0;
				this.EF6.SelectedIndex = 0;
				this.EF7.SelectedIndex = 0;
				this.EF8.SelectedIndex = 0;
				this.EF9.SelectedIndex = 0;
				this.EF10.SelectedIndex = 0;
				this.EF11.SelectedIndex = 0;
				this.EF12.SelectedIndex = 0;

				this.EFV1.Minimum = short.MinValue;
				this.EFV1.Maximum = short.MaxValue;

				this.EFV2.Minimum = short.MinValue;
				this.EFV2.Maximum = short.MaxValue;

				this.EFV3.Minimum = short.MinValue;
				this.EFV3.Maximum = short.MaxValue;

				this.EFV4.Minimum = short.MinValue;
				this.EFV4.Maximum = short.MaxValue;

				this.EFV5.Minimum = short.MinValue;
				this.EFV5.Maximum = short.MaxValue;

				this.EFV6.Minimum = short.MinValue;
				this.EFV6.Maximum = short.MaxValue;

				this.EFV7.Minimum = short.MinValue;
				this.EFV7.Maximum = short.MaxValue;

				this.EFV8.Minimum = short.MinValue;
				this.EFV8.Maximum = short.MaxValue;

				this.EFV9.Minimum = short.MinValue;
				this.EFV9.Maximum = short.MaxValue;

				this.EFV10.Minimum = short.MinValue;
				this.EFV10.Maximum = short.MaxValue;

				this.EFV11.Minimum = short.MinValue;
				this.EFV11.Maximum = short.MaxValue;

				this.EFV12.Minimum = short.MinValue;
				this.EFV12.Maximum = short.MaxValue;
			}
			catch ( Exception ex ) {
				MessageBox.Show ( ex.Message );
			}
		}

		public void LoadItemList ( ) {
			try {
				using ( OpenFileDialog find = new OpenFileDialog ( ) ) {
					find.Filter = "ItemList.bin|ItemList.bin";
					find.Title = "Selecione sua ItemList.bin";

					find.ShowDialog ( );

					if ( find.CheckFileExists ) {
						this.FilePath = find.FileName;

						if ( File.Exists ( this.FilePath ) ) {
							byte [] temp = File.ReadAllBytes ( this.FilePath );

							if ( temp.Length != 910004 ) {
								MessageBox.Show ( "Sua ItemList.bin é inválida! Selecione a original do seu cliente!" , "ItemList.bin inválida" , MessageBoxButtons.OK , MessageBoxIcon.Error );
								this.LoadItemList ( );
							}
							else {
								for ( int i = 0 ; i < temp.Length ; i++ ) {
									temp [ i ] ^= 0x5A;
								}

								this.List = Pak.ToStruct<st_ItemList> ( temp );

								Task.Run ( ( ) => {
									this.listBox.Invoke ( new MethodInvoker ( delegate ( ) {
										for ( int i = 0 ; i < this.List.Item.Length ; i++ ) {
											this.listBox.Items.Add ( $"{i.ToString ( ).PadLeft ( 4 , '0' )}: {this.List.Item [ i ].Name}" );
										}
									} ) );
								} );
							}
						}
						else {
							Environment.Exit ( 0 );
						}
					}
					else {
						Environment.Exit ( 0 );
					}
				}
			}
			catch ( Exception ex ) {
				MessageBox.Show ( ex.Message );
			}
		}

		public void LoadItem ( ) {
			try {
				st_ItemListItem item = this.List.Item [ this.Editing ];

				this.name.Text = item.Name;
				this.mesh.Text = item.Mesh.ToString ( );
				this.textura.Text = item.Texture.ToString ( );
				this.level.Text = item.Level.ToString ( );
				this.forca.Text = item.Str.ToString ( );
				this.inteligencia.Text = item.Int.ToString ( );
				this.destreza.Text = item.Dex.ToString ( );
				this.constituicao.Text = item.Con.ToString ( );
				this.preco.Text = item.Price.ToString ( );
				this.unique.Text = item.Unique.ToString ( );
				this.posicao.Text = item.Pos.ToString ( );
				this.anct.Text = item.Extreme.ToString ( );
				this.grau.Text = item.Grade.ToString ( );

				this.EF1.SelectedIndex = item.Effect [ 0 ].Index;
				this.EF2.SelectedIndex = item.Effect [ 1 ].Index;
				this.EF3.SelectedIndex = item.Effect [ 2 ].Index;
				this.EF4.SelectedIndex = item.Effect [ 3 ].Index;
				this.EF5.SelectedIndex = item.Effect [ 4 ].Index;
				this.EF6.SelectedIndex = item.Effect [ 5 ].Index;
				this.EF7.SelectedIndex = item.Effect [ 6 ].Index;
				this.EF8.SelectedIndex = item.Effect [ 7 ].Index;
				this.EF9.SelectedIndex = item.Effect [ 8 ].Index;
				this.EF10.SelectedIndex = item.Effect [ 9 ].Index;
				this.EF11.SelectedIndex = item.Effect [ 10 ].Index;
				this.EF12.SelectedIndex = item.Effect [ 11 ].Index;

				this.EFV1.Value = item.Effect [ 0 ].Value;
				this.EFV2.Value = item.Effect [ 1 ].Value;
				this.EFV3.Value = item.Effect [ 2 ].Value;
				this.EFV4.Value = item.Effect [ 3 ].Value;
				this.EFV5.Value = item.Effect [ 4 ].Value;
				this.EFV6.Value = item.Effect [ 5 ].Value;
				this.EFV7.Value = item.Effect [ 6 ].Value;
				this.EFV8.Value = item.Effect [ 7 ].Value;
				this.EFV9.Value = item.Effect [ 8 ].Value;
				this.EFV10.Value = item.Effect [ 9 ].Value;
				this.EFV11.Value = item.Effect [ 10 ].Value;
				this.EFV12.Value = item.Effect [ 11 ].Value;

				this.CheckPosition ( item.Pos );

				this.itemBox.Text = $"ITEM {this.Editing.ToString ( ).PadLeft ( 4 , '0' )}";
			}
			catch ( Exception ex ) {
				MessageBox.Show ( ex.Message );
			}
		}
		public void SaveItem ( ) {
			try {
				st_ItemListItem item = new st_ItemListItem ( ).New ( );

				item.Name = this.name.Text;

				item.Mesh = Convert.ToInt16 ( this.mesh.Value );
				item.Texture = Convert.ToInt32 ( this.textura.Value );

				item.Level = Convert.ToInt16 ( this.level.Value );
				item.Str = Convert.ToInt16 ( this.forca.Value );
				item.Int = Convert.ToInt16 ( this.inteligencia.Value );
				item.Dex = Convert.ToInt16 ( this.destreza.Value );
				item.Con = Convert.ToInt16 ( this.constituicao.Value );

				item.Price = Convert.ToInt32 ( this.preco.Value );
				item.Unique = Convert.ToUInt16 ( this.unique.Value );
				item.Pos = Convert.ToUInt16 ( this.posicao.Text );
				item.Extreme = Convert.ToUInt16 ( this.anct.Value );
				item.Grade = Convert.ToUInt16 ( this.grau.Value );

				item.Effect [ 0 ].Index = ( short ) ( this.EF1.SelectedIndex );
				item.Effect [ 1 ].Index = ( short ) ( this.EF2.SelectedIndex );
				item.Effect [ 2 ].Index = ( short ) ( this.EF3.SelectedIndex );
				item.Effect [ 3 ].Index = ( short ) ( this.EF4.SelectedIndex );
				item.Effect [ 4 ].Index = ( short ) ( this.EF5.SelectedIndex );
				item.Effect [ 5 ].Index = ( short ) ( this.EF6.SelectedIndex );
				item.Effect [ 6 ].Index = ( short ) ( this.EF7.SelectedIndex );
				item.Effect [ 7 ].Index = ( short ) ( this.EF8.SelectedIndex );
				item.Effect [ 8 ].Index = ( short ) ( this.EF9.SelectedIndex );
				item.Effect [ 9 ].Index = ( short ) ( this.EF10.SelectedIndex );
				item.Effect [ 10 ].Index = ( short ) ( this.EF11.SelectedIndex );
				item.Effect [ 11 ].Index = ( short ) ( this.EF12.SelectedIndex );

				item.Effect [ 0 ].Value = Convert.ToInt16 ( this.EFV1.Value );
				item.Effect [ 1 ].Value = Convert.ToInt16 ( this.EFV2.Value );
				item.Effect [ 2 ].Value = Convert.ToInt16 ( this.EFV3.Value );
				item.Effect [ 3 ].Value = Convert.ToInt16 ( this.EFV4.Value );
				item.Effect [ 4 ].Value = Convert.ToInt16 ( this.EFV5.Value );
				item.Effect [ 5 ].Value = Convert.ToInt16 ( this.EFV6.Value );
				item.Effect [ 6 ].Value = Convert.ToInt16 ( this.EFV7.Value );
				item.Effect [ 7 ].Value = Convert.ToInt16 ( this.EFV8.Value );
				item.Effect [ 8 ].Value = Convert.ToInt16 ( this.EFV9.Value );
				item.Effect [ 9 ].Value = Convert.ToInt16 ( this.EFV10.Value );
				item.Effect [ 10 ].Value = Convert.ToInt16 ( this.EFV11.Value );
				item.Effect [ 11 ].Value = Convert.ToInt16 ( this.EFV12.Value );

				this.listBox.Items [ this.Editing ] = $"{this.Editing.ToString ( ).PadLeft ( 4 , '0' )}: {item.Name}";

				this.List.Item [ this.Editing ] = item;
			}
			catch ( Exception ex ) {
				MessageBox.Show ( ex.Message );
			}
		}

		public void CheckPosition ( ushort Pos ) {
			if ( Pos >= 32768 ) {
				Pos -= 32768;
				this.slot15.Checked = true;
			}
			else {
				this.slot15.Checked = false;
			}

			if ( Pos >= 16384 ) {
				Pos -= 16384;
				this.slot14.Checked = true;
			}
			else {
				this.slot14.Checked = false;
			}

			if ( Pos >= 8192 ) {
				Pos -= 8192;
				this.slot13.Checked = true;
			}
			else {
				this.slot13.Checked = false;
			}

			if ( Pos >= 4096 ) {
				Pos -= 4096;
				this.slot12.Checked = true;
			}
			else {
				this.slot12.Checked = false;
			}

			if ( Pos >= 2048 ) {
				Pos -= 2048;
				this.slot11.Checked = true;
			}
			else {
				this.slot11.Checked = false;
			}

			if ( Pos >= 1024 ) {
				Pos -= 1024;
				this.slot10.Checked = true;
			}
			else {
				this.slot10.Checked = false;
			}

			if ( Pos >= 512 ) {
				Pos -= 512;
				this.slot9.Checked = true;
			}
			else {
				this.slot9.Checked = false;
			}

			if ( Pos >= 256 ) {
				Pos -= 256;
				this.slot8.Checked = true;
			}
			else {
				this.slot8.Checked = false;
			}

			if ( Pos >= 128 ) {
				Pos -= 128;
				this.slot7.Checked = true;
			}
			else {
				this.slot7.Checked = false;
			}

			if ( Pos >= 64 ) {
				Pos -= 64;
				this.slot6.Checked = true;
			}
			else {
				this.slot6.Checked = false;
			}

			if ( Pos >= 32 ) {
				Pos -= 32;
				this.slot5.Checked = true;
			}
			else {
				this.slot5.Checked = false;
			}

			if ( Pos >= 16 ) {
				Pos -= 16;
				this.slot4.Checked = true;
			}
			else {
				this.slot4.Checked = false;
			}

			if ( Pos >= 8 ) {
				Pos -= 8;
				this.slot3.Checked = true;
			}
			else {
				this.slot3.Checked = false;
			}

			if ( Pos >= 4 ) {
				Pos -= 4;
				this.slot2.Checked = true;
			}
			else {
				this.slot2.Checked = false;
			}

			if ( Pos >= 2 ) {
				Pos -= 2;
				this.slot1.Checked = true;
			}
			else {
				this.slot1.Checked = false;
			}

			if ( Pos >= 1 ) {
				Pos -= 1;
				this.slot0.Checked = true;
			}
			else {
				this.slot0.Checked = false;
			}
		}
		public ushort UpdatePosition ( ) {
			ushort Pos = 0;

			if ( this.slot0.Checked ) { Pos += 1; }
			if ( this.slot1.Checked ) { Pos += 2; }
			if ( this.slot2.Checked ) { Pos += 4; }
			if ( this.slot3.Checked ) { Pos += 8; }
			if ( this.slot4.Checked ) { Pos += 16; }
			if ( this.slot5.Checked ) { Pos += 32; }
			if ( this.slot6.Checked ) { Pos += 64; }
			if ( this.slot7.Checked ) { Pos += 128; }
			if ( this.slot8.Checked ) { Pos += 256; }
			if ( this.slot9.Checked ) { Pos += 512; }
			if ( this.slot10.Checked ) { Pos += 1024; }
			if ( this.slot11.Checked ) { Pos += 2048; }
			if ( this.slot12.Checked ) { Pos += 4096; }
			if ( this.slot13.Checked ) { Pos += 8192; }
			if ( this.slot14.Checked ) { Pos += 16384; }
			if ( this.slot15.Checked ) { Pos += 32768; }

			return Pos;
		}
	}
}