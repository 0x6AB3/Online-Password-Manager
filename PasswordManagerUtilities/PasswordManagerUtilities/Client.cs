using System.Net.Sockets;
using System.Text;

namespace PasswordManagerUtilities
{
    public class Client : NetworkNode
    {
        // The session key is used to encrypt/decrypt all data that is exchanged with the server.
        public AES_GCM sessionKey { get; set; }

        public Client(string IP, int port) : base(IP, port) { }

        // Begin method runs before any commands are sent.
        public override void Begin()
        {
            // Connecting to server and exchanging the session key to establish secure connection.
            serverSocket.Connect(serverEndPoint);
            sessionKey = SecureConnection(serverSocket);
        }

        // Command method that is used to send commands to the server after being securely connected.
        public string SendCommand(string opcode, string username = null, byte[] authenticationHash = null, byte[] rawVault = null, string vaultName = null)
        {
            string result = null;

            switch (opcode)
            {
                // Signup and login both transmit an opcode, followed by username and authentication hash.
                case "SIGNUP":
                case "LOGIN":
                    SecureTransmit(serverSocket, sessionKey, UTF8Encoding.UTF8.GetBytes($"{opcode} {username} {System.Convert.ToBase64String(authenticationHash)}"));
                    result = UTF8Encoding.UTF8.GetString(SecureReceive(serverSocket, sessionKey));
                    break;

                // Upload command transmits the opcode and vault name in one string, the actual vault is then sent in a seperate transmission as the vault is not stored as a string.
                case "UPLOAD":
                    SecureTransmit(serverSocket, sessionKey, UTF8Encoding.UTF8.GetBytes($"{opcode} {vaultName}"));
                    SecureTransmit(serverSocket, sessionKey, rawVault);
                    result = UTF8Encoding.UTF8.GetString(SecureReceive(serverSocket, sessionKey));
                    break;

                // Download and delete both transmit an opcode, followed by the name of the vault that is being downloaded/deleted. The vault is received as a Json string.
                case "DOWNLOAD":
                case "DELETE":
                    SecureTransmit(serverSocket, sessionKey, UTF8Encoding.UTF8.GetBytes($"{opcode} {vaultName}"));
                    result = UTF8Encoding.UTF8.GetString(SecureReceive(serverSocket, sessionKey));
                    break;

                // List only transmits an opcode to the server. The server then responds with a whitespace seperated list of vault names belonging to the user.
                case "LIST":
                    SecureTransmit(serverSocket, sessionKey, UTF8Encoding.UTF8.GetBytes($"{opcode}"));
                    result = UTF8Encoding.UTF8.GetString(SecureReceive(serverSocket, sessionKey));
                    break;

                // Continue and cancel are used for confirmation of events, such as successful form instantiation or cancelling a vault upload.
                case "CONTINUE":
                case "CANCEL":
                case "PING":
                    SecureTransmit(serverSocket, sessionKey, UTF8Encoding.UTF8.GetBytes($"{opcode}"));
                    break;
            }

            // The result of the transmission is returned. null is returned if no transmission was made due to the command being invalid. Error messages are also returned.
            return result;
        }

        // Client-side generation and transmission of session key.
        public override AES_GCM SecureConnection(Socket connection)
        {
            // Instantiating cryptography objects.
            AES_GCM streamCipher = new AES_GCM();
            RSA_4096 asymmetricEncryption = new RSA_4096();

            // Receiving the public server key in order to encrypt the session key.
            byte[] publicKey = Receive(connection);
            asymmetricEncryption.SetPublicKey(publicKey);

            // Encryption and transmission of session key.
            byte[] encryptedSessionKey = asymmetricEncryption.Encrypt(streamCipher.Key);

            Transmit(encryptedSessionKey, serverSocket);

            return streamCipher;
        }

        // Polling server to check if client is still connected.
        public bool IsAlive()
        {
            return serverSocket.Poll(5000, SelectMode.SelectRead);
        }
    }
}
