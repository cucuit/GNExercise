EXERCISE:
Commit to Github a simple IP address generator console app capable of processing any public subnet class B or smaller.
The input, which could be badly formatted or complete gibberish consists of a string, which is a network IP address in CIDR notation, such as 10.10.10.128/25.

The output should either
a) spell out any parsing errors or
b) output the list of all the valid IP addresses based on said CIDR, not including broadcast and network

Bonus (if time permits, no coding of any kind needed):
If you had to write everything in SQL, briefly describe:
c) The tables you would create and its fields and relationships in order to persist both subnets and the generated IP addresses
d) The additional SQL objects to recreate any logic designed in a) and b)
e) pros and cons of doing it in SQL vs C#?

--------------------------------------------------------------------------------------

You have to run the consoleApp, and you will be asked to input the address in CIDR notation.
If the input is valid you will be provided with the set of valids Ips in that subnet (not including Brodcast and Network addresses).

As the exercise statement says that "processing any public subnet class B or smaller", class A addresses will be consider invalid input

Any invalid input will throw an ArgumentException from the validator that will be catched by the console app and show the message to the user

****
C) I would create 
					1.- a table with the subnet especification and a id as PK
					2.- other table with the generated IP Address, the subnet table ID (as FK) and here you could add data as for exaple if tha address is in use, by who, since when, etc

					3.- if we need we could add other table with the custumer/center data and have a FK to this table from the subnet table in order to keep track of every subnet in multiple customers/centers (not included in the statement)

D) I would use a SP that recibes the CIDR and does the work
E) Pros of C#
		+ Performing unit test is much easier
		+ CI/CD is easier
		+ Debuging is easier

	Pros of SQL
		+ Less network traffic (SP only serialize the name of the SP and the arguments)
		+ No need to recompile in order to make changes


 