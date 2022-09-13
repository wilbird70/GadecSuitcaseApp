'Gadec Engineerings Software (c) 2020

''' <summary>
''' <para><see cref="CryptoWrapper"/> can encrypt and decrypt textstrings.</para>
''' </summary>
Public Class CryptoWrapper
    ''' <summary>
    ''' Contains the <see cref="Security.Cryptography.TripleDESCryptoServiceProvider"/> for the present wrapper.
    ''' </summary>
    Private ReadOnly _cryptoServiceProvider As New Security.Cryptography.TripleDESCryptoServiceProvider

    'class

    ''' <summary>
    ''' Initializes a new instance of <see cref="CryptoWrapper"/> with the specified password.
    ''' <para><see cref="CryptoWrapper"/> can encrypt and decrypt data.</para>
    ''' </summary>
    ''' <param name="password">The password to use encrypting and decrypting textstrings.</param>
    Sub New(password As String)
        ' Initialize the crypto provider.
        _cryptoServiceProvider.Key = TruncateHash(password, _cryptoServiceProvider.KeySize \ 8)
        _cryptoServiceProvider.IV = TruncateHash("", _cryptoServiceProvider.BlockSize \ 8)
    End Sub

    'functions

    ''' <summary>
    ''' Encrypts the plain text to encrypted string.
    ''' </summary>
    ''' <param name="plainText">The plain text.</param>
    ''' <returns>The encrypted string.</returns>
    Public Function EncryptData(ByVal plainText As String) As String
        ' Convert the plaintext string to a byte array.
        Dim plainTextBytes() As Byte = Text.Encoding.Unicode.GetBytes(plainText)
        ' Create the stream.
        Dim memoryStream As New IO.MemoryStream
        ' Create the encoder to write to the stream.
        Dim cryptoStream As New Security.Cryptography.CryptoStream(memoryStream, _cryptoServiceProvider.CreateEncryptor(), Security.Cryptography.CryptoStreamMode.Write)
        ' Use the crypto stream to write the byte array to the stream.
        cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length)
        cryptoStream.FlushFinalBlock()
        ' Convert the encrypted stream to a printable string.
        Return Convert.ToBase64String(memoryStream.ToArray)
    End Function

    ''' <summary>
    ''' Decrypts the encrypted string to plain text.
    ''' </summary>
    ''' <param name="encryptedText">The encrypted string.</param>
    ''' <returns>The plain text.</returns>
    Public Function DecryptData(ByVal encryptedText As String) As String
        ' Convert the encrypted text string to a byte array.
        Dim cryptoBytes() As Byte = Convert.FromBase64String(encryptedText)
        ' Create the stream.
        Dim memoryStream As New System.IO.MemoryStream
        ' Create the decoder to write to the stream.
        Dim cryptoStream As New Security.Cryptography.CryptoStream(memoryStream, _cryptoServiceProvider.CreateDecryptor(), Security.Cryptography.CryptoStreamMode.Write)
        ' Use the crypto stream to write the byte array to the stream.
        cryptoStream.Write(cryptoBytes, 0, cryptoBytes.Length)
        cryptoStream.FlushFinalBlock()
        ' Convert the plaintext stream to a string.
        Return System.Text.Encoding.Unicode.GetString(memoryStream.ToArray)
    End Function

    'private functions

    ''' <summary>
    ''' Get the secret key for the algorithmfor the <see cref="Security.Cryptography.TripleDESCryptoServiceProvider"/>.
    ''' </summary>
    ''' <param name="password">The password to use.</param>
    ''' <param name="length">The size in bits of the password (?!?).</param>
    ''' <returns>The secret key for the algorithm.</returns>
    Private Function TruncateHash(password As String, length As Integer) As Byte()
        Dim cryptoServiceProvider As New Security.Cryptography.SHA1CryptoServiceProvider
        ' Hash the key.
        Dim keyBytes() As Byte = Text.Encoding.Unicode.GetBytes(password)
        Dim hash() As Byte = cryptoServiceProvider.ComputeHash(keyBytes)
        ' Truncate or pad the hash.
        ReDim Preserve hash(length - 1)
        Return hash
    End Function

End Class