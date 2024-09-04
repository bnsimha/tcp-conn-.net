# TCP Server and Client in C# using .NET

This project demonstrates a simple implementation of a TCP server and client in C# using the .NET framework. The server can handle multiple client connections simultaneously, and both the server and clients can send and receive messages in real time. 

## Table of Contents
- [Features](#features)
- [Prerequisites](#prerequisites)
- [GettingStarted](#gettingstarted)
- [Usage](#usage)

## Features

- **TCP Server:** 
  - Listens for incoming connections from multiple clients.
  - Handles multiple clients concurrently using multithreading.
  - Receives and processes messages from connected clients.
  - Provides feedback to clients based on their messages.

- **TCP Client:**
  - Connects to the TCP server.
  - Sends messages to the server and receives responses.
  - Handles connection and disconnection gracefully.

- **Multithreading:**
  - Uses threading to manage multiple client connections without blocking the main application.
  
- **Data Transmission:**
  - Sends and receives messages as strings, demonstrating basic data communication over TCP.

## Prerequisites

  - .NET SDK installed from [.NET download page](https://dotnet.microsoft.com/download/dotnet)


## GettingStarted

1. Clone the repository:
   ```bash
   git clone https://github.com/bnsimha/tcp-conn-.net


## Usage

  - Start the server first, followed by one or more clients.
  - Type a message in the client terminal and press Enter to send it to the server.
  - The server will process the message and respond accordingly.
  - To start a server or a client, go to the directory and -
    ```bash
    dotnet build
    dotnet run


