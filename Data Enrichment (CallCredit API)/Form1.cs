using System;
using System.Windows.Forms;
using System.IO;
using System.Collections;
using System.Net;
using System.Text;

namespace Data_Enrichment__CallCredit_API_
{
    public partial class MainWindow : Form
    {
        int total_recs;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void button_Enrich_Click(object sender, EventArgs e)
        {
            // Declare variables
            string file_location;

            // Retrieve file location
            file_location = fileSelected.Text;

            // Check all fields are not blank
            if ((map_n.Text == "") && (map_t.Text == "") && (map_f.Text == "") && (map_s.Text == "") && (map_mob.Text == "") && (map_a1.Text == "") && (map_a2.Text == "") && (map_a3.Text == "") && (map_a4.Text == "") && (map_a5.Text == "") && (map_a6.Text == "") && (map_a6.Text == "") && (map_a7.Text == "") && (map_zip.Text == ""))
            {
                MessageBox.Show("No fields are mapped.");
            }
            else 
            { 

                // Check blank
                if ((file_location != "") && (label_FileOkay.Text != "File invalid."))
                {
                    // Check that there are no matching mapped fields
                    string error = CheckDropDowns();

                    // Proceed (or not)
                    if (error == "")
                    {
                        // Set filter options
                        saveFileDialog.Filter = "CSV Files (*.csv)|*.csv";

                        // Set default save location
                        saveFileDialog.InitialDirectory = file_location.Replace(".csv", "_enriched.csv");

                        // Display save prompt
                        saveFileDialog.ShowDialog();

                        // Select save location
                        string save_location = saveFileDialog.FileName;

                        // Proceed once save file selected
                        if (save_location != "")
                        {
                            // Perform main operations using file
                            MainOperation(file_location, save_location);
                        }

                    }
                    else
                    {
                        MessageBox.Show(error);
                    }

                }

            }

        }

        private void Button_Browse_Click(object sender, EventArgs e)
        {
            // Set filter options
            openFileDialog.Filter = "CSV Files (*.csv)|*.csv";

            // Call openFileDialog
            openFileDialog.ShowDialog();

            // Store file selected
            fileSelected.Text = openFileDialog.FileName;

            // Check not blank
            if (fileSelected.Text != "")
            {
                // Check file selected okay
                if (fileSelected.Text.Substring(fileSelected.TextLength - 3, 3).ToUpper() == "CSV")
                {
                    // Count Records
                    int record_qty = CountFileRecords(fileSelected.Text);

                    // Display okay
                    label_FileOkay.Text = "File valid (" + record_qty + " records). It is okay to proceed.";

                    // Clear Drop Downs
                    ResetDropDowns(fileSelected.Text);

                    // Populate drop downs
                    PopulateDropDowns(fileSelected.Text);

                    // Enable Drop Downs
                    EnableDropDowns("enable");

                    // Update StatusBox
                    StatusBoxText.Text = "Input: " + fileSelected.Text;
                }
                else
                {
                    // Check not okay
                    label_FileOkay.Text = "File invalid.";
                }
            }

        }

        private void MainOperation(string file, string save_location)
        {
            // Disable GUI
            EnableDropDowns("Disable");

            // Open FileStream
            StreamReader read_stream = new StreamReader(file);

            // Counter
            int num_of_records = -1;
            string line = "";

            // Open StreamWriter
            StreamWriter write_stream = new StreamWriter(save_location);

            // Record start time
            DateTime startTime = DateTime.Now;

            // Loop through data file (each record)
            do
            {
                // Iterate count
                num_of_records++;

                // Progress
                label_FileOkay.Text = "Processing record " + num_of_records + " of " + total_recs;

                // Update
                Application.DoEvents();

                // Read line
                line = read_stream.ReadLine();

                // Append to current record
                if (num_of_records == 0)
                {
                    // New headings
                    string headings = @",""AgeBand"",""AgeBand_Desc"",""AgeBand_mdl"",""AgeBand_mdl_Desc"",""CAMEOUKCategories_Domicile"",""CAMEOUKCategories_Domicile_Desc"",""CAMEOUKCategories_Individual"",""CAMEOUKCategories_Individual_Desc"",""CAMEOUKGroup_Domicile"",""CAMEOUKGroup_Domicile_Desc"",""CAMEOUKGroup_Individual"",""CAMEOUKGroup_Individual_Desc"",""DOB"",""EarlyAdopter_BND"",""EarlyAdopter_BND_Desc"",""EarlyAdopter_YN"",""EmploymentStatusId"",""EmploymentStatusId_Desc"",""GreenEthical"",""GreenEthical_Desc"",""HouseholdIncome_mdl"",""HouseholdIncome_mdl_Desc"",""HouseholdLifestyle_mdl"",""HouseholdLifestyle_mdl_Desc"",""HouseholdSocialClass_mdl"",""HouseholdSocialClass_mdl_Desc"",""LengthOfResidency"",""MaritalStatusId"",""MaritalStatusId_Desc"",""OccupationBandId"",""OccupationBandId_Desc"",""SavingsAccounts_BND"",""SavingsAccounts_BND_Desc"",""Sex"",""MATCH_LEVEL""";

                    // Append new headings
                    write_stream.WriteLine(line + "" + headings);
                }
                else
                {
                    // Retrieve record details
                    string[] mapped_data = RetrieveMappedData(line);

                    // New data
                    string appended_data = CallAppendingAPI(mapped_data);

                    // Append data
                    write_stream.WriteLine(line + "" + appended_data);
                }

                // Calculate time
                TimeSpan timeRemaining = TimeSpan.FromTicks(DateTime.Now.Subtract(startTime).Ticks * (total_recs - (num_of_records + 1)) / (num_of_records + 1));

                // Output ETA
                label_ETA.Text = "ETA: " + timeRemaining.Hours + ":" + timeRemaining.Minutes + ":" + timeRemaining.Seconds;

            } while (read_stream.EndOfStream == false);

            // Close StreamReader
            read_stream.Close();

            // Close StreamWriter
            write_stream.Close();

            // Progress
            label_FileOkay.Text = "Finished, " + num_of_records + " records processed.";

        }

        private int CountFileRecords(string file)
        {
            // Records var
            int num_of_records = -2; // (Minus header + 0 count)

            // Update
            label_FileOkay.Text = "Opening file...";

            // Open File
            StreamReader file_reader = new StreamReader(file);

            // Loop through records
            do
            {
                // Iterate count
                num_of_records++;

                // Progress
                label_FileOkay.Text = "Counting record: " + num_of_records;

                // Update
                Application.DoEvents();

            } while (file_reader.ReadLine() != null);

            total_recs = num_of_records;

            return num_of_records;
        }

        private void PopulateDropDowns(string file)
        {
            // Open File
            StreamReader file_reader = new StreamReader(file);

            // Read first line into array
            string[] fields = file_reader.ReadLine().Split(new char[] { ',' });

            // Loop through fields
            for (int i = 0; i < fields.Length; i++)
            {
                if (i < 10)
                {
                    // Add column number to field
                    fields[i] = "0" + i + " - " + fields[i];
                }
                else
                {
                    // Add column number to field
                    fields[i] = i + " - " + fields[i];
                }


            }

            // Add defaults
            // Add to name drop downs
            map_n.Items.Add("-");
            map_t.Items.Add("-");
            map_f.Items.Add("-");
            map_s.Items.Add("-");
            map_dob.Items.Add("-");

            map_mob.Items.Add("-");

            // Add to add drop downs
            map_a1.Items.Add("-");
            map_a2.Items.Add("-");
            map_a3.Items.Add("-");
            map_a4.Items.Add("-");
            map_a5.Items.Add("-");
            map_a6.Items.Add("-");
            map_a7.Items.Add("-");
            map_zip.Items.Add("-");

            // Populate drop downs
            foreach (string field in fields)
            {
                // Add to name drop downs
                map_n.Items.Add(field);
                map_t.Items.Add(field);
                map_f.Items.Add(field);
                map_s.Items.Add(field);
                map_dob.Items.Add(field);

                map_mob.Items.Add(field);

                // Add to add drop downs
                map_a1.Items.Add(field);
                map_a2.Items.Add(field);
                map_a3.Items.Add(field);
                map_a4.Items.Add(field);
                map_a5.Items.Add(field);
                map_a6.Items.Add(field);
                map_a7.Items.Add(field);
                map_zip.Items.Add(field);
            }
        }

        private void ResetDropDowns(string file)
        {
            map_n.Items.Clear();
            map_t.Items.Clear();
            map_f.Items.Clear();
            map_s.Items.Clear();
            map_dob.Items.Clear();
            map_mob.Items.Clear();
            map_a1.Items.Clear();
            map_a2.Items.Clear();
            map_a3.Items.Clear();
            map_a4.Items.Clear();
            map_a5.Items.Clear();
            map_a6.Items.Clear();
            map_a7.Items.Clear();
            map_zip.Items.Clear();
        }

        private string CheckDropDowns()
        {
            // Create list
            ArrayList current_selected = new ArrayList();

            // Add used values
            if ((map_n.Text != "-") && (map_n.Text != "")) { current_selected.Add(map_n.Text); }
            if ((map_t.Text != "-") && (map_t.Text != "")) { current_selected.Add(map_t.Text); }
            if ((map_f.Text != "-") && (map_f.Text != "")) { current_selected.Add(map_f.Text); }
            if ((map_s.Text != "-") && (map_s.Text != "")) { current_selected.Add(map_s.Text); }
            if ((map_dob.Text != "-") && (map_dob.Text != "")) { current_selected.Add(map_dob.Text); }
            if ((map_mob.Text != "-") && (map_mob.Text != "")) { current_selected.Add(map_mob.Text); }
            if ((map_a1.Text != "-") && (map_a1.Text != "")) { current_selected.Add(map_a1.Text); }
            if ((map_a2.Text != "-") && (map_a2.Text != "")) { current_selected.Add(map_a2.Text); }
            if ((map_a3.Text != "-") && (map_a3.Text != "")) { current_selected.Add(map_a3.Text); }
            if ((map_a4.Text != "-") && (map_a4.Text != "")) { current_selected.Add(map_a4.Text); }
            if ((map_a5.Text != "-") && (map_a5.Text != "")) { current_selected.Add(map_a5.Text); }
            if ((map_a6.Text != "-") && (map_a6.Text != "")) { current_selected.Add(map_a6.Text); }
            if ((map_a7.Text != "-") && (map_a7.Text != "")) { current_selected.Add(map_a7.Text); }
            if ((map_zip.Text != "-") && (map_zip.Text != "")) { current_selected.Add(map_zip.Text); }

            // Sort ArrayList
            current_selected.Sort();

            // Return message
            string msg = "";

            // Loop through
            for (int i = 0; i < current_selected.Count; i++)
            {
                if (i > 0)
                {
                    if (current_selected[i].ToString() == current_selected[i - 1].ToString())
                    {
                        msg = "'" + current_selected[i].ToString() + "' has been selected more than once.";
                    }
                }

            }

            return msg;
        }

        private void EnableDropDowns(string lock_status)
        {
            if (lock_status == "enable")
            {
                map_n.Enabled = true;
                map_t.Enabled = true;
                map_f.Enabled = true;
                map_s.Enabled = true;
                map_dob.Enabled = true;

                map_mob.Enabled = true;

                map_a1.Enabled = true;
                map_a2.Enabled = true;
                map_a3.Enabled = true;
                map_a4.Enabled = true;
                map_a5.Enabled = true;
                map_a6.Enabled = true;
                map_a7.Enabled = true;
                map_zip.Enabled = true;

                button_Enrich.Enabled = true;
            }
            else
            {
                map_n.Enabled = false;
                map_t.Enabled = false;
                map_f.Enabled = false;
                map_s.Enabled = false;
                map_dob.Enabled = false;

                map_mob.Enabled = false;

                map_a1.Enabled = false;
                map_a2.Enabled = false;
                map_a3.Enabled = false;
                map_a4.Enabled = false;
                map_a5.Enabled = false;
                map_a6.Enabled = false;
                map_a7.Enabled = false;
                map_zip.Enabled = false;

                button_Enrich.Enabled = false;
            }
        }

        private string CallAppendingAPI(string[] mapped_fields)
        {
            // [CREDENTIALS]
            string username = @"DefineAPI\ClientTest5";
            string password = "E6;0z~.i~A";

            // --- [CREATE REQUEST]
            DataAppendService.DataVariablesRequest request = new DataAppendService.DataVariablesRequest();

            // --- [PARAMETERS] 

            //request.ClientUrn = ""; 
            //request.ClientBillingRef = ""

            request.Purpose = "Prospect";   // [MANDATORY] (or "Customer")
            request.Licence = "Perpetual";  // [MANDATORY]

            // --- [RECORD INFORMATION]

            // Name - Either full, or split into name elements
            if (mapped_fields[0] == "")
            {
                request.Fullname = mapped_fields[1] + " " + mapped_fields[2] + " " + mapped_fields[3];
            }
            else
            {
                request.Fullname = mapped_fields[0];
            }

            request.NameElements = new DataAppendService.NameElements();

            request.NameElements.Title = mapped_fields[1];
            request.NameElements.Forename = mapped_fields[2];
            request.NameElements.MiddleName = mapped_fields[3];
            request.NameElements.Surname = mapped_fields[4];

            //request.DateOfBirth = Convert.ToDateTime(mapped_fields[5]);

            // CHECK IF MOBILE IS MAPPED
            if ((map_mob.Text != "-") && (map_mob.Text != "")) {

                // SETUP MOBILE KEYFIELD
                request.KeyFields = new DataAppendService.KeyField[1];

                // Loop through uninitialised keyfields
                for (int i = 0; i < request.KeyFields.Length; i++)
                {
                    // Initialise each field
                    request.KeyFields[i] = new DataAppendService.KeyField();
                }

                // Declare mobile keyfield
                request.KeyFields[0].Field = "Define.Mobile";
                request.KeyFields[0].Value = mapped_fields[14];

            }
           
            // Address Array - NEEDS TO BE MADE VARIABLE
            string[] address = new string[7];

            address[0] = mapped_fields[6];
            address[1] = mapped_fields[7];
            address[2] = mapped_fields[8];
            address[3] = mapped_fields[9];
            address[4] = mapped_fields[10];
            address[5] = mapped_fields[11];
            address[6] = mapped_fields[12];

            request.AddressLines = address;
            request.Postcode = mapped_fields[13];

            // --- [REQUESTED FIELDS]

            // Set requested fields - SET SIZE
            request.RequestedFields = new DataAppendService.RequestedField[19];

            // Loop through uninitialised requests
            for (int i = 0; i < request.RequestedFields.Length; i++)
            {
                // Initialise each field
                request.RequestedFields[i] = new DataAppendService.RequestedField();
            }

            // Apply field requests
            request.RequestedFields[0].Field = "Define.AgeBand";
            request.RequestedFields[1].Field = "Define.AgeBand_mdl";
            request.RequestedFields[2].Field = "Define.CAMEOUKCategories_Domicile";
            request.RequestedFields[3].Field = "Define.CAMEOUKCategories_Individual";
            request.RequestedFields[4].Field = "Define.CAMEOUKGroup_Domicile";
            request.RequestedFields[5].Field = "Define.CAMEOUKGroup_Individual";
            request.RequestedFields[6].Field = "Define.DOB";
            request.RequestedFields[7].Field = "Define.EarlyAdopter_BND";
            request.RequestedFields[8].Field = "Define.EarlyAdopter_YN";
            request.RequestedFields[9].Field = "Define.EmploymentStatusId";
            request.RequestedFields[10].Field = "Define.GreenEthical";
            request.RequestedFields[11].Field = "Define.HouseholdIncome_mdl";
            request.RequestedFields[12].Field = "Define.HouseholdLifestyle_mdl";
            request.RequestedFields[13].Field = "Define.HouseholdSocialClass_mdl";
            request.RequestedFields[14].Field = "Define.LengthOfResidency";
            request.RequestedFields[15].Field = "Define.MaritalStatusId";
            request.RequestedFields[16].Field = "Define.OccupationBandId";
            request.RequestedFields[17].Field = "Define.SavingsAccounts_BND";
            request.RequestedFields[18].Field = "Define.Sex";

            // --- [SEND REQUEST]

            // Open Client
            DataAppendService.DataAppendServiceClient api_client = new DataAppendService.DataAppendServiceClient();

            // Set credentials
            api_client.ClientCredentials.UserName.UserName = username;
            api_client.ClientCredentials.UserName.Password = password;

            // Set response and make request
            DataAppendService.DataVariablesResponse response = api_client.GetDataVariables(request);
           
            // Close connection
            api_client.Close();

            // --- [HANDLE RESPONSE]

            // Array
            string[] to_format = new string[20];

            // Determine success
            if (response.Status == "Success")
            {
                // Save match level
                to_format[19] = response.MatchLevels[0].Level;

            } 

            for (int i = 0; i < response.OutputFields.Length; i++)
            {

                // Default fields
                if (response.Status == "Success")
                {
                    // Set default
                    //response.OutputFields[i].Value = "0";

                }

                // Ensure same order in array
                if (response.OutputFields[i].Field == "Define.AgeBand") { to_format[0] = response.OutputFields[i].Value; }
                if (response.OutputFields[i].Field == "Define.AgeBand_mdl") { to_format[1] = response.OutputFields[i].Value; }
                if (response.OutputFields[i].Field == "Define.CAMEOUKCategories_Domicile") { to_format[2] = response.OutputFields[i].Value; }
                if (response.OutputFields[i].Field == "Define.CAMEOUKCategories_Individual") { to_format[3] = response.OutputFields[i].Value; }
                if (response.OutputFields[i].Field == "Define.CAMEOUKGroup_Domicile") { to_format[4] = response.OutputFields[i].Value; }
                if (response.OutputFields[i].Field == "Define.CAMEOUKGroup_Individual") { to_format[5] = response.OutputFields[i].Value; }
                if (response.OutputFields[i].Field == "Define.DOB") { to_format[6] = response.OutputFields[i].Value; }
                if (response.OutputFields[i].Field == "Define.EarlyAdopter_BND") { to_format[7] = response.OutputFields[i].Value; }
                if (response.OutputFields[i].Field == "Define.EarlyAdopter_YN") { to_format[8] = response.OutputFields[i].Value; }
                if (response.OutputFields[i].Field == "Define.EmploymentStatusId") { to_format[9] = response.OutputFields[i].Value; }
                if (response.OutputFields[i].Field == "Define.GreenEthical") { to_format[10] = response.OutputFields[i].Value; }
                if (response.OutputFields[i].Field == "Define.HouseholdIncome_mdl") { to_format[11] = response.OutputFields[i].Value; }
                if (response.OutputFields[i].Field == "Define.HouseholdLifestyle_mdl") { to_format[12] = response.OutputFields[i].Value; }
                if (response.OutputFields[i].Field == "Define.HouseholdSocialClass_mdl") { to_format[13] = response.OutputFields[i].Value; }
                if (response.OutputFields[i].Field == "Define.LengthOfResidency") { to_format[14] = response.OutputFields[i].Value; }
                if (response.OutputFields[i].Field == "Define.MaritalStatusId") { to_format[15] = response.OutputFields[i].Value; }
                if (response.OutputFields[i].Field == "Define.OccupationBandId") { to_format[16] = response.OutputFields[i].Value; }
                if (response.OutputFields[i].Field == "Define.SavingsAccounts_BND") { to_format[17] = response.OutputFields[i].Value; }
                if (response.OutputFields[i].Field == "Define.Sex") { to_format[18] = response.OutputFields[i].Value; }

            }

            // Format response fields
            StringBuilder output = new StringBuilder();

            foreach (var field in FormattedOutput(to_format))
            {
                output.Append(",\"" + field + "\"");
            }

            // Update GUI
            StatusBoxText.Text = "  Statistics";
            StatusBoxText.AppendText(Environment.NewLine + Environment.NewLine + "  Records:      " + "0");

            StatusBoxText.AppendText(Environment.NewLine + Environment.NewLine + "  Matched:      " + "0");

            StatusBoxText.AppendText(Environment.NewLine + Environment.NewLine + "  Individual:   " + "0");
            StatusBoxText.AppendText(Environment.NewLine + "  Household:    " + "0");
            StatusBoxText.AppendText(Environment.NewLine + "  Postcode:     " + "0");

            StatusBoxText.AppendText(Environment.NewLine + Environment.NewLine + "  Unmatched:    " + "0");

            // Return string
            return output.ToString();
        }

        private string[] RetrieveMappedData(string record)
        {

            // Break string
            string[] record_arr = record.Split(new char[] { ',' });

            // Define indexes
            int index_n, index_t, index_f, index_s, index_mob; //index_dob;

            // Define ADDRESS indexes
            int index_a1, index_a2, index_a3, index_a4, index_a5, index_a6, index_a7, index_zip;

            // Default fields - NAME
            if ((map_n.Text == null) || (map_n.Text == "") || (map_n.Text == "-")) { index_n = 0; } else { index_n = Convert.ToInt32(map_n.Text.Substring(1, 2).Replace(" ", "")); }
            if ((map_t.Text == null) || (map_t.Text == "") || (map_t.Text == "-")) { index_t = 0; } else { index_t = Convert.ToInt32(map_t.Text.Substring(1, 2).Replace(" ", "")); }
            if ((map_f.Text == null) || (map_f.Text == "") || (map_f.Text == "-")) { index_f = 0; } else { index_f = Convert.ToInt32(map_f.Text.Substring(1, 2).Replace(" ", "")); }
            if ((map_s.Text == null) || (map_s.Text == "") || (map_s.Text == "-")) { index_s = 0; } else { index_s = Convert.ToInt32(map_s.Text.Substring(1, 2).Replace(" ", "")); }
            //if ((map_dob.Text == null) || (map_dob.Text != "") || (map_dob.Text != "-")) { index_dob = 0; } 
            if ((map_mob.Text == null) || (map_mob.Text == "") || (map_mob.Text == "-")) { index_mob = 0; } else { index_mob = Convert.ToInt32(map_mob.Text.Substring(1, 2).Replace(" ", "")); }

            // Default fields - ADDRESS
            if ((map_a1.Text == null) || (map_a1.Text == "") || (map_a1.Text == "-")) { index_a1 = 0; } else { index_a1 = Convert.ToInt32(map_a1.Text.Substring(1, 2).Replace(" ", "")); }
            if ((map_a2.Text == null) || (map_a2.Text == "") || (map_a2.Text == "-")) { index_a2 = 0; } else { index_a2 = Convert.ToInt32(map_a2.Text.Substring(1, 2).Replace(" ", "")); }
            if ((map_a3.Text == null) || (map_a3.Text == "") || (map_a3.Text == "-")) { index_a3 = 0; } else { index_a3 = Convert.ToInt32(map_a3.Text.Substring(1, 2).Replace(" ", "")); }
            if ((map_a4.Text == null) || (map_a4.Text == "") || (map_a4.Text == "-")) { index_a4 = 0; } else { index_a4 = Convert.ToInt32(map_a4.Text.Substring(1, 2).Replace(" ", "")); }
            if ((map_a5.Text == null) || (map_a5.Text == "") || (map_a5.Text == "-")) { index_a5 = 0; } else { index_a5 = Convert.ToInt32(map_a5.Text.Substring(1, 2).Replace(" ", "")); }
            if ((map_a6.Text == null) || (map_a6.Text == "") || (map_a6.Text == "-")) { index_a6 = 0; } else { index_a6 = Convert.ToInt32(map_a6.Text.Substring(1, 2).Replace(" ", "")); }
            if ((map_a7.Text == null) || (map_a7.Text == "") || (map_a7.Text == "-")) { index_a7 = 0; } else { index_a7 = Convert.ToInt32(map_a7.Text.Substring(1, 2).Replace(" ", "")); }
            if ((map_zip.Text == null) || (map_zip.Text == "") || (map_zip.Text == "-")) { index_zip = 0; } else { index_zip = Convert.ToInt32(map_zip.Text.Substring(1, 2).Replace(" ", "")); }

            // Create output array
            string[] output_array = new string[15];

            // Loop through record fields
            for (int i = 0; i < record_arr.Length; i++)
            {
                // Store mapped fields - NAME
                if (i == index_n) { output_array[0] = record_arr[index_n].Replace("\"", ""); }
                if (i == index_t) { output_array[1] = record_arr[index_t].Replace("\"", ""); }
                if (i == index_f) { output_array[2] = record_arr[index_f].Replace("\"", ""); }
                //if (i == index_m) { output_array[3] = record_arr[i]; }
                if (i == index_s) { output_array[4] = record_arr[index_s].Replace("\"", ""); }
                //if (i == index_dob) { output_array[5] = record_arr[i]; }

                if (i == index_mob) { output_array[14] = record_arr[index_mob].Replace("\"", ""); }

                // Store mapped fields - ADDRESS
                if (i == index_a1) { output_array[6] = record_arr[index_a1].Replace("\"", ""); }
                if (i == index_a2) { output_array[7] = record_arr[index_a2].Replace("\"", ""); }
                if (i == index_a3) { output_array[8] = record_arr[index_a3].Replace("\"", ""); }
                if (i == index_a4) { output_array[9] = record_arr[index_a4].Replace("\"", ""); }
                if (i == index_a5) { output_array[10] = record_arr[index_a5].Replace("\"", ""); }
                if (i == index_a6) { output_array[11] = record_arr[index_a6].Replace("\"", ""); }
                if (i == index_a7) { output_array[12] = record_arr[index_a7].Replace("\"", ""); }
                if (i == index_zip) { output_array[13] = record_arr[index_zip].Replace("\"", ""); }

            }

            return output_array;

        }

        private string[] FormattedOutput(string[] to_format)
        {
            // New array
            string[] output = new string[35];

            // 0 - Format AgeBand [WILL BE CHANGING]
            if (to_format[0] == "1") { output[0]=to_format[0]; output[1] = "Unknown"; }
            if (to_format[0] == "2") { output[0]=to_format[0]; output[1] = "18-20"; }
            if (to_format[0] == "3") { output[0]=to_format[0]; output[1] = "21-24"; }
            if (to_format[0] == "4") { output[0]=to_format[0]; output[1] = "25-29"; }
            if (to_format[0] == "5") { output[0]=to_format[0]; output[1] = "30-34"; }
            if (to_format[0] == "6") { output[0]=to_format[0]; output[1] = "35-39"; }
            if (to_format[0] == "7") { output[0]=to_format[0]; output[1] = "40-44"; }
            if (to_format[0] == "8") { output[0]=to_format[0]; output[1] = "45-49"; }
            if (to_format[0] == "9") { output[0]=to_format[0]; output[1] = "50-54"; }
            if (to_format[0] == "10") { output[0]=to_format[0]; output[1] = "55-59"; }
            if (to_format[0] == "11") { output[0]=to_format[0]; output[1] = "60-64"; }
            if (to_format[0] == "12") { output[0]=to_format[0]; output[1] = "65-69"; }
            if (to_format[0] == "13") { output[0]=to_format[0]; output[1] = "70-74"; }
            if (to_format[0] == "14") { output[0]=to_format[0]; output[1] = "75+"; }
            if (to_format[0] == null) { output[0] = "0"; output[1] = ""; }
            if (to_format[0] == "") { output[0] = "0"; output[1] = ""; }

            // 1 - Format AgeBand_mdl [WILL BE CHANGING]
            if (to_format[1] == "1") { output[2]=to_format[1]; output[3] = "Unknown"; }
            if (to_format[1] == "2") { output[2]=to_format[1]; output[3] = "18-20"; }
            if (to_format[1] == "3") { output[2]=to_format[1]; output[3] = "21-24"; }
            if (to_format[1] == "4") { output[2]=to_format[1]; output[3] = "25-29"; }
            if (to_format[1] == "5") { output[2]=to_format[1]; output[3] = "30-34"; }
            if (to_format[1] == "6") { output[2]=to_format[1]; output[3] = "35-39"; }
            if (to_format[1] == "7") { output[2]=to_format[1]; output[3] = "40-44"; }
            if (to_format[1] == "8") { output[2]=to_format[1]; output[3] = "45-49"; }
            if (to_format[1] == "9") { output[2]=to_format[1]; output[3] = "50-54"; }
            if (to_format[1] == "10") { output[2]=to_format[1]; output[3] = "55-59"; }
            if (to_format[1] == "11") { output[2]=to_format[1]; output[3] = "60-64"; }
            if (to_format[1] == "12") { output[2]=to_format[1]; output[3] = "65-69"; }
            if (to_format[1] == "13") { output[2]=to_format[1]; output[3] = "70-74"; }
            if (to_format[1] == "14") { output[2]=to_format[1]; output[3] = "75+"; }
            if (to_format[1] == null) { output[2] = "0"; output[3] = ""; }
            if (to_format[1] == "") { output[2] = "0"; output[3] = ""; }

            // 2 - Format CAMEOUKCategories_Domicile
            if (to_format[2] == "01A") { output[4]="1"; output[5] = "Urban Ambition"; }
            if (to_format[2] == "01B") { output[4]="2"; output[5] = "City Slickers"; }
            if (to_format[2] == "01C") { output[4]="3"; output[5] = "High Flying Professionals"; }
            if (to_format[2] == "01D") { output[4]="4"; output[5] = "Moneyed Suburbs"; }
            if (to_format[2] == "01E") { output[4]="5"; output[5] = "Old Money"; }
            if (to_format[2] == "02A") { output[4]="6"; output[5] = "Cosmopolitan Chic"; }
            if (to_format[2] == "02B") { output[4]="7"; output[5] = "Rural Retreats"; }
            if (to_format[2] == "02C") { output[4]="8"; output[5] = "Family Fortunes"; }
            if (to_format[2] == "02D") { output[4]="9"; output[5] = "Sunset Success"; }
            if (to_format[2] == "03A") { output[4]="10"; output[5] = "Fast-Track Sophisticates"; }
            if (to_format[2] == "03B") { output[4]="11"; output[5] = "Enterprising Households"; }
            if (to_format[2] == "03C") { output[4]="12"; output[5] = "Well-Feathered Nest"; }
            if (to_format[2] == "03D") { output[4]="13"; output[5] = "Upper Crust"; }
            if (to_format[2] == "03E") { output[4]="14"; output[5] = "Escape To The Country"; }
            if (to_format[2] == "03F") { output[4]="15"; output[5] = "Retiring In Comfort"; }
            if (to_format[2] == "04A") { output[4]="16"; output[5] = "Sensible Savers"; }
            if (to_format[2] == "04B") { output[4]="17"; output[5] = "Cash Conscious"; }
            if (to_format[2] == "04C") { output[4]="18"; output[5] = "Established Finances"; }
            if (to_format[2] == "04D") { output[4]="19"; output[5] = "Settled Urbanites"; }
            if (to_format[2] == "04E") { output[4]="20"; output[5] = "Secure Seniors"; }
            if (to_format[2] == "04F") { output[4]="21"; output[5] = "Old School Comfort"; }
            if (to_format[2] == "04G") { output[4]="22"; output[5] = "Mature Middle"; }
            if (to_format[2] == "05A") { output[4]="23"; output[5] = "Single Spenders"; }
            if (to_format[2] == "05B") { output[4]="24"; output[5] = "Savvy Networkers"; }
            if (to_format[2] == "05C") { output[4]="25"; output[5] = "On The Up"; }
            if (to_format[2] == "05D") { output[4]="26"; output[5] = "Steady Suburbs"; }
            if (to_format[2] == "05E") { output[4]="27"; output[5] = "Married To The Mortgage"; }
            if (to_format[2] == "05F") { output[4]="28"; output[5] = "Empty Nest Professionals"; }
            if (to_format[2] == "05G") { output[4]="29"; output[5] = "Gardening Time"; }
            if (to_format[2] == "06A") { output[4]="30"; output[5] = "Starting Out"; }
            if (to_format[2] == "06B") { output[4]="31"; output[5] = "Family Credit"; }
            if (to_format[2] == "06C") { output[4]="32"; output[5] = "Market Town Mortgages"; }
            if (to_format[2] == "06D") { output[4]="33"; output[5] = "Town And Country"; }
            if (to_format[2] == "06E") { output[4]="34"; output[5] = "Post Family Provincials"; }
            if (to_format[2] == "07A") { output[4]="35"; output[5] = "Metropolitan Diversity"; }
            if (to_format[2] == "07B") { output[4]="36"; output[5] = "Inner City Terraces"; }
            if (to_format[2] == "07C") { output[4]="37"; output[5] = "Settled Suburbs"; }
            if (to_format[2] == "07D") { output[4]="38"; output[5] = "School Run Families"; }
            if (to_format[2] == "07E") { output[4]="39"; output[5] = "Established Suburban Communities"; }
            if (to_format[2] == "07F") { output[4]="40"; output[5] = "Financially Stable Homeowners"; }
            if (to_format[2] == "07G") { output[4]="41"; output[5] = "Value Conscious Families"; }
            if (to_format[2] == "07H") { output[4]="42"; output[5] = "Blue Collar Squeeze"; }
            if (to_format[2] == "08A") { output[4]="43"; output[5] = "Strained City"; }
            if (to_format[2] == "08B") { output[4]="44"; output[5] = "Connected Renters"; }
            if (to_format[2] == "08C") { output[4]="45"; output[5] = "Modest Means"; }
            if (to_format[2] == "08D") { output[4]="46"; output[5] = "Stretched Families"; }
            if (to_format[2] == "08E") { output[4]="47"; output[5] = "Marginalised Urbanites"; }
            if (to_format[2] == "08F") { output[4]="48"; output[5] = "Struggling Suburbs"; }
            if (to_format[2] == "08G") { output[4]="49"; output[5] = "Working Towards Retirement"; }
            if (to_format[2] == "08H") { output[4]="50"; output[5] = "Bargain Hunters"; }
            if (to_format[2] == "08I") { output[4]="51"; output[5] = "Frugal Families"; }
            if (to_format[2] == "08J") { output[4]="52"; output[5] = "Maturing Suburbia"; }
            if (to_format[2] == "09A") { output[4]="53"; output[5] = "Crowded Houses"; }
            if (to_format[2] == "09B") { output[4]="54"; output[5] = "Young Family Renters"; }
            if (to_format[2] == "09C") { output[4]="55"; output[5] = "Stretched Start-Ups"; }
            if (to_format[2] == "09D") { output[4]="56"; output[5] = "High Rise Hardship"; }
            if (to_format[2] == "09E") { output[4]="57"; output[5] = "Suburban Endeavours"; }
            if (to_format[2] == "09F") { output[4]="58"; output[5] = "Single Strugglers"; }
            if (to_format[2] == "09G") { output[4]="59"; output[5] = "Budget Families"; }
            if (to_format[2] == "09H") { output[4]="60"; output[5] = "Community Spirit"; }
            if (to_format[2] == "09I") { output[4]="61"; output[5] = "Thrifty Terraces"; }
            if (to_format[2] == "10A") { output[4]="62"; output[5] = "Diverse Renters"; }
            if (to_format[2] == "10B") { output[4]="63"; output[5] = "Blue Collar Semis"; }
            if (to_format[2] == "10C") { output[4]="64"; output[5] = "Strained Estates"; }
            if (to_format[2] == "10D") { output[4]="65"; output[5] = "Disadvantaged Fringe"; }
            if (to_format[2] == "10E") { output[4]="66"; output[5] = "Small Town Tenants"; }
            if (to_format[2] == "10F") { output[4]="67"; output[5] = "Provincial Pressure"; }
            if (to_format[2] == "10G") { output[4]="68"; output[5] = "Making Ends Meet"; }
            if (to_format[2] == null) { output[4] = "0"; output[5] = ""; }
            if (to_format[2] == "") { output[4] = "0"; output[5] = ""; }

            // 3 - Format CAMEOUKCategories_Individual
            if (to_format[3] == "01A") { output[6]="1"; output[7] = "Urban Ambition"; }
            if (to_format[3] == "01B") { output[6]="2"; output[7] = "City Slickers"; }
            if (to_format[3] == "01C") { output[6]="3"; output[7] = "High Flying Professionals"; }
            if (to_format[3] == "01D") { output[6]="4"; output[7] = "Moneyed Suburbs"; }
            if (to_format[3] == "01E") { output[6]="5"; output[7] = "Old Money"; }
            if (to_format[3] == "02A") { output[6]="6"; output[7] = "Cosmopolitan Chic"; }
            if (to_format[3] == "02B") { output[6]="7"; output[7] = "Rural Retreats"; }
            if (to_format[3] == "02C") { output[6]="8"; output[7] = "Family Fortunes"; }
            if (to_format[3] == "02D") { output[6]="9"; output[7] = "Sunset Success"; }
            if (to_format[3] == "03A") { output[6]="10"; output[7] = "Fast-Track Sophisticates"; }
            if (to_format[3] == "03B") { output[6]="11"; output[7] = "Enterprising Households"; }
            if (to_format[3] == "03C") { output[6]="12"; output[7] = "Well-Feathered Nest"; }
            if (to_format[3] == "03D") { output[6]="13"; output[7] = "Upper Crust"; }
            if (to_format[3] == "03E") { output[6]="14"; output[7] = "Escape To The Country"; }
            if (to_format[3] == "03F") { output[6]="15"; output[7] = "Retiring In Comfort"; }
            if (to_format[3] == "04A") { output[6]="16"; output[7] = "Sensible Savers"; }
            if (to_format[3] == "04B") { output[6]="17"; output[7] = "Cash Conscious"; }
            if (to_format[3] == "04C") { output[6]="18"; output[7] = "Established Finances"; }
            if (to_format[3] == "04D") { output[6]="19"; output[7] = "Settled Urbanites"; }
            if (to_format[3] == "04E") { output[6]="20"; output[7] = "Secure Seniors"; }
            if (to_format[3] == "04F") { output[6]="21"; output[7] = "Old School Comfort"; }
            if (to_format[3] == "04G") { output[6]="22"; output[7] = "Mature Middle"; }
            if (to_format[3] == "05A") { output[6]="23"; output[7] = "Single Spenders"; }
            if (to_format[3] == "05B") { output[6]="24"; output[7] = "Savvy Networkers"; }
            if (to_format[3] == "05C") { output[6]="25"; output[7] = "On The Up"; }
            if (to_format[3] == "05D") { output[6]="26"; output[7] = "Steady Suburbs"; }
            if (to_format[3] == "05E") { output[6]="27"; output[7] = "Married To The Mortgage"; }
            if (to_format[3] == "05F") { output[6]="28"; output[7] = "Empty Nest Professionals"; }
            if (to_format[3] == "05G") { output[6]="29"; output[7] = "Gardening Time"; }
            if (to_format[3] == "06A") { output[6]="30"; output[7] = "Starting Out"; }
            if (to_format[3] == "06B") { output[6]="31"; output[7] = "Family Credit"; }
            if (to_format[3] == "06C") { output[6]="32"; output[7] = "Market Town Mortgages"; }
            if (to_format[3] == "06D") { output[6]="33"; output[7] = "Town And Country"; }
            if (to_format[3] == "06E") { output[6]="34"; output[7] = "Post Family Provincials"; }
            if (to_format[3] == "07A") { output[6]="35"; output[7] = "Metropolitan Diversity"; }
            if (to_format[3] == "07B") { output[6]="36"; output[7] = "Inner City Terraces"; }
            if (to_format[3] == "07C") { output[6]="37"; output[7] = "Settled Suburbs"; }
            if (to_format[3] == "07D") { output[6]="38"; output[7] = "School Run Families"; }
            if (to_format[3] == "07E") { output[6]="39"; output[7] = "Established Suburban Communities"; }
            if (to_format[3] == "07F") { output[6]="40"; output[7] = "Financially Stable Homeowners"; }
            if (to_format[3] == "07G") { output[6]="41"; output[7] = "Value Conscious Families"; }
            if (to_format[3] == "07H") { output[6]="42"; output[7] = "Blue Collar Squeeze"; }
            if (to_format[3] == "08A") { output[6]="43"; output[7] = "Strained City"; }
            if (to_format[3] == "08B") { output[6]="44"; output[7] = "Connected Renters"; }
            if (to_format[3] == "08C") { output[6]="45"; output[7] = "Modest Means"; }
            if (to_format[3] == "08D") { output[6]="46"; output[7] = "Stretched Families"; }
            if (to_format[3] == "08E") { output[6]="47"; output[7] = "Marginalised Urbanites"; }
            if (to_format[3] == "08F") { output[6]="48"; output[7] = "Struggling Suburbs"; }
            if (to_format[3] == "08G") { output[6]="49"; output[7] = "Working Towards Retirement"; }
            if (to_format[3] == "08H") { output[6]="50"; output[7] = "Bargain Hunters"; }
            if (to_format[3] == "08I") { output[6]="51"; output[7] = "Frugal Families"; }
            if (to_format[3] == "08J") { output[6]="52"; output[7] = "Maturing Suburbia"; }
            if (to_format[3] == "09A") { output[6]="53"; output[7] = "Crowded Houses"; }
            if (to_format[3] == "09B") { output[6]="54"; output[7] = "Young Family Renters"; }
            if (to_format[3] == "09C") { output[6]="55"; output[7] = "Stretched Start-Ups"; }
            if (to_format[3] == "09D") { output[6]="56"; output[7] = "High Rise Hardship"; }
            if (to_format[3] == "09E") { output[6]="57"; output[7] = "Suburban Endeavours"; }
            if (to_format[3] == "09F") { output[6]="58"; output[7] = "Single Strugglers"; }
            if (to_format[3] == "09G") { output[6]="59"; output[7] = "Budget Families"; }
            if (to_format[3] == "09H") { output[6]="60"; output[7] = "Community Spirit"; }
            if (to_format[3] == "09I") { output[6]="61"; output[7] = "Thrifty Terraces"; }
            if (to_format[3] == "10A") { output[6]="62"; output[7] = "Diverse Renters"; }
            if (to_format[3] == "10B") { output[6]="63"; output[7] = "Blue Collar Semis"; }
            if (to_format[3] == "10C") { output[6]="64"; output[7] = "Strained Estates"; }
            if (to_format[3] == "10D") { output[6]="65"; output[7] = "Disadvantaged Fringe"; }
            if (to_format[3] == "10E") { output[6]="66"; output[7] = "Small Town Tenants"; }
            if (to_format[3] == "10F") { output[6]="67"; output[7] = "Provincial Pressure"; }
            if (to_format[3] == "10G") { output[6]="68"; output[7] = "Making Ends Meet"; }
            if (to_format[3] == null) { output[6] = "0"; output[7] = ""; }
            if (to_format[3] == "") { output[6] = "0"; output[7] = ""; }

            // 4 - Format CAMEOUKGroup_Domicile
            if (to_format[4] == "01") { output[8]=to_format[4]; output[9] = "Business Elite"; }
            if (to_format[4] == "02") { output[8]=to_format[4]; output[9] = "Prosperous Professionals"; }
            if (to_format[4] == "03") { output[8]=to_format[4]; output[9] = "Flourishing Society"; }
            if (to_format[4] == "04") { output[8]=to_format[4]; output[9] = "Content Communities"; }
            if (to_format[4] == "05") { output[8]=to_format[4]; output[9] = "White Collar Neighbourhoods"; }
            if (to_format[4] == "06") { output[8]=to_format[4]; output[9] = "Enterprising Mainstream"; }
            if (to_format[4] == "07") { output[8]=to_format[4]; output[9] = "Paying The Mortgage"; }
            if (to_format[4] == "08") { output[8]=to_format[4]; output[9] = "Cash Conscious Communities"; }
            if (to_format[4] == "09") { output[8]=to_format[4]; output[9] = "On A Budget"; }
            if (to_format[4] == "10") { output[8]=to_format[4]; output[9] = "Family Value"; }
            if (to_format[4] == null) { output[8] = "0"; output[9] = ""; }
            if (to_format[4] == "") { output[8] = "0"; output[9] = ""; }

            // 5 - Format CAMEOUKGroup_Individual
            if (to_format[5] == "01") { output[10]=to_format[5]; output[11] = "Business Elite"; }
            if (to_format[5] == "02") { output[10]=to_format[5]; output[11] = "Prosperous Professionals"; }
            if (to_format[5] == "03") { output[10]=to_format[5]; output[11] = "Flourishing Society"; }
            if (to_format[5] == "04") { output[10]=to_format[5]; output[11] = "Content Communities"; }
            if (to_format[5] == "05") { output[10]=to_format[5]; output[11] = "White Collar Neighbourhoods"; }
            if (to_format[5] == "06") { output[10]=to_format[5]; output[11] = "Enterprising Mainstream"; }
            if (to_format[5] == "07") { output[10]=to_format[5]; output[11] = "Paying The Mortgage"; }
            if (to_format[5] == "08") { output[10]=to_format[5]; output[11] = "Cash Conscious Communities"; }
            if (to_format[5] == "09") { output[10]=to_format[5]; output[11] = "On A Budget"; }
            if (to_format[5] == "10") { output[10]=to_format[5]; output[11] = "Family Value"; }
            if (to_format[5] == null) { output[10] = "0"; output[11] = ""; }
            if (to_format[5] == "") { output[10] = "0"; output[11] = ""; }

            // 6 - Format DOB
            if (to_format[6] == null) { output[12] = "0";}
            if (to_format[6] == "") { output[12] = "0"; }

            // 7 - Format EarlyAdopter_BND
            if (to_format[7] == "1") { output[13]=to_format[7]; output[14] = "1 - Very High"; }
            if (to_format[7] == "2") { output[13]=to_format[7]; output[14] = "2 - High"; }
            if (to_format[7] == "3") { output[13]=to_format[7]; output[14] = "3 - Above Average"; }
            if (to_format[7] == "4") { output[13]=to_format[7]; output[14] = "4 - Below Average"; }
            if (to_format[7] == "5") { output[13]=to_format[7]; output[14] = "5 - Low"; }
            if (to_format[7] == "6") { output[13]=to_format[7]; output[14] = "6 - Very Low"; }
            if (to_format[7] == null) { output[13] = "0"; output[14] = ""; }
            if (to_format[7] == "") { output[13] = "0"; output[14] = ""; }

            // 8 - Format EarlyAdopter_YN
            if (to_format[8] == null) { output[15] = "0";}
            if (to_format[8] == "") { output[15] = "0";}

            // 9 - Format EmploymentStatusId
            if (to_format[9] == "1") { output[16]=to_format[9]; output[17] = "CLUB ASSOCIATION"; }
            if (to_format[9] == "2") { output[16]=to_format[9]; output[17] = "(OBSOLETE) CLUB/ASSOC."; }
            if (to_format[9] == "3") { output[16]=to_format[9]; output[17] = "DIRECTOR"; }
            if (to_format[9] == "4") { output[16]=to_format[9]; output[17] = "DISABILITY"; }
            if (to_format[9] == "5") { output[16]=to_format[9]; output[17] = "EMPLOYED"; }
            if (to_format[9] == "6") { output[16]=to_format[9]; output[17] = "(OBSOLETE) EMPLOYEE"; }
            if (to_format[9] == "7") { output[16]=to_format[9]; output[17] = "(OBSOLETE) EMPLOYEE/STAFF"; }
            if (to_format[9] == "8") { output[16]=to_format[9]; output[17] = "FULL TIME EDUCATION"; }
            if (to_format[9] == "9") { output[16]=to_format[9]; output[17] = "(OBSOLETE) FULL-TIME EDUCATION"; }
            if (to_format[9] == "10") { output[16]=to_format[9]; output[17] = "FULL/PART TIME EDUCATION"; }
            if (to_format[9] == "11") { output[16]=to_format[9]; output[17] = "GOVERNMENT"; }
            if (to_format[9] == "12") { output[16]=to_format[9]; output[17] = "(OBSOLETE) GOVERNMENT/AGENCY"; }
            if (to_format[9] == "13") { output[16]=to_format[9]; output[17] = "HOUSE PERSON"; }
            if (to_format[9] == "14") { output[16]=to_format[9]; output[17] = "(OBSOLETE) HOUSEHOLD DUTIES"; }
            if (to_format[9] == "15") { output[16]=to_format[9]; output[17] = "(OBSOLETE) HOUSEPERSON"; }
            if (to_format[9] == "16") { output[16]=to_format[9]; output[17] = "LIMITED COMPANY"; }
            if (to_format[9] == "17") { output[16]=to_format[9]; output[17] = "(OBSOLETE) LTD COMPANY"; }
            if (to_format[9] == "18") { output[16]=to_format[9]; output[17] = "OTHER"; }
            if (to_format[9] == "19") { output[16]=to_format[9]; output[17] = "PROP./PARTNER"; }
            if (to_format[9] == "20") { output[16]=to_format[9]; output[17] = "RETIRED"; }
            if (to_format[9] == "21") { output[16]=to_format[9]; output[17] = "SELF EMPLOYED"; }
            if (to_format[9] == "22") { output[16]=to_format[9]; output[17] = "UNEMPLOYED"; }
            if (to_format[9] == "23") { output[16]=to_format[9]; output[17] = "VOLUNTARY WORK"; }
            if (to_format[9] == "24") { output[16]=to_format[9]; output[17] = "INDEPENDENT MEANS"; }
            if (to_format[9] == "25") { output[16]=to_format[9]; output[17] = "EMPLOYED PART TIME"; }
            if (to_format[9] == null) { output[16] = "0"; output[17] = ""; }
            if (to_format[9] == "") { output[16] = "0"; output[17] = ""; }

            // 10 - Format GreenEthical
            if (to_format[10] == "A") { output[18]="1"; output[19] = "A - The Righteous Rich"; }
            if (to_format[10] == "B") { output[18]="2"; output[19] = "B - Green Is The New Black"; }
            if (to_format[10] == "C") { output[18]="3"; output[19] = "C - Eco Enthusiasts"; }
            if (to_format[10] == "D") { output[18]="4"; output[19] = "D - Money Talks"; }
            if (to_format[10] == "E") { output[18]="5"; output[19] = "E - The Good Life"; }
            if (to_format[10] == "F") { output[18]="6"; output[19] = "F - Doing Their Bit"; }
            if (to_format[10] == "G") { output[18]="7"; output[19] = "G - Green Drivers"; }
            if (to_format[10] == "H") { output[18]="8"; output[19] = "H - Stuck In The Middle"; }
            if (to_format[10] == "I") { output[18]="9"; output[19] = "I - It's Not Easy"; }
            if (to_format[10] == "J") { output[18]="10"; output[19] = "J - Not My Problem"; }
            if (to_format[10] == "K") { output[18]="11"; output[19] = "K - Skint Sceptics"; }
            if (to_format[10] == "L") { output[18]="12"; output[19] = "L - Other Priorities"; }
            if (to_format[10] == "M") { output[18]="13"; output[19] = "M - Am I Bothered"; }
            if (to_format[10] == null) { output[18] = "0"; output[19] = ""; }
            if (to_format[10] == "") { output[18] = "0"; output[19] = ""; }

            // 11 - Format HouseholdIncome_mdl
            if (to_format[11] == "1") { output[20]=to_format[11]; output[21] = "1 - Established Wealth : income 75k+"; }
            if (to_format[11] == "2") { output[20]=to_format[11]; output[21] = "2 - Secure Affluence : income 50-75k"; }
            if (to_format[11] == "3") { output[20]=to_format[11]; output[21] = "3 - Rising Prosperity : income 40-50k"; }
            if (to_format[11] == "4") { output[20]=to_format[11]; output[21] = "4 - Comfortably Secure : income 30-40k"; }
            if (to_format[11] == "5") { output[20]=to_format[11]; output[21] = "5 - Budgeted Stability : income 25-30k"; }
            if (to_format[11] == "6") { output[20]=to_format[11]; output[21] = "6 - Limited Resources : income 20-25k"; }
            if (to_format[11] == "7") { output[20]=to_format[11]; output[21] = "7 - Uncertain Means : income 15-20k"; }
            if (to_format[11] == "8") { output[20]=to_format[11]; output[21] = "8 - Economically Challended : income 10-15k"; }
            if (to_format[11] == "9") { output[20]=to_format[11]; output[21] = "9 - Entrenched Struggle : income under 10k"; }
            if (to_format[11] == null) { output[20] = "0"; output[21] = ""; }
            if (to_format[11] == "") { output[20] = "0"; output[21] = ""; }

            // 12 - Format HouseholdLifestyle_mdl
            if (to_format[12] == "A") { output[22]="1"; output[23] = "A - Accomplished Singles"; }
            if (to_format[12] == "B") { output[22]="2"; output[23] = "B - Go Getting DINKys"; }
            if (to_format[12] == "C") { output[22]="3"; output[23] = "C - Family Feelgoods"; }
            if (to_format[12] == "D") { output[22]="4"; output[23] = "D - Maintained Single Parents"; }
            if (to_format[12] == "E") { output[22]="5"; output[23] = "E - Unattached Traditionalists"; }
            if (to_format[12] == "F") { output[22]="6"; output[23] = "F - Contented Greys"; }
            if (to_format[12] == "G") { output[22]="7"; output[23] = "G - Contemporary Elders"; }
            if (to_format[12] == "H") { output[22]="8"; output[23] = "H - Secure Singles"; }
            if (to_format[12] == "J") { output[22]="9"; output[23] = "J - Poundstretching Twosomes"; }
            if (to_format[12] == "K") { output[22]="10"; output[23] = "K - Friends Together"; }
            if (to_format[12] == "L") { output[22]="11"; output[23] = "L - Thriving and Thrifty Families"; }
            if (to_format[12] == "M") { output[22]="12"; output[23] = "M - Mature and Stable Sedentaries"; }
            if (to_format[12] == "N") { output[22]="13"; output[23] = "N - Young Optimists"; }
            if (to_format[12] == "P") { output[22]="14"; output[23] = "P - Constrained Solos"; }
            if (to_format[12] == "Q") { output[22]="15"; output[23] = "Q - Struggling Families"; }
            if (to_format[12] == "R") { output[22]="16"; output[23] = "R - Proud Parents Coping Alone"; }
            if (to_format[12] == "S") { output[22]="17"; output[23] = "S - Penny-Wise Pensioners"; }
            if (to_format[12] == null) { output[22] = "0"; output[23] = ""; }
            if (to_format[12] == "") { output[22] = "0"; output[23] = ""; }

            // 13 - Format HouseholdSocialClass_mdl
            if (to_format[13] == "AB") { output[24]="1"; output[25] = "AB - Higher and Intermediate Managerial/Administrative/Professional"; }
            if (to_format[13] == "C1") { output[24]="2"; output[25] = "C1 - Supervisory, Clerical, Junior Managerial/Administrative/Professional"; }
            if (to_format[13] == "C2") { output[24]="3"; output[25] = "C2 - Skilled Manual Workers"; }
            if (to_format[13] == "D") { output[24]="4"; output[25] = "D - Semi-skilled and Unskilled Manual Workers"; }
            if (to_format[13] == "E") { output[24]="5"; output[25] = "E - On State Benefit, Unemployed, Lowest grade Workers"; }
            if (to_format[13] == null) { output[24] = "0"; output[25] = ""; }
            if (to_format[13] == "") { output[24] = "0"; output[25] = ""; }

            // 14 - Format LengthOfResidency
            if (to_format[14] == null) { output[26] = "0"; }
            if (to_format[14] == "") { output[26] = "0"; }

            // 15 - Format MaritalStatusId
            if (to_format[15] == "9") { output[27]=to_format[15]; output[28] = "CIVIL PARTNERSHIP"; }
            if (to_format[15] == "4") { output[27]=to_format[15]; output[28] = "COHABITING / PARTNERED"; }
            if (to_format[15] == "3") { output[27]=to_format[15]; output[28] = "DIVORCED"; }
            if (to_format[15] == "8") { output[27]=to_format[15]; output[28] = "MAR'D COMMONLAW"; }
            if (to_format[15] == "6") { output[27]=to_format[15]; output[28] = "MARRIED"; }
            if (to_format[15] == "5") { output[27]=to_format[15]; output[28] = "SEPARATED"; }
            if (to_format[15] == "7") { output[27]=to_format[15]; output[28] = "SINGLE"; }
            if (to_format[15] == "1") { output[27]=to_format[15]; output[28] = "UNKNOWN OR INVALID"; }
            if (to_format[15] == "2") { output[27]=to_format[15]; output[28] = "WIDOWED"; }
            if (to_format[15] == null) { output[27] = "0"; output[28] = ""; }
            if (to_format[15] == "") { output[27] = "0"; output[28] = ""; }

            // 16 - Format OccupationBandId
            if (to_format[16] == "1") { output[29]=to_format[16]; output[30] = "UNKNOWN OR INVALID"; }
            if (to_format[16] == "2") { output[29]=to_format[16]; output[30] = "ARTS/MEDIA"; }
            if (to_format[16] == "3") { output[29]=to_format[16]; output[30] = "DIRECTOR"; }
            if (to_format[16] == "4") { output[29]=to_format[16]; output[30] = "EDUCATION"; }
            if (to_format[16] == "5") { output[29]=to_format[16]; output[30] = "EMERGENCY/ARMED SERVICES"; }
            if (to_format[16] == "6") { output[29]=to_format[16]; output[30] = "HEALTH"; }
            if (to_format[16] == "7") { output[29]=to_format[16]; output[30] = "HOUSE PERSON"; }
            if (to_format[16] == "8") { output[29]=to_format[16]; output[30] = "LAND/SEA MANAGEMENT"; }
            if (to_format[16] == "9") { output[29]=to_format[16]; output[30] = "MANAGEMENT"; }
            if (to_format[16] == "10") { output[29]=to_format[16]; output[30] = "MANUAL/FACTORY"; }
            if (to_format[16] == "11") { output[29]=to_format[16]; output[30] = "OFFICE/CLERICAL"; }
            if (to_format[16] == "12") { output[29]=to_format[16]; output[30] = "PROFESSIONAL"; }
            if (to_format[16] == "13") { output[29]=to_format[16]; output[30] = "PROPERTY"; }
            if (to_format[16] == "14") { output[29]=to_format[16]; output[30] = "PUBLIC SECTOR"; }
            if (to_format[16] == "15") { output[29]=to_format[16]; output[30] = "RELIGIOUS"; }
            if (to_format[16] == "16") { output[29]=to_format[16]; output[30] = "RETAIL"; }
            if (to_format[16] == "17") { output[29]=to_format[16]; output[30] = "SERVICE SECTOR"; }
            if (to_format[16] == "18") { output[29]=to_format[16]; output[30] = "SKILLED TRADESMAN"; }
            if (to_format[16] == "19") { output[29]=to_format[16]; output[30] = "SPORTS"; }
            if (to_format[16] == "20") { output[29]=to_format[16]; output[30] = "STUDENT"; }
            if (to_format[16] == "21") { output[29]=to_format[16]; output[30] = "UNEMPLOYED"; }
            if (to_format[16] == "22") { output[29]=to_format[16]; output[30] = "RETIRED"; }
            if (to_format[16] == "23") { output[29]=to_format[16]; output[30] = "SELF-EMPLOYED"; }
            if (to_format[16] == null) { output[29] = "0"; output[32] = ""; }
            if (to_format[16] == "") { output[29] = "0"; output[32] = ""; }

            // 17 - Format SavingsAccounts_BND
            if (to_format[17] == "1") { output[31]=to_format[17]; output[32] = "1 - Very High"; }
            if (to_format[17] == "2") { output[31]=to_format[17]; output[32] = "2 - High"; }
            if (to_format[17] == "3") { output[31]=to_format[17]; output[32] = "3 - Above Average"; }
            if (to_format[17] == "4") { output[31]=to_format[17]; output[32] = "4 - Below Average"; }
            if (to_format[17] == "5") { output[31]=to_format[17]; output[32] = "5 - Low"; }
            if (to_format[17] == "6") { output[31]=to_format[17]; output[32] = "6 - Very Low"; }
            if (to_format[17] == null) { output[31]="0"; output[32] = ""; }
            if (to_format[17] == "") { output[31] = "0"; output[32] = ""; }

            // 18 - Format Sex
            if (to_format[18] == null) { output[33] = ""; }

            // 19 - Store match level
            if (to_format[19] == "Person") { output[34] = "Individual"; }
            if (to_format[19] == "Domicile") { output[34] = "Household"; }
            if (to_format[19] == "Postcode") { output[34] = "Postcode"; }

            return output;
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
