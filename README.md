# Security Case
This project is simulation of data security in client and server when send and receive data.

## Installation
1. Download this project
2. Compile the client and server program
3. Run the Server Program.exe
4. Run the Client Program.exe

## Flow
Initialization Client Connected:
1. Client is start, and load server public key
2. Client generate assymetric key.
3. Client encryt the client public key, and send it to server.
4. Server receive the encrypted client public key, and decrypt it with server private key.
5. Server generate symmetric key, encrypt the symmectric key with client public key, and send it to client.
6. Client receive encrypted symmetric key, and decrypt it with client private key.
7. Client use the symmetric key to send message data with server.
8. Initialization Done. 

## Usage
Just open the server first, and then open the client. And try to send message from client to server. 
