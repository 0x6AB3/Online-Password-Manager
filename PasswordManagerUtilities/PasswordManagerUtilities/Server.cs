using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Text;
using System.Data.OleDb;
using System.Security.Cryptography;
using System.IO;

namespace PasswordManagerUtilities
{
    public class Server : NetworkNode
    {
        private RSA_4096 asymmetricEncryption;
        private OleDbConnection databaseConnection;

        public Server(string IP, int port, string path) : base(IP, port)
        {
            // Setting server address.
            IPAddress serverIP = IPAddress.Parse(IP);
            serverEndPoint = new IPEndPoint(serverIP, port);

            serverSocket.Bind(serverEndPoint);

            // Generating a new asymmetric encryption key pair.
            asymmetricEncryption = new RSA_4096();

            // Password database connection.
            databaseConnection = new OleDbConnection($"Provider=Microsoft.Jet.OLEDB.4.0; Data Source={path}");

            // Directory in which vaults are stored.
            Directory.CreateDirectory("ClientVaults");

        }

        // Main runtime method.
        public override void Begin()
        {
            // Allowing a maximum queue of 20 clients attempting to connect.
            serverSocket.Listen(20);

            while (true)
            {
                // Accepting the connecting client.
                Socket clientConnection = serverSocket.Accept();

                // Outputting client details to the console.
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"[NETWORK]\t{clientConnection.RemoteEndPoint} CONNECTED");
                Console.ResetColor();

                // Creating a seperate thread which handles the client so that other clients can still be accepted by the server.
                Thread clientThread = new Thread(new ParameterizedThreadStart(HandleClient));
                clientThread.Start(clientConnection);

                
            }
        }

        // Method that processes client requests (runs on a seperate thread)
        public void HandleClient(object connection)
        {
            // Casting parameter to a Socket object (parameterised threads require object parameters).
            Socket clientConnection = (Socket)connection;

            // Receiving the session key from the client.
            AES_GCM sessionKey = SecureConnection(clientConnection);
            
            // The opcode and operands of an instruction sent by the client.
            string[] commandParameters;

            // Storing the AccountID of the client so that it is only retrieved from the database once.
            string ID = null;

            // Client login/signup.
            try
            {
                do
                {
                    commandParameters = UTF8Encoding.UTF8.GetString(SecureReceive(clientConnection, sessionKey)).Split();
                }
                while (!ClientLoggedIn(commandParameters, ref ID, clientConnection, sessionKey));
            }

            // If client did not login/signup, the client software either closed intentionally or through an error.
            catch
            {
                // Aborting thread.
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"[DISCONNECTED]\t{clientConnection.RemoteEndPoint}");
                Console.ResetColor();
                return;
            }

            // At this point, the client has logged in or signed up and they are instantiating a VaultViewForm.
            try
            {
                // Allowing the client 10 seconds to initialise the VaultViewForm and send back confirmation.
                clientConnection.ReceiveTimeout = 10000;
                SecureReceive(clientConnection, sessionKey);
                clientConnection.ReceiveTimeout = 0;

                // Outputting successful login to the server console.
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"[LOGIN]\t\t{clientConnection.RemoteEndPoint}");
                Console.ResetColor();
            }
            catch (SocketException)
            {
                // As the client took too long to confirm successful instantiation, the server assumes a client-side timeout.
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"[TIMEOUT]\t{clientConnection.RemoteEndPoint}");
                Console.ResetColor();
            }

            // Main command handling loop.
            try
            {
                while (true)
                {
                    commandParameters = UTF8Encoding.UTF8.GetString(SecureReceive(clientConnection, sessionKey)).Split();

                    databaseConnection.Open();
                    HandleCommand(commandParameters, ID, clientConnection, sessionKey);
                    databaseConnection.Close();
                }
            }

            // An exception occurs when the client disconnects.
            catch {}

            // Outputting client disconnect.
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"[DISCONNECTED]\t{clientConnection.RemoteEndPoint}");
            Console.ResetColor();
        }

        // Handling client commands (runs in the main command handling loop).
        public void HandleCommand(string[] commandParameters, string ID, Socket clientConnection, AES_GCM sessionKey)
        {
            // Local variables.
            string action = commandParameters[0];
            string vaultName, vaultPath;
            byte[] rawVault;
            OleDbCommand databaseCommand;
            OleDbDataReader resultReader;

            
            if (action == "PING")
            {
                return;
            }

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"[COMMAND]\t{clientConnection.RemoteEndPoint} {action}");
            Console.ResetColor();

            switch (action)
            {
                // Client choses to list all vaults stored on the database.
                case "LIST":

                    // Searching for vault names that correspond to the user ID.
                    databaseCommand = new OleDbCommand("SELECT VaultName FROM tblVaults WHERE tblVaults.AccountID = @id", databaseConnection);
                    databaseCommand.Parameters.AddWithValue("@id", ID);
                    resultReader = databaseCommand.ExecuteReader();

                    string result = "";

                    // Appending any vaults found to the result string.
                    while (resultReader.Read())
                    {
                        result += $"{resultReader[0]} ";
                    }

                    // Transmitting the vault names.
                    SecureTransmit(clientConnection, sessionKey, UTF8Encoding.UTF8.GetBytes(result));
                    break;

                // Client choses to upload their vault.
                case "UPLOAD":

                    try
                    {
                        // Receiving the client vault.
                        rawVault = SecureReceive(clientConnection, sessionKey);

                        // Setting the path of where the vault will be stored on the server.
                        vaultName = commandParameters[1];
                        vaultPath = Directory.GetCurrentDirectory() + $@"\ClientVaults\{ID}\{vaultName}";

                        // Checking if the vault already exists on the server.
                        if (File.Exists(vaultPath))
                        {
                            // Informing client of existing vault.
                            SecureTransmit(clientConnection, sessionKey, UTF8Encoding.UTF8.GetBytes("ERROR VAULT_EXISTS"));
                            
                            // Awaiting confirmation/cancellation of vault overwrite.
                            string confirmation = UTF8Encoding.UTF8.GetString(SecureReceive(clientConnection, sessionKey));
                            if (confirmation == "CONTINUE")
                            {
                                File.WriteAllBytes(vaultPath, rawVault);
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine($"[UPLOAD]\t{clientConnection.RemoteEndPoint} {rawVault.Length} BYTES");
                                Console.ResetColor();
                                return;
                            }

                            else
                            {
                                return;
                            }
                        }

                        // Copying the vault onto the server.
                        File.WriteAllBytes(vaultPath, rawVault);

                        // Adding record of vault into the database.
                        databaseCommand = new OleDbCommand("INSERT INTO tblVaults VALUES(@vaultID, @vaultName, @vaultLocation, @accountID)", databaseConnection);

                        databaseCommand.Parameters.AddWithValue("@vaultID", Guid.NewGuid().ToString());
                        databaseCommand.Parameters.AddWithValue("@vaultName", vaultName);
                        databaseCommand.Parameters.AddWithValue("@vaultLocation", vaultPath);
                        databaseCommand.Parameters.AddWithValue("@accountID", ID);
                        databaseCommand.ExecuteNonQuery();

                        SecureTransmit(clientConnection, sessionKey, UTF8Encoding.UTF8.GetBytes("SUCCESS"));
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"[UPLOAD]\t{clientConnection.RemoteEndPoint} {rawVault.Length} BYTES");
                        Console.ResetColor();
                    }

                    // Informing client of failure.
                    catch
                    {
                        SecureTransmit(clientConnection, sessionKey, UTF8Encoding.UTF8.GetBytes("ERROR FAILED"));
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"[UPLOAD]\t{clientConnection.RemoteEndPoint} FAILURE");
                        Console.ResetColor();
                    }
                    break;


                // Client choses to retrieve a vault that is stored on the server.
                case "DOWNLOAD":

                    vaultName = commandParameters[1];
                    
                    // Retrieving the vault path.
                    databaseCommand = new OleDbCommand("SELECT VaultLocation FROM tblVaults WHERE tblVaults.AccountID = @id AND tblVaults.VaultName = @name", databaseConnection);
                    databaseCommand.Parameters.AddWithValue("@id", ID);
                    databaseCommand.Parameters.AddWithValue("@name", vaultName);


                    try
                    {
                        // Executing SQL search query.
                        resultReader = databaseCommand.ExecuteReader();
                        resultReader.Read();

                        string path = resultReader[0].ToString();
                        
                        // Retrieving the raw vault data and transmitting it to client.
                        byte[] vault = File.ReadAllBytes(path);
                        SecureTransmit(clientConnection, sessionKey, vault);
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"[DOWNLOAD]\t{clientConnection.RemoteEndPoint} {vault.Length} BYTES");
                        Console.ResetColor();
                    }
                    catch
                    {
                        SecureTransmit(clientConnection, sessionKey, UTF8Encoding.UTF8.GetBytes("ERROR FAILED"));
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"[DOWNLOAD]\t{clientConnection.RemoteEndPoint} FAILURE");
                        Console.ResetColor();
                    }
                    break;


                // Client choses to delete a specified vault.
                case "DELETE":

                    vaultName = commandParameters[1];

                    // Retreiving location of vault.
                    databaseCommand = new OleDbCommand("SELECT VaultLocation FROM tblVaults WHERE tblVaults.AccountID = @id AND tblVaults.VaultName = @name", databaseConnection);
                    databaseCommand.Parameters.AddWithValue("@id", ID);
                    databaseCommand.Parameters.AddWithValue("@name", vaultName);

                    try
                    {
                        resultReader = databaseCommand.ExecuteReader();
                        resultReader.Read();

                        string path = resultReader[0].ToString();

                        // Deleting the vault file if one exists.
                        if (File.Exists(path))
                        {
                            File.Delete(path);
                        }

                        // Deleting the vault record from the database.
                        databaseCommand = new OleDbCommand("DELETE FROM tblVaults WHERE tblVaults.AccountID = @id AND tblVaults.VaultName = @name", databaseConnection);
                        databaseCommand.Parameters.AddWithValue("@id", ID);
                        databaseCommand.Parameters.AddWithValue("@name", vaultName);
                        databaseCommand.ExecuteNonQuery();

                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"[DELETE]\t{clientConnection.RemoteEndPoint} SUCCESS");
                        Console.ResetColor();
                        SecureTransmit(clientConnection, sessionKey, UTF8Encoding.UTF8.GetBytes("SUCCESS"));
                    }

                    // Catching an error and informing client (most commonly due to the vault not existing).
                    catch
                    {
                        SecureTransmit(clientConnection, sessionKey, UTF8Encoding.UTF8.GetBytes("ERROR FAILED"));
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"[DELETE]\t{clientConnection.RemoteEndPoint} FAILURE");
                        Console.ResetColor();
                    }
                    break;
            }
        }

        // Handling client login/signup.
        public bool ClientLoggedIn(string[] commandParameters, ref string ID, Socket clientConnection, AES_GCM sessionKey)
        {
            // Local variables.
            bool loggedIn = false;

            OleDbCommand databaseCommand;

            string action = commandParameters[0];
            string username = commandParameters[1];
            string authenticationHash = commandParameters[2];

            databaseConnection.Open();

            // Setting the user ID from the given username.
            ID = GetID(username);

            switch ( (action, ID is null) )
            {
                // Client choses to signup and their desired username is available.
                case ("SIGNUP", true):

                    // Creating a new globally unique identifier.
                    ID = Guid.NewGuid().ToString();

                    // Generation of a pseudo random salt.
                    byte[] salt = new byte[16];
                    RandomNumberGenerator.Fill(salt);
                    
                    // Hashing the received authentication hash 50,000 times with the randomly generated salt.
                    byte[] hash = PBKDF2.DeriveKey(System.Convert.FromBase64String(authenticationHash), salt, 50000);

                    // Adding the new account to the password database.
                    databaseCommand = new OleDbCommand("INSERT INTO tblAccounts VALUES(@id, @username, @hash, @salt)", databaseConnection);

                    databaseCommand.Parameters.AddWithValue("@id", ID);
                    databaseCommand.Parameters.AddWithValue("@username", username);
                    databaseCommand.Parameters.AddWithValue("@hash", System.Convert.ToBase64String(hash));
                    databaseCommand.Parameters.AddWithValue("@salt", System.Convert.ToBase64String(salt));
                    databaseCommand.ExecuteNonQuery();

                    // Creating a new folder named after the user ID for storage of client vaults.
                    Directory.CreateDirectory(Directory.GetCurrentDirectory() + @"\ClientVaults\" + ID);

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"[DATABASE]\t{clientConnection.RemoteEndPoint} ACCOUNT CREATED {username}");
                    Console.ResetColor();

                    // Informing client device of successful account creation.
                    SecureTransmit(clientConnection, sessionKey, UTF8Encoding.UTF8.GetBytes("SUCCESS"));
                    loggedIn = true;
                    break;


                // Client choses to signup but their desired username is not available.
                case ("SIGNUP", false):

                    // Transmitting error message.
                    SecureTransmit(clientConnection, sessionKey, UTF8Encoding.UTF8.GetBytes("ERROR ACCOUNT_EXISTS"));
                    loggedIn = false;
                    break;


                // Client choses to login and their user ID exists in the database.
                case ("LOGIN", false):

                    // Retrieving corresponding authentication hash and salt from the database.
                    databaseCommand = new OleDbCommand("SELECT AuthenticationHash, Salt FROM tblAccounts WHERE Username = @username", databaseConnection);
                    databaseCommand.Parameters.AddWithValue("@username", username);

                    OleDbDataReader results = databaseCommand.ExecuteReader();
                    results.Read();

                    string storedHash = results[0].ToString();
                    byte[] storedSalt = System.Convert.FromBase64String(results[1].ToString());

                    // Hashing the authentication hash with the retrieved salt to check if it hashes to the correct value.
                    byte[] receivedhash = PBKDF2.DeriveKey(System.Convert.FromBase64String(authenticationHash), storedSalt, 50000);

                    // Informing client of successful login if hashes match.
                    if (storedHash == System.Convert.ToBase64String(receivedhash))
                    {
                        SecureTransmit(clientConnection, sessionKey, UTF8Encoding.UTF8.GetBytes("SUCCESS"));
                        loggedIn = true;
                    }

                    // Informing client of incorrect login if hashes mismatch.
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"[LOGIN]\t\t{clientConnection.RemoteEndPoint} INCORRECT HASH");
                        Console.ResetColor();

                        SecureTransmit(clientConnection, sessionKey, UTF8Encoding.UTF8.GetBytes("ERROR INCORRECT_HASH"));
                        loggedIn = false;
                    }
                    break;


                // Client choses to login but their user ID doesn't exist.
                case ("LOGIN", true):

                    // Transmitting error message
                    SecureTransmit(clientConnection, sessionKey, UTF8Encoding.UTF8.GetBytes("ERROR ACCOUNT_DOESNT_EXIST"));
                    loggedIn = false;
                    break;
            }

            databaseConnection.Close();
            return loggedIn;
        }

        // Searching for a user ID given a plaintext username
        public string GetID(string username)
        {
            string ID = null;

            // Parameterising SQL statement to prevent SQL injections.
            OleDbCommand databaseCommand = new OleDbCommand("SELECT AccountID FROM tblAccounts WHERE Username = @username", databaseConnection);
            databaseCommand.Parameters.AddWithValue("@username", username);

            // Executing the search and returning an ID if one is found.
            var searchResults = databaseCommand.ExecuteScalar();

            if (searchResults != null)
            {
                ID  = searchResults.ToString();
            }

            return ID;
        }

        // Server-side reception of session key.
        public override AES_GCM SecureConnection(Socket connection)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"[NETWORK]\t{connection.RemoteEndPoint} PUBLIC KEY TRANSFER");
            Console.ResetColor();
            // Transmitting public key to client.
            Transmit(asymmetricEncryption.GetPublicKey(), connection);

            // Receiving and decrypting session key.
            byte[] encryptedSessionKey = Receive(connection);
            byte[] sessionKey = asymmetricEncryption.Decrypt(encryptedSessionKey);

            // Returning an AES_GCM object.
            AES_GCM streamCipher = new AES_GCM();
            streamCipher.Key = sessionKey;

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"[NETWORK]\t{connection.RemoteEndPoint} END-TO-END ENCRYPTION ESTABLISHED");
            Console.ResetColor();

            return streamCipher;
        }
    }
}
