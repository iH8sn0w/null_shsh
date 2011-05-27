Imports System.IO
Module Module1
    Sub Main(ByVal args() As String)
        If args.Length = 0 Then
            Console.WriteLine()
            Console.Write("Usage: null_shsh.exe <*signed* img3(s)>")
            Console.WriteLine()
            Exit Sub
        Else
            Console.WriteLine()
            Console.WriteLine("-------------------")
            Console.WriteLine("SHSH Nullifier v1.0")
            Console.WriteLine("By: iH8sn0w")
            Console.WriteLine("-------------------")
            Dim i As Integer = 1
            Dim i2 As Integer
            Dim SHSH_offset As String
            Dim bw As BinaryWriter
            Dim blob As Byte() = String_To_Bytes("00000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000")
            For Each arg2 As String In args
                i2 = i2 + 1
            Next
            For Each arg As String In args
                Console.WriteLine("Processing " & i & " of " & i2)
                SHSH_offset = Find_SHSH_offset(arg)
                bw = New BinaryWriter(File.Open(arg, FileMode.Open, FileAccess.ReadWrite))

                bw.BaseStream.Seek(SHSH_offset, SeekOrigin.Begin)
                bw.Write(blob)
                bw.Close()
                i = i + 1
            Next arg
            Console.WriteLine("-------------------")
        End If
        Exit Sub
    End Sub
    Public Function String_To_Bytes(ByVal strInput As String) As Byte()
        Dim i As Integer = 0
       Dim x As Integer = 0
        Dim bytes As Byte() = New Byte((strInput.Length) \ 2 - 1) {}
        While strInput.Length > i + 1
            Dim lngDecimal As Long = Convert.ToInt32(strInput.Substring(i, 2), 16)
            bytes(x) = Convert.ToByte(lngDecimal)
            i = i + 2
            x += 1
        End While
        Return bytes
    End Function
    Public Function ByteArrayToString(ByVal arrInput() As Byte) As String
        Dim strOutput As New System.Text.StringBuilder(arrInput.Length)
        For i As Integer = 0 To arrInput.Length - 1
            strOutput.Append(arrInput(i).ToString("X2"))
        Next
        Return strOutput.ToString().ToLower
    End Function
    Public Function Find_SHSH_offset(ByVal infile As String)
        'Loadup img3...
        Dim Reader As New BinaryReader(File.OpenRead(infile))
        'Advance to 0xC (SHSH offset origin)
        Reader.BaseStream.Seek(&HC, SeekOrigin.Begin)
        Dim SHSH_bytes As Byte() = Reader.ReadBytes(4)
        Dim SHSH_bytes_string As String = ByteArrayToString(SHSH_bytes)
        Dim shsh_orign As String = SHSH_bytes_string.Substring(6, 2) & SHSH_bytes_string.Substring(4, 2) & SHSH_bytes_string.Substring(2, 2) & SHSH_bytes_string.Substring(0, 2)
        Reader.Close()
        Return SubHex(shsh_orign, "2C")
    End Function
    Public Function SubHex(ByVal Address As String, ByVal Value As String)
        Dim add_value As Integer = Integer.Parse(Value, Globalization.NumberStyles.HexNumber)
        Dim add_address As Integer = Integer.Parse(Address, Globalization.NumberStyles.HexNumber)
        Dim i As Integer = add_address - add_value
        Dim finale As Long = CType(i, Long)
        Return finale
    End Function
End Module
