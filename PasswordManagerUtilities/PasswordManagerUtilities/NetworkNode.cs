using System;
using System.Net;
using System.Net.Sockets;

namespace PasswordManagerUtilities
{
    // The base network class which provides child classes with methods for transmission of data between each other.
    public abstract class NetworkNode
    {
        /*
         The header is the default amount of bytes to send and receive by NetworkNode instances. The header is sent so that the receiving machine 
         knows how many bytes of data to expect. It is set to 4 as that is the Uint32 size which allows for values ranging between 0-4294967295,
         hence allowing up to just about 4 gibibytes to be sent per transmission.
        */
        protected const int HEADER = 4;

        // Clients require the address of the server and a socket which connects them and allows for transmission.
        // Servers require their own address and socket so that they can bind themselves to it and listen for connections from clients.
        protected IPEndPoint serverEndPoint;
        protected Socket serverSocket;

        // Assigning the socket and address of the server.
        public NetworkNode(string IP, int port)
        {
            serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            serverEndPoint = new IPEndPoint(IPAddress.Parse(IP), port);
        }

        // The main runtime method for child classes.
        public abstract void Begin();

        // Raw, unencrypted data transmission.
        public void Transmit(byte[] data, Socket connection)
        {
            // Storing data length in a 32-bit unsigned int header (negative numbers wont be required).
            UInt32 dataLength = (uint) data.Length;
            byte[] rawDataLength = BitConverter.GetBytes(dataLength);

            // Different computers may use different endianness, reversing the array will ensure that little endian machines will send big endian data.
            CorrectEndianness(rawDataLength);

            // Sending the data length in header size, followed by the actual data.
            connection.Send(rawDataLength);
            connection.Send(data);
        }

        // Raw, unencrypted data reception.
        public byte[] Receive(Socket connection)
        {
            // Receiving the amount of bytes to expect.
            byte[] rawDataLength = new byte[HEADER];
            connection.Receive(rawDataLength);
            CorrectEndianness(rawDataLength);

            UInt32 dataLength = BitConverter.ToUInt32(rawDataLength);

            // Receiving the expected amount of bytes.
            byte[] data = new byte[dataLength];
            connection.Receive(data);

            return data;
        }

        // Clients and Servers secure their connection in different ways. Child classes override this function to specify
        // how they carry out their part of securing the connection.
        public abstract AES_GCM SecureConnection(Socket connection);

        // Secure data reception using the AES_GCM_256 session key returned from the SecureConnection method.
        public byte[] SecureReceive(Socket connection, AES_GCM streamCipher)
        {
            // Receiving the ciphertext, followed by the IV used to decrypt it and the authentication tag that is used to verify it.
            byte[] ciphertext = Receive(connection);
            streamCipher.IV = Receive(connection);
            streamCipher.Tag = Receive(connection);

            try
            {
                // Decrypting the data and returning it to the caller.
                byte[] plaintext = streamCipher.Decrypt(ciphertext);
                return plaintext;
            }

            // In the event of the data being changed after encryption (usually after being intercepted by an attacker), null is returned.
            catch
            {
                return null;
            }   
        }

        // Secure data transmission using the AES_GCM_256 session key returned from the SecureConnection method.
        public void SecureTransmit(Socket connection, AES_GCM streamCipher, byte[] plaintext)
        {
            // Generating a random IV and using it to encrypt the plaintext.
            streamCipher.GenerateIV();
            byte[] ciphertext = streamCipher.Encrypt(plaintext);

            // Transmitting the plaintext, IV and tag to the receiver.
            Transmit(ciphertext, connection);
            Transmit(streamCipher.IV, connection);
            Transmit(streamCipher.Tag, connection);
        }

        // Polling the socket to verify that the other node is connected.
        public bool RemoteConnected(Socket connection)
        {
            if (connection == null)
            {
                return false;
            }
            return connection.Poll(2000000, SelectMode.SelectRead);
        }

        // Changing endianness to big endian for transmission/receiving.
        private void CorrectEndianness(byte[] array)
        {
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(array);
            }
        }
    }
}
