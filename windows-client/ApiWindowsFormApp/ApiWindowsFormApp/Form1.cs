using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Windows.Forms;

namespace ApiWindowsFormApp
{
    public partial class Form1 : Form
    {
        private static readonly HttpClient client = new HttpClient();

        public Form1()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string baseUrl = txtURL.Text.Trim();
            string loginUrl = "http://" + baseUrl + "/api/v1/login";

            // Debugging: Print the login URL to check if it's correct
            Console.WriteLine("Login URL: " + loginUrl); // Check what URL is being constructed

            string username = txtUsername.Text;
            string password = txtPassword.Text;

            var loginData = new { username, password };
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(loginData);

            try
            {
                var response = client.PostAsync(loginUrl, new StringContent(json, Encoding.UTF8, "application/json")).Result;
                if (response.IsSuccessStatusCode)
                {
                    // Read the response content as a string
                    string responseContent = response.Content.ReadAsStringAsync().Result;

                    // Deserialize the response JSON into a dynamic object
                    dynamic responseObject = Newtonsoft.Json.JsonConvert.DeserializeObject(responseContent);

                    // Extract the access_token and assign it to txtToken
                    string token = responseObject.access_token;
                    txtToken.Text = token;

                    MessageBox.Show("Login successful!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Login failed!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void btnSearch_Click(object sender, EventArgs e)
        {
            // Set txtLabel to indicate the search has started
            txtTime.Text = "Searching in progress...";

            // Check if the token exists
            if (string.IsNullOrEmpty(txtToken.Text))
            {
                MessageBox.Show("You must log in first to perform the search.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Exit the method if no token exists
            }

            // Get the API URL from the txtURL TextBox
            string apiUrl = txtURL.Text;

            string searchUrl = "http://" + $"{apiUrl}/api/v1/search";
            string title = txtSearchTitle.Text;
            string message = txtSearchMessage.Text;
            string token = txtToken.Text;

            var searchQuery = new { query = $"here {title} - {message}" };
            var json = JsonConvert.SerializeObject(searchQuery);

            // Create a stopwatch instance to measure the time
            Stopwatch stopwatch = new Stopwatch();

            try
            {
                // Start measuring time
                stopwatch.Start();

                // Set the Authorization header with the Bearer token
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                // Make the POST request
                var response = client.PostAsync(searchUrl, new StringContent(json, Encoding.UTF8, "application/json")).Result;

                // Stop measuring time
                stopwatch.Stop();

                // Get the elapsed time in milliseconds and seconds
                long elapsedMilliseconds = stopwatch.ElapsedMilliseconds;
                double elapsedSeconds = stopwatch.Elapsed.TotalSeconds;

                // Display the elapsed time in the txtTime label (formatted)
                txtTime.Text = $"Search completed in {elapsedMilliseconds} ms ({elapsedSeconds:F2} s)";


                // Handle the response
                if (response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    txtResults.Text = result;  // Display the result in txtResults
                }
                else
                {
                    MessageBox.Show("Search failed!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtTime.Text = "Search failed. Please try again.";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtTime.Text = "An error occurred during the search.";
            }
        }



        private void btnPostMemory_Click(object sender, EventArgs e)
        {
            // Check if the token exists
            if (string.IsNullOrEmpty(txtToken.Text))
            {
                MessageBox.Show("You must log in first to post memory.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Exit the method if no token exists
            }

            // Check if all fields have data
            string missingFields = "";
            if (string.IsNullOrEmpty(txtMemoryTitle.Text))
                missingFields += "Title, ";
            if (string.IsNullOrEmpty(txtMemoryIssue.Text))
                missingFields += "Issue, ";
            if (string.IsNullOrEmpty(txtMemorySolution.Text))
                missingFields += "Solution, ";
            if (string.IsNullOrEmpty(txtMemoryCategory.Text))
                missingFields += "Category, ";

            // If any field is missing, construct the error message
            if (!string.IsNullOrEmpty(missingFields))
            {
                missingFields = missingFields.TrimEnd(' ', ','); // Remove trailing comma and space
                string errorMessage = $"{{\"msg\":\"{missingFields} are required\"}}";

                // Log the error message to the txtResults for debugging
                txtResults.AppendText("Error: \n" + errorMessage + "\n\n");

                MessageBox.Show("Please fill in all required fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Exit the method if any field is empty
            }

            // Get the API URL from the txtURL TextBox
            string apiUrl = txtURL.Text;

            // Get the values from the text boxes
            string postMemoryUrl = "http://" + $"{apiUrl}/api/v1/memory";
            string title = txtMemoryTitle.Text;
            string issue = txtMemoryIssue.Text;
            string solution = txtMemorySolution.Text;
            string category = txtMemoryCategory.Text;
            string customFields = txtMemoryCustomFields.Text;

            // Create the memory data
            var memoryData = new
            {
                title,
                issue,
                solution,
                category,
                customFields
            };

            // Serialize to JSON
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(memoryData);

            // Log the request JSON for debugging
            txtResults.AppendText("Request Data (JSON): \n" + json + "\n\n");

            try
            {
                // Set the Authorization header with the Bearer token
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", txtToken.Text);

                // Make the POST request
                var response = client.PostAsync(postMemoryUrl, new StringContent(json, Encoding.UTF8, "application/json")).Result;

                // Handle the response
                if (response.IsSuccessStatusCode)
                {
                    string responseBody = response.Content.ReadAsStringAsync().Result;

                    // Log the successful response data
                    txtResults.AppendText("Response Data: \n" + responseBody + "\n\n");

                    MessageBox.Show("Memory posted successfully!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    string errorResponse = response.Content.ReadAsStringAsync().Result;

                    // Log the error response data
                    txtResults.AppendText("Error Response: \n" + errorResponse + "\n\n");

                    MessageBox.Show("Failed to post memory!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                // Log the exception
                txtResults.AppendText("Exception: \n" + ex.Message + "\n\n");

                MessageBox.Show($"Error: {ex.Message}", "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



    }
}
