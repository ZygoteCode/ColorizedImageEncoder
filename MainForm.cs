using System.Windows.Forms;
using MetroSuite;
using System.Drawing;
using System.Text;

public partial class MainForm : MetroForm
{
    private byte[] currentOpenedFile;
    private string currentOpenedFileName, currentOpenedFileExtension;
    private DecodedFileData currentDecodedFile;

    public MainForm()
    {
        InitializeComponent();
    }

    private void gunaGradientButton3_Click(object sender, System.EventArgs e)
    {
        try
        {
            Clipboard.SetText(gunaTextBox1.Text);
        }
        catch
        {

        }
    }

    private void gunaGradientButton2_Click(object sender, System.EventArgs e)
    {
        try
        {
            Clipboard.SetImage(pictureBox1.BackgroundImage);
        }
        catch
        {

        }
    }

    private void gunaGradientButton5_Click(object sender, System.EventArgs e)
    {
        try
        {
            pictureBox2.BackgroundImage = Clipboard.GetImage();
        }
        catch
        {

        }
    }

    private void gunaGradientButton1_Click(object sender, System.EventArgs e)
    {
        try
        {
            saveFileDialog1.FileName = "";

            if (saveFileDialog1.ShowDialog().Equals(DialogResult.OK))
            {
                pictureBox1.BackgroundImage.Save(saveFileDialog1.FileName);
            }
        }
        catch
        {

        }
    }

    private void gunaGradientButton6_Click(object sender, System.EventArgs e)
    {
        try
        {
            openFileDialog1.FileName = "";

            if (openFileDialog1.ShowDialog().Equals(DialogResult.OK))
            {
                pictureBox2.BackgroundImage = Image.FromFile(openFileDialog1.FileName);
            }
        }
        catch
        {

        }
    }

    private void gunaGradientButton4_Click(object sender, System.EventArgs e)
    {
        try
        {
            Clipboard.SetText(gunaTextBox2.Text);

        }
        catch
        {

        }
    }

    private void gunaGradientButton17_Click(object sender, System.EventArgs e)
    {
        try
        {
            pictureBox1.BackgroundImage = ColorizedImageTranslator.StringConvertBytesToBitmap(Encoding.UTF8.GetBytes(gunaTextBox1.Text), 512, 512);
        }
        catch
        {

        }
    }

    private void gunaGradientButton7_Click(object sender, System.EventArgs e)
    {
        try
        {
            byte[] bytes = ColorizedImageTranslator.StringConvertBitmapToBytes((Bitmap)pictureBox2.BackgroundImage);
            gunaTextBox2.Text = Encoding.UTF8.GetString(bytes);
            metroLabel2.Text = "Informations written: " + bytes.Length.ToString() + "/" + gunaTextBox2.MaxLength;
        }
        catch
        {

        }
    }

    private void gunaTextBox1_TextChanged(object sender, System.EventArgs e)
    {
        try
        {
            metroLabel1.Text = "Informations written: " + Encoding.UTF8.GetByteCount(gunaTextBox1.Text) + "/" + gunaTextBox1.MaxLength;
        }
        catch
        {

        }
    }

    private void gunaGradientButton10_Click(object sender, System.EventArgs e)
    {
        try
        {
            saveFileDialog1.FileName = "";

            if (saveFileDialog1.ShowDialog().Equals(DialogResult.OK))
            {
                pictureBox3.BackgroundImage.Save(saveFileDialog1.FileName);
            }
        }
        catch
        {

        }
    }

    private void gunaGradientButton11_Click(object sender, System.EventArgs e)
    {
        try
        {
            Clipboard.SetImage(pictureBox3.BackgroundImage);
        }
        catch
        {

        }
    }

    private void gunaGradientButton8_Click(object sender, System.EventArgs e)
    {
        try
        {
            openFileDialog2.FileName = "";

            if (openFileDialog2.ShowDialog().Equals(DialogResult.OK))
            {
                byte[] fileBytes = System.IO.File.ReadAllBytes(openFileDialog2.FileName);

                if (fileBytes.Length > 260000)
                {
                    return;
                }

                currentOpenedFile = fileBytes;
                currentOpenedFileExtension = System.IO.Path.GetExtension(openFileDialog2.FileName).Substring(1);
                currentOpenedFileName = System.IO.Path.GetFileNameWithoutExtension(openFileDialog2.FileName);

                metroLabel3.Text = "File bytes: " + fileBytes.Length + "/260000\r\n" +
                    "File name: " + currentOpenedFileName + "\r\n" +
                    "File extension: " + currentOpenedFileExtension + "\r\n" +
                    "File name with extension: " + currentOpenedFileName + "." + currentOpenedFileExtension;
            }
        }
        catch
        {

        }
    }

    private void gunaGradientButton9_Click(object sender, System.EventArgs e)
    {
        try
        {
            pictureBox3.BackgroundImage = ColorizedImageTranslator.FileConvertBytesToBitmap(currentOpenedFile, currentOpenedFileName, currentOpenedFileExtension, 512, 512);
        }
        catch
        {

        }
    }

    private void gunaGradientButton12_Click(object sender, System.EventArgs e)
    {
        try
        {
            openFileDialog1.FileName = "";

            if (openFileDialog1.ShowDialog().Equals(DialogResult.OK))
            {
                pictureBox4.BackgroundImage = Image.FromFile(openFileDialog1.FileName);
            }
        }
        catch
        {

        }
    }

    private void gunaGradientButton13_Click(object sender, System.EventArgs e)
    {
        try
        {
            pictureBox4.BackgroundImage = Clipboard.GetImage();
        }
        catch
        {

        }
    }

    private void gunaGradientButton14_Click(object sender, System.EventArgs e)
    {
        try
        {
            currentDecodedFile = ColorizedImageTranslator.ConvertBitmapToFile((Bitmap)pictureBox4.BackgroundImage);
            metroLabel4.Text = "File bytes: " + currentDecodedFile.FileSize + "/260000\r\n" +
                "File name: " + currentDecodedFile.FileName + "\r\n" +
                "File extension: " + currentDecodedFile.FileExtension + "\r\n" +
                "File name with extension: " + currentDecodedFile.FileName + "." + currentDecodedFile.FileExtension;
        }
        catch
        {

        }
    }

    private void gunaGradientButton15_Click(object sender, System.EventArgs e)
    {
        try
        {
            saveFileDialog2.Filter = currentDecodedFile.FileExtension.ToUpper() + " file (*." + currentDecodedFile.FileExtension + ")|*." + currentDecodedFile.FileExtension;
            saveFileDialog2.FileName = currentDecodedFile.FileName;

            if (saveFileDialog2.ShowDialog().Equals(DialogResult.OK))
            {
                System.IO.File.WriteAllBytes(saveFileDialog2.FileName, currentDecodedFile.FileData);
            }
        }
        catch
        {

        }
    }
}