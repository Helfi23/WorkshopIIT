Imports System.Drawing
Imports System.Drawing.Imaging

Public Class Form1
    Private Sub btnProcess_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If OpenFileDialog1.ShowDialog() = DialogResult.OK Then
            Dim originalImage As Bitmap = New Bitmap(OpenFileDialog1.FileName)

            ' Proses pengolahan citra ke 5 warna
            Dim processedImage As Bitmap = ProcessImage(originalImage)

            ' Simpan hasil pengolahan citra ke file
            SaveFileDialog1.Filter = "JPEG Image|*.jpg"
            If SaveFileDialog1.ShowDialog() = DialogResult.OK Then
                processedImage.Save(SaveFileDialog1.FileName, ImageFormat.Jpeg)
            End If
        End If
    End Sub

    Private Function ProcessImage(originalImage As Bitmap) As Bitmap
        ' Buat daftar warna yang diinginkan (5 warna)
        Dim colors As Color() = {Color.Red, Color.Green, Color.Blue, Color.Yellow, Color.Purple}

        Dim width As Integer = originalImage.Width
        Dim height As Integer = originalImage.Height

        Dim processedImage As New Bitmap(width, height)

        For y As Integer = 0 To height - 1
            For x As Integer = 0 To width - 1
                ' Dapatkan warna piksel dari gambar asli
                Dim originalColor As Color = originalImage.GetPixel(x, y)

                ' Cari warna terdekat dalam daftar warna yang diinginkan
                Dim nearestColor As Color = FindNearestColor(originalColor, colors)

                ' Set piksel pada gambar hasil dengan warna terdekat
                processedImage.SetPixel(x, y, nearestColor)
            Next
        Next

        Return processedImage
    End Function

    Private Function FindNearestColor(targetColor As Color, colors As Color()) As Color
        Dim nearestColor As Color = colors(0)
        Dim minDistance As Double = GetColorDistance(targetColor, colors(0))

        For Each color As Color In colors
            Dim distance As Double = GetColorDistance(targetColor, color)
            If distance < minDistance Then
                nearestColor = color
                minDistance = distance
            End If
        Next

        Return nearestColor
    End Function

    Private Function GetColorDistance(color1 As Color, color2 As Color) As Double
        Dim rDiff As Integer = CInt(color1.R) - color2.R
        Dim gDiff As Integer = CInt(color1.G) - color2.G
        Dim bDiff As Integer = CInt(color1.B) - color2.B

        Return Math.Sqrt(rDiff * rDiff + gDiff * gDiff + bDiff * bDiff)
    End Function
End Class
