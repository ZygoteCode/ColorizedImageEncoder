using System;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

public class ColorizedImageTranslator
{
    public static Bitmap StringConvertBytesToBitmap(byte[] data, int imageWidth, int imageHeight)
    {
        if (data == null || imageWidth <= 10 || imageHeight <= 10)
        {
            return null;
        }

        byte[] completeData = BitConverter.GetBytes(data.Length);

        completeData = Combine(completeData, GetMD5(data));
        completeData = Combine(completeData, new byte[1] { 89 });
        completeData = Combine(completeData, data);
        completeData = Combine(completeData, new byte[1] { 0xF8 });

        int totalLength = completeData.Length;
        Bitmap bitmap = new Bitmap(imageWidth, imageHeight);
        int initialIndex = 0;

        for (int i = 0; i < bitmap.Width; i++)
        {
            for (int j = 0; j < bitmap.Height; j++)
            {
                if (initialIndex >= totalLength)
                {
                    bitmap.SetPixel(i, j, Color.White);
                }
                else
                {
                    byte item = completeData[initialIndex];
                    bitmap.SetPixel(i, j, Color.FromArgb(item, item, item));
                    initialIndex++;

                    if (initialIndex == totalLength)
                    {
                        bitmap.SetPixel(i, j, Color.FromArgb(45, 47, 50));
                    }
                }
            }
        }

        return bitmap;
    }

    public static byte[] StringConvertBitmapToBytes(Bitmap bitmap)
    {
        try
        {
            if (bitmap.Width != 512 || bitmap.Height != 512)
            {
                return null;
            }

            byte[] completeData = null;
            bool finish = false;

            for (int i = 0; i < bitmap.Width; i++)
            {
                if (!finish)
                {
                    for (int j = 0; j < bitmap.Height; j++)
                    {
                        if (!finish)
                        {
                            if (bitmap.GetPixel(i, j).R == 45 && bitmap.GetPixel(i, j).G == 47 && bitmap.GetPixel(i, j).B == 50)
                            {
                                finish = true;
                                break;
                            }
                            else
                            {
                                if (completeData == null)
                                {
                                    completeData = new byte[1] { bitmap.GetPixel(i, j).R };
                                }
                                else
                                {
                                    completeData = Combine(completeData, new byte[1] { bitmap.GetPixel(i, j).R });
                                }
                            }
                        }
                    }
                }
            }

            int dataSize = BitConverter.ToInt32(completeData.Take(4).ToArray(), 0);
            completeData = completeData.Skip(4).ToArray();

            byte[] md5Hash = completeData.Take(16).ToArray();
            completeData = completeData.Skip(16).ToArray();

            byte isFile = completeData.Take(1).ToArray()[0];
            completeData = completeData.Skip(1).ToArray();

            if (isFile != 89)
            {
                return null;
            }

            if (!CompareByteArrays(md5Hash, GetMD5(completeData)))
            {
                return null;
            }

            if (dataSize != completeData.Length)
            {
                return null;
            }

            return completeData;
        }
        catch
        {
            return null;
        }
    }

    public static Bitmap FileConvertBytesToBitmap(byte[] data, string fileName, string fileExtension, int imageWidth, int imageHeight)
    {
        try
        {
            if (data == null || imageWidth <= 10 || imageHeight <= 10)
            {
                return null;
            }

            byte[] completeData = BitConverter.GetBytes(data.Length);

            completeData = Combine(completeData, GetMD5(data));
            completeData = Combine(completeData, new byte[1] { 54 });

            byte[] fileNameBytes = Encoding.UTF8.GetBytes(fileName);
            byte[] fileExtensionBytes = Encoding.UTF8.GetBytes(fileExtension);

            completeData = Combine(completeData, BitConverter.GetBytes(fileNameBytes.Length));
            completeData = Combine(completeData, fileNameBytes);

            completeData = Combine(completeData, BitConverter.GetBytes(fileExtension.Length));
            completeData = Combine(completeData, fileExtensionBytes);

            completeData = Combine(completeData, data);
            completeData = Combine(completeData, new byte[1] { 0xF8 });

            int totalLength = completeData.Length;
            Bitmap bitmap = new Bitmap(imageWidth, imageHeight);
            int initialIndex = 0;

            for (int i = 0; i < bitmap.Width; i++)
            {
                for (int j = 0; j < bitmap.Height; j++)
                {
                    if (initialIndex >= totalLength)
                    {
                        bitmap.SetPixel(i, j, Color.White);
                    }
                    else
                    {
                        byte item = completeData[initialIndex];
                        bitmap.SetPixel(i, j, Color.FromArgb(item, item, item));
                        initialIndex++;

                        if (initialIndex == totalLength)
                        {
                            bitmap.SetPixel(i, j, Color.FromArgb(45, 47, 50));
                        }
                    }
                }
            }

            return bitmap;
        }
        catch
        {
            return null;
        }
    }

    public static DecodedFileData ConvertBitmapToFile(Bitmap bitmap)
    {
        try
        {
            if (bitmap.Width != 512 || bitmap.Height != 512)
            {
                return null;
            }

            byte[] completeData = null;
            bool finish = false;

            for (int i = 0; i < bitmap.Width; i++)
            {
                if (!finish)
                {
                    for (int j = 0; j < bitmap.Height; j++)
                    {
                        if (!finish)
                        {
                            if (bitmap.GetPixel(i, j).R == 45 && bitmap.GetPixel(i, j).G == 47 && bitmap.GetPixel(i, j).B == 50)
                            {
                                finish = true;
                                break;
                            }
                            else
                            {
                                if (completeData == null)
                                {
                                    completeData = new byte[1] { bitmap.GetPixel(i, j).R };
                                }
                                else
                                {
                                    completeData = Combine(completeData, new byte[1] { bitmap.GetPixel(i, j).R });
                                }
                            }
                        }
                    }
                }
            }

            int dataSize = BitConverter.ToInt32(completeData.Take(4).ToArray(), 0);
            completeData = completeData.Skip(4).ToArray();

            byte[] md5Hash = completeData.Take(16).ToArray();
            completeData = completeData.Skip(16).ToArray();

            byte isFile = completeData.Take(1).ToArray()[0];
            completeData = completeData.Skip(1).ToArray();

            if (isFile != 54)
            {
                return null;
            }

            int fileNameSize = BitConverter.ToInt32(completeData.Take(4).ToArray(), 0);
            completeData = completeData.Skip(4).ToArray();

            string fileName = Encoding.UTF8.GetString(completeData.Take(fileNameSize).ToArray());
            completeData = completeData.Skip(fileNameSize).ToArray();

            int fileExtensionSize = BitConverter.ToInt32(completeData.Take(4).ToArray(), 0);
            completeData = completeData.Skip(4).ToArray();

            string fileExtension = Encoding.UTF8.GetString(completeData.Take(fileExtensionSize).ToArray());
            completeData = completeData.Skip(fileExtensionSize).ToArray();

            if (dataSize != completeData.Length)
            {
                return null;
            }

            if (!CompareByteArrays(md5Hash, GetMD5(completeData)))
            {
                return null;
            }

            return new DecodedFileData()
            {
                FileName = fileName,
                FileExtension = fileExtension,
                FileData = completeData,
                FileSize = completeData.Length
            };
        }
        catch
        {
            return null;
        }
    }

    private static byte[] Combine(byte[] first, byte[] second)
    {
        byte[] ret = new byte[first.Length + second.Length];

        Buffer.BlockCopy(first, 0, ret, 0, first.Length);
        Buffer.BlockCopy(second, 0, ret, first.Length, second.Length);

        return ret;
    }

    private static byte[] GetMD5(byte[] data)
    {
        return MD5.Create().ComputeHash(data);
    }

    private static bool CompareByteArrays(byte[] first, byte[] second)
    {
        if (first.Length != second.Length)
        {
            return false;
        }

        for (int i = 0; i < first.Length; i++)
        {
            if (first[i] != second[i])
            {
                return false;
            }
        }

        return true;
    }
}