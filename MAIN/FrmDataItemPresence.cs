using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AsterixDisplayAnalyser
{
    public partial class FrmDataItemPresence : Form
    {
        public SharedData.Supported_Asterix_CAT_Type CAT_Type_To_Analyze;

        public FrmDataItemPresence()
        {
            InitializeComponent();
        }

        private void FrmDataItemPresence_Shown(object sender, EventArgs e)
        {

            // First determine what category is is to be analyzed and set a label
            if (CAT_Type_To_Analyze == SharedData.Supported_Asterix_CAT_Type.Undefined)
            {  // Ops no category is defined, let user know about it
                this.Text = this.Text + " NO Asterix CAT is set";
                MessageBox.Show("No Asterix category is set. Please call the method with Asterix categroy set");

            }
            else
            {
                // We know that category is set, so lets first indicate what category 
                // items we are displaying
                this.Text = this.Text + " " + CAT_Type_To_Analyze.ToString();

                // Here do a switch on the CAT_Type_To_Analyze and depending on 
                // the desired category determine what UAP data items are present. 

                // Now do a switch based on the category received

                switch (CAT_Type_To_Analyze)
                {

                    case SharedData.Supported_Asterix_CAT_Type.CAT001:
                        {

                            // Now retrieve data from the CAT001 class and populate
                            // list for each UAP data item
                            foreach (CAT01.CAT01DataItem Item in CAT01.I001DataItems)
                            {
                                this.DataItemListBox.Items.Add(Item.ID + "     " + Item.Description);
                                this.StatusListBox.Items.Add(Item.HasBeenPresent.ToString());
                            }

                            break;
                        }
                    case SharedData.Supported_Asterix_CAT_Type.CAT002:
                        {

                            // Now retrieve data from the CAT002 class and populate
                            // list for each UAP data item
                            foreach (CAT02.CAT02DataItem Item in CAT02.I002DataItems)
                            {
                                this.DataItemListBox.Items.Add(Item.ID + "     " + Item.Description);
                                this.StatusListBox.Items.Add(Item.HasBeenPresent.ToString());
                            }
                            break;
                        }

                    case SharedData.Supported_Asterix_CAT_Type.CAT008:
                        {

                            // Now retrieve data from the CAT008 class and populate
                            // list for each UAP data item
                            foreach (CAT08.I008DataItem Item in CAT08.I008DataItems)
                            {
                                this.DataItemListBox.Items.Add(Item.ID + "     " + Item.Description);
                                this.StatusListBox.Items.Add(Item.IsPresent.ToString());
                            }

                            break;
                        }

                    case SharedData.Supported_Asterix_CAT_Type.CAT034:
                        {

                            // Now retrieve data from the CAT034 class and populate
                            // list for each UAP data item

                            //string ingredient in sandwich

                            // Now retrieve data from the CAT001 class and populate
                            // list for each UAP data item
                            foreach (CAT34.CAT34DataItem Item in CAT34.I034DataItems)
                            {
                                this.DataItemListBox.Items.Add(Item.ID + "     " + Item.Description);
                                this.StatusListBox.Items.Add(Item.HasBeenPresent.ToString());
                            }
                            break;
                        }

                    case SharedData.Supported_Asterix_CAT_Type.CAT048:
                        {

                            // Now retrieve data from the CAT048 class and populate
                            // list for each UAP data item
                            foreach (CAT48.CAT48DataItem Item in CAT48.I048DataItems)
                            {
                                this.DataItemListBox.Items.Add(Item.ID + "     " + Item.Description);
                                this.StatusListBox.Items.Add(Item.HasBeenPresent.ToString());
                            }

                            break;
                        }

                    case SharedData.Supported_Asterix_CAT_Type.CAT062:
                        {

                            // Now retrieve data from the CAT062 class and populate
                            // list for each UAP data item
                            foreach (CAT62.CAT062DataItem Item in CAT62.I062DataItems)
                            {
                                this.DataItemListBox.Items.Add(Item.ID + "     " + Item.Description);
                                this.StatusListBox.Items.Add(Item.HasBeenPresent.ToString());
                            }

                            break;
                        }

                    case SharedData.Supported_Asterix_CAT_Type.CAT063:
                        {

                            // Now retrieve data from the CAT002 class and populate
                            // list for each UAP data item
                            foreach (CAT63.I063DataItem Item in CAT63.I063DataItems)
                            {
                                this.DataItemListBox.Items.Add(Item.ID + "     " + Item.Description);
                                this.StatusListBox.Items.Add(Item.IsPresent.ToString());
                            }

                            break;
                        }

                    case SharedData.Supported_Asterix_CAT_Type.CAT065:
                        {

                            // Now retrieve data from the CAT002 class and populate
                            // list for each UAP data item
                            foreach (CAT65.I065DataItem Item in CAT65.I065DataItems)
                            {
                                this.DataItemListBox.Items.Add(Item.ID + "     " + Item.Description);
                                this.StatusListBox.Items.Add(Item.IsPresent.ToString());
                            }

                            break;
                        }

                    default:

                        break;

                }

            }

        }

        private void FrmDataItemPresence_Load(object sender, EventArgs e)
        {

        }

        // On form closed always clear the correspoding collection of the category handled
        // as next time it is shown it shows fresh data.
        private void FrmDataItemPresence_FormClosed(object sender, FormClosedEventArgs e)
        {
          
        }
    }
}
