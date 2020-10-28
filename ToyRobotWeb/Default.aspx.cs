using System;
using System.IO;
using System.Web;
using System.Web.UI;
using RestSharp;

namespace ToyRobotWeb
{

    public partial class Default : System.Web.UI.Page
    {
        protected void ButtonSubmit_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(TextBoxInput.Text))
            {
                var commandList = TextBoxInput.Text.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
                var apiURL = TextBoxAPIURL.Text;
                var client = new RestClient(apiURL);
            
                foreach (var command in commandList)
                {
                    if (string.IsNullOrEmpty(command.Trim()))
                        continue;
                    var request = new RestRequest(string.Format("robotcommand/{0}", command), Method.GET);
                    var queryResult = client.Execute<string>(request).Data;
                    if (!string.IsNullOrEmpty(queryResult))
                        TextBoxOutput.Text += (Environment.NewLine + queryResult);
                }
            }
        }

        protected void ButtonUpload_Click(object sender, EventArgs e)
        {
            if (FileUploadInput.HasFile)
            {
                var uploadDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "InputFiles");
                if (!Directory.Exists(uploadDir))
                    Directory.CreateDirectory(uploadDir);
                var uploadFilePath = Path.Combine(uploadDir, FileUploadInput.FileName);
                //Clear Exisiting File.
                if (File.Exists(uploadFilePath))
                    File.Delete(uploadFilePath);
                FileUploadInput.SaveAs(uploadFilePath);
                TextBoxInput.Text = File.ReadAllText(uploadFilePath);
                File.Delete(uploadFilePath);
            }
        }
    }
}
