# Edge AI Occupancy Monitoring System

This repository contains C# code for a client-server application that simulates occupancy sensors sending occupancy data to a server and requesting aggregated occupancy data. The server stores the data points and provides the aggregated data upon request.
## Objective

The objective of this application is to develop a client-server system where clients represent occupancy sensors. The clients send occupancy data points to the server, and the server aggregates this data to provide the latest occupancy status for each location.

## Data Format

### Aggregated Occupancy Data (Server to Client): ###

Occupancy Matrix: The server sends the aggregated occupancy data as a 2D array, with elements representing the occupancy status at each [x, y] location. The server aggregates the data, providing the latest received occupancy value for each location.
## Implementation Details ##

The server is implemented using TCP/IP sockets in C#. It listens for incoming client connections and handles data communication.
Each client, representing an occupancy sensor, sends occupancy data points to the server and can request aggregated occupancy data.
The server stores received occupancy data points and provides aggregated data upon request.
Data is temporarily stored in memory using a dictionary. No permanent storage or database connection is required for this implementation.
Both client and server hardware are assumed to be powerful enough to run a general-purpose Linux distribution, comparable to the performance of a Raspberry Pi.

## Usage ##
### Server ###
Compile and run the Server project.
The server listens for client connections on the specified port.
Once connected, the server receives occupancy data from clients and provides aggregated occupancy data upon request.


## Code Structure ##
### Server ###
Program.cs: Main server application responsible for handling client connections and data processing.


## Dependencies ##
.NET Core 3.1 or later

## How to Run ##
Clone this repository to your local machine.
Compile and run the Server project.
Compile and run the Client project.

## Contributor ##
Masoumeh (Fereshteh) Jafari-Kaffash
