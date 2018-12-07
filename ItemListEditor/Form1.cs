using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ItemListEditor
{
	public partial class FormItemList : Form
	{
		public String FilePath = "";
		public st_ItemList List;
		public Int32 Editing;

		public FormItemList()
		{
			try
			{
				this.InitializeComponent();

				this.FixNumericValues();

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
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void FormShow(Object sender, EventArgs e)
		{
			try
			{
				this.LoadItemList();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void ChangeItemSelect(Object sender, MouseEventArgs e)
		{
			try
			{
				this.Editing = this.listBox.SelectedIndex;

				this.LoadItem();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void SlotChange(Object sender, EventArgs e)
		{
			this.posicao.Text = this.UpdatePosition().ToString();
		}

		private void SaveItem(Object sender, EventArgs e)
		{
			try
			{
				this.SaveItem();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void CancelItem(Object sender, EventArgs e)
		{
			try
			{
				if (MessageBox.Show("Ao confirmar será carregado o ultimo save deste item.", "Deseja cancelar a edição desse item?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
				{
					this.LoadItem();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void CleanItem(Object sender, EventArgs e)
		{
			try
			{
				if (MessageBox.Show("Ao confirmar você terá de salvar para que a mudança tome efeito.", "Deseja limpar este item?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
				{
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

					this.CheckPosition(0);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void SaveItemListCSV(Object sender, EventArgs e)
		{
			try
			{
				using (SaveFileDialog save = new SaveFileDialog())
				{
					save.Filter = "*.csv|*.csv";
					save.Title = "Selecione onde deseja salvar sua ItemList.csv";
					save.ShowDialog();

					if (save.FileName != "")
					{
						File.Create(save.FileName).Close();

						List<String> Itens = new List<String>();
						ComboBox Combo = new ComboBox();
						String Temp = "";

						Defines.ItemEffects(Combo);

						for (Int32 i = 0; i < 6500; i++)
						{
							st_ItemListItem Item = this.List.Item[i];

							if (Item.Name != "")
							{
								Temp = $"{i},{Item.Name},{Item.Mesh}.{Item.Texture},{Item.Level}.{Item.Str}.{Item.Int}.{Item.Dex}.{Item.Con},{Item.Unique},{Item.Price},{Item.Pos},{Item.Extreme},{Item.Grade}";

								for (Int32 j = 0; j < Item.Effect.Length; j++)
								{
									if (Item.Effect[j].Index != 0 && Item.Effect[j].Index > 0 && Item.Effect[j].Index < Combo.Items.Count)
									{
										Temp += $",{Combo.Items[Item.Effect[j].Index]},{Item.Effect[j].Value}";
									}
								}

								Itens.Add(Temp);
							}
						}

						File.WriteAllLines(save.FileName, Itens);

						MessageBox.Show($"Arquivo {save.FileName} salvo com sucesso!");
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void SaveItemListBIN(Object sender, EventArgs e)
		{
			try
			{
				using (SaveFileDialog save = new SaveFileDialog())
				{
					save.Filter = "*.bin|*.bin";
					save.Title = "Selecione onde deseja salvar sua ItemList.bin";
					save.ShowDialog();

					if (save.FileName != "")
					{
						Byte[] toSave = Pak.ToByteArray<st_ItemList>(this.List);

						for (Int32 i = 0; i < toSave.Length; i++)
							toSave[i] ^= 0x5A;

						File.Create(save.FileName).Close();
						File.WriteAllBytes(save.FileName, toSave);

						MessageBox.Show($"Arquivo {save.FileName} salvo com sucesso!");
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void FixNumericValues()
		{
			try
			{

				this.mesh.Minimum = Int16.MinValue;
				this.mesh.Maximum = Int16.MaxValue;

				this.textura.Minimum = Int32.MinValue;
				this.textura.Maximum = Int32.MaxValue;

				this.level.Minimum = Int16.MinValue;
				this.level.Maximum = Int16.MaxValue;

				this.forca.Minimum = Int16.MinValue;
				this.forca.Maximum = Int16.MaxValue;

				this.inteligencia.Minimum = Int16.MinValue;
				this.inteligencia.Maximum = Int16.MaxValue;

				this.destreza.Minimum = Int16.MinValue;
				this.destreza.Maximum = Int16.MaxValue;

				this.constituicao.Minimum = Int16.MinValue;
				this.constituicao.Maximum = Int16.MaxValue;

				this.preco.Minimum = Int32.MinValue;
				this.preco.Maximum = Int32.MaxValue;

				this.unique.Minimum = UInt16.MinValue;
				this.unique.Maximum = UInt16.MaxValue;

				this.anct.Minimum = UInt16.MinValue;
				this.anct.Maximum = UInt16.MaxValue;

				this.grau.Minimum = UInt16.MinValue;
				this.grau.Maximum = UInt16.MaxValue;

				Defines.ItemEffects(this.EF1);
				Defines.ItemEffects(this.EF2);
				Defines.ItemEffects(this.EF3);
				Defines.ItemEffects(this.EF4);
				Defines.ItemEffects(this.EF5);
				Defines.ItemEffects(this.EF6);
				Defines.ItemEffects(this.EF7);
				Defines.ItemEffects(this.EF8);
				Defines.ItemEffects(this.EF9);
				Defines.ItemEffects(this.EF10);
				Defines.ItemEffects(this.EF11);
				Defines.ItemEffects(this.EF12);

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

				this.EFV1.Minimum = Int16.MinValue;
				this.EFV1.Maximum = Int16.MaxValue;

				this.EFV2.Minimum = Int16.MinValue;
				this.EFV2.Maximum = Int16.MaxValue;

				this.EFV3.Minimum = Int16.MinValue;
				this.EFV3.Maximum = Int16.MaxValue;

				this.EFV4.Minimum = Int16.MinValue;
				this.EFV4.Maximum = Int16.MaxValue;

				this.EFV5.Minimum = Int16.MinValue;
				this.EFV5.Maximum = Int16.MaxValue;

				this.EFV6.Minimum = Int16.MinValue;
				this.EFV6.Maximum = Int16.MaxValue;

				this.EFV7.Minimum = Int16.MinValue;
				this.EFV7.Maximum = Int16.MaxValue;

				this.EFV8.Minimum = Int16.MinValue;
				this.EFV8.Maximum = Int16.MaxValue;

				this.EFV9.Minimum = Int16.MinValue;
				this.EFV9.Maximum = Int16.MaxValue;

				this.EFV10.Minimum = Int16.MinValue;
				this.EFV10.Maximum = Int16.MaxValue;

				this.EFV11.Minimum = Int16.MinValue;
				this.EFV11.Maximum = Int16.MaxValue;

				this.EFV12.Minimum = Int16.MinValue;
				this.EFV12.Maximum = Int16.MaxValue;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		public void LoadItemList()
		{
			try
			{
				using (OpenFileDialog find = new OpenFileDialog())
				{
					find.Filter = "ItemList.bin|ItemList.bin";
					find.Title = "Selecione sua ItemList.bin";

					find.ShowDialog();

					if (find.CheckFileExists)
					{
						this.FilePath = find.FileName;

						if (File.Exists(this.FilePath))
						{
							Byte[] temp = File.ReadAllBytes(this.FilePath);

							if (temp.Length != 910004)
							{
								MessageBox.Show("Sua ItemList.bin é inválida! Selecione a original do seu cliente!", "ItemList.bin inválida", MessageBoxButtons.OK, MessageBoxIcon.Error);
								this.LoadItemList();
							}
							else
							{
								for (Int32 i = 0; i < temp.Length; i++)
									temp[i] ^= 0x5A;

								this.List = Pak.ToStruct<st_ItemList>(temp);

								Task.Run(() =>
								{
									this.listBox.Invoke(new MethodInvoker(delegate ()
									{
										for (Int32 i = 0; i < this.List.Item.Length; i++)
										{
											this.listBox.Items.Add($"{i.ToString().PadLeft(4, '0')}: {this.List.Item[i].Name}");
										}
									}));
								});
							}
						}
						else
						{
							Environment.Exit(0);
						}
					}
					else
					{
						Environment.Exit(0);
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		public void LoadItem()
		{
			try
			{
				st_ItemListItem item = this.List.Item[this.Editing];

				this.name.Text = item.Name;
				this.mesh.Text = item.Mesh.ToString();
				this.textura.Text = item.Texture.ToString();
				this.level.Text = item.Level.ToString();
				this.forca.Text = item.Str.ToString();
				this.inteligencia.Text = item.Int.ToString();
				this.destreza.Text = item.Dex.ToString();
				this.constituicao.Text = item.Con.ToString();
				this.preco.Text = item.Price.ToString();
				this.unique.Text = item.Unique.ToString();
				this.posicao.Text = item.Pos.ToString();
				this.anct.Text = item.Extreme.ToString();
				this.grau.Text = item.Grade.ToString();

				this.EF1.SelectedIndex = item.Effect[0].Index;
				this.EF2.SelectedIndex = item.Effect[1].Index;
				this.EF3.SelectedIndex = item.Effect[2].Index;
				this.EF4.SelectedIndex = item.Effect[3].Index;
				this.EF5.SelectedIndex = item.Effect[4].Index;
				this.EF6.SelectedIndex = item.Effect[5].Index;
				this.EF7.SelectedIndex = item.Effect[6].Index;
				this.EF8.SelectedIndex = item.Effect[7].Index;
				this.EF9.SelectedIndex = item.Effect[8].Index;
				this.EF10.SelectedIndex = item.Effect[9].Index;
				this.EF11.SelectedIndex = item.Effect[10].Index;
				this.EF12.SelectedIndex = item.Effect[11].Index;

				this.EFV1.Value = item.Effect[0].Value;
				this.EFV2.Value = item.Effect[1].Value;
				this.EFV3.Value = item.Effect[2].Value;
				this.EFV4.Value = item.Effect[3].Value;
				this.EFV5.Value = item.Effect[4].Value;
				this.EFV6.Value = item.Effect[5].Value;
				this.EFV7.Value = item.Effect[6].Value;
				this.EFV8.Value = item.Effect[7].Value;
				this.EFV9.Value = item.Effect[8].Value;
				this.EFV10.Value = item.Effect[9].Value;
				this.EFV11.Value = item.Effect[10].Value;
				this.EFV12.Value = item.Effect[11].Value;

				this.CheckPosition(item.Pos);

				this.itemBox.Text = $"ITEM {this.Editing.ToString().PadLeft(4, '0')}";
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		public void SaveItem()
		{
			try
			{
				st_ItemListItem item = new st_ItemListItem().New();

				item.Name = this.name.Text;

				item.Mesh = Convert.ToInt16(this.mesh.Value);
				item.Texture = Convert.ToInt32(this.textura.Value);

				item.Level = Convert.ToInt16(this.level.Value);
				item.Str = Convert.ToInt16(this.forca.Value);
				item.Int = Convert.ToInt16(this.inteligencia.Value);
				item.Dex = Convert.ToInt16(this.destreza.Value);
				item.Con = Convert.ToInt16(this.constituicao.Value);

				item.Price = Convert.ToInt32(this.preco.Value);
				item.Unique = Convert.ToUInt16(this.unique.Value);
				item.Pos = Convert.ToUInt16(this.posicao.Text);
				item.Extreme = Convert.ToUInt16(this.anct.Value);
				item.Grade = Convert.ToUInt16(this.grau.Value);

				item.Effect[0].Index = (Int16)(this.EF1.SelectedIndex);
				item.Effect[1].Index = (Int16)(this.EF2.SelectedIndex);
				item.Effect[2].Index = (Int16)(this.EF3.SelectedIndex);
				item.Effect[3].Index = (Int16)(this.EF4.SelectedIndex);
				item.Effect[4].Index = (Int16)(this.EF5.SelectedIndex);
				item.Effect[5].Index = (Int16)(this.EF6.SelectedIndex);
				item.Effect[6].Index = (Int16)(this.EF7.SelectedIndex);
				item.Effect[7].Index = (Int16)(this.EF8.SelectedIndex);
				item.Effect[8].Index = (Int16)(this.EF9.SelectedIndex);
				item.Effect[9].Index = (Int16)(this.EF10.SelectedIndex);
				item.Effect[10].Index = (Int16)(this.EF11.SelectedIndex);
				item.Effect[11].Index = (Int16)(this.EF12.SelectedIndex);

				item.Effect[0].Value = Convert.ToInt16(this.EFV1.Value);
				item.Effect[1].Value = Convert.ToInt16(this.EFV2.Value);
				item.Effect[2].Value = Convert.ToInt16(this.EFV3.Value);
				item.Effect[3].Value = Convert.ToInt16(this.EFV4.Value);
				item.Effect[4].Value = Convert.ToInt16(this.EFV5.Value);
				item.Effect[5].Value = Convert.ToInt16(this.EFV6.Value);
				item.Effect[6].Value = Convert.ToInt16(this.EFV7.Value);
				item.Effect[7].Value = Convert.ToInt16(this.EFV8.Value);
				item.Effect[8].Value = Convert.ToInt16(this.EFV9.Value);
				item.Effect[9].Value = Convert.ToInt16(this.EFV10.Value);
				item.Effect[10].Value = Convert.ToInt16(this.EFV11.Value);
				item.Effect[11].Value = Convert.ToInt16(this.EFV12.Value);

				this.listBox.Items[this.Editing] = $"{this.Editing.ToString().PadLeft(4, '0')}: {item.Name}";

				this.List.Item[this.Editing] = item;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		public void CheckPosition(UInt16 Pos)
		{
			if (Pos >= 32768)
			{
				Pos -= 32768;
				slot15.Checked = true;
			}
			else
			{
				slot15.Checked = false;
			}

			if (Pos >= 16384)
			{
				Pos -= 16384;
				slot14.Checked = true;
			}
			else
			{
				slot14.Checked = false;
			}

			if (Pos >= 8192)
			{
				Pos -= 8192;
				slot13.Checked = true;
			}
			else
			{
				slot13.Checked = false;
			}

			if (Pos >= 4096)
			{
				Pos -= 4096;
				slot12.Checked = true;
			}
			else
			{
				slot12.Checked = false;
			}

			if (Pos >= 2048)
			{
				Pos -= 2048;
				slot11.Checked = true;
			}
			else
			{
				slot11.Checked = false;
			}

			if (Pos >= 1024)
			{
				Pos -= 1024;
				slot10.Checked = true;
			}
			else
			{
				slot10.Checked = false;
			}

			if (Pos >= 512)
			{
				Pos -= 512;
				slot9.Checked = true;
			}
			else
			{
				slot9.Checked = false;
			}

			if (Pos >= 256)
			{
				Pos -= 256;
				slot8.Checked = true;
			}
			else
			{
				slot8.Checked = false;
			}

			if (Pos >= 128)
			{
				Pos -= 128;
				slot7.Checked = true;
			}
			else
			{
				slot7.Checked = false;
			}

			if (Pos >= 64)
			{
				Pos -= 64;
				slot6.Checked = true;
			}
			else
			{
				slot6.Checked = false;
			}

			if (Pos >= 32)
			{
				Pos -= 32;
				slot5.Checked = true;
			}
			else
			{
				slot5.Checked = false;
			}

			if (Pos >= 16)
			{
				Pos -= 16;
				slot4.Checked = true;
			}
			else
			{
				slot4.Checked = false;
			}

			if (Pos >= 8)
			{
				Pos -= 8;
				slot3.Checked = true;
			}
			else
			{
				slot3.Checked = false;
			}

			if (Pos >= 4)
			{
				Pos -= 4;
				slot2.Checked = true;
			}
			else
			{
				slot2.Checked = false;
			}

			if (Pos >= 2)
			{
				Pos -= 2;
				slot1.Checked = true;
			}
			else
			{
				slot1.Checked = false;
			}

			if (Pos >= 1)
			{
				Pos -= 1;
				slot0.Checked = true;
			}
			else
			{
				slot0.Checked = false;
			}
		}
		public UInt16 UpdatePosition()
		{
			UInt16 Pos = 0;

			if (slot0.Checked)
				Pos += 1;
			if (slot1.Checked)
				Pos += 2;
			if (slot2.Checked)
				Pos += 4;
			if (slot3.Checked)
				Pos += 8;
			if (slot4.Checked)
				Pos += 16;
			if (slot5.Checked)
				Pos += 32;
			if (slot6.Checked)
				Pos += 64;
			if (slot7.Checked)
				Pos += 128;
			if (slot8.Checked)
				Pos += 256;
			if (slot9.Checked)
				Pos += 512;
			if (slot10.Checked)
				Pos += 1024;
			if (slot11.Checked)
				Pos += 2048;
			if (slot12.Checked)
				Pos += 4096;
			if (slot13.Checked)
				Pos += 8192;
			if (slot14.Checked)
				Pos += 16384;
			if (slot15.Checked)
				Pos += 32768;

			return Pos;
		}
	}
}